
using System.Collections;
using UnityEngine;
public class MobileAssetBundleLoader : AssetBundleLoader
{
    int dependCount = 1;
    protected AssetBundle bundle;
    public override void Start()
    {
        switch (state)
        {
            case EnLoadState.None:
                state = EnLoadState.Loading;
                break;
            case EnLoadState.Loading:
                break;
            case EnLoadState.Error:
            case EnLoadState.LoadComplete:
                break;
        }
    }

    public override void AddDependList(AssetBundleLoader[] list)
    {
        base.AddDependList(list);
        ExcuteDependList(AddDependChild);
    }

    protected void AddDependChild(AssetBundleLoader info)
    {
        if (info.isLoadComplete)
            return;
        dependCount++;
        info.Retain();
        info.AddLoadComplete(DependLoadComplete);
    }

    void ReleaseLoader(AssetBundleLoader info)
    {
        info.Release();
    }

    void DependLoadComplete(AssetBundleLoader info)
    {
        dependCount--;
        CheckDepComplete();
    }

    void CheckDepComplete()
    {
        if (dependCount == 0)
        {
            ExcuteDependList(ReleaseLoader);
            Complete();
        }
    }

    public override IEnumerator LoadFromCachedFile(System.Action<AssetBundleLoader> action)
    {
        state = EnLoadState.Loading;
        AssetBundleCreateRequest req = AssetBundle.LoadFromFileAsync(PathUtil.StreamingPath(data.abPath, false));
        yield return req;
        //把自己的去掉
        dependCount--;
        bundle = req.assetBundle;
        state = EnLoadState.LoadComplete;
        //Logger.Error("LoadComplete : " + data.abPath);
        CheckDepComplete();
        if (action != null)
            action(this);
    }

    public override IEnumerator LoadAssetAsync(System.Action<AssetBundleLoader> action)
    {
        if (data.isNeedCreateGameObject)
        {
            if (bundle == null)
                Loger.Error(data.assetPath + " " + state);
            state = EnLoadState.Creating;
            string[] names = bundle.GetAllAssetNames();
            AssetBundleRequest request = bundle.LoadAssetAsync(names[0]);
            yield return request;
            mainObject = request.asset;
        }
        state = EnLoadState.CreateComplete;
        if (action != null)
            action(this);
    }

    public override GameObject CreateObject()
    {
        if (mainObject != null && data.isNeedCreateGameObject)
        {
            if (mPools.Count > 0)
                return mPools.Dequeue();
            return GameObject.Instantiate(mainObject) as GameObject;
        }
        return null;
    }


    public override T LoadAsset<T>(string url = null)
    {
        if (url == null)
            url = data.assetPath;
        return bundle.LoadAsset<T>(url);
    }

    public override void UnLoader(bool unloadAllLoadedObjects)
    {
        base.UnLoader(unloadAllLoadedObjects);
        if (bundle != null)
        {
            bundle.Unload(unloadAllLoadedObjects);
        }
    }
}

