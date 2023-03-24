using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideCam : MonoBehaviour
{
    Camera m_Camera;
    private Vector3 startingCameraPosition;
    private Vector3 currentCameraPosition;

    // Start is called before the first frame update
    void Awake()
    {
        m_Camera = Camera.main;
        startingCameraPosition = m_Camera.transform.position;
        currentCameraPosition = m_Camera.transform.position;
    }

    // Update is called once per frame
    void updateCameraPos(float value)
    {
        currentCameraPosition.x = value;
        m_Camera.transform.position = currentCameraPosition;
    }
    public void onSliderChanged(float sliderValue)
    {
        updateCameraPos(sliderValue);
    }
}
