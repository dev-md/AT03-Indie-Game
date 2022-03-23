using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    public delegate bool FinishAllButton(bool ifFinish);

    private BoxCollider thisCol;
    private GameObject canvas;
    private GameObject gameTextEnd;

    private void Awake()
    {
        ButtonEventManger.confrimIncreaseTotal += activeTrigger;
        thisCol = GetComponent<BoxCollider>();
        thisCol.enabled = false;

        canvas = GameObject.Find("Canvas");
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.tag == "Player") && (thisCol.enabled == true))
        {
            thisCol.enabled = false;
            Debug.Log("END GAME");

            //Text text = canvas.AddComponent<Text>();
            //text.text = "ESACPED";
            activeEndGUI();
        }
    }
    public int activeTrigger(int num)
    {
        //Debug.Log(num);
        if(num < -1)
        {
            thisCol.enabled = true;
            return -1;
        }
        return num;
    }

    public void activeEndGUI()
    {
        gameTextEnd = new GameObject("TextEndStuff");

        Text text = gameTextEnd.AddComponent<Text>();
        text.text = "ESACPED";
        text.alignment = TextAnchor.MiddleCenter;

    }
}
