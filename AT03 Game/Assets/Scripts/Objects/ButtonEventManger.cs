using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventManger : MonoBehaviour
{
    private static ButtonEventManger Instance;

    public delegate int increaseTotal(int addMore);
    public static increaseTotal confrimIncreaseTotal;
    //confrimIncreaseTotal.Invoke(1);

    private int totalMax;
    private void Awake()
    {
        confrimIncreaseTotal += AddTotal;
    }
    private void Update()
    {
        
    }

    private int AddTotal(int num)
    {
        totalMax += num;
        return totalMax;
    }
}
