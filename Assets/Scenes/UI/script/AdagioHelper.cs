﻿using UnityEngine;
using System.Collections;

public static class AdagioHelper
{
    // WaitForSeconds that works under timeScale 0
    // http://blog.projectmw.net/wait-for-real-seconds-class-using-static-function-in-unity

    public static IEnumerator WaitForRealSeconds(float seconds)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + seconds)
        {
            yield return null;
        }
    }
}
