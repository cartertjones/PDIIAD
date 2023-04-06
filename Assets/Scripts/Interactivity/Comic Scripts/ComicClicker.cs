using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ComicClicker : MonoBehaviour
{
    Camera m_Camera;
    [SerializeField] GameObject[] interactivity;
    [SerializeField] GameObject[] pages;
    [SerializeField] GameObject[] inversePages;

    public SlideCam slideCam;
    public PageManager pageManager;
    public MovingDan movingDan;
    public PageTracker pageTracker;
    public VideoPlayerScript videoScript;



    void Awake()
    {
        m_Camera = Camera.main;


    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            //Debug.Log("Object clicked: " + hit.collider.gameObject);
            if (!hit)
            {
                return;
            }
            if (hit.collider.gameObject == interactivity[0])//IF the Interactivity0 (fortune Cookie) gameobject is clicked -
            {
                Debug.Log("Clicked " + hit.collider.gameObject);
                if (pageTracker.timesThroughForward == 1) // if first time through story -- play the first video in the video array -- also activate 10th page
                {
                    Debug.Log("Calling player");
                    videoScript.StartVideo1();
                    pageManager.ActivatePage11();
                    slideCam.AddPage11();
                }
                if (pageTracker.timesThroughForward == 2) // if second time through story -- play the second video in the video array -- also activate 11th page
                {
                  //  videoPanels[1].SetActive(true);
                  //  videoPlayers[1].Play();
                    pageManager.ActivatePage11();
                    slideCam.AddPage11();
                }
                if (pageTracker.timesThroughForward == 3) // if third time through story -- play the third video in the video array also activate 11th & 12th page
                {
                //    videoPanels[2].SetActive(true);
                 //   videoPlayers[2].Play();
                    pageManager.ActivatePage11();
                    slideCam.AddPage11();
                    pageManager.ActivatePage12();
                    slideCam.AddPage12();
                }


                Debug.Log("check 1");
            }
            if (hit.collider.gameObject == interactivity[1])//IF the interactivity1 (backwards button) gameobject is clicked -
            {
                slideCam.ActivateSlider();
                pageManager.InvertPages();
                movingDan.ActivateDan();
                pageTracker.movingBackward = true;
                pageTracker.movingForward = false;

            }


        }

    }
    private void StopVideo()
    {
        

    }

}
