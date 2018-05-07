using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
/// <summary>
/// 按钮监听事件系统
/// </summary>
public  class EventTriggerListener:EventTrigger
{
    enum State
    {
        None,
        Down,
        Press,
        Up,
    }
    public enum PressState
    {
        /// <summary>
        /// 未开始
        /// </summary>
        None = 0,
        /// <summary>
        /// 长按开始（只调用一次）
        /// </summary>
        PressStart,
        /// <summary>
        /// 长按中（每帧触发回调）
        /// </summary>
        Pressing,
        /// <summary>
        /// 长按结束（只调用一次）
        /// </summary>
        PressEnd,
    }
    State state;
    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onBeginDrag;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;

    private Action<GameObject, PressState> _onPressAction; //提供另一个长按事件

    public VoidDelegate onEndDrag;


    private float _pressTriggerTime;
    private float _currentPressTime;
    private bool _isPressing;
    static public EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if (listener == null) listener = go.AddComponent<EventTriggerListener>();
        return listener;
    }

    /// <summary>
    /// 添加长按事件
    /// </summary>
    /// <param name="pressAction">长按回调，触发长按时调用一次pressAction(go, PressState.PressStart) , 触发后每帧调用一次pressAction(go, PressState.Pressing), 松开时调用pressAction(go, PressState.PressEnd)</param>
    /// <param name="triggerTime">按住多久触发长按</param>
    public void AddPressEvent(Action<GameObject, PressState> pressAction, float triggerTime = 1f)
    {
        _onPressAction = pressAction;
        _pressTriggerTime = triggerTime;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (state == State.Down)
        {
            state = State.Up;
            if (onClick != null) onClick(gameObject);
        }
        state = State.None;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
        state = State.Down;
        _currentPressTime = 0;
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        state = State.None;
        if (onExit != null) onExit(gameObject);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null) onBeginDrag(gameObject);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDrag != null) onEndDrag(gameObject);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) onSelect(gameObject);
    }
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null) onUpdateSelect(gameObject);
    }

    void Update()
    {
        if (state == State.Down)
        {
            if (_onPressAction != null)
            {
                _currentPressTime += Time.deltaTime;
                if (_currentPressTime >= _pressTriggerTime)
                {
                    state = State.Press;
                    _isPressing = true;
                    InvokePressAction(PressState.PressStart);
                }
            }
        }
        else if (state == State.Press)
        {
            InvokePressAction(PressState.Pressing);
        }
        else if (_isPressing)
        {
            _isPressing = false;
            _currentPressTime = 0;
            InvokePressAction(PressState.PressEnd);
        }
    }

    void OnDisable()
    {
        state = State.None;
        if (_isPressing)
        {
            _isPressing = false;
            InvokePressAction(PressState.PressEnd);
        }
        _currentPressTime = 0;
    }

    private void InvokePressAction(PressState ps)
    {
        if (_onPressAction != null)
        {
            _onPressAction.Invoke(gameObject, ps);
        }
    }
}

