using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using DG.Tweening;

public delegate void ActionOnComplete(WindowBase obj);

public class UIdata
{
    public int id;
    public T_UIConfig config; //配置信息
    public WindowBase windowBase;
    public object obj; //用户传的参数
    public ActionOnComplete onComplete; //完成回调函数

}
enum MaskType
{
    None = 0,
    /// <summary>
    /// 添加黑背景
    /// </summary>
    BlackMask = 1,
    /// <summary>
    /// 添加模糊背景
    /// </summary>
    BlurTexture = 2,
    /// <summary>
    /// 隐藏自己下面的UI
    /// </summary>
    HidenUnderUI = 3,
}
public class UIManager : MonoBehaviour
{
    #region 变量
    private static UIManager m_instance = null;
    Camera uiCamera;

    GameObject RootUI;
    /// <summary>
    /// 遮罩
    /// </summary>
    GameObject m_FullScreenMask;
    /// <summary>
    /// 模糊贴图背景;
    /// </summary>
    RawImage m_BlurTexture;
    /// <summary>
    /// UI列表
    /// </summary>
    Dictionary<int, WindowBase> m_UIList;
    /// <summary>
    /// 数据
    /// </summary>
    Dictionary<int, UIdata> m_DataModule;
    /// <summary>
    /// 同一个界面对应的多个实例
    /// </summary>
    Dictionary<T_UIConfig.E_UI, List<int>> m_EUIToIdDec;
    /// <summary>
    /// 层级先后顺序
    /// </summary>
    List<int> m_IdPosList;
    /// <summary>
    /// 节点
    /// </summary>
    Transform m_UnderWinListGo;
    /// <summary>
    /// 窗口节点
    /// </summary>
    Transform m_WinListGo;
    /// <summary>
    /// tips节点
    /// </summary>
    Transform m_TiplistGo;
    /// <summary>
    /// 最上层UI节点
    /// </summary>
    Transform m_ToplistGo;
    /// <summary>
    /// 开始深度
    /// </summary>
    int m_StartDepth = 1000;
    /// <summary>
    /// 间隔深度
    /// </summary>
    int m_depth = 50;
    /// <summary>
    /// UI反射获取类
    /// </summary>
    public UIClazzBase clazzAdder;

    /// <summary>
    /// 场景摄像机;
    /// </summary>
    Camera SceneCamera = null;
    /// <summary>
    /// 场景摄像机对象;
    /// </summary>
    GameObject SceneCameraObj = null;
    /// <summary>
    /// 最上层WindowsBase;
    /// </summary>
    public WindowBase topWinBase;
    public Func<T_UIConfig.E_UI, bool> IsOpenUI = null;
    #endregion

    #region 对外变量
    /// <summary>
    /// ui 摄像机
    /// </summary>
    public Camera UICamera
    {
        get { return uiCamera; }
    }

    /// <summary>
    /// 最上层WindowsBase;
    /// </summary>
    public WindowBase TopWinBase
    {
        get { return topWinBase; }
    }
    #endregion
    /// <summary>
    /// 设定的分辨率
    /// </summary>
    public Vector2 uiResolution;

    #region 初始化
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static UIManager F_GetInstance()
    {
        if (m_instance == null)
        {
            m_instance = GameObject.FindObjectOfType<UIManager>();
            if (m_instance == null)
            {
                GameObject go = new GameObject("UIManager");
                GameObject parent = GameObject.Find("Manager");
                if (parent != null)
                {
                    go.transform.SetParent(parent.transform);
                }
                m_instance = go.AddComponent<UIManager>();
            }
        }
        return m_instance;
    }

    void Awake()
    {
        m_DataModule = new Dictionary<int, UIdata>();
        m_UIList = new Dictionary<int, WindowBase>();
        m_EUIToIdDec = new Dictionary<T_UIConfig.E_UI, List<int>>();
        m_IdPosList = new List<int>();
        F_CreateUIRoot();
    }

    void OnDestroy()
    {
        //销毁事件

    }

