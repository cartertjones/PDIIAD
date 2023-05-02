using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{

    //start halfway, go back, read through full story, go back, play interactivity, finish
    public PageManager pageManager;
    public PageValues pageValues;
    public SlideCam slideCam;
    public DivorceInteractivity divorceInteractivity;
    public MessageInteractivity messageInteractivity;
    public MagnifyingGlass weddingInteractivity;
    public PulsingAlpha cookiePulsingAlpha;
    public PulsingAlpha breakupPulsingAlpha;
    public PulsingAlpha bullyPulsingAlpha;

    public PulsingAlpha panelHighlights;


    public float timesThroughForward = 0;
    public float timesThroughBackward = 0;
    public bool movingForward;

    private int pageVal;
    [SerializeField] private Slider slideCamSlider;

    public bool interactivityActive;

    public bool divorceFinished;
    public bool bullyStarted;

    [SerializeField] private Slider slider;
    private bool onPage1;
    private bool onLastPage;
   
    public bool intPanelorPage = false; // bool for  if on a page view or interactive panel
    public bool onIntPanel = false;

    public bool breakupInt;

    [SerializeField] private List<GameObject> instructionsList;

    private bool pulseHighlight;

    public bool PulseHighlight
    {
        get { return pulseHighlight; }
        set { pulseHighlight = value; }
    }

        

    void Start()
    {
        pulseHighlight = false;
        movingForward = true;
        interactivityActive = false;
        breakupInt = false;
        divorceFinished = false;

        //hide all interactivity at start
        HideAllInteractivity();
    }

    void Update()
    {
        //get current page
        pageVal = slideCam.PageVal;

        //activate interactivity if second time through
        if(timesThroughForward == 1)
        {
            interactivityActive = true;
        }

        //hide all interactivity if the camera is moving
        if(slideCam.IsMoving)
        {
            HideAllInteractivity();
        }

        if(pageVal == 0 && movingForward)
        {
            pageManager.ActivateCover(true);
        }
        else if(!movingForward)
        {
            pageManager.ActivateCover(true);
        }
        else
        {
            pageManager.ActivateCover(false);
        }

        switch(pageVal)
        {
            case 0:
                pulseHighlight = false;
                cookiePulsingAlpha.cookieClicked = false; //reset cookie pulsing
                if (!movingForward && slideCamSlider.value == 0)
                {
                    timesThroughBackward++;
                    movingForward = true;
                    if (timesThroughForward != 3)
                    {
                        Debug.Log("hide");
                        slideCam.Hide11();
                    }
                }
                break;
            case 1:
                //activate divorce if moving forward and interactivity enabled (2nd time through) and camera not moving
                if (interactivityActive && movingForward && !slideCam.IsMoving)
                {
                    if (slideCam.onAPanel)
                    {
                        intPanelorPage = false;
                    }
                    else if (!onIntPanel)
                    {
                        intPanelorPage = true;
                    }
                    //if activity has not been played yet
                    if(!divorceInteractivity.ActivityComplete && intPanelorPage)
                    {
                        divorceInteractivity.Show();
                        //lock camera
                        slideCam.SliderUnlocked = false;
                    }

                    
                    //unlock when complete
                    if(divorceInteractivity.ActivityComplete)
                    {
                        slideCam.SliderUnlocked = true;
                    }
                }
                break;
            case 2:
                if (interactivityActive && movingForward && !slideCam.IsMoving)
                {
                    if(!bullyStarted)
                    {
                        slideCam.SliderUnlocked = false;
                        bullyPulsingAlpha.PulseEnable();
                    }
                    if (bullyStarted)
                    {
                        bullyPulsingAlpha.StopPulsing();
                        slideCam.SliderUnlocked = true;
                    }
                }
                break;
            case 3:
                if(interactivityActive && movingForward)
                {
                    breakupInt = true;
                    if(!messageInteractivity.ActivityComplete)
                    {
                        slideCam.SliderUnlocked = false;
                        breakupPulsingAlpha.PulseEnable();
                    }

                    //unlock when complete
                    if(messageInteractivity.ActivityComplete)
                    {
                        slideCam.SliderUnlocked = true;
                        breakupPulsingAlpha.StopPulsing();
                        breakupPulsingAlpha.transform.gameObject.SetActive(false);
                    }
                }
                break;
            case 4:
                if(interactivityActive && movingForward)
                {
                    if(!weddingInteractivity.ActivityComplete)
                    {
                        slideCam.SliderUnlocked = false;
                        if(!weddingInteractivity.ActivityActive)
                        {
                            weddingInteractivity.Show();
                        }
                    }

                    //unlock when complete
                    if(weddingInteractivity.ActivityComplete)
                    {
                        slideCam.SliderUnlocked = true;
                    }
                }
                break;
            case 7:
                if (movingForward)
                {
                    if (slideCam.onSliderView && timesThroughForward == 0)
                    {
                        pulseHighlight = true;
                        panelHighlights.PulseEnable();
                    }
                    else
                    {
                        pulseHighlight = false;
                        panelHighlights.StopPulsing();
                    }

                }
                break;
            case 8:
                if (movingForward)
                {
                    panelHighlights.StopPulsing();
                }
                break;
            case 10:
                if (movingForward)
                {
                    cookiePulsingAlpha.PulseEnable(); // start pulsing aplha of the fortune cookie object
                }    
                break;
            case 11:
                if (movingForward)
                {
                    timesThroughForward++;
                    movingForward = false;
                    Invoke("ActivateDan", 1);
                }


                break;
        }

        if (movingForward)
        {
            pageManager.RevertPages();
            slideCam.BackgroundToWhite();
        }
        else
        {
            pageManager.InvertPages();
            slideCam.BackgroundToBlack();
        }

    }

    private void HideAllInteractivity()
    {
        divorceInteractivity.Hide();
        weddingInteractivity.Hide();
        //TODO add more interactivity connections

        //hide instructions also
        foreach(GameObject obj in instructionsList)
        {
            obj.SetActive(false);
        }
    }

    //referenced in switch, called by invoke line 189
    private void ActivateDan()
    {
        MovingDan movingDan = GameObject.Find("Moving Dan").GetComponent<MovingDan>();
        movingDan.ActivateDan();
    }

}
