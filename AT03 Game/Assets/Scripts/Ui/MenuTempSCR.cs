using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTempSCR : MonoBehaviour
{
    public void DebugMessage(string msg)
    {
        Debug.Log(msg);
        if(msg == "Game Plane")
        {
            SceneManager.LoadScene("Game Plane");
        }
    }
}