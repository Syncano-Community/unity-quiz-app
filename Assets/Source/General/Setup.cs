using UnityEngine;
using System.Collections;

public struct Setup
{
    public static Quiz quiz;

    public static bool IsValid()
    {
        return quiz != null && quiz.IsValid();
    }
}
