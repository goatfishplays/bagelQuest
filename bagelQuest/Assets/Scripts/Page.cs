using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    private BaseInfo baseThing;
    /*    private List<float> ingredCount = new List<float>();
        private List<IngredInfo> ingredList = new List<IngredInfo>();*/
    private IngredInfo[] ingredArray;
    private float[] ingredCount;
    private int stability;
    private int maxStability;
    private int weight;
    private int maxWeight;
    private string reciName;
    private string reciDesc;

    public Page(BaseInfo baseThing, List<float> ingredCount, List<IngredInfo> ingredList, int stability, int maxStability, int weight, int maxWeight, string reciName, string reciDesc)
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
        this.reciName = reciName;
        this.reciDesc = reciDesc;
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
        reciName = "";
        reciDesc = "";
    }

}
