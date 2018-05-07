using System;
using UnityEngine;
public class UIClazzAdder : UIClazzBase
{
    public override WindowBase AddClazz(GameObject go, string str)
    {
        Type type = Type.GetType(str);
        WindowBase wdbase = go.GetComponent(type) as WindowBase;
        if (wdbase == null)
        {
            wdbase = go.AddComponent(type) as WindowBase;
        }
        return wdbase;
    }
}