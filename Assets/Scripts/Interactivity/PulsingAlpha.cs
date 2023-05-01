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
    private float currentAlpha = 1f;
    private Renderer render;
    private GameObject object1;

    public bool cookieClicked = false;

    void Start()
    {
        render = GetComponent<Renderer>();
        object1 = GetComponent<GameObject>();
        render.enabled = false;
    }

    void Update()
    {
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
            color.a = 1f;
            render.material.color = color;
        }
    }

    public void PulseEnable()
    {
        Invoke("StartPulsing", startDelay);
    }

    public void StartPulsing()
    {
        if (!cookieClicked)
        {
            render.enabled = true;
            isPulsing = true;

        }
    }

    public void StopPulsing()
    {
        isPulsing = false;
    }
}
