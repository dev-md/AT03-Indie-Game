using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSliderValue : MonoBehaviour
{
    private Slider slider;
    private Text text;
    private void Awake()
    {
        if(this.transform.parent.TryGetComponent(out Slider _slider))
        {
            slider = _slider;
            if(transform.TryGetComponent(out Text _text))
            {
                text = _text;
            }
        }
    }
    private void Update()
    {
        text.text = slider.value.ToString();
    }
}
