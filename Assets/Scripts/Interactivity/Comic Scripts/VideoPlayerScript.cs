using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    [SerializeField] GameObject[] videoPanels;
    [SerializeField] VideoPlayer[] videoPlayers;

    private float totalFrames;
    private float currentFrame;

    private bool videoPlayer1;
    private bool videoPlayer2;
    private bool videoPlayer3;

    private void Awake()
    {

        for (int i = 0; i < videoPanels.Length; i++)
        {
            videoPanels[i].gameObject.SetActive(false);
            videoPlayers[i].frame = 1;
        }
    }
    void Start()
    {
        for (int i = 0; i < videoPlayers.Length; i++)
        {
            videoPlayers[i].Prepare();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer1)
        {
            StopVideo1();
        }
        if (videoPlayer2)
        {
            StopVideo2();
        }
        if (videoPlayer3)
        {
            StopVideo3();
        }
    }
    public void StartVideo1()
    {
        Debug.Log("check video player");
        videoPanels[0].SetActive(true);
        videoPlayers[0].Play();
        videoPlayer1 = true;
    }
    public void StartVideo2()
    {
        videoPanels[1].SetActive(true);
        videoPlayers[1].Play();
        videoPlayer2 = true;
    }
    public void StartVideo3()
    {
        Debug.Log("check video player");
        videoPanels[2].SetActive(true);
        videoPlayers[2].Play();
        videoPlayer3 = true;
    }
    public void StopVideo1()
    {

            totalFrames = videoPlayers[0].frameCount - 1;
            currentFrame = videoPlayers[0].frame;
            if (currentFrame >= totalFrames)
            {
                videoPanels[0].gameObject.SetActive(false);
                //videoPlaying = false;
            }
            /*if (videoPlayers[i].isPlaying)
            {
                videoPanels[i].gameObject.SetActive(true);
            }*/
    }
    public void StopVideo2()
    {

        totalFrames = videoPlayers[1].frameCount - 1;
        currentFrame = videoPlayers[1].frame;
        if (currentFrame >= totalFrames)
        {
            videoPanels[1].gameObject.SetActive(false);
            //videoPlaying = false;
        }
        /*if (videoPlayers[i].isPlaying)
        {
            videoPanels[i].gameObject.SetActive(true);
        }*/
    }
    public void StopVideo3()
    {

        totalFrames = videoPlayers[2].frameCount - 1;
        currentFrame = videoPlayers[2].frame;
        if (currentFrame >= totalFrames)
        {
            videoPanels[2].gameObject.SetActive(false);
            //videoPlaying = false;
        }
        /*if (videoPlayers[i].isPlaying)
        {
            videoPanels[i].gameObject.SetActive(true);
        }*/
    }
}
