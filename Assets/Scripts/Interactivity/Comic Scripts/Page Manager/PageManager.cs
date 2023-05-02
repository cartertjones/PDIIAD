using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    [SerializeField] GameObject[] pages;
    [SerializeField] GameObject[] inversePages;
    [SerializeField] GameObject[] interactivepages;
    [SerializeField] private GameObject cover;
    // Start is called before the first frame update


    private float minAlpha = 0f; // the minimum alpha value for the fade effect
    private float maxAlpha = 1f; // the maximum alpha value for the fade effect
    private float currentAlpha = 0f;
    private float fadeDuration = 2f; // Fade duration in seconds
    private float elapsedTime; // Time elapsed since fading started
    private float t;



    void Start()
    {
        Debug.Log("hide pages");
        pages[11].gameObject.SetActive(false);
        interactivepages[0].gameObject.SetActive(false);

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
            //skip cover
            if(i != 0)
            {
                pages[i].gameObject.SetActive(false);
            }
        }
    }
    public void RevertPages()
    {
        for (int i = 0; i < pages.Length-1; i++)
        {
            //skip cover
            if(i != 0)
            {
                pages[i].gameObject.SetActive(true);
            } 
        }
    }
    public void ActivatePage11()
    {
        pages[11].gameObject.SetActive(true);
    }
    public void ActivateCover(bool param)
    {
        cover.SetActive(param);
    }
    public void ActivateBully()
    {
        StartCoroutine(FadeCoroutine());
        /*
        interactivepages[0].gameObject.SetActive(true);
        elapsedTime += Time.deltaTime;
        t = Mathf.Clamp01(elapsedTime / fadeDuration);
        currentAlpha = Mathf.Lerp(minAlpha, maxAlpha, t);
        */
        // add stuff here to fade in
    }
    private IEnumerator FadeCoroutine()
    {
        Debug.Log("Fade coroutine called");
        interactivepages[0].gameObject.SetActive(true);
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
            currentAlpha = Mathf.Lerp(minAlpha, maxAlpha, elapsedTime / fadeDuration);
            interactivepages[0].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, currentAlpha);
        }
    }


}
