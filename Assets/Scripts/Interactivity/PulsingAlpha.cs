using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingAlpha : MonoBehaviour
{

    public float pulseSpeed = 1f; // the speed at which the alpha value pulses
    public float minAlpha = 0f; // the minimum alpha value for the pulsing effect
    public float maxAlpha = 1f; // the maximum alpha value for the pulsing effect
    public float startDelay = 2f; // the amount of time to wait before starting the pulsing effect

    private bool isPulsing = false;
    private float currentAlpha = 0f;
    private Renderer render;
    private GameObject object1;

    private enum Purpose{DIVORCE, BULLYING, BREAKUP, WEDDING, COOKIE};
    [SerializeField] private Purpose purpose;

    private ProgressTracker progressTracker;

    public bool cookieClicked = false;

    void Start()
    {
        render = GetComponent<Renderer>();
        object1 = GetComponent<GameObject>();
        render.enabled = false;

        progressTracker = GameObject.Find("Page Manager").GetComponent<ProgressTracker>();
    }

    void Update()
    {
        //Debug.Log("is pulsing" + isPulsing);

        if (isPulsing)
        {
            currentAlpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(Time.time * pulseSpeed, 1f));
            Color color = render.material.color;
            color.a = currentAlpha;
            render.material.color = color;
        }
        else
        {

            Color color = render.material.color;
            color.a = 0f;
            render.material.color = color;
        }

        //used to access specific booleans depending on the pulsor's purpose. some are unused but added in case of need for expansion.
        //turns out this wasnt needed but im leaving it here -carter
        switch(purpose.ToString())
        {
            case "DIVORCE":
                break;
            case "BULLYING":
                if(progressTracker.bullyStarted)
                {
                    StopPulsing();
                }
                break;
            case "BREAKUP":
                break;
            case "WEDDING":
                break;
            case "COOKIE":
                break;
            default:
                break;
        }
    }

    public void PulseEnable()
    {
        Invoke("StartPulsing", startDelay);
    }

    public void StartPulsing()
    {
        if (!cookieClicked && !isPulsing && progressTracker.PulseHighlight)
        {
            render.enabled = true;
            isPulsing = true;
            //Debug.Log("is starting");
        }
    }

    public void StopPulsing()
    {
        isPulsing = false;
        render.enabled = false;
    }
}
