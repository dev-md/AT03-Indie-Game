using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerEventSCR : MonoBehaviour
{
    private bool toggleBoard;
    // Start is called before the first frame update
    private void Awake()
    {
        ButtonEventManger.confrimIncreaseTotal += AddTotal;
    }

    private void LateUpdate()
    {
        if (toggleBoard)
        {
            this.transform.parent.SendMessage("SwitchBoard");
        }
    }
    private int AddTotal(int num)
    {
        if (num < -1)
        {
            toggleBoard = true;
        }
        return num;
    }
}
