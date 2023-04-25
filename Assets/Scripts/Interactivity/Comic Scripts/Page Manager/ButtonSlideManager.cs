using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ButtonSlideManager : MonoBehaviour
{
    public ProgressTracker progressTracker;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject nextPage;
    [SerializeField] private GameObject prevPage;

    private bool activated = false;
    void Start()
    {
        slider.gameObject.SetActive(false);
        nextPage.gameObject.SetActive(false);
        prevPage.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!progressTracker.movingForward)
        {
            prevPage.gameObject.SetActive(true);
            nextPage.gameObject.SetActive(false);
        }
        if (progressTracker.movingForward && activated)
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
    public void ActivateElements()
    {
        slider.gameObject.SetActive(true);
        nextPage.gameObject.SetActive(true);
        activated = true;
    }

}
