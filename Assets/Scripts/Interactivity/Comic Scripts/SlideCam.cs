using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideCam : MonoBehaviour
{
    public ProgressTracker progressTracker;
    public PageValues pageValues;
    public CameraManager cameraManager;

    [SerializeField] private GameObject virtualCameraOne;
    

    private bool sliderUnlocked;
    public bool SliderUnlocked
    {
        get{ return sliderUnlocked; }
        set{ sliderUnlocked = value; }
    }

    public bool onLastPanelInPage = false; // bool for checking in on last panel in a page
    public bool onAPanel = false;
    public bool onSliderView = true;


    Camera m_Camera;
    [SerializeField] Slider slider;
    [SerializeField] private GameObject[] movementButtons;

    private float distanceBetweenPages;

    private float halfPage;
    private Vector3 startingCameraPosition;
    private Vector3 currentCameraPosition;
    private float currentSliderValue;
    private float startingSliderValue;
    private bool isActivated = false;
    private int pageVal;

    //used to lock arrow key in panel view
    private bool activityComplete;
    public bool ActivityComplete
    {
        get { return activityComplete; }
        set { activityComplete = value; }
    }

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
    private bool isMoving;
    public bool IsMoving
    {
        get{ return isMoving; }
    }

    private bool onInteractivePanel;
    public bool OnInteractivePanel
    {
        get{ return onInteractivePanel; }
        set { onInteractivePanel = value; }
    }

    private float previousValue;

    void Awake()
    {
        onLastPanelInPage = false;
        isMoving = false;
        sliderUnlocked = true;
        //Camera Stuff
        m_Camera = Camera.main;
        startingCameraPosition = m_Camera.transform.position;
        currentCameraPosition = m_Camera.transform.position;

        //Set distance between pages as what is set in PageValues Script
        distanceBetweenPages = pageValues.distanceBetweenPages;

        //Starting Slider value, cover
        startingSliderValue = distanceBetweenPages * 0;


        //Slider Values
        slider.value = startingSliderValue;
        previousValue = startingSliderValue;

        //Half a Page Value
        halfPage = distanceBetweenPages / 2;


        foreach(GameObject obj in movementButtons)
        {
            obj.SetActive(false);
        }

        onInteractivePanel = false;
        activityComplete = true;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdatePageVal(slider.value);

        //move to page 8 once user clicks on cover first time through
        if(progressTracker.timesThroughForward == 0 && pageVal == 0 && Input.GetMouseButtonDown(0))
        {
            startValue = slider.value;
            endValue = distanceBetweenPages * 7;
            StartMoving();
        }

        if (!isActivated && slider.value < startingSliderValue)//lock the slider from going backwards unless activated
        {
            slider.value = startingSliderValue;
        }
        if (slider.value > (distanceBetweenPages * 10) - 2) //if slider is on page 11, activate the slider
        {
            ActivateSlider();
        }
        if (onAPanel || onLastPanelInPage)
        {
            onSliderView = false;
        }
        //disable movement ui if locked and not on first time through
        if(progressTracker.timesThroughForward >= 0)
        {
            foreach(GameObject obj in movementButtons)
            {
                //initially disable, will reenable if should be active
                obj.SetActive(false);

                //if on cover for first time, skip
                if(pageVal == 0 && progressTracker.timesThroughForward == 0){continue;}

                switch(obj.name)
                {
                    case "Slider":
                        //set slider to sliderUnlocked value
                        if (onSliderView)
                        {
                            obj.SetActive(sliderUnlocked);
                        }    

                        break;
                    case "Slider Backing":
                        if (onSliderView)
                        {
                            obj.SetActive(sliderUnlocked);
                        }
                        break;
                    case "Next Page":
                        if(progressTracker.movingForward){obj.SetActive(true);}
                        else{obj.SetActive(false);}

                        //hide if on panel with incomplete interactivity
                        if (onInteractivePanel && !activityComplete) { obj.SetActive(false); }
                        else { obj.SetActive(true); }

                        if (slider.value == slider.maxValue){obj.SetActive(false);}

                        if(!sliderUnlocked && !onAPanel){obj.SetActive(false);}
                        break;
                    case "Back Page":
                        if(!progressTracker.movingForward){obj.SetActive(true);}
                        else{obj.SetActive(false);}

                        if(slider.value == slider.minValue){obj.SetActive(false);}

                        if(!sliderUnlocked && !onAPanel){obj.SetActive(false);}
                        break;
                }
            }
        }
        

        //disallow slider movement opposite of story direction
        float currentVal = slider.value;
        if(progressTracker.movingForward && currentVal < previousValue)
        {
            slider.value = previousValue;
        }
        else if(!progressTracker.movingForward && currentVal > previousValue)
        {
            slider.value = previousValue;
        }
        previousValue = slider.value;

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
        virtualCameraOne.transform.position = currentCameraPosition;
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
        if (sliderUnlocked && onLastPanelInPage || sliderUnlocked && onSliderView)
        {
            onLastPanelInPage = false;
            onSliderView = true;
            cameraManager.SetMainCamera();
            movingForward = true;
            CheckPage(slider.value);
        }
        if (onAPanel)
        {
            Debug.Log("Next Page clicked");
            cameraManager.SettingCam = true;
            cameraManager.GoToNextPanel();
        }
        if(pageVal == 0 && progressTracker.timesThroughForward == 0)
        {
            cameraManager.SetMainCamera();
            movingForward = true;
            CheckPage(8 * distanceBetweenPages);
            Debug.Log("going to page 8");
        }
    }
    public void BackPage()
    {
        if(sliderUnlocked && onLastPanelInPage || sliderUnlocked && onSliderView)
        {
            onLastPanelInPage = false;
            cameraManager.SetMainCamera();
            movingForward = false;
            CheckPage(slider.value);
        }
    }
    public void CheckPage(float sliderVal) //check what page the slider is on, give it a variable, move the slider to the middle of that page
    {
        UpdatePageVal(sliderVal);
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
        AudioManager.Instance.PlaySFX("turn page");
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

    public void UpdatePageVal(float sliderVal)
    {
        if(progressTracker.movingForward)
        {
            pageVal = (int)Mathf.Floor(sliderVal / distanceBetweenPages);
        }
        else
        {
            pageVal = (int)Mathf.Ceil(sliderVal / distanceBetweenPages);
        }
    }

    public void BackgroundToBlack()
    {
        m_Camera.backgroundColor = Color.black;
    }
    public void BackgroundToWhite()
    {
        m_Camera.backgroundColor = Color.white;
    }

}

