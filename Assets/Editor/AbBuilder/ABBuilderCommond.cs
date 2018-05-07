
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class ABBuilderCommond
{
    public static void CreateReplaceShaderScript()
    {
        CreateReplaceShaderScript(Directory.GetFiles("Assets/ABRes", "*.prefab", SearchOption.AllDirectories));
    }

    static void CreateReplaceShaderScript(string[] files)
    {
        List<Renderer> renderList = new List<Renderer>();
        List<Image> imageList = new List<Image>();
        for (int i = 0; i < files.Length; i++)
        {
            renderList.Clear();
            imageList.Clear();
            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(files[i]);
            var renders = obj.GetComponentsInChildren<Renderer>(true);
            ABBuilderTools.DisplayProgressBar("shader", i, files.Length);

            for (int k = 0; k < renders.Length; k++)
            {
                Renderer render = renders[k];
                if (render.sharedMaterial != null && !string.IsNullOrEmpty(render.sharedMaterial.shader.name))
                {
                    renderList.Add(render);
                }
            }

            var images = obj.GetComponentsInChildren<Image>(true);
            for (int k = 0; k < images.Length; k++)
            {
                Image image = images[k];
                if (image.material != null && !string.IsNullOrEmpty(image.material.shader.name) && image.material.shader.name.IndexOf("Error") == -1)
                {
                    imageList.Add(image);
                }
            }
            ShaderReplace shaderReplace = obj.GetComponent<ShaderReplace>();
            if (renderList.Count == 0 && imageList.Count == 0)
            {
                if (shaderReplace != null)
                {
                    GameObject.DestroyImmediate(shaderReplace, true);
                    EditorUtility.SetDirty(obj);
                }
            }
            else
            {
                if (shaderReplace == null)
                    shaderReplace = obj.AddComponent<ShaderReplace>();
                shaderReplace.renderArray = renderList.ToArray();
                shaderReplace.imageArray = imageList.ToArray();
                EditorUtility.SetDirty(obj);
            }
        }
        ABBuilderTools.ClearProgressBar();
    }

    public static void CreateShader()
    {
        string url = "Assets/ABRes/ShaderTools.prefab";
        if (!File.Exists(url))
            return;
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(url);
        //GameObject shader = GameObject.Instantiate(obj);
        //shader = PrefabUtility.ConnectGameObjectToPrefab(shader, obj);
        ShaderArray utils = obj.GetComponent<ShaderArray>();
        if (utils == null)
            utils = obj.AddComponent<ShaderArray>();
        string[] files = Directory.GetFiles("Assets/Shader", "*.shader", SearchOption.AllDirectories);
        Shader[] list = new Shader[files.Length];
        for (int i = 0; i < files.Length; i++)
        {
            Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(files[i]);
            list[i] = shader;
        }
        utils.gameList = list;
        EditorUtility.SetDirty(obj);
    }

    public static void AssetBundleMark(bool isPahtToGUID)
    {
        ABBuilder builder = ABBuilderTools.Create(true);
        AssetTargetConfig config = builder.config;
        int count = config.filters.Count;

        for (int i = 0; i < count; i++)
        {
            AssetTarget target = config.filters[i];
            if (EnAssetBundleExportType.Root != target.exportType)
                continue;
            ABBuilderTools.DisplayProgressBar("检查", i, count);
            switch (target.filterType)
            {
                case EnAssetBundleFilter.默认:
                    config.SetChildDependenciesAssetBundle(target);
                    break;
                case EnAssetBundleFilter.图片:
                    config.SetIconAssetBundle(target);
                    break;
            }
        }
        int rootCount = 0;
        count = config.filters.Count;
        Dictionary<string, bool> mergeMaps = new Dictionary<string, bool>();
        for (int i = 0; i < count; i++)
        {
            AssetTarget target = config.filters[i];
            if (target.exportType == EnAssetBundleExportType.None)
            {
                Loger.Error(string.Format("{0}->exportType is None", target.assetPath));
                return;
            }
            if (target.exportType != EnAssetBundleExportType.Asset)
            {
                if (target.exportType == EnAssetBundleExportType.RootMerge)
                {
                    if (!mergeMaps.ContainsKey(target.abPath))
                    {
                        mergeMaps[target.abPath] = true;
                        rootCount++;
                    }
                }
                else
                {
                    rootCount++;
                }
            }
            if (target.isChangeAbName)
            {
                ABBuilderTools.DisplayProgressBar("标记", i, count);
                target.UpdateAssetAbName();
            }
        }
        Loger.Error(string.Format("需要打包 -> {0} ", rootCount));
        builder.SaveConfig();
        ABBuilderTools.ClearProgressBar();
    }


    public static void AssetBundleClearMark()
    {
        string[] list = AssetDatabase.GetAllAssetBundleNames();
        for (int i = 0; i < list.Length; i++)
        {
            AssetDatabase.RemoveAssetBundleName(list[i], true);
        }
        ABBuilder builder = ABBuilderTools.Create(true);
        builder.config.ClearMark();
        builder.SaveConfig();
    }
}

