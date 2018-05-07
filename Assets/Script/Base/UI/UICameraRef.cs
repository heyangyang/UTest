using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UICameraRef : MonoBehaviour
{
    static int Ref = 0;

    void Awake()
    {
        Ref++;
        if (Ref > 1)
        {
            Debug.LogError("创建多个UI摄像机");
            GameObject.Destroy(this.gameObject);
        }
    }
}

