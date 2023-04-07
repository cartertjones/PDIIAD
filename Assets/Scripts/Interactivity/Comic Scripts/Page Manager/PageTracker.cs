using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageTracker : MonoBehaviour
{

    public PageManager pageManager;
    public PageValues pageValues;
    public SlideCam slideCam;
    public TransparentSlider transparentSlider;

    public float timesThroughForward = 0;
    public float timesThroughBackward = 0;
    public bool movingForward;
    public bool movingBackward;
    public bool divorceFinished;

    [SerializeField] private Slider slider;
    private bool onPage1;
    private bool onLastPage;



    void Start()
    {
        movingForward = true;
        divorceFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSliderVal();
    }
    public void addTimeForward()
    {
        timesThroughForward++;
    }
    public void addTimeBackward()
    {
        if (!onPage1)
        {
            timesThroughBackward++;
        }

    }
    private void CheckSliderVal()
    {
        if (slider.value < 5)
        {
            movingForward = true;
            movingBackward = false;
            addTimeBackward();
            pageManager.RevertPages();
            onPage1 = true;
            slideCam.Hide11();
            if (timesThroughBackward == 1)
            {

            }
        }
        if (slider.value > 15)
        {
            onPage1 = false;
        }
        if (slider.value > ((pageValues.distanceBetweenPages * 10) - 5) && !onLastPage) 
        {
            Debug.Log("slider val" + slider.value);
            Debug.Log("page values max slider val" + pageValues.currentSliderMax);
            Debug.Log("slider max" + slider.maxValue);
            addTimeForward();
            onLastPage = true;
        }
        if (slider.value < ((pageValues.distanceBetweenPages * 10) - 5))
        {
            onLastPage = false;
        }
        if (slider.value == pageValues.distanceBetweenPages && !divorceFinished && timesThroughForward == 2 && timesThroughBackward == 2)
        {
            slideCam.sliderUnlocked = false;
            transparentSlider.ShowSquares();
        }
    }
}
