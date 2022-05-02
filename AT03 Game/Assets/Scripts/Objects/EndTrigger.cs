using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    public delegate bool FinishAllButton(bool ifFinish);
    private BoxCollider thisCol;
    [SerializeField] private GameObject winScreen;


    private void Awake()
    {
        ButtonEventManger.confrimIncreaseTotal += activeTrigger;
        thisCol = GetComponent<BoxCollider>();
        thisCol.enabled = false;
        winScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.tag == "Player") && (thisCol.enabled == true))
        {
            thisCol.enabled = false;
            Debug.Log("END GAME");
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
        winScreen.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
