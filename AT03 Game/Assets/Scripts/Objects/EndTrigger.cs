using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    public delegate bool FinishAllButton(bool ifFinish);
    private BoxCollider thisCol;
    private bool toggleCol;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject enemyGM;
    [SerializeField] private GameObject playerGM;
    [SerializeField] private GameObject objectmisson;


    private void Awake()
    {
        ButtonEventManger.confrimIncreaseTotal += activeTrigger;
        thisCol = GetComponent<BoxCollider>();
        thisCol.enabled = false;
        winScreen.SetActive(false);
    }

    private void LateUpdate()
    {
        if(toggleCol == true)
        {
            thisCol.enabled = true;
        }
        if (toggleCol == false)
        {
            thisCol.enabled = false;
        }
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
            toggleCol = true;
            return -1;
        }
        return num;
    }

    public void activeEndGUI()
    {
        winScreen.SetActive(true);
        enemyGM.SetActive(false);
        playerGM.SetActive(false);
        objectmisson.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
