using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class brewManager : MonoBehaviour
{
    [SerializeField] private Image brewFill;
    private void Start()
    {
    }

    public void setFlaskType(int i)
    {
        Player.flaskType = i;
    }
}
