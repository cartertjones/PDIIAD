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
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer1)
        {
            StopVideo1();
        }
    }
    public void StartVideo1()
    {
        Debug.Log("check video player");
        videoPanels[0].SetActive(true);
        videoPlayers[0].Play();
        videoPlayer1 = true;
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
}
