
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
public class AssetBundleConfigWindow : EditorWindow
{

    [MenuItem("AssetBundle/ConfigManger")]
    public static void Open()
    {
        GetWindow<AssetBundleConfigWindow>("ABSystem", false);
    }
    AssetTargetConfig mConfig;

    private ReorderableList mList;
    private Vector2 mScrollPosition = Vector2.zero;
    string s_search_text;
    public List<AssetTarget> filterList;
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
        GUILayout.Space(10);

        string before = s_search_text;
        string after = EditorGUILayout.TextField("", before, "SearchTextField", GUILayout.Width(200));
        if (before != after)
        {
            s_search_text = after.Trim();
            if (!string.IsNullOrEmpty(s_search_text))
            {
                filterList.Clear();
                for (int i = 0; i < mConfig.filters.Count; i++)
                {
                    AssetTarget target = mConfig.filters[i];
                    if (target.shortPath.IndexOf(s_search_text) >= 0)
                    {
                        filterList.Add(target);
                    }
                }
                mList = null;
                return;
            }
        }
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
    }

    void InitConfig()
    {
        mConfig = ABBuilderTools.LoadAssetAtPath<AssetTargetConfig>(ABBuilderTools.AbConfigPath);
        if (mConfig == null)
            mConfig = new AssetTargetConfig();
        filterList = mConfig.filters;
    }

    void InitFilterListDrawer()
    {
        mList = new ReorderableList(filterList, typeof(AssetBundleFilter), false, true, false, false);
        mList.drawElementCallback = OnListElementGUI;
        mList.drawHeaderCallback = OnListHeaderGUI;
        mList.elementHeight = 22;
    }

    void OnListElementGUI(Rect rect, int index, bool isactive, bool isfocused)
    {
        AssetTarget filter = filterList[index];
        rect.y++;

        Rect r = rect;
        r.width = 300;
        GUI.Label(r, filter.shortPath);

        r.x += r.width + 10;
        r.width = 400;
        GUI.Label(r, filter.abPath);

        r.x += r.width + 10;
        r.width = 80;
        GUI.Label(r, filter.exportType.ToString());
    }

    void OnListHeaderGUI(Rect rect)
    {
        EditorGUI.LabelField(rect, "Asset Filter");
    }
}

