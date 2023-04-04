using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideCam : MonoBehaviour
{
    Camera m_Camera;
    [SerializeField] Slider slider;
    private Vector3 startingCameraPosition;
    private Vector3 currentCameraPosition;
    private float startingSliderValue = 270;
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

    // Start is called before the first frame update
    void Awake()
    {
        m_Camera = Camera.main;
        startingCameraPosition = m_Camera.transform.position;
        currentCameraPosition = m_Camera.transform.position;
        slider.value = startingSliderValue;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isActivated && slider.value < startingSliderValue)
        {
            slider.value = startingSliderValue;
        }
        if (slider.value > 358)
        {
            ActivateSlider();
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
        if (sliderVal >= 0 && sliderVal < 15)
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
        if (sliderVal > 15 && sliderVal < 45)
        {
            pageVal = 1;
            slider.value = 30;
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }

        }
        if (sliderVal > 45 && sliderVal < 75)
        {
            pageVal = 2;
            slider.value = 60;
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > 75 && sliderVal < 105)
        {
            pageVal = 3;
            slider.value = 90;
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > 105 && sliderVal < 135)
        {
            pageVal = 4;
            slider.value = 120;
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > 135 && sliderVal < 175)
        {
            pageVal = 5;
            slider.value = 150;
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > 175 && sliderVal < 205)
        {
            pageVal = 6;
            slider.value = 180;
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > 205 && sliderVal < 235)
        {
            pageVal = 7;
            slider.value = 210;
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > 235 && sliderVal < 265)
        {
            pageVal = 8;
            slider.value = 240;
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0 && isActivated)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > 265 && sliderVal < 295)
        {
            pageVal = 9;
            slider.value = 270;
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > 295 && sliderVal < 325)
        {
            pageVal = 10;
            slider.value = 300;
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        if (sliderVal > 325 && sliderVal < 355)
        {
            pageVal = 11;
            slider.value = 330;
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }
        /*if (sliderVal > 355 && sliderVal < 385)
        {
            pageVal = 12;
            slider.value = 360;
            if (forwardOrBack == 1)
            {
                ChangePageForward();
            }
            else if (forwardOrBack == 0)
            {
                ChangePageBackward();
            }
        }*/

    }
    private void ChangePageForward() //Set startValue to the current slider value, and set endValue to current slider value + 30, Call the start moving Method  -- changing the page forward
    {
        if (canClick)
        {
            canClick = false;
            startValue = slider.value;
            endValue = slider.value + 30;
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
            endValue = slider.value - 30;
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
    public void IncreaseSlider()
    {
        slider.maxValue = 360;
    }
}

