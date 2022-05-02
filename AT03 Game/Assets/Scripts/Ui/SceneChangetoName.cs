using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangetoName : MonoBehaviour
{
    private List<string> scenes = new List<string>();
    private void Awake()
    {
       foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
       {
           if (scene.enabled)
            {
                scenes.Add(scene.path);
                //print(scene.path);
            }
       }
       for(int i = 0; i < scenes.Count; i++)
        {
            if((scenes[i].IndexOf("SCE_")) > 0)
            {
                scenes[i] = scenes[i].Substring(scenes[i].IndexOf("SCE_"));
                scenes[i] = scenes[i].Replace(".unity", "");
            }
            else if ((scenes[i].IndexOf("SCE_")) < 0)
            {
                scenes[i] = null;
            }
        }
    }
    public void DebugMessage(string msg)
    {
        //Debug.Log(msg);
        foreach(string scene in scenes)
        {
            //print(scene);
            if (msg == scene)
            {
                SceneManager.LoadScene(scene);
            }
        }
        
    }
}