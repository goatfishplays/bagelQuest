using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeBook : MonoBehaviour
{
    private Page[] pages = new Page[16];
    private Transform leftPage;
    private Transform rightPage;
    private Sprite[] icons = new Sprite[30];
    public Sprite s0;
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite s5;
    private Color transparent = new Color(0, 0, 0, 0);
    private int page = 0;
    

    private void Start()
    {
        leftPage = transform.Find("PageLeft");
        rightPage = transform.Find("PageRight");
        /*        string path = System.IO.Directory.GetCurrentDirectory().Replace("Scripts", "Sprites\\IngredIcons\\icon");
                string path = System.IO.Directory.GetCurrentDirectory() + "\\Assets\\Sprites\\IngredIcons\\icon";
                string path = "Sprites/IngredIcons/icon";
                print(path);
                icons[0] = Resources.Load(path + 0) as Sprite;
                icons[1] = Resources.Load(path + 1) as Sprite;
                icons[2] = Resources.Load(path + 2) as Sprite;
                icons[3] = Resources.Load(path + 3) as Sprite;
                icons[4] = Resources.Load(path + 4) as Sprite;
                icons[5] = Resources.Load(path + 5) as Sprite;
                print(icons[0]);*/

        icons[0] = s0;
        icons[1] = s1;
        icons[2] = s2;
        icons[3] = s3;
        icons[4] = s4;
        icons[5] = s5;

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i] = new Page();
        }

        NextPage();
        NextPage();
        PrevPage();
        PrevPage();
    }

    public void Exit()
    {
        this.transform.parent.gameObject.SetActive(false);
    }


    public void PrevPage()
    {
        if(page > 1)
        {
            page--;
            if(page % 2 == 0)
            {
                UpdatePg(leftPage, pages[page - 1]);
                UpdatePg(rightPage, pages[page]);
                UpdatePgNum(leftPage, page - 1);
                UpdatePgNum(rightPage, page);
            }
        }
    }

    public void NextPage()
    {
        if (page < pages.Length)
        {
            page++;
            if (page % 2 == 1)
            {
                UpdatePg(rightPage, pages[page + 1]);
                UpdatePg(leftPage, pages[page]);
                UpdatePgNum(rightPage, page + 1);
                UpdatePgNum(leftPage, page);
            }
        }
    }

    public void EraseRecipe()
    {
        pages[page] = new Page();
        UpdatePg(LeftOrRight(), pages[page]);
    }

    public void SaveRecipe(brewManager bM)
    {
        if(!pages[page].hasStuff && bM.HasStuff())
        {
            pages[page] = bM.ToPage(pages[page].reciName, pages[page].reciDesc);
            UpdatePg(LeftOrRight(), pages[page]);
        }
    }

    public void LoadRecipe(brewManager bM)
    {
        if(!pages[page].hasStuff)
        {
            bM.Dump();
            Exit();
            return;
        }
        bM.LoadPage(pages[page]);
        Exit();
    }

    private Transform LeftOrRight()
    {
        if(page % 2 == 0)
        {
            return rightPage;
        }
        return leftPage;
    }

    private void UpdatePgNum(Transform pg, int pgNum)
    {
        pg.Find("PageNum").GetComponent<TMP_Text>().text = pgNum.ToString();
    }

    private void UpdatePg(Transform pg, Page curPage)
    {
        Transform iconParent = pg.Find("Ingreds");
        if(curPage.baseThing != null)
        {
            SetIcon(iconParent.GetChild(0), curPage.baseThing.Index() + 24, 1);
        }
        else
        {
            ClearIcon(iconParent.GetChild(0));
        }
        for(int i = 1; i < 25; i++)
        {
            if(curPage.ingredArray[i-1] != null)
            {
                SetIcon(iconParent.GetChild(i), curPage.ingredArray[i-1].Index(), curPage.ingredCount[i-1]);
            }
            else
            {
                ClearIcon(iconParent.GetChild(i));
            }
        }
        Slider stab = pg.Find("Stability").GetComponent<Slider>();
        stab.maxValue = curPage.maxStability;
        stab.value = curPage.stability;
        Slider weight = pg.Find("Weight").GetComponent<Slider>();
        weight.maxValue = curPage.maxWeight;
        weight.value = curPage.weight;
        pg.Find("RecipeName").GetComponent<TMP_InputField>().text = curPage.reciName;
        pg.Find("Description").GetComponent<TMP_InputField>().text = curPage.reciDesc;
        pg.Find("PotPreview").GetChild(0).GetComponent<Image>().color = curPage.finalColor;
    }

    private void SetIcon(Transform icon, int ingredIndex, float ingredCount)
    {
        icon.GetComponent<Image>().color = Color.white;
        icon.GetComponent<Image>().sprite = icons[ingredIndex];
        icon.GetChild(0).GetComponent<TMP_Text>().text = "x" + ingredCount;
    }
    private void ClearIcon(Transform icon)
    {
        icon.GetComponent<Image>().color = transparent;
        icon.GetChild(0).GetComponent<TMP_Text>().text = "";
    }

    public void UpdateName(Transform pageSide)
    {
        if (pageSide == leftPage && page % 2 == 0)
        {
            pages[page - 1].reciName = pageSide.Find("RecipeName").GetComponent<TMP_InputField>().text;
        }
        else if (pageSide == rightPage && page % 2 != 0)
        {
            pages[page + 1].reciName = pageSide.Find("RecipeName").GetComponent<TMP_InputField>().text;
        }
        else
        {
            pages[page].reciName = pageSide.Find("RecipeName").GetComponent<TMP_InputField>().text;
        }
    }

    public void UpdateDesc(Transform pageSide)
    {
        if (pageSide == leftPage && page % 2 == 0)
        {
            pages[page - 1].reciDesc = pageSide.Find("Description").GetComponent<TMP_InputField>().text;
        }
        else if (pageSide == rightPage && page % 2 != 0)
        {
            pages[page + 1].reciDesc = pageSide.Find("Description").GetComponent<TMP_InputField>().text;
        }
        else
        {
            pages[page].reciDesc = pageSide.Find("Description").GetComponent<TMP_InputField>().text;
        }
    }
}
