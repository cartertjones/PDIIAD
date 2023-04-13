using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideCam : MonoBehaviour
{
    public PageTracker pageTracker;
    public PageValues pageValues;

    public bool sliderUnlocked;

    Camera m_Camera;
    [SerializeField] Slider slider;

    private float distanceBetweenPages;

    private float halfPage;
    private Vector3 startingCameraPosition;
    private Vector3 currentCameraPosition;
    private float currentSliderValue;
    private float startingSliderValue;
    private bool isActivated = false;
    private int pageVal;

    public int PageVal{
        get
        {
            return pageVal;
        }
        set
        {
            pageVal = value;
        }
    }
    private bool movingForward;

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
        sliderUnlocked = true;
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

        if (!isActivated && slider.value < startingSliderValue)//lock the slider from going backwards unless activated
        {
            slider.value = startingSliderValue;
        }
        if (slider.value > (distanceBetweenPages * 10) - 2) //if slider is on page 11, activate the slider
        {
            ActivateSlider();
        }
    
        float currentValue = slider.value;
       
        if (currentValue != previousValue) //Checks the previous slider value against current value, depending on if moving backwards of forwards, lock slider in opposite direcion
        {
            if (pageTracker.movingForward && currentValue < previousValue)
            {
                slider.value = previousValue;
            }
            else if (pageTracker.movingBackward && currentValue > previousValue)
            {
                slider.value = previousValue;
            }
            if (!sliderUnlocked && previousValue < currentValue) // if locked - keep slider same value
            {
                slider.value = previousValue;
            }
            if (!sliderUnlocked && previousValue > currentValue)
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
        movingForward = true;
        CheckPage(slider.value);
    }
    public void BackPage()
    {
        movingForward = false;
        CheckPage(slider.value);
    }
    public void CheckPage(float sliderVal) //check what page the slider is on, give it a variable, move the slider to the middle of that page
    {
        pageVal = (int)(slider.value / (distanceBetweenPages));
        ChangePage(movingForward);

    }
    private void ChangePage(bool movingForward) //Set startValue to the current slider value, and set endValue to current slider value + 30, Call the start moving Method  -- changing the page forward
    {
        if (canClick)
        {
            canClick = false;
            startValue = slider.value;

            if(movingForward)
            {
                endValue = (pageVal + 1) * distanceBetweenPages;
            }
            else
            {
                endValue = (pageVal - 1) * distanceBetweenPages;
            }

            StartMoving();
            Debug.Log("Page Old " + pageVal + " New Page " + (pageVal + 1));
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

