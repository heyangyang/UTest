using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void OnClickDelegate(GameObject obj);
public class ViewBase : MonoBehaviour
{
    private int viewBaseId  = 0;

    Dictionary<int, ViewBase> DicView = new Dictionary<int, ViewBase>();

    /// <summary>
    /// 是否初始化
    /// </summary>
    public bool m_IsInit = false;
    private int FreeViewBaseId
    {
        get
        {
            viewBaseId++;
            return viewBaseId;
        }
    }

    //初始化
    public virtual void F_OnInit()
    {
       
    }
    public virtual void RegisterEvent()
    {
        using (var list = DicView.GetEnumerator())
        {
            while (list.MoveNext())
            {
                list.Current.Value.RegisterEvent();
            }
            list.Dispose();
        }
    }
    public virtual void UnRegisterEvent()
    {
        using (var list = DicView.GetEnumerator())
        {
            while (list.MoveNext())
            {
                list.Current.Value.UnRegisterEvent();
            }
            list.Dispose();
        }
    }

    protected Button FindButton(string buttonPath, UnityAction action = null, bool isAnimator = true, bool isSound = true,T_Etc.ID SoundId = T_Etc.ID.ButtonSound)
    {
        return ViewBase.FindButtonByTrans(transform, buttonPath, action, isAnimator, isSound, SoundId);
    }

