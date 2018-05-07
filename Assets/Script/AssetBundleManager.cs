
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public delegate void CreateGameObjectDelegate(string resName, GameObject obj);
public delegate void LoaderCompleteHandler(AssetBundleLoader info);

public class AssetBundleManager : MonoBehaviour
{
    private static AssetBundleManager m_Instance;
    public static AssetBundleManager GetInstance()
    {
        m_Instance = GameObject.FindObjectOfType<AssetBundleManager>();
        if (m_Instance == null)
        {
            GameObject go = new GameObject("ResourceManager");
            m_Instance = go.AddComponent<AssetBundleManager>();
        }
        return m_Instance;
    }
    int LOAD_FILE_COUNT_MAX;
    int LOAD_ASSET_COUNT_MAX;
    int CREATE_COUNT_MAX;
    Dictionary<string, AssetBundleLoader> resourceMaps = new Dictionary<string, AssetBundleLoader>();
    bool isSortWaitLoadFile;
    bool isSortWaitLoadAsset;
    bool isSortCreate;
    List<AssetBundleLoader> waitLoadFileList = new List<AssetBundleLoader>();
    List<AssetBundleLoader> loadingFileList = new List<AssetBundleLoader>();
    List<AssetBundleLoader> waitLoadAssetList = new List<AssetBundleLoader>();
    List<AssetBundleLoader> loadingAssetList = new List<AssetBundleLoader>();
    List<AssetBundleLoader> createList = new List<AssetBundleLoader>();
    /// <summary>
    /// 用于回收
    /// </summary>
    Dictionary<GameObject, AssetBundleLoader> gameObjectMaps = new Dictionary<GameObject, AssetBundleLoader>();
    Dictionary<string, AssetBundleData> assetBundleDataMaps = new Dictionary<string, AssetBundleData>();
    List<AssetBundleData> assetBundleDataList = new List<AssetBundleData>();
    AssetBundleManifest m_Manifest;
    bool isEditorLoad = true;
    void Awake()
    {
#if UNITY_EDITOR
        LOAD_FILE_COUNT_MAX = 5;
        LOAD_ASSET_COUNT_MAX = 5;
        CREATE_COUNT_MAX = 2;
#else
        LOAD_FILE_COUNT_MAX = 1;
        LOAD_ASSET_COUNT_MAX = 1;
        CREATE_COUNT_MAX = 1;
#endif

    }

    public IEnumerator LoadManifest()
    {
        string path = PathUtil.StreamingPath(PathUtil.activeBuildTarget, false);
        AssetBundle manifestBundle = AssetBundle.LoadFromFile(path);
        yield return manifestBundle;
        m_Manifest = (AssetBundleManifest)manifestBundle.LoadAsset("AssetBundleManifest");
        manifestBundle.Unload(false);
    }

    public IEnumerator LoadAssetBundleData()
    {
        string path = PathUtil.StreamingRoot("abconfig");
        WWW wwwLoad = new WWW(path);
        yield return wwwLoad;
        MemoryStream stream = new MemoryStream(wwwLoad.bytes);
        stream.Position = 0;
        ByteArray byteArray = new ByteArray();
        byteArray.F_ReFresh(stream.ToArray());
        stream.Dispose();
        int count = byteArray.ReadInt();
        for (int i = 0; i < count; i++)
        {
            AssetBundleData data = new AssetBundleData();
            data.shortName = byteArray.ReadUTF();
            data.assetPath = byteArray.ReadUTF();
            data.abPath = byteArray.ReadUTF();
            data.exportType = (EnExportType)byteArray.ReadByte();
            data.isNeedCreateGameObject = data.assetPath.EndsWith(PathUtil.PrefabExtensionName);
            assetBundleDataMaps[data.abPath] = data;
            assetBundleDataList.Add(data);
        }
        for (int i = 0; i < assetBundleDataList.Count; i++)
        {
            AssetBundleData data = assetBundleDataList[i];
            string[] allDependencies = m_Manifest.GetAllDependencies(data.abPath);
            for (int k = 0; k < allDependencies.Length; k++)
            {
                if (data.dependList == null)
                    data.dependList = new List<AssetBundleData>();
                string abName = allDependencies[k];
                if (assetBundleDataMaps.ContainsKey(abName))
                    data.dependList.Add(assetBundleDataMaps[abName]);
                else if (abName.IndexOf("map/") == -1)
                    Loger.Error("not find : " + abName);
            }
        }
    }