    /// <summary>
    /// 创建UI挂机点
    /// </summary>
    void F_CreateUIRoot()
    {
        AssetBundleManager.GetInstance().Load("RootUI", EnResourceType.Common, EnLoadPrority.Fast, LoadRootUIComplete);
    }
    void LoadRootUIComplete(string sourceName, GameObject Obj)
    {
        RootUI = Obj;
        RootUI.transform.position = new Vector3(0, 0, -400);
        RootUI.transform.localScale = Vector3.one;
        RootUI.layer = LayerMask.NameToLayer("UI");
        m_UnderWinListGo = RootUI.transform.FindChild("UnderWinList");
        m_WinListGo = RootUI.transform.FindChild("WinList");
        m_TiplistGo = RootUI.transform.FindChild("TipList");
        m_ToplistGo = RootUI.transform.FindChild("TopList");
        uiCamera = RootUI.transform.FindChild("UICamera").GetComponent<Camera>();
        uiCamera.gameObject.AddComponent<UICameraRef>();
        TransformUtil.Add<DontDestroyTool>(RootUI);
        uiResolution = RootUI.GetComponent<CanvasScaler>().referenceResolution;

        SetRootPos(m_UnderWinListGo, T_UIConfig.UI_TYPE.UNDER_WIN);
        SetRootPos(m_WinListGo, T_UIConfig.UI_TYPE.WIN);
        SetRootPos(m_TiplistGo, T_UIConfig.UI_TYPE.TIP);
        SetRootPos(m_TiplistGo, T_UIConfig.UI_TYPE.TOP);
    }
    void SetRootPos(Transform root, T_UIConfig.UI_TYPE type)
    {
        Array typeArray = Enum.GetValues(typeof(T_UIConfig.UI_TYPE));
        root.localPosition = new Vector3(0, 0, m_StartDepth * (typeArray.Length - (int)type));
    }
    /// <summary>
    /// 遮罩设置最顶层
    /// </summary>
    void setFullScreenTop()
    {
        if (m_FullScreenMask != null)
        {
            m_FullScreenMask.SetActive(true);
            m_FullScreenMask.transform.SetParent(m_ToplistGo.transform);
            m_FullScreenMask.transform.SetAsLastSibling();
        }
    }
    /// <summary>
    /// 设置全屏遮罩
    /// </summary>
    void SetFullScreenMask(MaskType type = MaskType.None)
    {
        if (type == MaskType.BlackMask && m_FullScreenMask == null)
        {
            //string str = "FullScreenMask";
            //ResMgr.GetInstance().F_LoadAsset(str, EnResType.UI, LoadFullScreenComplete, null, ResClear.NotClear);
            return;
        }
        SetFullScreenPos();
    }
    void LoadFullScreenComplete(string sourceName, GameObject pAssetNameAndObj, object p)
    {
        m_FullScreenMask = pAssetNameAndObj;
        //Color color = m_FullScreenMask.GetComponent<Image>().color;
        //color.a = 0;
        //m_FullScreenMask.GetComponent<Image>().color = color;
        SetFullScreenPos();
    }

