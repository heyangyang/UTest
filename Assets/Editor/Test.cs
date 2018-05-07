using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class Test : EditorWindow
{
    [MenuItem("AssetBundle/UpdateUnity &F5", false, 1)]
    static void UpdateUnity()
    {
        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/ClearConsole &c", false)]
    public static void ClearConsole()
    {
        var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }

    [MenuItem("SVN/添加UI到场景 &4", false, 12)]
    static void AddUIToStage()
    {
        UnityEngine.Object selected = Selection.activeObject;
        if (selected == null)
            return;
        string path = AssetDatabase.GetAssetPath(selected);
        if (string.IsNullOrEmpty(path))
            return;
        if (path.IndexOf("Assets/ABRes/UI") == -1)
            return;

        GameObject root = GameObject.Find("RootUI");
        if (root == null)
        {
            GameObject gameObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/ABRes/Common/RootUI.prefab");
            root = GameObject.Instantiate(gameObj);
            root.name = "RootUI";
        }
        Transform UICamera = root.transform.Find("WinList");
        if (UICamera.childCount > 0)
        {
            for (int i = UICamera.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(UICamera.GetChild(i).gameObject);
            }
        }
        GameObject view = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(path), UICamera) as GameObject;
        view.name = selected.name;
        PrefabUtility.ConnectGameObjectToPrefab(view, Selection.activeGameObject);
    }

    static void Create()
    {
        byte[] jpgData = ReadData("C:\\1.jpg");
        byte[] soundData = ReadData("C:\\1.wav");
        List<byte> newData = new List<byte>();
        newData.AddRange(jpgData);
        newData.AddRange(soundData);
        FileStream fs = new FileStream("C:\\11.jpg", FileMode.Create);
        fs.Write(newData.ToArray(), 0, newData.Count);
        fs.Close();
        fs.Dispose();
    }

    static void Parse()
    {
        float time = Time.realtimeSinceStartup;
        byte[] jpgData = ReadData("E:\\AndroidSDK\\1.rar");
        Log(Time.realtimeSinceStartup - time);
        Log(MD5Helper.MD5_Hash(jpgData));
        Log(MD5Helper.SHA1_Hash(jpgData));
        Log(Time.realtimeSinceStartup - time);
    }

    public static byte[] ReadData(string url)
    {
        FileStream fs = new FileStream(url, FileMode.Open);
        long size = fs.Length;
        byte[] array = new byte[size];
        //将文件读到byte数组中
        fs.Read(array, 0, array.Length);
        fs.Close();
        return array;
    }

    private static void Log(object str)
    {
        UnityEngine.Debug.Log(str);
    }

    private static void Warning(object str)
    {
        UnityEngine.Debug.LogWarning(str);
    }

    private static void Error(object str)
    {
        UnityEngine.Debug.LogError(str);
    }
}
