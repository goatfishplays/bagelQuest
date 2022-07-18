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
    private List<float> ingredCount = new List<float>();
    private List<IngredInfo> ingredList = new List<IngredInfo>();
    private int stability = 0;
    private int weight = 0;
    private int totalColorStrength = 0;
    private int curFlask = 0;
    

    public void OpenRecipe()
    {
        transform.Find("RecipeBookParent").gameObject.SetActive(true);
    }

    public bool HasStuff()
    {
        return baseThing != null;
    }

    public Page ToPage(string name, string desc)
    {
        return new Page(baseThing, ingredCount, ingredList, stability, (int)sSlid.maxValue, weight, (int)wSlid.maxValue, brewFill.color, name, desc);
    }

    public void LoadPage(Page pg)
    {
        totalColorStrength = baseThing.ColorStrength();
        this.ingredList = new List<IngredInfo>();
        this.ingredCount = new List<float>();
        for(int i = 0; i < pg.ingredArray.Length; i++)
        {
            if(pg.ingredCount[i] == 0)
            {
                break;
            }
            this.ingredList.Add(pg.ingredArray[i]);
            this.ingredCount.Add(pg.ingredCount[i]);
            totalColorStrength += ingredList[i].ColorStrength() * (int)ingredCount[i];
        }
        this.baseThing = pg.baseThing;
        this.stability = pg.stability;
        sSlid.maxValue = pg.maxStability;
        this.weight = pg.weight;
        wSlid.maxValue = pg.maxWeight;
        UpdateColor();
        UpdateBars();
    }

    public void SaveAtk()
    {
        Player.flaskColor = brewFill.color;
        Player.flaskType = curFlask;
        FlaskManager.atkComp = CompileEffect();
    }

    private float[] CompileEffect()
    {
        // key for the thing [01 Combo, 02 Combo, 12 Combo, 234 Combo, 0, 1, 2, 3, 4, 5, 6, 7, 8], make sure update num of thing in atkComp
        float[] atkComp = new float[13];
        float[] ingredCountTemp = ingredCount.ToArray();

        List<int> ingredIndList = new List<int>();
        foreach (IngredInfo i in ingredList)
        {
            ingredIndList.Add(i.Index());
        }

        if(ContainCheck(ingredIndList, new int[] {0,1}) != null)
        {
            atkComp[0] = Deplete(ingredCountTemp, ContainCheck(ingredIndList, new int[] { 0, 1 }), new float[]{1, 1}); // 01 combo with 1 to 1 ratio
        }
        if(ContainCheck(ingredIndList, new int[] {0,2}) != null)
        {
            atkComp[1] = Deplete(ingredCountTemp, ContainCheck(ingredIndList, new int[] { 0, 2 }), new float[]{1, 2}); // 02 combo with 1 to 2 ratio
        }
        if (ContainCheck(ingredIndList, new int[] { 1, 2 }) != null)
        {
            atkComp[2] = Deplete(ingredCountTemp, ContainCheck(ingredIndList, new int[] { 1, 2 }), new float[]{3, 2}); // 12 combo with 3 to 2 ratio
        }
        if (ContainCheck(ingredIndList, new int[] { 2, 3, 4 }) != null)
        {
            atkComp[3] = Deplete(ingredCountTemp, ContainCheck(ingredIndList, new int[] { 2, 3, 4 }), new float[] {1, 2, 5}); // 234 combo with 1:2:5 ratio
        }
        if (ingredIndList.Contains(0))
        {
            atkComp[4] = ingredCountTemp[ingredIndList.IndexOf(0)];
        }
        if (ingredIndList.Contains(1))
        {
            atkComp[5] = ingredCountTemp[ingredIndList.IndexOf(1)];
        }
        if (ingredIndList.Contains(2))
        {
            atkComp[6] = ingredCountTemp[ingredIndList.IndexOf(2)];
        }
        if (ingredIndList.Contains(3))
        {
            atkComp[7] = ingredCountTemp[ingredIndList.IndexOf(3)];
        }
        if (ingredIndList.Contains(4))
        {
            atkComp[8] = ingredCountTemp[ingredIndList.IndexOf(4)];
        }
        if (ingredIndList.Contains(5))
        {
            atkComp[9] = ingredCountTemp[ingredIndList.IndexOf(5)];
        }
        return atkComp;
    }
/*    // yes I know I could just send in lists and for loop it but thats so much effort
    private float Deplete(float[] ingCount, int ind1, int ind2, float ratio1, float ratio2)
    {
        float retVal = ingCount[ind1] + ingCount[ind2];
        float part1 = ingCount[ind1] * ratio1;
        float part2 = ingCount[ind2] * ratio2;
        float min = Mathf.Min(part1, part2);
        part1 -= min;
        part2 -= min;
        ingCount[ind1] = part1/ratio1;
        ingCount[ind2] = part2/ratio2;
        retVal -= ingCount[ind1] + ingCount[ind2];
        return retVal;
    }*/

    private float Deplete(float[] ingCount, int[] inds, float[] ratios)
    {
        float retVal = 0;
        float[] parts = new float[inds.Length];
        for(int i = 0; i < inds.Length; i++)
        {
            retVal += ingCount[inds[i]];
            parts[i] = ingCount[inds[i]] * ratios[i];
        }
        float min = Mathf.Min(parts);
        for (int i = 0; i < inds.Length; i++)
        {
            parts[i] -= min;
            ingCount[inds[i]] = parts[i] / ratios[i];
            retVal -= ingCount[inds[i]];
        }
        return retVal;
    }

    private int[] ContainCheck(List<int> ingredInds, int[] indexes)
    {
        List<int> inds = new List<int>();
        foreach(int i in indexes)
        {
            if(!ingredInds.Contains(i))
            {
                return null;
            }
            inds.Add(ingredInds.IndexOf(i));
        }
        inds.Sort();
        return inds.ToArray();
    }

    public void SetFlaskType(int i)
    {
        transform.Find("flaskParent").GetChild(curFlask).GetComponent<Image>().color = Color.white;
        transform.Find("flaskParent").GetChild(i).GetComponent<Image>().color = new Color32(200, 200, 200, 255);
        curFlask = i;
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
        ingredCount = new List<float>();
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
        brewFill.color = colorAcc;
    }
}
