using System.Collections.Generic;
using UnityEngine;

public class TransformUtil
{
    /// <summary>
    /// 搜索子物体组件-GameObject版
    /// </summary>
    public static T Get<T>(GameObject go, string subnode) where T : Component
    {
        if (go != null)
        {
            Transform sub = go.transform.FindChild(subnode);
            if (sub != null) return sub.GetComponent<T>();
        }
        return null;
    }

    /// <summary>
    /// 搜索子物体组件-Transform版
    /// </summary>
    public static T Get<T>(Transform go, string subnode) where T : Component
    {
        if (go != null)
        {
            Transform sub = go.FindChild(subnode);
            if (sub != null) return sub.GetComponent<T>();
        }
        return null;
    }

    /// <summary>
    /// 搜索子物体组件-Component版
    /// </summary>
    public static T Get<T>(Component go, string subnode) where T : Component
    {
        return go.transform.FindChild(subnode).GetComponent<T>();
    }

    /// <summary>
    /// 添加组件
    /// </summary>
    public static T Add<T>(GameObject go) where T : Component
    {
        if (go != null)
        {
            T ts = go.GetComponent<T>();
            if (ts == null)
            {
                return go.gameObject.AddComponent<T>();
            }
            return ts;
        }
        return null;
    }
    /// <summary>
    /// 删除组件
    /// </summary>
    public static void Remove<T>(GameObject go) where T : Component
    {
        if (go != null)
        {
            T ts = go.GetComponent<T>();
            if (ts != null)
            {
                GameObject.Destroy(ts);
            }
        }
    }
    /// <summary>
    /// 删除组件
    /// </summary>
    public static void Remove<T>(Transform go) where T : Component
    {
        Remove<T>(go.gameObject);
    }
    /// <summary>
    /// 添加组件
    /// </summary>
    public static T Add<T>(Transform go) where T : Component
    {
        return Add<T>(go.gameObject);
    }


    public static Transform Child(Transform tf, string name, bool isContains = false)
    {
        if ((!isContains && tf.name == name) || (isContains && tf.name.IndexOf(name) != -1))
        {
            return tf;
        }
        int num = tf.childCount;
        for (int i = 0; i < num; i++)
        {
            Transform result = Child(tf.GetChild(i), name);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
    /// <summary>
    /// 取平级对象
    /// </summary>
    public static GameObject Peer(GameObject go, string subnode)
    {
        return Peer(go.transform, subnode);
    }

    /// <summary>
    /// 取平级对象
    /// </summary>
    public static GameObject Peer(Transform go, string subnode)
    {
        Transform tran = go.parent.FindChild(subnode);
        if (tran == null) return null;
        return tran.gameObject;
    }


    /// <summary>
    /// 清除所有子节点
    /// </summary>
    public static void ClearChild(Transform go)
    {
        if (go == null) return;
        for (int i = go.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(go.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 根据角度获取位置
    /// </summary>
    /// <param name="radius">半径</param>
    /// <param name="angle">角度</param>
    /// <param name="initialAngle">初始角度</param>
    /// <returns></returns>
    public static Vector2 GetRoundPosByAngle(float radius, float angle, float initialAngle = 0)
    {
        float x = radius * Mathf.Cos((angle + initialAngle) / 180 * Mathf.PI);
        float y = radius * Mathf.Sin((angle + initialAngle) / 180 * Mathf.PI);
        Vector2 pos = new Vector2(x, y);
        return pos;
    }

    /// <summary>
    /// 修改层级
    /// </summary>
    /// <param name="tf">修改层级父物体</param>
    /// <param name="newLayer">新层号</param>
    /// <param name="oldLayer">旧层号，-1为所有</param>
    public static void ChangeLayer(Transform tf, int newLayer, int oldLayer = -1)
    {
        if (oldLayer == -1 || tf.gameObject.layer == oldLayer)
        {
            tf.gameObject.layer = newLayer;
        }

        int childCount = tf.childCount;
        for (int i = 0; i < childCount; i++)
        {
            ChangeLayer(tf.GetChild(i), newLayer, oldLayer);
        }
    }

    public static void ChangUiLayer(GameObject gameObj, int layer)
    {
        Renderer[] particlelist = gameObj.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < particlelist.Length; i++)
        {
            particlelist[i].sortingOrder = layer;
        }

        Canvas[] renderlist = gameObj.GetComponentsInChildren<Canvas>();
        for (int i = 0; i < renderlist.Length; i++)
        {
            renderlist[i].sortingOrder = layer;
        }
    }
    //屏幕坐标转UI坐标 
    public static Vector3 GetUIPosByScreen(Vector3 screenPos)
    {
        Vector3 v = Vector3.zero;

        v.x = screenPos.x * 1280f / Screen.width;
        v.y = screenPos.y * 720f / Screen.height;

        return v;
    }

    public static Vector3 GetGroundPos(Vector3 pos, float height)
    {
        RaycastHit hit;
        if (Physics.Raycast(pos + new Vector3(0, height, 0), Vector3.down, out hit, 2000f, LayerMask.GetMask("MapGround")))
        {
            return hit.point + new Vector3(0, 0.1f, 0);
        }
        return pos;
    }

    /// <summary>
    /// 屏幕坐标转UI绝对坐标
    /// </summary>
    /// <param name="screenpos"></param>
    /// <param name="Target"></param>
    /// <param name="UICamera"></param>
    /// <returns></returns>
    public static Vector3 ScreenPointToWorldPoint(Vector2 screenpos, Transform Target, Camera UICamera)
    {
        Vector3 Wordpos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(Target as RectTransform, screenpos, UICamera, out Wordpos))
        {
            Wordpos.z = 0;
            return Wordpos;
        }
        return Vector3.zero;
    }

    public static void GetChildTransformList(Transform pTarget, ref List<Transform> list)
    {
        foreach (Transform child in pTarget)
        {
            if (child.childCount != 0)
            {
                GetChildTransformList(child, ref list);
            }
            list.Add(child);
        }

    }

    /// <summary>
    /// 将trans设置为parent的子级，并将trans的位置重置为(0, 0, 0),缩放重置为(1, 1, 1), 旋转重置为(0, 0, 0)
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="parent"></param>
    public static void SetParentNormalize(Transform trans, Transform parent)
    {
        if (parent)
        {
            trans.SetParent(parent);
        }
        trans.localPosition = Vector3.zero;
        trans.localScale = Vector3.one;
        trans.localRotation = Quaternion.identity;
    }

    public static void SetX(RectTransform rect, float x)
    {
        Vector2 anchoredPosition = rect.anchoredPosition;
        rect.localPosition = Vector3.zero;
        anchoredPosition.x = x;
        rect.anchoredPosition = anchoredPosition;
    }

    public static void SetY(RectTransform rect, float y)
    {
        Vector2 anchoredPosition = rect.anchoredPosition;
        rect.localPosition = Vector3.zero;
        anchoredPosition.y = y;
        rect.anchoredPosition = anchoredPosition;
    }
}

