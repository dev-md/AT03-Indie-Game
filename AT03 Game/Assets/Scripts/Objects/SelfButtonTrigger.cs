using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfButtonTrigger : MonoBehaviour, IInteractable
{
    //private GameObject canvas;
    //private GameObject imgObject;
    private bool disableSCR;
    public Outline outlineObject;
    private void Awake()
    {
        outlineObject = GetComponent<Outline>();
        outlineObject.enabled = false;
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
    public void OnInteract()
    {
        ButtonEventManger.confrimIncreaseTotal.Invoke(1);
        disableSCR = true;
    }

    private void Update()
    {
        if (disableSCR)
        {
            this.gameObject.SetActive(false);
        }
    }
}