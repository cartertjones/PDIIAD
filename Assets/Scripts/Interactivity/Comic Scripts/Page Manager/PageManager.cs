using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    [SerializeField] GameObject[] pages;
    [SerializeField] GameObject[] inversePages;
    // Start is called before the first frame update
    void Start()
    {
        pages[11].gameObject.SetActive(false);
        pages[12].gameObject.SetActive(false);
        inversePages[11].gameObject.SetActive(false);
        inversePages[12].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddPages()
    {
        pages[12].gameObject.SetActive(true);
        pages[13].gameObject.SetActive(true);
    }
    public void InvertPages()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].gameObject.SetActive(false);
            Debug.Log("Pages Inverted");
        }
    }
    public void RevertPages()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].gameObject.SetActive(true);
        }
    }
    public void ActivatePage11()
    {
        pages[11].gameObject.SetActive(true);
        inversePages[11].gameObject.SetActive(true);
    }
    public void ActivatePage12()
    {
        pages[12].gameObject.SetActive(true);
        inversePages[12].gameObject.SetActive(true);
    }
}
