using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HFuncs : MonoBehaviour
{
    // Hellper functions

    public static string StrArray(float[] fArray)
    {
        if(fArray.Length == 0 || fArray == null)
        {
            return "no vals";
        }
        string comp = "{" + fArray[0];
        for (int i = 1; i < fArray.Length; i++)
        {
            comp += ", " + fArray[i];
        }
        return comp + "}";
    }
    public static string StrArray(int[] fArray)
    {
        if (fArray.Length == 0 || fArray == null)
        {
            return "no vals";
        }
        string comp = "{" + fArray[0];
        for (int i = 1; i < fArray.Length; i++)
        {
            comp += ", " + fArray[i];
        }
        return comp + "}";
    }
}
