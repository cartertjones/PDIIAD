using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSlideManager : MonoBehaviour
{
    public PageTracker pageTracker;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject nextPage;
    [SerializeField] private GameObject prevPage;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pageTracker.movingBackward)
        {
            prevPage.gameObject.SetActive(true);
            nextPage.gameObject.SetActive(false);
        }
        if (pageTracker.movingForward)
        {
            prevPage.gameObject.SetActive(false);
            nextPage.gameObject.SetActive(true);
        }
        if (slider.value == slider.minValue)
        {
            prevPage.gameObject.SetActive(false);
        }
        if (slider.value == slider.maxValue)
        {
            nextPage.gameObject.SetActive(false);
        }

    }

}
