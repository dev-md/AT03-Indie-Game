using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandling : MonoBehaviour
{
    private Slider thisSlider;
    private void Awake()
    {
        if(TryGetComponent(out Slider _slider))
        {
            thisSlider = _slider;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") < 0)
        {
            thisSlider.value -= 0.05f;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            thisSlider.value += 0.05f;
        }
    }
}