    void SetFullScreenPos()
    {
        SetFullScreenPos(MaskType.BlackMask, GetMask(MaskType.BlackMask));
        SetFullScreenPos(MaskType.BlurTexture, GetMask(MaskType.BlurTexture));
        SetWorldCameraVisible();
    }
    /// <summary>
    /// 是否隐藏WorldCamera
    /// </summary>
    void SetWorldCameraVisible()
    {
        for (int i = 0; i < m_IdPosList.Count; i++)
        {
            T_UIConfig config = GetConfigData(GetConfigUI(m_IdPosList[i]));
            if (config == null || config.UI_Type != T_UIConfig.UI_TYPE.WIN) continue;
            if (config.IsCloseWorldCamera == true)
            {
                if (SceneCameraObj == null)
                {
                    SceneCameraObj = GameObject.Find("WorldCamera");
                }
                if (null != SceneCameraObj)
                {
                    SceneCameraObj.SetActive(false);
                }
                return;
            }
        }
        if (null != SceneCameraObj)
        {
            SceneCameraObj.SetActive(true);
        }
    }
    /// <summary>
    ///确定全屏遮罩位置
    /// </summary>
    void SetFullScreenPos(MaskType type, GameObject MaskObj)
    {
        if (MaskObj == null) return;
        T_UIConfig tmp = null;
        int id = -1;
        for (int i = 0; i < m_IdPosList.Count; i++)
        {
            T_UIConfig config = GetConfigData(GetConfigUI(m_IdPosList[i]));
            if (config == null) continue;
            if (config.ShowMask == (int)type)
            {
                if (tmp == null)
                {
                    tmp = config;
                    id = m_IdPosList[i];
                }
                else
                {
                    if (tmp.UI_Type < config.UI_Type)
                    {
                        tmp = config;
                        id = m_IdPosList[i];
                    }
                    else if (m_UIList.ContainsKey(id) && m_UIList[id] != null && tmp.UI_Type == config.UI_Type)
                    {
                        if (m_UIList[id].transform.GetSiblingIndex() < m_UIList[m_IdPosList[i]].transform.GetSiblingIndex())
                        {
                            tmp = config;
                            id = m_IdPosList[i];
                        }
                    }
                }
            }
        }

        if (tmp != null && m_UIList.ContainsKey(id) && m_UIList[id] != null)
        {
            MaskObj.SetActive(true);
            MaskObj.transform.SetParent(m_UIList[id].transform.parent);
            MaskObj.transform.SetAsLastSibling();
            int index = m_UIList[id].transform.GetSiblingIndex();
            MaskObj.transform.SetSiblingIndex(index);
            Vector3 pos = m_UIList[id].transform.localPosition;
            pos.z = pos.z - 1;
            MaskObj.transform.localPosition = pos;
        }
        else
        {
            MaskObj.SetActive(false);
            //MaskAnim(type,MaskObj, false);
        }
    }
    /// <summary>
    /// 遮罩动画
    /// </summary>
    /// <param name="maskObj"></param>
    /// <param name="visible"></param>
    void MaskAnim(MaskType type, GameObject maskObj, bool visible, float time = 0.5f)
    {
        Graphic imge = maskObj.GetComponent<Image>() as Graphic;
        if (imge == null)
        {
            imge = maskObj.GetComponent<RawImage>() as Graphic;
        }
        if (DOTween.IsTweening(maskObj))
        {
            DOTween.Pause(maskObj);
        }
        if (imge == null)
        {
            maskObj.SetActive(visible);
            return;
        }

        float value = visible ? 1 : 0;
        if (type == MaskType.BlackMask && visible)
        {
            value = 0.8f;
        }
        DOTween.ToAlpha(() => imge.color, (color) => { imge.color = color; }, value, time).OnComplete(() =>
        {
            maskObj.SetActive(visible);
        });
    }
    GameObject GetMask(MaskType maskType)
    {
        GameObject mask = null;
        switch (maskType)
        {
            case MaskType.BlackMask:
                mask = m_FullScreenMask;
                break;
            case MaskType.BlurTexture:
                if (m_BlurTexture != null)
                    mask = m_BlurTexture.gameObject;
                break;
            default:
                break;
        }
        return mask;
    }

