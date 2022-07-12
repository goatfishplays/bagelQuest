using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredInfo : MonoBehaviour
{
    // 0: ingred, 1: agent
    [SerializeField] private int type;
    [SerializeField] private int stability;
    [SerializeField] private int weight;
    [SerializeField] private Color color;
    [SerializeField] private int colorStrength;

    public int Type()
    {
        return type;
    }

    public int Stability()
    {
        return stability;
    }

    public int Weight()
    {
        return weight;
    }

    public Vector4 Color()
    {
        return color;
    }

    public int ColorStrength()
    {
        return colorStrength;
    }

}
