using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideCam : MonoBehaviour
{
    public PageTracker pageTracker;
    public PageValues pageValues;

    Camera m_Camera;
    [SerializeField] Slider slider;

    private float distanceBetweenPages;

    private float halfPage;
    private Vector3 startingCameraPosition;
    private Vector3 currentCameraPosition;
    private float currentSliderValue;
    private float startingSliderValue;
    private bool isActivated = false;
    private float pageVal;
    private float forwardOrBack; //float to check if page is going forward or back -- 0 is back , 1 is forward

    // Values to gradually move slider and cam from one position to another

    public float moveDuration = 2.0f;

    private bool canClick = true;
    private float startValue;
    private float endValue;
    private float elapsedTime = 0.0f;
    private bool isMoving = false;

    private float previousValue;

    void Awake()
    {
        //Camera Stuff
        m_Camera = Camera.main;
        startingCameraPosition = m_Camera.transform.position;
        currentCameraPosition = m_Camera.transform.position;

        //Set distance between pages as what is set in PageValues Script
        distanceBetweenPages = pageValues.distanceBetweenPages;

        //Starting Slider value, 8th page
        startingSliderValue = distanceBetweenPages * 7;

        //Slider Values
        slider.value = startingSliderValue;
        previousValue = startingSliderValue;

        //Half a Page Value
        halfPage = distanceBetweenPages / 2;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isActivated && slider.value < startingSliderValue)
        {
            slider.value = startingSliderValue;
        }
        if (slider.value > (distanceBetweenPages * 10) - 2)
        {
            ActivateSlider();
        }
        float currentValue = slider.value;

        if (currentValue != previousValue)
        {
            if (pageTracker.movingForward && currentValue < previousValue)
            {
                slider.value = previousValue;
            }
            else if (pageTracker.movingBackward && currentValue > previousValue)
            {
                slider.value = previousValue;
            }

            previousValue = slider.value;
        }
        // Code to move slider and camera from one position to another
        //checks if the slider is currently moving, and if it is, it calculates the current time elapsed and lerps the slider's value from its starting value
        //to the end value using Mathf.Lerp(). The Mathf.Clamp01() function is used to ensure that t (the interpolation factor) remains between 0 and 1.
        //If t reaches 1, the movement is complete and isMoving is set to false.
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            float value = Mathf.Lerp(startValue, endValue, t);
            slider.value = value;

            if (t >= 1.0f)
            {
                isMoving = false;
                canClick = true;
            }
        }
    }
    void updateCameraPos(float value) // Give currentCameraPosition the x value of the slider, then move the camera to this new position
    {
        currentCameraPosition.x = value;
        m_Camera.transform.position = currentCameraPosition;
    }
    public void onSliderChanged(float sliderValue)
    {
        updateCameraPos(sliderValue);
    }
    public void ActivateSlider()
    {
        isActivated = true;
    }
    public void NextPage()
    {
        forwardOrBack = 1;
        CheckPage(slider.value);
    }
    public void BackPage()
    {
        forwardOrBack = 0;
        CheckPage(slider.value);
    }
    public void CheckPage(float sliderVal) //check what page the slider is on, give it a variable, move the slider to the middle of that page
    {
        if (sliderVal >= 0 && sliderVal < halfPage)
        {
            pageVal = 0;
            slider.value = 0;
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
            
        }
        if (sliderVal > halfPage && sliderVal < (distanceBetweenPages + halfPage))
        {
            pageVal = 1;
            slider.value = distanceBetweenPages;
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }

        }
        if (sliderVal > (distanceBetweenPages + halfPage) && sliderVal < ((distanceBetweenPages *2 ) + halfPage))
        {
            pageVal = 2;
            slider.value = (distanceBetweenPages * 2);
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > ((distanceBetweenPages * 2) + halfPage) && sliderVal < ((distanceBetweenPages * 3) + halfPage))
        {
            pageVal = 3;
            slider.value = (distanceBetweenPages * 3);
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > ((distanceBetweenPages * 3) + halfPage) && sliderVal < ((distanceBetweenPages * 4) + halfPage))
        {
            pageVal = 4;
            slider.value = (distanceBetweenPages * 4);
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > ((distanceBetweenPages * 4) + halfPage) && sliderVal < ((distanceBetweenPages * 5) + halfPage))
        {
            pageVal = 5;
            slider.value = (distanceBetweenPages * 5);
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > ((distanceBetweenPages * 5) + halfPage) && sliderVal < ((distanceBetweenPages * 6) + halfPage))
        {
            pageVal = 6;
            slider.value = (distanceBetweenPages * 6);
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > ((distanceBetweenPages * 6) + halfPage) && sliderVal < ((distanceBetweenPages * 7) + halfPage))
        {
            pageVal = 7;
            slider.value = (distanceBetweenPages * 7);
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > ((distanceBetweenPages * 7) + halfPage) && sliderVal < ((distanceBetweenPages * 8) + halfPage))
        {
            pageVal = 8;
            slider.value = (distanceBetweenPages * 8);
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > ((distanceBetweenPages * 8) + halfPage) && sliderVal < ((distanceBetweenPages * 9) + halfPage))
        {
            pageVal = 9;
            slider.value = (distanceBetweenPages * 9);
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0 && isActivated)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > ((distanceBetweenPages * 9) + halfPage) && sliderVal < ((distanceBetweenPages * 10) + halfPage))
        {
            pageVal = 10;
            slider.value = (distanceBetweenPages * 10);
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > ((distanceBetweenPages * 10) + halfPage) && sliderVal < ((distanceBetweenPages * 11) + halfPage))
        {
            pageVal = 11;
            slider.value = (distanceBetweenPages * 11);
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > ((distanceBetweenPages * 11) + halfPage) && sliderVal < ((distanceBetweenPages * 12) + halfPage))
        {
            pageVal = 12;
            slider.value = (distanceBetweenPages * 12);
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }

    }
    private void ChangePageForward() //Set startValue to the current slider value, and set endValue to current slider value + 30, Call the start moving Method  -- changing the page forward
    {
        if (canClick)
        {
            canClick = false;
            startValue = slider.value;
            endValue = slider.value + distanceBetweenPages;
            StartMoving();
            Debug.Log("Page Old " + pageVal + " New Page " + (pageVal + 1));
        }
    }

    private void ChangePageBackward() ////Set startValue to the current slider value, and set endValue to current slider value - 30, Call the start moving Method -- changing the page backward
    {
        if (canClick)
        {
            canClick = false;
            startValue = slider.value;
            endValue = slider.value - distanceBetweenPages;
            StartMoving();
            Debug.Log("Page Old " + pageVal + " New Page " + (pageVal - 1));
        }

    }

    // Code to start the slider movement
    // sets the starting value of the slider to its current value, resets the elapsed time to 0, and sets isMoving to true to start the movement.
    public void StartMoving()
    {
        startValue = slider.value;
        elapsedTime = 0.0f;
        isMoving = true;
    }
    public void AddPage11()
    {
        slider.maxValue = (distanceBetweenPages * 11);
    }
    public void AddPage12()
    {
        slider.maxValue = (distanceBetweenPages * 12);
    }
    public void Hide11()
    {
        slider.maxValue = (distanceBetweenPages * 10);
    }

}

