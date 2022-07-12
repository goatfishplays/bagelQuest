using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInfo : MonoBehaviour
{
    [SerializeField] private int maxStability;
    [SerializeField] private int weight;
    [SerializeField] private Color color;
    [SerializeField] private int colorStrength;

    public int MaxStability()
    {
        return maxStability;
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