    /// <summary>
    /// 查找Button
    /// </summary>
    /// <param name="buttonPath">路径</param>
    /// <param name="action">事件</param>
    /// <param name="isAnimator">是否带点击动画</param>
    /// <param name="isSound">是否有音效</param>
    /// <param name="SoundId">音效Id (T_Etc表配置)</param>
    /// <returns></returns>
    public static Button FindButtonByTrans(Transform tf, string buttonPath, UnityAction action = null, bool isAnimator = true, bool isSound = true, T_Etc.ID SoundId = T_Etc.ID.ButtonSound)
    {
        Transform child = tf;
        if (!string.IsNullOrEmpty(buttonPath))
        {
            child = tf.FindChild(buttonPath);
        }
        
        if (child == null){
            Loger.Error("not find button:" + buttonPath);
            return null;
        }
        Button btn = child.GetComponent<Button>();
        if (btn == null){
            btn = child.gameObject.AddComponent<Button>();
        }
        //添加事件
        if (action != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => {
                if (isSound == true)
                {
                    //音效
                    //SoundManager.GetInstance().F_PlayUISound(ConfigManager.Etc.GetData(SoundId).IntValue);
                }
                action();
            });
        }
        if (isAnimator == true) {
            btn.transition = Selectable.Transition.None;
            TransformUtil.Add<ButtonAnimator>(child);
        }
        return btn;
    }
    /// <summary>
    /// 添加动画
    /// </summary>
    /// <param name="obj"></param>
    protected void AddAnimator(GameObject obj)
    {
        TransformUtil.Add<ButtonAnimator>(obj);
    }
    /// <summary>
    /// 删除动画
    /// </summary>
    /// <param name="obj"></param>
    protected void RemoveAnimator(GameObject obj)
    {
        TransformUtil.Remove<ButtonAnimator>(obj);
    }
    /// <summary>
    /// 查找Text
    /// </summary>
    /// <param name="textPath">路径</param>
    /// <returns></returns>
    protected Text FindText(string textPath)
    {
        Transform child = transform.FindChild(textPath);
        if (child == null){
            Loger.Error("not find text: " + textPath);
            return null;
        }
        Text text = child.GetComponent<Text>();
        if (text == null){
            Loger.Error("not find text  component");
            return null;
        }
        return text;
    }
    /// <summary>
    /// 查找GridLayoutGroup;
    /// </summary>
    /// <param name="textPath"></param>
    /// <returns></returns>
    protected GridLayoutGroup FindGridLayoutGroup(string textPath)
    {
        Transform child = transform.FindChild(textPath);
        if (child == null)
        {
            Loger.Error("not find text: " + textPath);
            return null;
        }
        GridLayoutGroup dropDown = child.GetComponent<GridLayoutGroup>();
        if (dropDown == null)
        {
            Loger.Error("not find text  component");
            return null;
        }
        return dropDown;
    }

    /// <summary>
    /// 查找RawIamge
    /// </summary>
    /// <param name="imagePath"></param>
    /// <returns></returns>
    protected RawImage FindRawImage(string imagePath)
    {
        Transform child = transform.FindChild(imagePath);
        if (child == null)
        {
            Loger.Error("not find image: " + imagePath);
            return null;
        }
        RawImage image = child.GetComponent<RawImage>();
        if (image == null)
        {
            Loger.Error("not find image  component");
            return null;
        }
        return image;
    }

    /// <summary>
    /// 查找Image
    /// </summary>
    /// <param name="imagePath"></param>
    /// <returns></returns>
    protected Image FindImage(string imagePath)
    {
        Transform child = transform.FindChild(imagePath);
        if (child == null){
            Loger.Error("not find image: "+imagePath);
            return null;
        }
        Image image = child.GetComponent<Image>();
        if (image == null){
            Loger.Error("not find image  component");
            return null;
        }
        return image;
    }

    /// <summary>
    /// 查找InputField
    /// </summary>
    /// <param name="imagePath"></param>
    /// <returns></returns>
    protected InputField FindInputField(string path)
    {
        Transform child = transform.FindChild(path);
        if (child == null){
            Loger.Error("not find InputField: " + path);
            return null;
        }
        InputField inputfield = child.GetComponent<InputField>();
        if (inputfield == null){
            Loger.Error("not find InputField  component");
            return null;
        }
        return inputfield;
    }

    protected Dropdown FindDropdown(string path)
    {
        Transform child = transform.FindChild(path);
        if (child == null) {
            Loger.Error("not find InputField: " + path);
            return null;
        }
        Dropdown dropdown = child.GetComponent<Dropdown>();
        if (dropdown == null){
            Loger.Error("not find InputField  component");
            return null;
        }
        return dropdown;
    }

    protected Toggle FindToggle(string path,UnityAction<bool> action = null)
    {
        Transform child = transform.FindChild(path);
        if (child == null)
        {
            Loger.Error("not find Toggle: " + path);
            return null;
        }
        Toggle toggle = child.GetComponent<Toggle>();
        if (toggle == null)
        {
            Loger.Error("not find Toggle  component");
            return null;
        }
        if (action != null)
        {
            toggle.onValueChanged.AddListener(action);
        }
        return toggle;
    }

    protected ScrollRect FindScrollRect(string path)
    {
        Transform child = transform.FindChild(path);
        if (child == null)
        {
            Loger.Error("not find ScrollRect: " + path);
            return null;
        }
        ScrollRect scroll = child.GetComponent<ScrollRect>();
        if (scroll == null)
        {
            Loger.Error("not find ScrollRect  component");
            return null;
        }
        return scroll;
    }

    protected RectTransform FindRectTransform(string path)
    {
        Transform child = transform.FindChild(path);
        if (child == null)
        {
            Loger.Error("not find RectTransform: " + path);
            return null;
        }
        RectTransform rectTransform = child.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Loger.Error("not find RectTransform  component");
            return null;
        }
        return rectTransform;
    }


    protected Scrollbar FindScrollbar(string path)
    {
        Transform child = transform.FindChild(path);
        if (child == null)
        {
            Loger.Error("not find Slider: " + path);
            return null;
        }
        Scrollbar scrollbar = child.GetComponent<Scrollbar>();
        if (scrollbar == null)
        {
            Loger.Error("not find Slider  component");
            return null;
        }
        return scrollbar;
    }


    protected Slider FindSlider(string path)
    {
        Transform child = transform.FindChild(path);
        if (child == null)
        {
            Loger.Error("not find Slider: " + path);
            return null;
        }
        Slider slider = child.GetComponent<Slider>();
        if (slider == null)
        {
            Loger.Error("not find Slider  component");
            return null;
        }
        return slider;
    }

    /// <summary>
    /// 查找子节点 transform
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    protected Transform FindTransform(string path)
    {
        Transform childtrans = transform.FindChild(path);
        if (childtrans == null)
        {
            Loger.Error("not find Transform by "+ path);
            return null;
        }
        return childtrans;
    }
    /// <summary>
    /// 查找子节点 GameObject
    /// </summary>
    /// <param name="path">path</param>
    /// <returns></returns>
    protected GameObject FindGameObject(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return gameObject;
        }

        Transform childtrans = transform.FindChild(path);
        if (childtrans == null)
        {
            Loger.Error("not find GameObject by  "+path);
            return null;
        }
        return childtrans.gameObject; 
    }

    /// <summary>
    /// 添加继承ViewBase的组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    protected virtual T AddView<T>(string path) where T :ViewBase
    {
        GameObject obj = FindGameObject(path);
        T view = TransformUtil.Add<T>(obj);
        if (view.m_IsInit == false)
        {
            view.F_OnInit();
            view.m_IsInit = true;
        }
        DicView[FreeViewBaseId] = view;
        return view;
    }


      /// <summary>
    /// 添加继承ViewBase的组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    protected virtual T AddView<T>(GameObject obj) where T : ViewBase
    {
        T view = TransformUtil.Add<T>(obj);
        if (view.m_IsInit == false)
        {
            view.F_OnInit();
            view.m_IsInit = true;
        }
        DicView[FreeViewBaseId] = view;
        return view;
    }
    /// <summary>
    /// 子节点添加组件
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="path">子节点 path</param>
    /// <returns>组件实例</returns>
    protected T AddComponent<T>(string path) where T : Component
    {
        GameObject obj = FindGameObject(path);
        return  TransformUtil.Add<T>(obj);
    }
    /// <summary>
    /// 添加点击按钮
    /// </summary>
    protected GameObject AddClick(string path, EventTriggerListener.VoidDelegate onclick = null, bool isAnima = true, bool isSound = true, T_Etc.ID SoundId = T_Etc.ID.ButtonSound)
    { 
        GameObject obj = FindGameObject(path);
        AddClick(obj, onclick, isAnima, isSound, SoundId);
        return obj;
    }

    /// <summary>
    /// 添加点击按钮
    /// </summary>
    protected GameObject AddClick(GameObject obj, EventTriggerListener.VoidDelegate onclick = null, bool isAnima = true, bool isSound = true, T_Etc.ID SoundId = T_Etc.ID.ButtonSound)
    {
        if (onclick != null)
        {
            //EventTriggerListener.Get(obj).onClick = onclick;
            if (isAnima == true)
            {
                AddAnimator(obj);
            }
            EventTriggerListener.Get(obj).onClick = 
            (gobj) =>{
                if (isSound == true)
                {
                    //音效
                    //SoundManager.GetInstance().F_PlayUISound(ConfigManager.Etc.GetData(SoundId).IntValue);
                }
                onclick(gobj);
            };
            
        }
        return obj;
    }
   
    /// <summary>
    /// 添加全屏点击事件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="onclick"></param>
    /// <param name="isAnima"></param>
    /// <returns></returns>
    protected GameObject AddFullClick(string path, EventTriggerListener.VoidDelegate onclick = null, bool isAnima = false)
    {
        GameObject obj = FindGameObject(path);
        TransformUtil.Add<OnlyRaycastPanel>(obj);
        AddClick(obj, onclick, isAnima);
        return obj;
    }

    /// <summary>
    /// 添加全屏点击事件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="onclick"></param>
    /// <param name="isAnima"></param>
    /// <returns></returns>
    protected GameObject AddFullClick(GameObject obj, EventTriggerListener.VoidDelegate onclick = null, bool isAnima = false)
    {
        TransformUtil.Add<OnlyRaycastPanel>(obj);
        AddClick(obj, onclick, isAnima);
        return obj;
    }
}