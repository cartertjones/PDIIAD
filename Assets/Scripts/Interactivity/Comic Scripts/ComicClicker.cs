using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class ComicClicker : MonoBehaviour
{
    Camera m_Camera;
    [SerializeField] GameObject[] interactivity;
    [SerializeField] GameObject[] pageZeroPanels;
    [SerializeField] GameObject[] pageOnePanels;
    [SerializeField] GameObject[] pageTwoPanels;
    [SerializeField] GameObject[] pageThreePanels;
    [SerializeField] GameObject[] pageFourPanels;
    [SerializeField] GameObject[] pageFivePanels;
    [SerializeField] GameObject[] pageSixPanels;
    [SerializeField] GameObject[] pageSevenPanels;
    [SerializeField] GameObject[] pageEightPanels;
    [SerializeField] GameObject[] pageNinePanels;
    [SerializeField] GameObject[] pageTenPanels;
    [SerializeField] GameObject[] pageElevenPanels;
    [SerializeField] GameObject[] pageTwelvePanels;


    [SerializeField] GameObject[] pages;
    [SerializeField] GameObject[] inversePages;

    private GameObject[][] panelsPages;

    public SlideCam slideCam;
    public PageManager pageManager;
    public MovingDan movingDan;
    public ProgressTracker progressTracker;
    public VideoPlayerScript videoScript;
    public DivorceInteractivity divorceInteractivity;
    public CameraManager cameraManager;
    public PulsingAlpha cookiePulsingAlpha;
    public PulsingAlpha breakupPulsingAlpha;



    void Awake()
    {
        m_Camera = Camera.main;
        panelsPages = new GameObject[][] { pageZeroPanels, pageOnePanels, pageTwoPanels, pageThreePanels, pageFourPanels, pageFivePanels, pageSixPanels, pageSevenPanels, pageEightPanels, pageNinePanels, pageTenPanels, pageElevenPanels, pageTwelvePanels };

    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Debug.Log("clicked");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
           // Debug.Log("Object clicked: " + hit.collider.gameObject);
            if (!hit)
            {
                return;
            }
            if (hit.collider.gameObject == interactivity[0])//IF the Interactivity0 (fortune Cookie) gameobject is clicked -
            {
                Debug.Log("Clicked " + hit.collider.gameObject);
                if (progressTracker.timesThroughForward == 0) // if first time through story -- play the first video in the video array -- also activate 10th page
                {
                    Debug.Log("Calling player");
                    videoScript.StartVideo1();
                    slideCam.AddPage11();
                    cookiePulsingAlpha.StopPulsing(); // stop the pulsing of the cookie
                    cookiePulsingAlpha.cookieClicked = true; // set cookie clicked bool so the case set in progress tracker doesnt continue to pulse it
                }
                if (progressTracker.timesThroughForward == 1) // if second time through story -- play the second video in the video array -- also activate 11th page
                {
                    //  videoPanels[1].SetActive(true);
                    //  videoPlayers[1].Play();
                    videoScript.StartVideo2();
                    slideCam.AddPage11();
                    cookiePulsingAlpha.StopPulsing(); // stop the pulsing of the cookie
                    cookiePulsingAlpha.cookieClicked = true; // set cookie clicked bool so the case set in progress tracker doesnt continue to pulse it

                }
                if (progressTracker.timesThroughForward == 2) // if third time through story -- play the third video in the video array also activate 11th & 12th page
                {
                    //    videoPanels[2].SetActive(true);
                    //   videoPlayers[2].Play();
                    videoScript.StartVideo3();
                    pageManager.ActivatePage11();
                    slideCam.AddPage11();
                    pageManager.ActivatePage12();
                    slideCam.AddPage12();
                    cookiePulsingAlpha.StopPulsing(); // stop the pulsing of the cookie
                    cookiePulsingAlpha.cookieClicked = true; // set cookie clicked bool so the case set in progress tracker doesnt continue to pulse it


                }


                Debug.Log("check 1");
            }
            if (hit.collider.gameObject == interactivity[1])//IF the interactivity1 (backwards button) gameobject is clicked -
            {
                slideCam.ActivateSlider();
                pageManager.InvertPages();
                movingDan.ActivateDan();
                progressTracker.movingForward = false;

            }
            if (hit.collider.gameObject == interactivity[2])
            {
                //bullying interactivity add stuff here
            }
            if (hit.collider.gameObject == interactivity[3]) // if the interactivity3 (breakup) gameobject is clicker -
            {
                cameraManager.MoveToPhone();
                breakupPulsingAlpha.StopPulsing();
               
            }
            for (int i = 0; i < panelsPages.Length; i++)
            {
                for (int j = 0; j < panelsPages[i].Length; j++)
                {
                    if (hit.collider.gameObject == panelsPages[i][j])
                    {
                        Debug.Log("An object in array " + i + " at position " + j + " was clicked!");
                        cameraManager.SettingCam = true;
                        cameraManager.MoveToPanel(i, j);
                        return;
                    }
                }
            }


        }

    }


}
