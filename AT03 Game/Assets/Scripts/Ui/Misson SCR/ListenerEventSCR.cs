using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerEventSCR : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        ButtonEventManger.confrimIncreaseTotal += AddTotal;
    }
    private int AddTotal(int num)
    {
        if (num < -1)
        {
            this.transform.parent.SendMessage("SwitchBoard");
        }
        return num;
    }
}
