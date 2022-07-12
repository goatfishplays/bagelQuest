using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class brewManager : MonoBehaviour
{
    [SerializeField] private Image brewFill;
    [SerializeField] private Slider sSlid;
    [SerializeField] private Slider wSlid;
    private BaseInfo baseThing;
    private List<int> ingredCount = new List<int>();
    private List<IngredInfo> ingredList = new List<IngredInfo>();
    private int stability = 0;
    private int weight = 0;
    private int totalColorStrength = 0;
    
    private void Start()
    {
        
    }

    public void SetFlaskType(int i)
    {
        Player.flaskType = i;
    }

    public void SetBase(BaseInfo bI)
    {
        if(baseThing == null)
        {
            baseThing = bI;
            sSlid.maxValue = baseThing.MaxStability();
            weight = baseThing.Weight();
            totalColorStrength = baseThing.ColorStrength();
            UpdateBars();
            UpdateColor();
        }
    }

    public void AddIngred(IngredInfo i)
    {
        if(baseThing != null)
        {
            if(!ingredList.Contains(i))
            {
                ingredList.Add(i);
                ingredCount.Add(1);
            }
            else
            {
                ingredCount[ingredList.IndexOf(i)]++;
            }
            stability += i.Stability();
            weight += i.Weight();
            totalColorStrength += i.ColorStrength();
            UpdateBars();
            UpdateColor();
        }
    }

    public void Dump()
    {
        stability = 0;
        weight = 0;
        ingredList.Clear();
        ingredCount = new List<int>();
        baseThing = null;
        brewFill.color = new Color(0, 0, 0, 0);
        UpdateBars();
        totalColorStrength = 0;
    }

    private void UpdateBars()
    {
        sSlid.value = stability;
        wSlid.value = weight;
    }

    private void UpdateColor()
    {
        Vector4 colorAcc = baseThing.ColorStrength() * baseThing.Color();
        for(int i = 0; i < ingredCount.Count; i++)
        {
            colorAcc += ingredCount[i] * ingredList[i].ColorStrength() * ingredList[i].Color();
        }
        colorAcc /= totalColorStrength;
        print(colorAcc);
        brewFill.color = colorAcc;
    }
}
