using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfButtonTrigger : MonoBehaviour, IInteractable
{
    //private GameObject canvas;
    //private GameObject imgObject;
    private bool disableSCR;

    public Sprite spriteImange;

    void Start()
    {
        //canvas = GameObject.Find("Canvas");
        //imgObject = new GameObject("Hover");
        //imgObject.SetActive(false);

        //RectTransform trans = imgObject.AddComponent<RectTransform>();
        //trans.transform.SetParent(canvas.transform); // setting parent
        //trans.localScale = Vector3.one;
        //trans.anchoredPosition = new Vector2(0f, 0f); // setting position, will be on center
        //trans.sizeDelta = new Vector2(100, 100); // custom size

        //Image image = imgObject.AddComponent<Image>();
        //image.sprite = spriteImange;
        //imgObject.transform.SetParent(canvas.transform);
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