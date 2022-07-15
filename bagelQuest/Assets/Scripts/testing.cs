using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneChanger.OpenMenu("PotionMaking");
        float[] fs = new float[] { 1, 2, 3 };
        print(HFuncs.StrArray(fs));
        testFunc(fs);
        print(HFuncs.StrArray(fs));
    }

    private void testFunc(float[] fs)
    {
        fs[0] = 22;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
