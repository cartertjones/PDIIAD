using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    [SerializeField] GameObject[] videoPanels;
    [SerializeField] VideoPlayer[] videoPlayers;

    [SerializeField] private GameObject continueButton;

    [SerializeField] private ProgressTracker progressTracker;

    private float totalFrames;
    private float currentFrame;

    private bool videoComplete;

    private bool playing;
    private float playTime;

    [SerializeField] private float videoLength;
    [SerializeField] private float endSceneLength;

    private void Awake()
    {

        for (int i = 0; i < videoPanels.Length; i++)
        {
            videoPanels[i].gameObject.SetActive(false);
            videoPlayers[i].frame = 1;
        }

        continueButton.SetActive(false);
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
        if (videoComplete)
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }
    public void StartVideo1()
    {
        videoPanels[0].SetActive(true);
        videoPlayers[0].Play();

        StartCoroutine(EndVideo(videoLength));
    }
    public void StartVideo2()
    {
        videoPanels[1].SetActive(true);
        videoPlayers[1].Play();

        StartCoroutine(EndVideo(videoLength));
    }
    public void StartVideo3()
    {
        videoPanels[2].SetActive(true);
        videoPlayers[2].Play();

        StartCoroutine(EndVideo(videoLength));
    }

    public void StartEndVideo()
    {
        videoPanels[3].SetActive(true);
        videoPlayers[3].Play();

        StartCoroutine(EndVideo(endSceneLength));
    }

    public void ContinueButtonPressed()
    {
        videoComplete = false;

        foreach(GameObject videoPanel in videoPanels)
        {
            videoPanel.SetActive(false);
        }

        continueButton.SetActive(false);

        if(progressTracker.timesThroughForward == 3)
        {
            StartEndVideo();
        }
    }

    private IEnumerator EndVideo(float videoLength)
    {
        yield return new WaitForSeconds(videoLength);
        videoComplete = true;
    }
}
