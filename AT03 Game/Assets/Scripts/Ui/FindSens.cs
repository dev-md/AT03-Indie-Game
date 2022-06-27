using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSens : MonoBehaviour
{
    private GameObject gameSense;
    private SliderHolder slider_script;
    private void Start()
    {
        gameSense = GameObject.FindGameObjectWithTag("OptionsSen");
        if(gameSense.TryGetComponent(out SliderHolder script))
        {
            slider_script = script;
        }
        print(slider_script.sliderValue);
        this.gameObject.SendMessage("ChangeSen", slider_script.sliderValue);
        Destroy(gameSense);
        //print(gameSense);
    }
}
