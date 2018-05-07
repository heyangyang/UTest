using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public  class WindowBase : ViewBase
{
    #region 变量
    /// <summary>
    /// 界面id
    /// </summary>
    private int m_id;
    /// <summary>
    /// UI 配置表唯一ID
    /// </summary>
    private T_UIConfig.E_UI m_ui;

    /// <summary>
    /// 界面类型
    /// </summary>
    private T_UIConfig.UI_TYPE  m_Type;
    /// <summary>
    /// 
    /// </summary>
    private T_UIConfig m_config;
    /// <summary>
    /// 层级
    /// </summary>
    private int m_SortingOrder;
    /// <summary>
    /// 层级
    /// </summary>
    public int V_SortingOrder
    {
        get { return m_SortingOrder; }
        set { 
            m_SortingOrder = value;
        }
    }
    #endregion
    

    #region 对外接口
    /// <summary>
    /// 初始设置
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ui"></param>
    /// <param name="type"></param>
    public void F_SetId(int id,T_UIConfig.E_UI ui, T_UIConfig.UI_TYPE type)
    {
        m_id = id;
        m_ui = ui;
        m_Type = type;
        m_config = ConfigManager.UIConfig.GetData(ui);
        ShowAnimation();
    }

    public void F_IsInit()
    {
        m_IsInit = true;
    }
    /// <summary>
    /// 获取界面ID
    /// </summary>
    /// <returns></returns>
    public int F_GetId()
    {
        return m_id;
    }

    /// <summary>
    /// 获取配置表唯一ID
    /// </summary>
    /// <returns></returns>
    public T_UIConfig.E_UI F_GetEUI()
    {
        return m_ui;
    }

    /// <summary>
    /// 获取UItype
    /// </summary>
    /// <returns></returns>
    public T_UIConfig.UI_TYPE F_GetUIType()
    {
        return m_Type;
    }

    public T_UIConfig F_GetUIConfig()
    {
        return m_config;
    }
    #endregion

    #region 继承
	
	// Update is called once per frame
   
    /// <summary>
    /// 初始化
    /// </summary>
    public override void F_OnInit()
    {
        base.F_OnInit();
    }
    /// <summary>
    /// 参数
    /// </summary>
    /// <param name="obj"></param>
    public virtual void F_OnData(object obj)
    {
      
    }

    /// <summary>
    /// 跳转;
    /// </summary>
    public virtual void F_OnJump(object data, params int[] pageIds)
    { 
    
    }

    /// <summary>
    /// 关闭界面调用
    /// </summary>
    public virtual void F_OnRemove()
    {
    
    }
    /// <summary>
    /// 更新
    /// </summary>
    public virtual void Update()
    {

    }
    /// <summary>
    /// 获取大小
    /// </summary>
    /// <returns></returns>
    public virtual Vector2 F_GetSize()
    {
        return (transform as RectTransform).sizeDelta;
    }
    /// <summary>
    /// 设置位置
    /// </summary>
    public virtual void F_InitPos()
    {
        RectTransform tf = transform as RectTransform;
        tf.sizeDelta = Vector2.zero;
        tf.position = Vector3.zero;
    }

    public virtual void F_TopWin(bool isTop)
    { 
        
    }
    #endregion

    /// <summary>
    /// 关闭
    /// </summary>
    protected virtual void F_Close()
    {
        HidenAnimation();
    }
    /// <summary>
    /// 添加音效和背景音乐
    /// </summary>
    public void F_AddSound()
    {
        //打开音效
        //SoundManager.GetInstance().F_PlayUISound(m_config.SE);
        //背景音乐
        //SoundManager.GetInstance().F_PushBGM(m_config.BGM);
    }
    /// <summary>
    /// 关闭音乐
    /// </summary>
    /// <param name="config"></param>
    public void F_RemoveSound()
    {
        //关闭背景音乐
        //SoundManager.GetInstance().F_PopBGM(m_config.BGM);
    }

    void ShowAnimation()
    {
        T_UIConfig config = ConfigManager.UIConfig.GetData(m_ui);
        if (config.IsAnimation ==1)
        {
            this.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
            //LeanTween.scale(this.gameObject, Vector3.one, 0.3f).setEaseOutBack();
        }
    }
    void HidenAnimation()
    {
        T_UIConfig config = ConfigManager.UIConfig.GetData(m_ui);
        if (config.IsAnimation ==1)
        {
            if (config.IsCloseWorldCamera == true)
            {
                UIManager.F_GetInstance().ShowSceneCameraObj();
            }
            this.transform.localScale = Vector3.one;

            transform.DOScale(new Vector3(0.5f,0.5f,0.5f), 0.3f).SetEase(Ease.InBack).OnComplete(HidenComplete);
           // LeanTween.scale(this.gameObject, Vector3.zero, 0.3f).setEaseInBack().setOnComplete(HidenComplete);

        }
        else
        {
            UIManager.F_GetInstance().F_CloseUI(m_id);
        }
    }
    void HidenComplete()
    {
        UIManager.F_GetInstance().F_CloseUI(m_id);
    }
}