    public AssetBundleLoader GetLoader(string abPath)
    {
        if (resourceMaps.ContainsKey(abPath))
        {
            return resourceMaps[abPath];
        }
        return null;
    }

    /// <summary>
    /// 开始加载文件
    /// </summary>
    /// <param name="url"></param>
    /// <param name="type"></param>
    /// <param name="prority"></param>
    /// <param name="Complete"></param>
    public void Load(string url, EnResourceType type, EnLoadPrority prority, CreateGameObjectDelegate Complete)
    {
        string abPath = PathUtil.GetAbUrl(url, type);
        AssetBundleLoader loader = GetLoader(abPath);
        //已经在加载
        if (loader != null && loader.isInit)
        {
            //加载完毕
            if (loader.isLoadComplete)
            {
                //是否需要创建
                if (loader.isCreateComplete)
                    CreateAsset(loader);
                else
                    LoadAsset(loader);
            }
        }
        //没有加载过
        else
        {
            if (!assetBundleDataMaps.ContainsKey(abPath))
            {
                Loger.Error(abPath);
                return;
            }
            loader = CreateLoader(assetBundleDataMaps[abPath], prority);
        }
        loader.AddCreateComplete(Complete);
        loader.AddLoadComplete(LoadAsset);
        loader.prority = prority;
        if (!loader.isLoadComplete)
            isSortWaitLoadFile = true;

    }

    /// <summary>
    /// 加入加载asset列表
    /// </summary>
    /// <param name="loader"></param>
    void LoadAsset(AssetBundleLoader loader)
    {
        //创建完成
        if (loader.state >= EnLoadState.Creating)
            return;
        if (!waitLoadAssetList.Contains(loader))
        {
            isSortWaitLoadAsset = true;
            waitLoadAssetList.Add(loader);
        }
    }

    /// <summary>
    /// 放入创建列表
    /// </summary>
    /// <param name="loader"></param>
    void CreateAsset(AssetBundleLoader loader)
    {
        if (loader.isCreateComplete && !createList.Contains(loader))
        {
            isSortCreate = true;
            createList.Add(loader);
        }
    }

    void ShowTips(List<AssetBundleLoader> waitLoadFileList)
    {
        for (int i = 0; i < waitLoadFileList.Count; i++)
        {
            Loger.Log(waitLoadFileList[i].ToString());
        }
    }

    void Update()
    {
        if (isSortWaitLoadFile)
        {
            isSortWaitLoadFile = false;
            waitLoadFileList.Sort(SortHandler);
            //ShowTips(waitLoadFileList);
        }
        if (isSortWaitLoadAsset)
        {
            isSortWaitLoadAsset = false;
            waitLoadAssetList.Sort(SortHandler);
        }
        if (isSortCreate)
        {
            isSortCreate = false;
            createList.Sort(SortHandler);
        }
        if (loadingFileList.Count <= LOAD_FILE_COUNT_MAX)
        {
            while (waitLoadFileList.Count > 0)
            {
                if (loadingFileList.Count >= LOAD_FILE_COUNT_MAX)
                    break;
                AssetBundleLoader loader = waitLoadFileList[0];
                waitLoadFileList.RemoveAt(0);
                loadingFileList.Add(loader);
                loader.Start();
                StartCoroutine(loader.LoadFromCachedFile(OnLoaderFileComplete));
            }
        }
        while (waitLoadAssetList.Count > 0 && loadingAssetList.Count <= LOAD_ASSET_COUNT_MAX)
        {
            AssetBundleLoader loader = waitLoadAssetList[0];
            waitLoadAssetList.RemoveAt(0);
            if (loader.data.isNeedCreateGameObject)
            {
                loadingAssetList.Add(loader);
            }
            StartCoroutine(loader.LoadAssetAsync(OnLoadAssetComplete));
        }

        createIndex = 0;
        while (createList.Count > 0)
        {
            if (createIndex >= CREATE_COUNT_MAX)
                break;
            AssetBundleLoader loader = createList[0];
            CreateLoadGameObject(loader);
            createList.RemoveAt(0);
            createIndex++;
        }
    }
    int createIndex;
    void CreateLoadGameObject(AssetBundleLoader loader)
    {
        if (loader.createComplete == null)
            return;
        System.Delegate[] list = loader.createComplete.GetInvocationList();
        for (int i = list.Length - 1; i >= 0; i--)
        {
            CreateGameObjectDelegate loadDelegate = list[i] as CreateGameObjectDelegate;
            try
            {
                GameObject obj = loader.CreateObject();
                if (obj != null)
                    gameObjectMaps[obj] = loader;
                loadDelegate(loader.data.shortName, obj);
            }
            catch (Exception e)
            {
                Loger.Error(e.ToString());
            }
        }
        loader.ClearCreateComplete();
    }