    #region 屏幕模糊
    /// <summary>
    /// 背景模糊;
    /// </summary>
    public void F_BlurScreenShot()
    {
        if (null != m_BlurTexture)
        {
            m_BlurTexture.gameObject.SetActive(false);
        }
        if (UICamera != null)
        {
            float scale = 0.4f;  //比例越小，速度越快，但越模糊
            Vector2 v2Screen = new Vector2(Screen.width, Screen.height);

            float UIScreenWidth = 1280;
            float UIScreenHeight = UIScreenWidth * v2Screen.y / v2Screen.x;
            v2Screen.x = UIScreenWidth;
            v2Screen.y = UIScreenHeight;

            float width = v2Screen.x * scale;
            float height = v2Screen.y * scale;
            RenderTexture rt = new RenderTexture((int)width, (int)height, 24);
            if (null == SceneCamera)
            {
                if (SceneCameraObj == null)
                {
                    SceneCameraObj = GameObject.Find("WorldCamera");
                }
                if (null != SceneCameraObj)
                {
                    SceneCamera = SceneCameraObj.GetComponent<Camera>();
                }
            }

            if (null != SceneCamera)
            {
                SceneCamera.targetTexture = rt;
                SceneCamera.Render();
                SceneCamera.targetTexture = null;
            }

            UICamera.targetTexture = rt;
            UICamera.Render();
            UICamera.targetTexture = null;
            RenderTexture.active = rt;

            Texture2D thumb2d = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);
            thumb2d.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            //Color32[] thumbPixels = thumb2d.GetPixels32();
            //thumb2d.SetPixels32(thumbPixels);
            thumb2d.Apply();

            if (m_BlurTexture == null)
            {
                GameObject go = new GameObject("ScreenBG");
                go.layer = LayerMask.NameToLayer("UI");
                go.transform.parent = m_WinListGo.transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
                m_BlurTexture = go.AddComponent<RawImage>();
                m_BlurTexture.color = new Color32(84, 84, 84, 255);
                Canvas canvas = TransformUtil.Add<Canvas>(go);
                canvas.overrideSorting = true;
                TransformUtil.Add<GraphicRaycaster>(go);
                m_BlurTexture.gameObject.SetActive(false);
            }


            //处理图片;
            m_BlurTexture.texture = UITextureBlurTool.GetInstance().F_HandleImage(thumb2d);
            m_BlurTexture.gameObject.SetActive(true);
            //Color color = m_BlurTexture.color;
            //color.a = 0;
            //m_BlurTexture.color = color;
            GameObject.Destroy(thumb2d);
        }
        else
        {
            Loger.Error("CatureScreenShot Fail, UICamera is NULL!");
        }

