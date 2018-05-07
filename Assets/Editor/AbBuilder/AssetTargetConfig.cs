
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
public class AssetTargetConfig : ScriptableObject
{
    public List<AssetTarget> filters = new List<AssetTarget>();
    public Dictionary<string, AssetTarget> maps = new Dictionary<string, AssetTarget>();

    public void SetChildDependenciesAssetBundle(AssetTarget target)
    {
        target.SetAbName(GetAbName(target.assetPath, true));
        string[] dependenciesList = AssetDatabase.GetDependencies(target.assetPath);
        for (int k = 0; k < dependenciesList.Length; k++)
        {
            string dependenciesUrl = dependenciesList[k];
            if (dependenciesUrl == target.assetPath || dependenciesUrl.EndsWith(".cs") || dependenciesUrl.EndsWith(".asset"))
                continue;
            AssetTarget dependenciesTarget = Get(dependenciesUrl);
            if (dependenciesTarget == null)
            {
                dependenciesTarget = ABBuilderTools.Load(new FileInfo(dependenciesUrl), null);
                Add(dependenciesTarget);
            }
            switch (dependenciesTarget.exportType)
            {
                case EnAssetBundleExportType.None:
                    dependenciesTarget.exportType = EnAssetBundleExportType.Asset;
                    dependenciesTarget.SetAbName(GetAbName(target.assetPath, false));
                    dependenciesTarget.AddParent(target.abPath);
                    break;
                case EnAssetBundleExportType.Asset:
                    dependenciesTarget.AddParent(target.abPath);
                    if (dependenciesTarget.ParentList.Count > 1)
                    {
                        dependenciesTarget.SetAbName(GetAbName(dependenciesUrl, false));
                        dependenciesTarget.exportType = EnAssetBundleExportType.Standalone;
                    }
                    break;
            }
        }
    }

    public void SetIconAssetBundle(AssetTarget target)
    {
        target.SetAbName(GetAbName(target.assetPath, false));
    }

    string GetAbName(string url, bool isScene)
    {
        url = url.ToLower();
        if (isScene && Path.GetExtension(url) == ".unity")
            return (Path.GetDirectoryName(url) + "\\" + Path.GetFileNameWithoutExtension(url) + "_scene" + ABBuilderTools.AbExtension).Replace("\\", "/");
        else
            return (Path.GetDirectoryName(url) + "\\" + Path.GetFileNameWithoutExtension(url) + ABBuilderTools.AbExtension).Replace("\\", "/");
    }


    public void Parse()
    {
        for (int i = 0; i < filters.Count; i++)
        {
            AssetTarget target = filters[i];
            maps[target.assetPath] = target;
        }
    }

    public void Add(AssetTarget target)
    {
        if (maps.ContainsKey(target.assetPath))
            return;
        filters.Add(target);
        maps[target.assetPath] = target;
    }

    public void Remove(AssetTarget target)
    {
        if (!maps.ContainsKey(target.assetPath))
            return;
        filters.Remove(target);
        maps.Remove(target.assetPath);
    }

    public AssetTarget Get(string assetPath)
    {
        if (maps.ContainsKey(assetPath))
            return maps[assetPath];
        return null;
    }

    public void ClearMark()
    {
        for (int i = 0; i < filters.Count; i++)
        {
            AssetTarget target = filters[i];
            //root不清理 
            if (target.exportType == EnAssetBundleExportType.Root)
                continue;
            if (target.exportType == EnAssetBundleExportType.RootMerge)
                continue;
            target.exportType = EnAssetBundleExportType.None;
            target.abPath = null;
        }
    }

    public void SaveBundle()
    {
        ByteArray bytes = new ByteArray();
        int count = 0;
        for (int i = 0; i < filters.Count; i++)
        {
            if (filters[i].exportType == EnAssetBundleExportType.Asset)
                continue;
            count++;
        }
        bytes.WriteInt(count);
        for (int i = 0; i < filters.Count; i++)
        {
            AssetTarget target = filters[i];
            if (target.exportType == EnAssetBundleExportType.Asset)
                continue;
            bytes.WriteUTF(target.shortPath);
            bytes.WriteUTF(target.assetPath);
            bytes.WriteUTF(target.abPath);
            bytes.WriteByte((byte)target.exportType);
        }
        try
        {
            byte[] byteArray = bytes.Buffer;
            FileStream fs = new FileStream(ABBuilderTools.AbDataPath, FileMode.Create);
            fs.Write(byteArray, 0, byteArray.Length);
            fs.Close();
            fs.Dispose();
        }
        catch (Exception e)
        {
            Loger.Error(e.ToString());
        }
    }
}
[System.Serializable]
public class AssetTarget
{
    public string shortPath;
    public string assetPath;
    public string abPath;
    public EnAssetBundleExportType exportType;
    public EnAssetBundleFilter filterType;
    [System.NonSerialized]
    public List<string> ParentList;
    [System.NonSerialized]
    public bool isChangeAbName;

    public void SetAbName(string name)
    {
        name = name.ToLower();
        if (string.IsNullOrEmpty(abPath) || abPath != name)
            isChangeAbName = true;
        abPath = name;
    }

    public void UpdateAssetAbName()
    {
        isChangeAbName = false;
        SetAssetBundleName(assetPath, abPath);
        if (filterType == EnAssetBundleFilter.图片)
        {
            string rootPath = Path.GetDirectoryName(assetPath);
            string iconPath = Path.ChangeExtension(assetPath, "png");
            string fileName = Path.GetFileNameWithoutExtension(shortPath);
            if (rootPath.EndsWith("HeroIcon"))
            {
                SetAssetBundleName(iconPath, abPath);
                SetAssetBundleName(rootPath + "/" + fileName + "A.png", abPath);
                SetAssetBundleName(rootPath + "/" + fileName + "B.png", abPath);
            }
            else
            {
                SetAssetBundleName(iconPath, abPath);
                SetAssetBundleName(rootPath + "/" + fileName + "_alpha.png", abPath);
                SetAssetBundleName(Path.ChangeExtension(assetPath, "asset"), abPath);
            }
        }
    }

    void SetAssetBundleName(string assetPath, string assetBundleName)
    {
        AssetImporter importer = AssetImporter.GetAtPath(assetPath);
        if (importer != null)
            importer.assetBundleName = assetBundleName;
    }


    public AssetTarget(FileInfo file, string assetPath)
    {
        shortPath = file.Name;
        this.assetPath = assetPath.Replace("\\", "/");
    }

    public void AddParent(string url)
    {
        if (ParentList == null)
            ParentList = new List<string>();
        if (!ParentList.Contains(url))
            ParentList.Add(url);
    }
}

