﻿using System;

public class Loger
{
    public static void Info(object str)
    {
        UnityEngine.Debug.Log(str);
    }

    public static void Log(object str)
    {
        UnityEngine.Debug.Log(str);
    }

    public static void Warning(object str)
    {
        UnityEngine.Debug.LogWarning(str);
    }

    public static void Error(object str)
    {
        UnityEngine.Debug.LogError(str);
    }
}

