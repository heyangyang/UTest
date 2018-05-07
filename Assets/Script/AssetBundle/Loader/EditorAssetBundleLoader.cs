
using System.Collections;
using UnityEngine;
public class EditorAssetBundleLoader : AssetBundleLoader
{
    public override IEnumerator LoadFromCachedFile(System.Action<AssetBundleLoader> action)
    {
        state = EnLoadState.Loading;
        mainObject = GameTools.LoadAssetAtPath(data.assetPath, typeof(UnityEngine.Object));
        state = EnLoadState.LoadComplete;
        yield return null;
        Complete();
        if (action != null)
            action(this);
    }

    public override IEnumerator LoadAssetAsync(System.Action<AssetBundleLoader> action)
    {
        if (data.isNeedCreateGameObject)
        {
            yield return null;
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
}

