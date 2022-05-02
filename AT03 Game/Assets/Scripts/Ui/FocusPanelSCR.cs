using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPanelSCR : MonoBehaviour
{
    private List<GameObject> listChildren = new List<GameObject>();
    private int numChild;

    private void Awake()
    {
        numChild = this.transform.childCount;
        for (int i = 0; i < numChild; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
            //print(this.transform.GetChild(i).gameObject.name);
            listChildren.Add(this.transform.GetChild(i).gameObject);
            
        }
        
    }

    private void FocusSwitch(GameObject objName)
    {
        foreach(GameObject child in listChildren)
        {
            child.SetActive(false);
        }
        objName.SetActive(true);
        
    }

    public void DebugMessage(string msg)
    {
        //print(msg);
        foreach(GameObject child in listChildren)
        {
            //print(child.name);
            if(child.name == msg)
            {
                FocusSwitch(child);
            }
        }
    }
}
