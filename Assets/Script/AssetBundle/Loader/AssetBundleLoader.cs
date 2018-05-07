
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleLoader : SReference
{
    static int sId = 0;
    public EnLoadState state { protected set; get; }
    public AssetBundleData data;
    public EnLoadPrority prority;
    protected AssetBundleLoader[] depLoaders;
    protected LoaderCompleteHandler mLoadComplete;
    public CreateGameObjectDelegate createComplete { protected set; get; }
    public UnityEngine.Object mainObject { protected set; get; }
    protected Queue<GameObject> mPools = new Queue<GameObject>();
    public int id { get; protected set; }

    public void CreateId()
    {
        id = sId++;
    }

    public void Release(GameObject obj)
    {
        if (!mPools.Contains(obj))
        {
            mPools.Enqueue(obj);
        }
    }

    public virtual void Start()
    {
    }

    /// <summary>
    /// 加载完毕回调
    /// </summary>
    /// <param name="complete"></param>
    public void AddLoadComplete(LoaderCompleteHandler complete)
    {
        if (complete == null)
            return;
        mLoadComplete += complete;
    }

    /// <summary>
    /// 创建完毕回调
    /// </summary>
    /// <param name="complete"></param>
    public void AddCreateComplete(CreateGameObjectDelegate complete)
    {
        if (complete == null)
            return;
        createComplete += complete;
    }

    public void ClearCreateComplete()
    {
        createComplete = null;
    }

    public virtual void AddDependList(AssetBundleLoader[] list)
    {
        this.depLoaders = list;
    }

    /// <summary>
    /// 更新有限级别
    /// </summary>
    public void UpdatePrority()
    {
        ExcuteDependList(RefreshPrority);
    }

    protected void ExcuteDependList(Action<AssetBundleLoader> action)
    {
        for (int i = 0; depLoaders != null && i < depLoaders.Length; i++)
        {
            AssetBundleLoader loader = depLoaders[i];
            action(loader);
            loader.UpdatePrority();
        }
    }

    protected void RefreshPrority(AssetBundleLoader info)
    {
        if (prority > info.prority)
            info.prority = prority;
        if (id < info.id)
            info.id = id;
    }

    public void Ready()
    {
        state = EnLoadState.Ready;
    }

    public bool isInit
    {
        get
        {
            return state != EnLoadState.None;
        }
    }

    public bool isLoadComplete
    {
        get
        {
            return state >= EnLoadState.LoadComplete;
        }
    }

    public bool isCreateComplete
    {
        get
        {
            return state >= EnLoadState.CreateComplete;
        }
    }

    protected virtual void Complete()
    {
        if (mLoadComplete == null)
            return;
        var handler = mLoadComplete;
        mLoadComplete = null;
        handler(this);
    }


    public virtual IEnumerator LoadFromCachedFile(Action<AssetBundleLoader> action)
    {
        yield return null;
    }

    public virtual IEnumerator LoadAssetAsync(Action<AssetBundleLoader> action)
    {
        yield return null;
    }

    public virtual GameObject CreateObject()
    {
        return null;
    }

    public virtual T LoadAsset<T>(string url = null) where T : UnityEngine.Object
    {
        return null;
    }

    public virtual void UnLoader(bool unloadAllLoadedObjects)
    {
        state = EnLoadState.None;
        createComplete = null;
        mLoadComplete = null;
        Loger.Log(data.assetPath);
    }

    public virtual void Dispose()
    {
        while (mPools.Count > 0)
        {
            GameObject.Destroy(mPools.Dequeue());
        }
        UnLoader(true);
    }

    //public override string ToString()
    //{
    //    return string.Format("prority->{0} id->{1} exportType->{2} assetPath->{3} abPath->{4}", prority, id, data.exportType, data.assetPath, data.abPath);
    //}
}




