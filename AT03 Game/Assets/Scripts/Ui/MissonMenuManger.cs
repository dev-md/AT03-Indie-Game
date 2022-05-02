using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissonMenuManger : MonoBehaviour
{
    private List<GameObject> listChildren = new List<GameObject>();
    public int numChild { get; private set; }

    private int indexChild = 0;

    private void Awake()
    {
        numChild = this.transform.childCount;
        for (int i = 0; i < numChild; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
            listChildren.Add(this.transform.GetChild(i).gameObject);
        }

        FocusSwitch(listChildren[indexChild]);
    }

    private void SwitchBoard()
    {
        indexChild += 1;
        FocusSwitch(listChildren[indexChild]);
    }
    private void FocusSwitch(GameObject objName)
    {
        foreach (GameObject child in listChildren)
        {
            child.SetActive(false);
        }
        objName.SetActive(true);

    }
}
