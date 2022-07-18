using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page
{
    public BaseInfo baseThing;
    /*    public List<float> ingredCount = new List<float>();
        public List<IngredInfo> ingredList = new List<IngredInfo>();*/
    public IngredInfo[] ingredArray;
    public float[] ingredCount;
    public int stability;
    public int maxStability;
    public int weight;
    public int maxWeight;
    public string reciName;
    public string reciDesc;
    public bool hasStuff;
    public Color finalColor;

    public Page(BaseInfo baseThing, List<float> ingredCount, List<IngredInfo> ingredList, int stability, int maxStability, int weight, int maxWeight, Color finalColor, string reciName, string reciDesc)
    {
        this.ingredArray = new IngredInfo[24];
        this.ingredCount = new float[24];
        this.baseThing = baseThing;
        for(int i = 0; i < ingredList.Count; i++)
        {
            ingredArray[i] = ingredList[i];
            this.ingredCount[i] = ingredCount[i];
        }
        this.stability = stability;
        this.maxStability = maxStability;
        this.weight = weight;
        this.maxWeight = maxWeight;
        this.finalColor = finalColor;
        this.reciName = reciName;
        this.reciDesc = reciDesc;
        hasStuff = true;
    }

    public Page()
    {
        this.ingredArray = new IngredInfo[24];
        this.ingredCount = new float[24];
        this.baseThing = null;
        this.stability = 0;
        this.maxStability = 1;
        this.weight = 0;
        this.maxWeight = 1;
        finalColor = new Color(0,0,0,0);
        reciName = "";
        reciDesc = "";
        hasStuff = false;
    }

}
