using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class numBoxRemainSCR : MonoBehaviour
{
    private Text txt;
    private int totalNum = 0;
    private int curNum = 0;
    [SerializeField] private GameObject buttonManger;
    private void Awake()
    {
        txt = this.transform.GetChild(0).gameObject.GetComponent<Text>();
        txt.text = "Remaining boxes " + "0/"+totalNum;

        ButtonEventManger.confrimIncreaseTotal += AddTotal;
    }
    private void Start()
    {
        buttonManger.SendMessage("MessageBounce", this.gameObject);
    }

    private void TotalBox(int numbox)
    {
        totalNum = numbox;
    }

    private int AddTotal(int num)
    {
        curNum += num;
        return num;
    }

    private void Update()
    {
        txt.text = "Remaining Boxes " + curNum +  "/" + totalNum;

        if(curNum >= totalNum)
        {
            this.transform.parent.SendMessage("SwitchBoard");
        }
    }
}
