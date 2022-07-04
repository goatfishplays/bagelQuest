using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static string curArea;

    public static void ChangeArea(string areaName)
    {
        SceneManager.LoadScene(areaName);
    }

    public static void OpenMenu(string menuName)
    {
        SceneManager.LoadScene(menuName, LoadSceneMode.Additive);
    }

    public static void CloseScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
