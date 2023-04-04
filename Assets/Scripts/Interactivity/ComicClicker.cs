using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicClicker : MonoBehaviour
{
    Camera m_Camera;
    [SerializeField] GameObject[] interactivity;
    [SerializeField] GameObject[] pages;
    [SerializeField] GameObject[] inversePages;
    public SlideCam slideCam;
    public PageManager pageManager;
    public MovingDan movingDan;


    void Awake()
    {
        m_Camera = Camera.main;
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
            //Debug.Log("Object clicked: " + hit.collider.gameObject);
            if (!hit)
            {
                return;
            }
            if (hit.collider.gameObject == interactivity[0])//IF the panel1 gameobject is clicked -
            {
                Debug.Log("Clicked " + hit.collider.gameObject);
                slideCam.ActivateSlider();
                pageManager.InvertPages();
                movingDan.ActivateDan();
                Debug.Log("check 1");
            }

        }
    }

}
