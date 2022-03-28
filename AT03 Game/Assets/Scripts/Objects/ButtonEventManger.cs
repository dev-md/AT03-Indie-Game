using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventManger : MonoBehaviour, IInteractable
{
    //private static ButtonEventManger Instance;

    public delegate int increaseTotal(int addMore);
    public static increaseTotal confrimIncreaseTotal;

    //confrimIncreaseTotal.Invoke(1);
    private bool disableSCR;
    private int totalMax;
    private Outline outlineObject;

    private void Awake()
    {
        confrimIncreaseTotal += AddTotal;
        outlineObject = GetComponent<Outline>();
        outlineObject.enabled = false;
    }
    private void Start()
    {
        disableSCR = false;
    }

    private void OnMouseOver()
    {
        //Debug.Log("LOOK AT ME");
        if (outlineObject.enabled == false)
        {
            outlineObject.enabled = true;
        }
    }
    private void OnMouseExit()
    {
        if (outlineObject.enabled == true)
        {
            outlineObject.enabled = false;
        }
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
            confrimIncreaseTotal.Invoke(-totalMax);
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
