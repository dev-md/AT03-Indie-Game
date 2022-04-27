using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangetoName : MonoBehaviour
{
    private void Awake()
    {
       List<string> scenes = new List<string>();
       foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
       {
           if (scene.enabled)
            {
                scenes.Add(scene.path);
                print(scene.);
            }
       }
    }
    public void DebugMessage(string msg)
    {
        Debug.Log(msg);
        if (msg == "Game Plane")
        {
            SceneManager.LoadScene("Game Plane");
        }
    }
}
