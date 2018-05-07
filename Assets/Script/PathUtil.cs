
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class PathUtil
{
    public static string activeBuildTarget;
    public const string PrefabExtensionName = ".prefab";
    public const string ImageExtendsionName = ".png";
    public const string SoundExtendsionName = ".ogg";
    public static string AddExtensionName(string url, EnResourceType type)
    {
        if (url.Split('.').Length == 1)
        {
            switch (type)
            {
                case EnResourceType.Shader:
                    url += ".shader";
                    break;
                case EnResourceType.UI:
                case EnResourceType.Role:
                case EnResourceType.Effect:
                case EnResourceType.Camera:
                case EnResourceType.Common:
                    url += ".prefab";
                    break;
                case EnResourceType.Icon:
                    url += ".png";
                    break;
            }
        }
        return url;
    }

    static string GetFullPath(string url, EnResourceType type)
    {
        switch (type)
        {
            case EnResourceType.Common:
                return "Assets/ABRes/Common/" + url;
            case EnResourceType.Shader:
                return "Assets/ABRes/" + url;
            case EnResourceType.UI:
                return "Assets/ABRes/UI/" + url;
            case EnResourceType.Role:
                return "Assets/ABRes/Role/" + url;
            case EnResourceType.Effect:
                return "Assets/ABRes/Effect/" + url;
            case EnResourceType.Camera:
                return "Assets/ABRes/Camera/" + url;
            case EnResourceType.Sound:
                return "Assets/ABRes/Sound/" + url;
            case EnResourceType.Icon:
                return "Assets/ResData/Hero_icon/" + url;
        }
        return null;
    }
    public static string GetAssetsUrl(string url, EnResourceType type)
    {
        url = AddExtensionName(url, type);
        url = GetFullPath(url, type);
        return null;
    }

    public static string GetAbUrl(string url, EnResourceType type)
    {
        return GetFullPath(url, type).ToLower() + ".ab";
    }

    public static string StreamingPath(string url, bool isw = true)
    {
#if UNITY_EDITOR
        return (isw ? "file://" : "") + Application.dataPath + "/StreamingAssets/" + activeBuildTarget + "/" + url;
#elif UNITY_ANDROID   //安卓
         return  (isw ? "jar:file://" : "") + Application.dataPath + "/assets/"+ activeBuildTarget + "/" + url;
#endif
    }

    public static string StreamingRoot(string url)
    {
#if UNITY_EDITOR
        return "file://" + Application.dataPath + "/StreamingAssets/" + url;
#elif UNITY_ANDROID   //安卓
        return "jar:file://" + Application.dataPath + "/!/assets/"+url;
#elif UNITY_IPHONE  //iPhone    
     return   Application.dataPath + "/StreamingAssets/";    
#else  
        return  string.Empty;
#endif
    }
}

