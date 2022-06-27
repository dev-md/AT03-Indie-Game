using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfButtonTrigger : MonoBehaviour, IInteractable
{
    //private GameObject canvas;
    //private GameObject imgObject;
    private bool disableSCR;
    private Outline outlineObject;
    [SerializeField] private GameObject _player;

    private void Awake()
    {
        outlineObject = GetComponent<Outline>();
        outlineObject.enabled = false;
    }
    public void OnInteract()
    {
        ButtonEventManger.confrimIncreaseTotal.Invoke(1);
        disableSCR = true;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, _player.transform.position) < 3f)
        {
            outlineObject.enabled = true;
        }
        else
        {
            outlineObject.enabled = false;
        }

        if (disableSCR)
        {
            this.gameObject.SetActive(false);
        }
    }
}