    /// <summary>
    /// 加载bundle文件完成
    /// </summary>
    /// <param name="info"></param>
    void OnLoaderFileComplete(AssetBundleLoader info)
    {
        loadingFileList.Remove(info);
    }

    /// <summary>
    /// 加载asset文件完成
    /// </summary>
    /// <param name="info"></param>
    void OnLoadAssetComplete(AssetBundleLoader info)
    {
        loadingAssetList.Remove(info);
        CreateAsset(info);
    }

    int SortHandler(AssetBundleLoader a, AssetBundleLoader b)
    {
        if (a.prority > b.prority)
            return -1;
        if (a.prority < b.prority)
            return 1;
        if (a.id < b.id)
            return -1;
        if (a.id > b.id)
            return 1;
        if (a.data.exportType < b.data.exportType)
            return -1;
        if (a.data.exportType > b.data.exportType)
            return 1;
        return 0;
    }

    protected AssetBundleLoader CreateLoader(AssetBundleData data, EnLoadPrority prority)
    {
        AssetBundleLoader loader = CreateLoader(data.abPath);
        loader.CreateId();
        loader.prority = prority;
        CreateLoader(data);
        loader.UpdatePrority();
        return loader;
    }

    protected AssetBundleLoader CreateLoader(AssetBundleData data)
    {
        AssetBundleLoader loader = CreateLoader(data.abPath);
        loader.data = data;
        return CreateLoader(loader);
    }

    protected AssetBundleLoader CreateLoader(AssetBundleLoader loader)
    {
        if (loader.data.dependList != null)
        {
            AssetBundleLoader[] dependList = new AssetBundleLoader[loader.data.dependList.Count];
            for (int i = 0; i < dependList.Length; i++)
            {
                dependList[i] = CreateLoader(loader.data.dependList[i]);
            }
            loader.AddDependList(dependList);
        }
        return loader;
    }

    protected AssetBundleLoader CreateLoader(string abName)
    {
        AssetBundleLoader loader;
        if (!resourceMaps.ContainsKey(abName))
        {
            loader = CreateLoader();
            resourceMaps[abName] = loader;
        }
        else
        {
            loader = resourceMaps[abName];
        }
        if (!loader.isInit && !waitLoadFileList.Contains(loader))
        {
            loader.Ready();
            waitLoadFileList.Add(loader);
        }
        return loader;
    }

    public void Release(GameObject obj)
    {
        if (obj == null)
            return;
        if (!gameObjectMaps.ContainsKey(obj))
        {
            GameObject.Destroy(obj);
            return;
        }
        gameObjectMaps[obj].Release(obj);
    }

    public void UnLoad(bool unloadAllLoadedObjects)
    {
        var list = resourceMaps.GetEnumerator();
        while (list.MoveNext())
        {
            AssetBundleLoader loader = list.Current.Value;
            if (loader.referenceCount == 0)
                loader.UnLoader(unloadAllLoadedObjects);
        }
    }

    protected AssetBundleLoader CreateLoader()
    {
#if UNITY_EDITOR
        if (isEditorLoad)
            return new EditorAssetBundleLoader();
        else
            return new MobileAssetBundleLoader();
#elif UNITY_IOS
            return new IOSAssetBundleLoader();
#elif UNITY_ANDROID
            return new MobileAssetBundleLoader();
#else
            return new MobileAssetBundleLoader();
#endif
    }

}

