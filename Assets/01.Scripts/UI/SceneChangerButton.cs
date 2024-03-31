using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerButton : MonoBehaviour
{
    public void ButtonClickEvent(string key)
    {
        switch(key)
        {
            case "Start":
                SceneManager.LoadScene("InGameScene");
                break;
            case "Quit":
                Application.Quit();
                break;
        }
    }
}
