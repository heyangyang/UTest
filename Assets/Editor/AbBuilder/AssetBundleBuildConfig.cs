using System.Collections.Generic;
using UnityEngine;

public class AssetBundleBuildConfig : ScriptableObject
{
    public List<AssetBundleFilter> filters = new List<AssetBundleFilter>();
}

[System.Serializable]
public class AssetBundleFilter
{
    public static string[] sType = { "默认", "合并", "图片" };
    public EnAssetBundleFilter type;
    public string path = string.Empty;
    public string filter = "*.prefab";
}

public enum EnAssetBundleFilter : byte
{
    默认,
    合并,
    图片,
}