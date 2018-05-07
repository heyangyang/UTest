using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.IO;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    void Start()
    {
        CoroutineTool.Init(this);
#if UNITY_EDITOR
        GameTools.LoadAssetAtPath = UnityEditor.AssetDatabase.LoadAssetAtPath;
        PathUtil.activeBuildTarget = EditorUserBuildSettings.activeBuildTarget.ToString();
#else
        PathUtil.activeBuildTarget = Application.platform.ToString();
#endif
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        Loger.Log("start");
        AppGlobalMonitor.F_GetInstance();
        yield return StartCoroutine(ConfigManager.Init());
        yield return StartCoroutine(AssetBundleManager.GetInstance().LoadManifest());
        yield return StartCoroutine(AssetBundleManager.GetInstance().LoadAssetBundleData());
        GameObject go = new GameObject("Manager");
        go.AddComponent<DontDestroyTool>();
        UIManager.F_GetInstance().clazzAdder = new UIClazzAdder();
        UIManager.F_GetInstance().F_ShowUI(T_UIConfig.E_UI.WIN_LOADING);
        //AssetBundleManager abMgr = AssetBundleManager.GetInstance();
        //abMgr.Load("ShaderTools", EnResourceType.UI, EnLoadPrority.Fast, null);
    }

}
