using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using Unity.VisualScripting;
using TMPro;

public class CameraManager : MonoBehaviour
{
    public SlideCam slideCam;
    public ProgressTracker progTracker;
    public DivorceInteractivity divInt;


    [SerializeField] private GameObject[] pageZeroCameras; //an array of all the virtual cameras on page 0
    [SerializeField] private GameObject[] pageOneCameras; //an array of all the virtual cameras on page 1
    [SerializeField] private GameObject[] pageTwoCameras; //an array of all the virtual cameras on page 2
    [SerializeField] private GameObject[] pageThreeCameras; //an array of all the virtual cameras on page 3
    [SerializeField] private GameObject[] pageFourCameras; //an array of all the virtual cameras on page 4
    [SerializeField] private GameObject[] pageFiveCameras; //an array of all the virtual cameras on page 5
    [SerializeField] private GameObject[] pageSixCameras; //an array of all the virtual cameras on page 6
    [SerializeField] private GameObject[] pageSevenCameras; //an array of all the virtual cameras on page 7
    [SerializeField] private GameObject[] pageEightCameras; //an array of all the virtual cameras on page 8
    [SerializeField] private GameObject[] pageNineCameras; //an array of all the virtual cameras on page 9
    [SerializeField] private GameObject[] pageTenCameras; //an array of all the virtual cameras on page 10
    [SerializeField] private GameObject[] pageElevenCameras; //an array of all the virtual cameras on page 11
    [SerializeField] private GameObject[] pageTwelveCameras; //an array of all the virtual cameras on page 12

    [SerializeField] private GameObject phoneScreen;

    private CinemachineBrain brain;



    private GameObject[][] pages;

    private int currentPage;
    private int currentPanel;

    private int lastPage;
    private int lastPanel;
    private int panelIndex;

    public bool SettingCam;

    public bool cameraStopped = false;

    void Awake()
    {
        Debug.Log("on panel" + slideCam.onAPanel);
        Debug.Log("on last panel" + slideCam.onLastPanelInPage);
        brain = FindObjectOfType<CinemachineBrain>();
        pages = new GameObject[][] { pageZeroCameras, pageOneCameras, pageTwoCameras, pageThreeCameras, pageFourCameras, pageFiveCameras, pageSixCameras, pageSevenCameras, pageEightCameras, pageNineCameras, pageTenCameras, pageElevenCameras, pageTwelveCameras };
        currentPage = 0;
        currentPanel = 0;
        SettingCam = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("on panel" + slideCam.onAPanel);
        //Debug.Log("on last panel" + slideCam.onLastPanelInPage);

        if (!SettingCam)
        {
            lastPage = currentPage;
            lastPanel = currentPanel;
        }
        if (cameraStopped)
        {
            progTracker.intPanelorPage = true;
            progTracker.onIntPanel = false;
        }

    }

    public void MoveToPanel(int page, int panel)
    {
        Debug.Log("Moved to panel");
        if ( slideCam.ActivityComplete)
        {
            pages[lastPage][lastPanel].gameObject.SetActive(false);
            pages[page][panel].gameObject.SetActive(true);
            currentPage = page;
            currentPanel = panel;
            SettingCam = false;
            Debug.Log("current page " + currentPage + " current panel " + currentPanel);
            Debug.Log("last page " + lastPage + " last panel " + lastPanel);
            // Debug.Log("pages[page][panel]" + page + panel);
            //Debug.Log("pages[page].Length " + pages[page].Length);
            panelIndex = panel;
            Debug.Log("panel index clicker " + panelIndex);
            if (page == 0 && panel == 0) //if on cover set slider view to true and on last panel to false
            {
                slideCam.onSliderView = true;
                slideCam.onLastPanelInPage = false;
            }
            if (page == 1 && panel != 3) // if on the first page but not panel 3 (divorce interactive panel) hide interactive slider
            {
                progTracker.intPanelorPage = false;
                divInt.Hide();
            }
            if (page == 2 && panel == 6) // if on page 2 panel 6 ( breakup interactive panel) interactive panel is true and complete is false
            {
                slideCam.OnInteractivePanel = true;
                slideCam.ActivityComplete = false;
            }
            if (page == 3 && panel == 2) //if on page 3 panel 2 ( bully interactive panel) interactive panel is true and complete is false
            {
                slideCam.OnInteractivePanel = true;
                slideCam.ActivityComplete = false;
            }
            if (page == 4 && panel == 5)
            {
                slideCam.OnInteractivePanel = true;
                slideCam.ActivityComplete = false;
            }
            if (panel == pages[page].Length - 1) // check if panel that we are on is the last panel by checking the length of the array. Have to minus one due to 0 position of array
            {
                Debug.Log("On last panel");
                if (page != 0 && panel != 0)
                {
                    slideCam.onLastPanelInPage = true;

                }
                slideCam.onAPanel = false;
                if (page == 1)
                {
                    progTracker.onIntPanel = true;
                    StartCoroutine(WaitAndSetBoolCoroutine());
                }
            }
            else
            {
                slideCam.onLastPanelInPage = false;
                slideCam.onAPanel = true;
            }
        }
    }
    public void SetMainCamera()
    {
        panelIndex = 0;
        pages[lastPage][lastPanel].gameObject.SetActive(false);
        pages[0][0].gameObject.SetActive(true);
        currentPage = 0;
        currentPanel = 0;
    }
    public void GoToNextPanel()
    {
        if (slideCam.onAPanel)
        {
            panelIndex++;

        }
        Debug.Log("next page check: current panel " + currentPanel);
        Debug.Log("next page check: panel Index " + panelIndex);
        Debug.Log("next page check: current panel + 1 " + currentPanel + 1);

        MoveToPanel(currentPage, panelIndex);
    }
    public void MoveToPhone()
    {
        pages[lastPage][lastPanel].gameObject.SetActive(false);
        phoneScreen.gameObject.SetActive(true);
    }
    public void ReturnToBreakupPage()
    {
        phoneScreen.gameObject.SetActive(false);
        pages[lastPage][lastPanel].gameObject.SetActive(true);
    }

    private IEnumerator WaitAndSetBoolCoroutine()
    {
        yield return new WaitForSeconds(2f);
        cameraStopped = true;
    }
}
