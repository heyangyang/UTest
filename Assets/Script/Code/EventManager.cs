using System.Collections.Generic;
using System;

public class EventManager
{
    Dictionary<string, List<Action<string, long>>> m_AllActionLong;
    Dictionary<string, List<Action<string, object>>> m_AllActionObject;
    Dictionary<string, List<Action<string, int>>> m_AllActionInt;
    Dictionary<string, List<Action<string, float>>> m_AllActionFloat;
    Dictionary<string, List<Action<string>>> m_AllActionZero;

    private static EventManager m_Instance;
    public static EventManager GetInstance()
    {
        if (m_Instance == null)
        {
            m_Instance = new EventManager();
        }
        return m_Instance;
    }

    public EventManager()
    {
        m_AllActionObject = new Dictionary<string, List<Action<string, object>>>();
        m_AllActionLong = new Dictionary<string, List<Action<string, long>>>();
        m_AllActionInt = new Dictionary<string, List<Action<string, int>>>();
        m_AllActionFloat = new Dictionary<string, List<Action<string, float>>>();
        m_AllActionZero = new Dictionary<string, List<Action<string>>>();
    }


    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="pEvent">事件名称</param>
    /// <param name="pAction">事件回调</param>
    public void F_RegisterObject(string pEvent, Action<string, object> pAction)
    {
        if (!m_AllActionObject.ContainsKey(pEvent))
        {
            m_AllActionObject.Add(pEvent, new List<Action<string, object>>());
        }

        var list = m_AllActionObject[pEvent];
        if (!list.Contains(pAction))
        {
            list.Add(pAction);
        }
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="pEvent">事件名称</param>
    /// <param name="pAction">事件回调</param>
    public void F_RegisterInt(string pEvent, Action<string, int> pAction)
    {
        if (!m_AllActionInt.ContainsKey(pEvent))
        {
            m_AllActionInt.Add(pEvent, new List<Action<string, int>>());
        }

        var list = m_AllActionInt[pEvent];
        if (!list.Contains(pAction))
        {
            list.Add(pAction);
        }
    }
    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="pEvent">事件名称</param>
    /// <param name="pAction">事件回调</param>
    public void F_RegisterLong(string pEvent, Action<string, long> pAction)
    {
        if (!m_AllActionLong.ContainsKey(pEvent))
        {
            m_AllActionLong.Add(pEvent, new List<Action<string, long>>());
        }

        var list = m_AllActionLong[pEvent];
        if (!list.Contains(pAction))
        {
            list.Add(pAction);
        }
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="pEvent">事件名称</param>
    /// <param name="pAction">事件回调</param>
    public void F_RegisterFloat(string pEvent, Action<string, float> pAction)
    {
        if (!m_AllActionFloat.ContainsKey(pEvent))
        {
            m_AllActionFloat.Add(pEvent, new List<Action<string, float>>());
        }

        var list = m_AllActionFloat[pEvent];
        if (!list.Contains(pAction))
        {
            list.Add(pAction);
        }
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="pEvent">事件名称</param>
    /// <param name="pAction">事件回调</param>
    public void F_RegisterZero(string pEvent, Action<string> pAction)
    {
        if (!m_AllActionZero.ContainsKey(pEvent))
        {
            m_AllActionZero.Add(pEvent, new List<Action<string>>());
        }

        var list = m_AllActionZero[pEvent];
        if (!list.Contains(pAction))
        {
            list.Add(pAction);
        }
    }

    /// <summary>
    /// 通知事件
    /// </summary>
    /// <param name="pEvent">事件名称</param>
    /// <param name="pValue">回调参数</param>
    public void F_Notity(string pEvent, object pValue)
    {
        if (!m_AllActionObject.ContainsKey(pEvent))
        {
            return;
        }

        var list = m_AllActionObject[pEvent];
        for (int i = 0; i < list.Count; i++)
        {
            try
            {
                list[i].Invoke(pEvent, pValue);
            }
            catch (Exception e)
            {
                Loger.Error(e);
            }
        }
    }

    /// <summary>
    /// 通知事件
    /// </summary>
    /// <param name="pEvent">事件名称</param>
    /// <param name="pValue">回调参数</param>
    public void F_Notity(string pEvent, long pValue)
    {
        if (!m_AllActionLong.ContainsKey(pEvent))
        {
            return;
        }

        var list = m_AllActionLong[pEvent];
        for (int i = 0; i < list.Count; i++)
        {
            try
            {
                list[i].Invoke(pEvent, pValue);
            }
            catch (Exception e)
            {
                Loger.Error(e);
            }
        }
    }

    /// <summary>
    /// 通知事件
    /// </summary>
    /// <param name="pEvent">事件名称</param>
    /// <param name="pValue">回调参数</param>
    public void F_Notity(string pEvent, int pValue)
    {
        if (!m_AllActionInt.ContainsKey(pEvent))
        {
            return;
        }

        var list = m_AllActionInt[pEvent];
        for (int i = 0; i < list.Count; i++)
        {
            try
            {
                list[i].Invoke(pEvent, pValue);
            }
            catch (Exception e)
            {
                Loger.Error(e);
            }
        }
    }

    /// <summary>
    /// 通知事件
    /// </summary>
    /// <param name="pEvent">事件名称</param>
    /// <param name="pValue">回调参数</param>
    public void F_Notity(string pEvent, float pValue)
    {
        if (!m_AllActionFloat.ContainsKey(pEvent))
        {
            return;
        }

        var list = m_AllActionFloat[pEvent];
        for (int i = 0; i < list.Count; i++)
        {
            try
            {
                list[i].Invoke(pEvent, pValue);
            }
            catch (Exception e)
            {
                Loger.Error(e);
            }
        }
    }

    /// <summary>
    /// 通知事件
    /// </summary>
    /// <param name="pEvent">事件名称</param>
    /// <param name="pValue">回调参数</param>
    public void F_Notity(string pEvent)
    {
        if (!m_AllActionZero.ContainsKey(pEvent))
        {
            return;
        }

        var list = m_AllActionZero[pEvent];
        for (int i = 0; i < list.Count; i++)
        {
            try
            {
                list[i].Invoke(pEvent);
            }
            catch (Exception e)
            {
                Loger.Error(e);
            }
        }
    }

    /// <summary>
    /// 移除对应名称所有事件
    /// </summary>
    /// <param name="pEvent">事件名称</param>
    public void F_Remove(string pEvent)
    {
        if (m_AllActionObject.ContainsKey(pEvent))
        {
            m_AllActionObject.Remove(pEvent);
        }
        if (m_AllActionInt.ContainsKey(pEvent))
        {
            m_AllActionInt.Remove(pEvent);
        }
        if (m_AllActionFloat.ContainsKey(pEvent))
        {
            m_AllActionFloat.Remove(pEvent);
        }
        if (m_AllActionZero.ContainsKey(pEvent))
        {
            m_AllActionZero.Remove(pEvent);
        }
        if (m_AllActionLong.ContainsKey(pEvent))
        {
            m_AllActionLong.Remove(pEvent);
        }
    }

    /// <summary>
    /// 移除指定事件
    /// </summary>
    /// <param name="pAction">指定事件</param>
    public void F_RemoveObject(string pEvent, Action<string, object> pAction)
    {
        if (m_AllActionObject.ContainsKey(pEvent))
        {
            var tmpList = m_AllActionObject[pEvent];
            for (int i = tmpList.Count - 1; i >= 0; i--)
            {
                if (tmpList[i] == pAction)
                {
                    tmpList.RemoveAt(i);
                    break;
                }
            }
        }

    }

    /// <summary>
    /// 移除指定事件
    /// </summary>
    /// <param name="pAction">指定事件</param>
    public void F_RemoveInt(string pEvent, Action<string, int> pAction)
    {
        if (m_AllActionInt.ContainsKey(pEvent))
        {
            var tmpList = m_AllActionInt[pEvent];
            for (int i = tmpList.Count - 1; i >= 0; i--)
            {
                if (tmpList[i] == pAction)
                {
                    tmpList.RemoveAt(i);
                    break;
                }
            }
        }
    }
    /// <summary>
    /// 移除指定事件
    /// </summary>
    /// <param name="pAction">指定事件</param>
    public void F_RemoveLong(string pEvent, Action<string, long> pAction)
    {
        if (m_AllActionLong.ContainsKey(pEvent))
        {
            var tmpList = m_AllActionLong[pEvent];
            for (int i = tmpList.Count - 1; i >= 0; i--)
            {
                if (tmpList[i] == pAction)
                {
                    tmpList.RemoveAt(i);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 移除指定事件
    /// </summary>
    /// <param name="pAction">指定事件</param>
    public void F_RemoveFloat(string pEvent, Action<string, float> pAction)
    {
        if (m_AllActionFloat.ContainsKey(pEvent))
        {
            var tmpList = m_AllActionFloat[pEvent];
            for (int i = tmpList.Count - 1; i >= 0; i--)
            {
                if (tmpList[i] == pAction)
                {
                    tmpList.RemoveAt(i);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 移除指定事件
    /// </summary>
    /// <param name="pAction">指定事件</param>
    public void F_RemoveZero(string pEvent, Action<string> pAction)
    {
        if (m_AllActionZero.ContainsKey(pEvent))
        {
            var tmpList = m_AllActionZero[pEvent];
            for (int i = tmpList.Count - 1; i >= 0; i--)
            {
                if (tmpList[i] == pAction)
                {
                    tmpList.RemoveAt(i);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 清理所有注册事件
    /// </summary>
    public void F_Clear()
    {
        m_AllActionFloat.Clear();
        m_AllActionInt.Clear();
        m_AllActionObject.Clear();
        m_AllActionZero.Clear();
        m_AllActionLong.Clear();
    }
}
