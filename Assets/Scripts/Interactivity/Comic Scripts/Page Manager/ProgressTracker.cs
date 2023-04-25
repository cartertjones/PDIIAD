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

    public float timesThroughForward = 0;
    public float timesThroughBackward = 0;
    public bool movingForward;

    private int pageVal;
    private Slider slideCamSlider;

    bool interactivityActive;

    public bool divorceFinished;

    [SerializeField] private Slider slider;
    private bool onPage1;
    private bool onLastPage;




    void Start()
    {
        movingForward = true;
        interactivityActive = false;

        divorceFinished = false;

        //hide all interactivity at start
        HideAllInteractivity();

        slideCamSlider = GameObject.Find("Canvas/Slider").GetComponent<Slider>();

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

        switch(pageVal)
        {
            case 0:
                if(!movingForward && slideCamSlider.value == 0)
                {
                    timesThroughBackward++;
                    movingForward = true;
                }
                break;
            case 1:
                //activate divorce if moving forward and interactivity enabled (2nd time through) and camera not moving
                if(interactivityActive && movingForward && !slideCam.IsMoving)
                {
                    //if activity has not been played yet
                    if(!divorceInteractivity.ActivityComplete)
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
            case 3:
                if(interactivityActive && movingForward)
                {

                }
                break;
            //add more cases to use interactivity
            case 11:
                if (movingForward)
                {
                    timesThroughForward++;
                    movingForward = false;
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
        //TODO add more interactivity connections
    }
}
