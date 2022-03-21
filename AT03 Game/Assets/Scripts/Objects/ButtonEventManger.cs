using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventManger : MonoBehaviour, IInteractable
{
    private static ButtonEventManger Instance;

    public delegate int increaseTotal(int addMore);
    public static increaseTotal confrimIncreaseTotal;
    //confrimIncreaseTotal.Invoke(1);
    private bool disableSCR;
    private int totalMax;

    private void Awake()
    {
        confrimIncreaseTotal += AddTotal;
    }
    private void Start()
    {
        disableSCR = false;
    }

    private int AddTotal(int num)
    {
        totalMax += num;
        return totalMax;
    }

    public void OnInteract()
    {
        if (totalMax > 2)
        {
            disableSCR = true;
        }
        else
        {
            Debug.Log("Boo");
        }
    }

    private void Update()
    {
        if (disableSCR)
        {
            this.gameObject.SetActive(false);
        }
    }
}
