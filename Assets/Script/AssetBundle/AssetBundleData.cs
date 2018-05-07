
using System.Collections.Generic;
public class AssetBundleData
{
    public string shortName;
    public string assetPath;
    public string abPath;
    public EnExportType exportType;
    public bool isNeedCreateGameObject;
    public List<AssetBundleData> dependList;
}

