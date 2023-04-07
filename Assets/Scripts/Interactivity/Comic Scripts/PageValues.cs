using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageValues : MonoBehaviour
{   

    [SerializeField] Slider slider;



    public float distanceBetweenPages = 50;
    public float halfPage;
    public float startingSliderValue;
    public float currentSliderMax;


    void Start()
    {
        halfPage = distanceBetweenPages / 2;
        currentSliderMax = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        currentSliderMax = slider.maxValue;
    }
}
