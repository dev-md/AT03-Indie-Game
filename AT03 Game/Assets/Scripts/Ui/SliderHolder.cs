using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public float sliderValue { get; private set; } = 1.25f;
    [SerializeField] private Slider _slider;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        sliderValue = _slider.value;
    }
}