    #endregion
    }

    #endregion

    #region 显示界面
    /// <summary>
    /// 显示界面;
    /// </summary>
    /// <param name="eUI">配置表ID</param>
    /// <param name="obj">参的参数</param>
    /// <param name="onComplete">完成回调函数</param>
    public void F_ShowUI(T_UIConfig.E_UI eUI, object obj = null, ActionOnComplete onComplete = null)
    {
        if (this.IsOpenUI != null && !this.IsOpenUI.Invoke(eUI))
        {
            return;
        }

        T_UIConfig config = GetConfigData(eUI);

        if (config.ShowMask == (int)MaskType.BlurTexture)
        {
            F_BlurScreenShot();
        }
        //使用已经打开的UI
        if (config.MoreOpen == false && m_EUIToIdDec.ContainsKey(eUI))
        {
            List<int> idlist = m_EUIToIdDec[eUI];
            if (idlist.Count > 0)
            {
                WindowBase wdb = m_UIList[idlist[0]];
                //如果需要背景模糊,得先处理再打开界面;
                wdb.gameObject.SetActive(true);
                wdb.RegisterEvent();
                wdb.F_OnData(obj);
                wdb.F_AddSound();
                //置顶
                MoveToTop(eUI);
                SetFullScreenMask();
                TransformUtil.ChangeLayer(wdb.transform, 5, 0);
                //回调
                if (onComplete != null)
                {
                    onComplete(wdb);
                }
                //设置Clear类型
                //ResMgr.GetInstance().SetClearType(wdb.gameObject, clear);
            }
            return;
        }

        if ((MaskType)config.ShowMask == MaskType.BlackMask)
        {
            setFullScreenTop();
        }

        //初始化新的UI
        if (!m_EUIToIdDec.ContainsKey(eUI))
        {
            List<int> ids = new List<int>();
            m_EUIToIdDec.Add(eUI, ids);
        }
        //生成Id
        int id = buildId();
        m_IdPosList.Add(id);

        UIdata data = new UIdata();
        data.id = id;
        data.config = config;
        data.obj = obj;
        data.onComplete = onComplete;
        m_DataModule.Add(id, data);

        if (data.config.IsShowLoading)
        {
            UIManager.F_GetInstance().F_ShowUI(T_UIConfig.E_UI.TIP_SHOW_LOADING);
        }
        AssetBundleManager.GetInstance().Load(config.Prefab, EnResourceType.UI, EnLoadPrority.UI, LoadAssetComplete);
    }

    void LoadAssetComplete(string sourceName, GameObject Obj)
    {
        //UIdata data = tdata as UIdata;
        UIdata data = null;
        GameObject gameobject = Obj;
        if (gameobject == null)
        {
            Loger.Error("Instantiate  failed " + data.config.Prefab);
        }
        gameobject.transform.SetParent(GetParent(data.config.UI_Type));
        gameobject.transform.localScale = Vector3.one;
        gameobject.transform.localRotation = Quaternion.identity;
        gameobject.layer = LayerMask.NameToLayer("UI");
        int count = GetCountByUIType(data.config.UI_Type);
        gameobject.transform.localPosition = new Vector3(0, 0, m_StartDepth * (int)data.config.UI_Type + count * m_depth);

        //检测是否添加Canvas
        CheckCanvas(gameobject);

        //检测是否添加配置表对应的Script
        WindowBase wdbase = clazzAdder.AddClazz(gameobject, data.config.Script);
        //初始化
        wdbase.F_SetId(data.id, data.config.E_ui, data.config.UI_Type);
        if (m_DataModule.ContainsKey(data.id) && m_DataModule[data.id] == data)
        {
            m_DataModule[data.id].windowBase = wdbase;
            m_UIList.Add(data.id, wdbase);
            m_EUIToIdDec[data.config.E_ui].Add(data.id);

            try
            {
                wdbase.F_InitPos();
                wdbase.F_OnInit();
                wdbase.F_IsInit();
                wdbase.RegisterEvent();
                wdbase.F_OnData(data.obj);
                wdbase.F_AddSound();
            }
            catch (Exception e)
            {
                Loger.Error(e);
            }
        }
        else
        {
            Unload(wdbase);
        }

        //设置深度
        ResetDepth(data.config.UI_Type);
        //是否添加遮罩
        SetFullScreenMask((MaskType)data.config.ShowMask);
        TransformUtil.ChangeLayer(wdbase.transform, 5, 0);
        //回调
        if (data.onComplete != null)
        {
            data.onComplete(wdbase);
        }


    }
    /// <summary>
    /// //检测是否添加Canvas
    /// </summary>
    /// <param name="go"></param>
    void CheckCanvas(GameObject go)
    {
        TransformUtil.Add<GraphicRaycaster>(go);
        Canvas canvas = TransformUtil.Add<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.overrideSorting = true;
    }

    #endregion

    #region 关闭界面种类

    /// <summary>
    /// 通过Id关闭界面
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isDestroy"></param>
    public void F_CloseUI(int id)
    {
        if (m_UIList.ContainsKey(id))
        {
            T_UIConfig.UI_TYPE type = CheckUIType(GetConfigUI(id));
            CloseUIById(id);
            ResetDepth(type);
            SetFullScreenMask();
        }
    }
    /// <summary>
    /// 关闭界面
    /// </summary>
    /// <param name="wins">WindowBase</param>
    /// <param name="isDestroy"></param>
    public void F_CloseUI(WindowBase wins)
    {
        T_UIConfig.UI_TYPE type = wins.F_GetUIType();
        CloseUIById(wins.F_GetId());
        ResetDepth(type);
        SetFullScreenMask();
    }
    void CloseUIById(int id)
    {
        if (m_UIList.ContainsKey(id))
        {
            WindowBase winbs = m_UIList[id];
            winbs.F_RemoveSound();
            winbs.UnRegisterEvent();
            winbs.F_OnRemove();

            m_UIList.Remove(id);
            RemoveEUIToIdDec(winbs.F_GetEUI(), id);
            m_IdPosList.Remove(id);
            m_DataModule.Remove(id);
            Unload(winbs);
        }
    }
    void Unload(WindowBase winbs)
    {
        if (null != winbs && null != winbs.gameObject)
        {
            //ResMgr.GetInstance().F_UnloadAsset(winbs.gameObject, clear);
        }
    }
    /// <summary>
    /// 通过 eui关闭界面
    /// </summary>
    /// <param name="eui"></param>
    /// <param name="isDestroy"></param>
    public void F_CloseUI(T_UIConfig.E_UI eui)
    {
        T_UIConfig.UI_TYPE type = CheckUIType(eui);
        for (int i = m_IdPosList.Count - 1; i >= 0; i--)
        {
            int id = m_IdPosList[i];
            if (m_UIList.ContainsKey(id))
            {
                WindowBase winb = m_UIList[id];
                if (winb.F_GetEUI() == eui)
                {
                    CloseUIById(id);
                }
            }
        }

        ResetDepth(type);
        SetFullScreenMask();
    }
    /// <summary>
    /// 通过 UItype关闭界面
    /// </summary>
    /// <param name="type"></param>
    /// <param name="isDestroy"></param>
    public void F_CloseUI(T_UIConfig.UI_TYPE type)
    {

        for (int i = m_IdPosList.Count - 1; i >= 0; i--)
        {
            int id = m_IdPosList[i];
            WindowBase winb = m_UIList[id];
            if (winb.F_GetUIType() == type)
            {
                CloseUIById(id);
            }
        }
        ResetDepth(type);
        SetFullScreenMask();
    }

    void RemoveEUIToIdDec(T_UIConfig.E_UI eui)
    {
        if (m_EUIToIdDec.ContainsKey(eui))
        {
            m_EUIToIdDec.Remove(eui);
        }
    }

    void RemoveEUIToIdDec(T_UIConfig.E_UI eui, int id)
    {
        if (m_EUIToIdDec.ContainsKey(eui))
        {
            List<int> idlist = m_EUIToIdDec[eui];
            if (idlist.Exists(delegate(int i) { return i == id; }))
            {
                idlist.Remove(id);
                if (idlist.Count == 0)
                {
                    m_EUIToIdDec.Remove(eui);
                }
            }
        }
    }
    #endregion

    #region 获取实例
    /// <summary>
    /// 通过ID查找UI实例
    /// </summary>
    /// <param name="id">id</param>
    /// <returns></returns>
    public WindowBase F_GetUIById(int id)
    {
        if (m_UIList.ContainsKey(id))
        {
            WindowBase winbs = m_UIList[id];
            return winbs;
        }
        return null;
    }
    /// <summary>
    /// 获取实例List
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<WindowBase> F_GetListScriptById(T_UIConfig.E_UI eui)
    {
        List<WindowBase> list = new List<WindowBase>();
        if (m_EUIToIdDec.ContainsKey(eui))
        {
            List<int> ids = m_EUIToIdDec[eui];
            for (int i = 0; i < ids.Count; i++)
            {
                int id = ids[i];
                if (m_UIList.ContainsKey(id))
                {
                    list.Add(m_UIList[id]);
                }
            }
        }
        return list;
    }
    /// <summary>
    /// 获取实例
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public WindowBase F_GetSimpleScriptByUI(T_UIConfig.E_UI eui)
    {
        List<WindowBase> list = F_GetListScriptById(eui);
        if (list.Count > 0)
        {
            return list[0];
        }
        else
        {
            Loger.Error("没有  " + eui.ToString() + "  实例");
            return null;
        }
    }
    /// <summary>
    /// 判断是否打开
    /// </summary>
    /// <param name="id">UI配置Id</param>
    /// <returns></returns>
    public bool F_IsOpenById(T_UIConfig.E_UI eui)
    {
        return m_EUIToIdDec.ContainsKey(eui) && m_EUIToIdDec[eui].Count > 0;
    }

    /// <summary>
    /// 打开场景摄像机
    /// </summary>
    public void ShowSceneCameraObj()
    {
        if (SceneCameraObj != null)
        {
            SceneCameraObj.SetActive(true);
        }
    }


    /// <summary>
    /// 设置到最上层
    /// </summary>
    public void MoveToTop(T_UIConfig.E_UI eui)
    {
        if (m_EUIToIdDec.ContainsKey(eui))
        {
            List<int> idlist = m_EUIToIdDec[eui];
            if (idlist.Count > 0)
            {
                m_IdPosList.Remove(idlist[0]);
                m_IdPosList.Add(idlist[0]);
                ResetDepth(CheckUIType(eui));
            }
        }
    }
    /// <summary>
    /// 设置最底层
    /// </summary>
    /// <param name="eui"></param>
    public void MoveToBottom(T_UIConfig.E_UI eui)
    {
        if (m_EUIToIdDec.ContainsKey(eui))
        {
            List<int> idlist = m_EUIToIdDec[eui];
            if (idlist.Count > 0)
            {
                m_IdPosList.Remove(idlist[0]);
                m_IdPosList.Insert(0, idlist[0]);
                ResetDepth(CheckUIType(eui));
            }
        }
    }
    #endregion


    #region 内部函数
    /// <summary>
    /// 根据不同类型获取不同父节点
    /// </summary>
    /// <param name="type">UI类型</param>
    /// <returns></returns>
    Transform GetParent(T_UIConfig.UI_TYPE type)
    {
        switch (type)
        {
            case T_UIConfig.UI_TYPE.UNDER_WIN:
                return m_UnderWinListGo;
            case T_UIConfig.UI_TYPE.WIN:
                return m_WinListGo;
            case T_UIConfig.UI_TYPE.TIP:
                return m_TiplistGo;
            case T_UIConfig.UI_TYPE.TOP:
                return m_ToplistGo;
            default:
                return m_WinListGo;
        }
    }
    /// <summary>
    /// 重新设置UI的层级
    /// </summary>
    /// <param name="type"></param>
    void ResetDepth(T_UIConfig.UI_TYPE currentType)
    {
        int undercount = 0;
        int wincount = 0;
        int tipcount = 0;
        int topcount = 0;
        int id;

        WindowBase tmpTop = null;

        for (int i = 0; i < m_IdPosList.Count; i++)
        {
            id = m_IdPosList[i];
            if (m_UIList.ContainsKey(id))
            {
                WindowBase winbs = m_UIList[id];
                if (winbs == null) continue;
                CheckTop(winbs, ref tmpTop);
                Vector3 localPos = winbs.transform.localPosition;
                T_UIConfig.UI_TYPE type = winbs.F_GetUIType();
                if (type != currentType) continue;
                Canvas canvas = TransformUtil.Add<Canvas>(winbs.gameObject);
                switch (type)
                {
                    case T_UIConfig.UI_TYPE.UNDER_WIN:
                        undercount++;
                        winbs.transform.SetSiblingIndex(undercount * 2);
                        localPos.z = m_StartDepth - undercount * m_depth;
                        winbs.transform.localPosition = localPos;
                        canvas.sortingOrder = m_StartDepth * (int)type + undercount * m_depth;
                        winbs.V_SortingOrder = canvas.sortingOrder;
                        break;
                    case T_UIConfig.UI_TYPE.WIN:
                        wincount++;
                        winbs.transform.SetSiblingIndex(wincount * 2);
                        localPos.z = m_StartDepth - wincount * m_depth;
                        winbs.transform.localPosition = localPos;
                        canvas.sortingOrder = m_StartDepth * (int)type + wincount * m_depth;
                        winbs.V_SortingOrder = canvas.sortingOrder;
                        break;
                    case T_UIConfig.UI_TYPE.TIP:
                        tipcount++;
                        winbs.transform.SetSiblingIndex(tipcount * 2);
                        localPos.z = m_StartDepth - tipcount * m_depth;
                        winbs.transform.localPosition = localPos;
                        canvas.sortingOrder = m_StartDepth * (int)type + tipcount * m_depth;
                        winbs.V_SortingOrder = canvas.sortingOrder;
                        break;
                    case T_UIConfig.UI_TYPE.TOP:
                        topcount++;
                        winbs.transform.SetSiblingIndex(topcount * 2);
                        localPos.z = m_StartDepth - topcount * m_depth;
                        winbs.transform.localPosition = localPos;
                        canvas.sortingOrder = m_StartDepth * (int)type + topcount * m_depth;
                        winbs.V_SortingOrder = canvas.sortingOrder;
                        break;
                }
            }
        }

        if (currentType == T_UIConfig.UI_TYPE.WIN)
        {
            CheckHidenWin();
        }
        //设置最上层WIN
        OnTopWin(tmpTop);
    }
    /// <summary>
    /// 检查是否隐藏UI
    /// </summary>
    void CheckHidenWin()
    {
        bool isHiden = false;
        for (int i = m_IdPosList.Count - 1; i >= 0; i--)
        {
            int id = m_IdPosList[i];
            if (m_UIList.ContainsKey(id))
            {
                WindowBase winbs = m_UIList[id];
                if (winbs == null) continue;
                T_UIConfig config = winbs.F_GetUIConfig();
                if (config.UI_Type == T_UIConfig.UI_TYPE.WIN)
                {
                    if (isHiden == true)
                    {
                        TransformUtil.ChangeLayer(winbs.transform, 0, 5);
                    }
                    else
                    {
                        TransformUtil.ChangeLayer(winbs.transform, 5, 0);
                    }

                    if (config.ShowMask == (int)MaskType.BlurTexture || config.ShowMask == (int)MaskType.HidenUnderUI)
                    {
                        isHiden = true;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 检查是否是最上层
    /// </summary>
    /// <param name="winbs"></param>
    void CheckTop(WindowBase winbs, ref WindowBase tmpTop)
    {
        T_UIConfig config = GetConfigData(winbs.F_GetEUI());
        if (config == null) return;
        if (config.TopFilter == true) return;
        if (tmpTop == null)
        {
            tmpTop = winbs;
        }
        else
        {

            if ((int)winbs.F_GetUIType() >= (int)tmpTop.F_GetUIType())
            {
                tmpTop = winbs;
            }
        }
    }
    /// <summary>
    ///  最上层
    /// </summary>
    /// <param name="tmpTop"></param>
    void OnTopWin(WindowBase tmpTop)
    {
        if (tmpTop != null)
        {
            if (topWinBase == null)
            {
                topWinBase = tmpTop;
                topWinBase.F_TopWin(true);
                return;
            }
            if (tmpTop.F_GetEUI() != TopWinBase.F_GetEUI())
            {
                topWinBase.F_TopWin(false);
                topWinBase = tmpTop;
                topWinBase.F_TopWin(true);
            }
        }
    }




    /// <summary>
    /// 获取EUI
    /// </summary>
    /// <param name="id">id</param>
    /// <returns></returns>
    T_UIConfig.E_UI GetConfigUI(int id)
    {
        if (m_UIList.ContainsKey(id))
        {
            return m_UIList[id].F_GetEUI();
        }
        return T_UIConfig.E_UI.NONE;
    }
    /// <summary>
    /// 判断UI类型
    /// </summary>
    /// <param name="id">UI 唯一配置表ID</param>
    /// <returns>UI类型</returns>
    T_UIConfig.UI_TYPE CheckUIType(T_UIConfig.E_UI id)
    {
        T_UIConfig config = GetConfigData(id);
        if (config == null)
        {
            return T_UIConfig.UI_TYPE.NONE;
        }
        return config.UI_Type;
    }

    /// <summary>
    /// 获取UI配置表信息
    /// </summary>
    /// <param name="id">UI 唯一配置表ID</param>
    /// <returns></returns>
    T_UIConfig GetConfigData(T_UIConfig.E_UI id)
    {
        if (id == T_UIConfig.E_UI.NONE) return null;
        T_UIConfig config = ConfigManager.UIConfig.GetData(id);
        if (config == null)
        {
            return null;
        }
        return config;
    }
    /// <summary>
    /// 获取不同类型的实例个数
    /// </summary>
    /// <param name="uitype"></param>
    /// <returns></returns>
    int GetCountByUIType(T_UIConfig.UI_TYPE uitype)
    {
        int count = 0;
        using (var view = m_UIList.GetEnumerator())
        {
            while (view.MoveNext())
            {
                var winbs = view.Current.Value;
                if (winbs != null)
                {
                    if (winbs.F_GetUIType() == uitype)
                    {
                        count++;
                    }
                }
            }
            view.Dispose();
        }
        return count;
    }

    /// <summary>
    /// 生成id
    /// </summary>
    /// <returns></returns>
    int buildId()
    {
        int id = 0;

        while (m_DataModule.ContainsKey(id))
        {
            id++;
        }
        return id;
    }
    /// <summary>
    /// 清除
    /// </summary>
    void Clear()
    {
        m_UIList.Clear();
        m_EUIToIdDec.Clear();
        m_IdPosList.Clear();
        m_DataModule.Clear();
    }
    #endregion
}
