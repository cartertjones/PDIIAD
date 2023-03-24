using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Clicker : MonoBehaviour
{
    Camera m_Camera;
    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject[] panelButtons;
    [SerializeField] GameObject[] pageButtons;
    [SerializeField] GameObject[] panelCameraPos;
    [SerializeField] GameObject[] pageCameraPos;
    [SerializeField] GameObject slider;
    private float newSize = 1.0f;
    private float oldSize;
    private Vector3 startingCameraPosition;
    private string currentCameraPos;
    private Vector3 cameraPos;

    void Awake()
    {
        m_Camera = Camera.main;
        oldSize = m_Camera.orthographicSize;
        startingCameraPosition = m_Camera.transform.position;
    }
    /* void Update()
      {
          if (Input.GetMouseButtonDown(0))
          {
              Vector3 mousePosition = Input.mousePosition;
              Ray ray = m_Camera.ScreenPointToRay(mousePosition);
              if (Physics.Raycast(ray, out RaycastHit hit))
              {
                  Debug.Log("" + hit);
              }
          }
      }*/
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            Debug.Log("Object clicked: " + hit.collider.gameObject);
            if (hit.collider.gameObject == panels[0]  && currentCameraPos!= "CameraPos1") //IF the panel1 gameobject is clicked - Move camera to that position
            {
                // Do something here when the object is clicked
                Debug.Log("Object clicked: " + hit.collider.gameObject);
                m_Camera.transform.position = panelCameraPos[0].transform.position;
                m_Camera.orthographicSize = newSize;
                currentCameraPos = panelCameraPos[0].gameObject.name;
                cameraPos = panelCameraPos[0].transform.position;

            }
            if (hit.collider.gameObject == panels[1] && currentCameraPos != "CameraPos1") //IF the panel2 gameobject is clicked - Move camera to that position
            {
                // Do something here when the object is clicked
                Debug.Log("Object clicked: " + hit.collider.gameObject);
                m_Camera.transform.position = panelCameraPos[1].transform.position;
                m_Camera.orthographicSize = newSize;
                currentCameraPos = panelCameraPos[1].gameObject.name;

            }
            if (hit.collider.gameObject == panels[2] && currentCameraPos != "CameraPos1") //IF the panel2 gameobject is clicked - Move camera to that position
            {
                // Do something here when the object is clicked
                Debug.Log("Object clicked: " + hit.collider.gameObject);
                m_Camera.transform.position = panelCameraPos[2].transform.position;
                m_Camera.orthographicSize = newSize;
                currentCameraPos = panelCameraPos[2].gameObject.name;

            }
            else if (hit.collider.gameObject == panelButtons[0]) //IF the next panel 1 gameobject is clicked - Move camera to that position
            {
                // Do something here when the object is clicked
                Debug.Log("Object clicked: " + hit.collider.gameObject);
                m_Camera.transform.position = panelCameraPos[1].transform.position;
                m_Camera.orthographicSize = newSize;
                currentCameraPos = panelCameraPos[1].gameObject.name;

            }
            else if (hit.collider.gameObject == panelButtons[1]) //IF the panel1 gameobject is clicked - Move camera to that position
            {
                // Do something here when the object is clicked
                Debug.Log("Object clicked: " + hit.collider.gameObject);
                m_Camera.transform.position = panelCameraPos[2].transform.position;
                m_Camera.orthographicSize = newSize;
                currentCameraPos = panelCameraPos[2].gameObject.name;

            }
            else if (hit.collider.gameObject == panelButtons[2]) //IF the panel1 gameobject is clicked - Move camera to that position
            {
                // Do something here when the object is clicked
                Debug.Log("Object clicked: " + hit.collider.gameObject);
                m_Camera.transform.position = startingCameraPosition;
                m_Camera.orthographicSize = oldSize;
                currentCameraPos = pageCameraPos[0].gameObject.name;

            }
        }
        for (int i = 0; i < panelCameraPos.Length; i++)
        if (cameraPos == pageCameraPos[i].transform.position)
        {
            slider.SetActive(false);
        }
        else slider.SetActive(true);

    }

}

