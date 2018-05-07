using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

public class AssetBundleWindow : EditorWindow
{
    [MenuItem("AssetBundle/ReplaceShader")]
    public static void CreateReplaceShaderScript()
    {
        ABBuilderCommond.CreateReplaceShaderScript();
    }

    [MenuItem("AssetBundle/CreateShaderPrefab")]
    public static void CreateShader()
    {
        ABBuilderCommond.CreateShader();
    }

    [MenuItem("AssetBundle/Manger")]
    public static void Open()
    {
        GetWindow<AssetBundleWindow>("ABSystem", true);
    }

    [MenuItem("AssetBundle/打包")]
    public static void AssetBundlePackage()
    {
        ABBuilder.Build();
        AssetDatabase.Refresh();
        Loger.Log("打包完毕");
    }

    [MenuItem("AssetBundle/添加标记")]
    static void AssetBundleMarkNormal()
    {
        ABBuilderCommond.AssetBundleMark(false);
        AssetDatabase.Refresh();
        Loger.Log("添加标记完毕");
    }

    [MenuItem("AssetBundle/清理标记")]
    static void AssetBundleClearMark()
    {
        ABBuilderCommond.AssetBundleClearMark();
        AssetDatabase.Refresh();
        Loger.Log("清理标记完毕");
    }

    AssetBundleBuildConfig mConfig;
    private ReorderableList mList;
    private Vector2 mScrollPosition = Vector2.zero;
    void OnGUI()
    {
        if (mConfig == null)
        {
            InitConfig();
        }

        if (mList == null)
        {
            InitFilterListDrawer();
        }

        bool execBuild = false;
        //tool bar
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        {
            if (GUILayout.Button("添加标记", EditorStyles.toolbarButton))
            {
                AssetBundleMarkNormal();
            }
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("清理标记", EditorStyles.toolbarButton))
            {
                AssetBundleClearMark();
            }
        }
        GUILayout.EndHorizontal();

        //context
        GUILayout.BeginVertical();
        {
            GUILayout.Space(10);

            mScrollPosition = GUILayout.BeginScrollView(mScrollPosition);
            {
                mList.DoLayoutList();
            }
            GUILayout.EndScrollView();
        }
        GUILayout.EndVertical();

        if (GUILayout.Button("打包", EditorStyles.toolbarButton))
        {
            execBuild = true;
        }

        //set dirty
        if (GUI.changed)
            Save();

        if (execBuild)
            AssetBundlePackage();
    }

    void InitConfig()
    {
        mConfig = ABBuilderTools.LoadAssetAtPath<AssetBundleBuildConfig>(ABBuilderTools.SavePath);
        if (mConfig == null)
        {
            mConfig = new AssetBundleBuildConfig();
        }
    }

    void InitFilterListDrawer()
    {
        mList = new ReorderableList(mConfig.filters, typeof(AssetBundleFilter));
        mList.drawElementCallback = OnListElementGUI;
        mList.drawHeaderCallback = OnListHeaderGUI;
        mList.draggable = true;
        mList.elementHeight = 22;
        mList.onAddCallback = (list) => Add();
    }

    void OnListElementGUI(Rect rect, int index, bool isactive, bool isfocused)
    {
        const float GAP = 5;

        AssetBundleFilter filter = mConfig.filters[index];
        rect.y++;

        Rect r = rect;
        r.height = 18;
        r.width = 100;
        filter.type = (EnAssetBundleFilter)GUI.Toolbar(r, (int)filter.type, AssetBundleFilter.sType);

        r.xMin = r.xMax + GAP;
        r.xMax = rect.xMax - 300;
        r.width = 200;
        GUI.enabled = false;
        filter.path = GUI.TextField(r, filter.path);
        GUI.enabled = true;


        r.xMin = r.xMax + GAP;
        r.width = 50;
        if (GUI.Button(r, "Select"))
        {
            var path = SelectFolder();
            if (path != null)
                filter.path = path;
        }

        r.xMin = r.xMax + GAP;
        r.xMax = rect.xMax;
        filter.filter = GUI.TextField(r, filter.filter);
    }

    string SelectFolder()
    {
        string dataPath = Application.dataPath;
        string selectedPath = EditorUtility.OpenFolderPanel("Path", dataPath, "");
        if (!string.IsNullOrEmpty(selectedPath))
        {
            if (selectedPath.StartsWith(dataPath))
            {
                return "Assets/" + selectedPath.Substring(dataPath.Length + 1);
            }
            else
            {
                ShowNotification(new GUIContent("不能在Assets目录之外!"));
            }
        }
        return null;
    }

    void OnListHeaderGUI(Rect rect)
    {
        EditorGUI.LabelField(rect, "Asset Filter");
    }

    void Add()
    {
        string path = SelectFolder();
        if (!string.IsNullOrEmpty(path))
        {
            var filter = new AssetBundleFilter();
            filter.path = path;
            mConfig.filters.Add(filter);
        }
    }

    void Save()
    {
        if (ABBuilderTools.LoadAssetAtPath<AssetBundleBuildConfig>(ABBuilderTools.SavePath) == null)
        {
            AssetDatabase.CreateAsset(mConfig, ABBuilderTools.SavePath);
        }
        else
        {
            EditorUtility.SetDirty(mConfig);
        }
    }


}
