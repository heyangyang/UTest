
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
public class ABBuilder
{
    public AssetTargetConfig config;

    public void IntiConfig()
    {
        ReadConfig();
        AssetBundleBuildConfig buildConfig = ABBuilderTools.LoadAssetAtPath<AssetBundleBuildConfig>(ABBuilderTools.SavePath);
        for (int i = 0; i < buildConfig.filters.Count; i++)
        {
            AssetBundleFilter f = buildConfig.filters[i];
            AddRootTargets(new DirectoryInfo(f.path), f.type, f.filter.Split('|'));
        }
    }

    void ReadConfig()
    {
        config = ABBuilderTools.LoadAssetAtPath<AssetTargetConfig>(ABBuilderTools.AbConfigPath);
        if (config == null)
        {
            config = ScriptableObject.CreateInstance<AssetTargetConfig>();
            AssetDatabase.CreateAsset(config, ABBuilderTools.AbConfigPath);
        }
        else
        {
            config.Parse();
        }
    }


    public void SaveConfig()
    {
        if (config != null)
        {
            config.SaveBundle();
            EditorUtility.SetDirty(config);
        }
    }

    public void AddRootTargets(DirectoryInfo bundleDir, EnAssetBundleFilter type, string[] partterns = null, SearchOption searchOption = SearchOption.AllDirectories)
    {
        if (partterns == null)
            partterns = new string[] { "*.*" };
        for (int i = 0; i < partterns.Length; i++)
        {
            FileInfo[] prefabs = bundleDir.GetFiles(partterns[i], searchOption);
            foreach (FileInfo file in prefabs)
            {
                AssetTarget target = ABBuilderTools.Load(file, null);
                target.filterType = type;
                if (type == EnAssetBundleFilter.合并)
                {
                    target.exportType = EnAssetBundleExportType.RootMerge;
                    int index = bundleDir.FullName.IndexOf("Assets");
                    target.SetAbName(bundleDir.FullName.Substring(index).Replace("\\", "/") + "/" + bundleDir.Name + ABBuilderTools.AbExtension);
                }
                else
                    target.exportType = EnAssetBundleExportType.Root;
                config.Add(target);
            }
        }
    }


    public static void Build()
    {
        string abPath = Application.streamingAssetsPath + "/" + EditorUserBuildSettings.activeBuildTarget;
        if (!Directory.Exists(abPath))
            Directory.CreateDirectory(abPath);
        BuildPipeline.BuildAssetBundles(abPath, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
    }
}

