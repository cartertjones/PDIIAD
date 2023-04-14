using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class DivorceInteractivity : MonoBehaviour
{
    //[SerializeField] private UnityEngine.UI.Slider slider;
    [SerializeField] private GameObject dadSquare;
    [SerializeField] private GameObject momSquare;
    [SerializeField] private Slider divorceSlider;
    private Material currentMatDad;
    private Material currentMatMom;

    private bool momSet = false;
    private bool dadSet = false;

    //used to determine if player completed the activity
    private bool activityComplete;
    public bool ActivityComplete
    {
        get{ return activityComplete; }
        set{ activityComplete = value; }
    }

    void Start()
    {
        currentMatDad = dadSquare.GetComponent<Renderer>().material;
        currentMatMom = momSquare.GetComponent<Renderer>().material;
        momSquare.gameObject.SetActive(false);
        dadSquare.gameObject.SetActive(false);
    }

     void OnSliderChanged(float alphaValue)
    {
       if (alphaValue < 0f && !momSet)
        {
            Color momOldColor = currentMatMom.color;
            Color momSquare = new Color(momOldColor.r, momOldColor.g, momOldColor.b, (alphaValue ) +1.1f);
            //Debug.Log("Slider" + (alphaValue));
            //Debug.Log("Alpha" + (alphaValue + 1f));
            currentMatMom.color = momSquare;
            if (alphaValue <= -1f)
            {
                momSet = true;
            }

        }
        if (alphaValue > 0f && !dadSet)
        {
            Color dadOldColor = currentMatDad.color;
            Color dadSquare = new Color(dadOldColor.r, dadOldColor.g, dadOldColor.b, (-alphaValue) +1.1f );
            //Debug.Log("" + (alphaValue));
            currentMatDad.color = dadSquare;
            if (alphaValue >= 1f)
            {
                dadSet = true;
            }
        }

        if(momSet && dadSet)
        {
            activityComplete = true;

            //hide slider when activity complete
            divorceSlider.gameObject.SetActive(false);
        }
    }
    public void ChangeAlpha(UnityEngine.UI.Slider slider)
    {
        OnSliderChanged(slider.value);
    }

    //show all relevant gameobjects
    public void Show()
    {
        divorceSlider.gameObject.SetActive(true);
        momSquare.gameObject.SetActive(true);
        dadSquare.gameObject.SetActive(true);
    }
    //hide all relevant gameobjects
    public void Hide()
    {
        divorceSlider.gameObject.SetActive(false);
        momSquare.gameObject.SetActive(false);
        dadSquare.gameObject.SetActive(false);
    }
    //reset activity progress
    public void Reset()
    {
        activityComplete = false;
    }
}
