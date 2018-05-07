using UnityEngine;
public class AppGlobalMonitor : MonoBehaviour
{
    #region 初始化
    private static AppGlobalMonitor m_instance = null;

    public static AppGlobalMonitor F_GetInstance()
    {
        if (m_instance == null)
        {
            m_instance = GameObject.FindObjectOfType<AppGlobalMonitor>();
            if (m_instance == null)
            {
                GameObject go = new GameObject("AppGlobalMonitor");
                GameObject parent = GameObject.Find("Manager");
                if (parent != null)
                {
                    go.transform.SetParent(parent.transform);
                }
                m_instance = go.AddComponent<AppGlobalMonitor>();
            }
        }
        return m_instance;
    }
    #endregion

    void Update()
    {
        CalcFps();
        CalcMemory();
    }

    void OnGUI()
    {
        GUI.color = Color.white;
        GUI.backgroundColor = Color.black;
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fontSize = 20;
        string fpsStr = "<color=#" + (_Fps > 20 ? "00ff00" : "ff0000") + ">FPS:" + _Fps.ToString("0.00") + "</color>";
        string memStr = "<color=#" + (_Memory < 300 ? "00ff00" : "ff0000") + ">MEM:" + _Memory.ToString("0.00") + "</color>";
        if (GUI.Button(new Rect(0, 10, 320, 70), fpsStr + " , " + memStr + "\n", myButtonStyle))
        {
            EventManager.GetInstance().F_Notity(Event.LOAD);
        }
    }


    public float _UpdateInterval = 0.5F;
    private float _LastInterval;
    private int _Frames = 0;
    private float _Fps;
    private float _Memory;
    private float _MemoryInterval = 0;
    private void CalcFps()
    {
        ++_Frames;
        if (Time.realtimeSinceStartup > _LastInterval + _UpdateInterval)
        {
            _Fps = _Frames / (Time.realtimeSinceStartup - _LastInterval);
            _Frames = 0;
            _LastInterval = Time.realtimeSinceStartup;
        }
    }

    private void CalcMemory()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _MemoryInterval += Time.deltaTime;
            if (_MemoryInterval > 1f)
            {
                _MemoryInterval = 0;
                //_Memory = MobileApi.Instance.GetUsedMemory() / 1024f;
            }
        }
        else
        {
            _Memory = UnityEngine.Profiler.GetTotalAllocatedMemory() / 1024f / 1024f;
        }

    }

}
