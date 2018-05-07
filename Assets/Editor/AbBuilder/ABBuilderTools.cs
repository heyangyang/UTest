
using System.IO;
using UnityEditor;
using UnityEngine;
public class ABBuilderTools
{
    static ABBuilder sBuilder;
    public static ABBuilder Create(bool isReLoad)
    {
        if (sBuilder == null || !File.Exists(AbConfigPath) || isReLoad)
        {
            sBuilder = new ABBuilder();
            sBuilder.IntiConfig();
        }
        return sBuilder;
    }
    public const string SavePath = "Assets/ABRes/config.asset";
    public const string AbConfigPath = "Assets/ABRes/AbConfig.asset";
    public const string AbDataPath = "Assets/StreamingAssets/abconfig";
    public const string AbExtension = ".ab";
    public static T LoadAssetAtPath<T>(string path) where T : Object
    {
#if UNITY_5
        return AssetDatabase.LoadAssetAtPath<T>(path);
#else
			return (T)AssetDatabase.LoadAssetAtPath(savePath, typeof(T));
#endif
    }

    public static AssetTarget Load(FileInfo file, System.Type t)
    {
        AssetTarget target = null;
        string fullPath = file.FullName;
        int index = fullPath.IndexOf("Assets");
        if (index == -1)
            return target;
        string assetPath = fullPath.Substring(index);
        target = new AssetTarget(file, assetPath);
        return target;
    }

    public static void DisplayProgressBar(string title, int index, int count)
    {
        EditorUtility.DisplayProgressBar(title, string.Format(" plase wait.....({0}/{1})", index, count), index / (float)count);
    }

    public static void ClearProgressBar()
    {
        EditorUtility.ClearProgressBar();
    }
}

