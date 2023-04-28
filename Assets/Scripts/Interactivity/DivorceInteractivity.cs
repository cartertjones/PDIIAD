using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivorceInteractivity : MonoBehaviour
{
    //[SerializeField] private UnityEngine.UI.Slider slider;
    //[SerializeField] private GameObject dadSquare;
    //[SerializeField] private GameObject momSquare;
    [SerializeField] private Slider divorceSlider;
    private Material currentMatDad;
    private Material currentMatMom;
    private Material currentMatBoth;

    //TODO FIX ALPHA TRANSITION

    //carter divorce change
    [SerializeField] private GameObject momPanel;
    [SerializeField] private GameObject dadPanel;
    [SerializeField] private GameObject finalPanel;
    private SpriteRenderer msr, dsr, fsr;

    private bool momSet = false;
    private bool dadSet = false;

    private float previousValue;

    //used to determine if player completed the activity
    private bool activityComplete;
    public bool ActivityComplete
    {
        get{ return activityComplete; }
        set{ activityComplete = value; }
    }

    private void Start()
    {
        msr = momPanel.GetComponent<SpriteRenderer>();
        dsr = dadPanel.GetComponent<SpriteRenderer>();
        fsr = finalPanel.GetComponent<SpriteRenderer>();

        Reset();
    }

    public void OnValueChanged()
    {
        float changeAmount = divorceSlider.value - previousValue;
        if (divorceSlider.value < 0f && !momSet)
        {
            Color momOldColor = currentMatMom.color;
            Color momSquare = new Color(momOldColor.r, momOldColor.g, momOldColor.b, momOldColor.a + Mathf.Abs(changeAmount));

            currentMatMom.color = momSquare;

            msr.sortingOrder = dsr.sortingOrder + 1;

            if (divorceSlider.value <= -0.99f)
            {
                momSet = true;
                if(!dadSet)
                {
                    AudioManager.Instance.PlaySFX("correct");
                }
            }
        }
        if (divorceSlider.value > 0f && !dadSet)
        {
            Color dadOldColor = currentMatDad.color;
            Color dadSquare = new Color(dadOldColor.r, dadOldColor.g, dadOldColor.b, dadOldColor.a + changeAmount);

            currentMatDad.color = dadSquare;

            dsr.sortingOrder = msr.sortingOrder + 1;

            if (divorceSlider.value >= 0.99f)
            {
                dadSet = true;
                if(!momSet)
                {
                    AudioManager.Instance.PlaySFX("correct");
                }
            }
        }

        if(momSet && dadSet)
        {
            activityComplete = true;

            fsr.sortingOrder++;

            //hide slider when activity complete
            divorceSlider.gameObject.SetActive(false);

            AudioManager.Instance.PlaySFX("complete");
        }

        previousValue = divorceSlider.value;
    }

    //show all relevant gameobjects
    public void Show()
    {
        divorceSlider.gameObject.SetActive(true);
    }
    //hide all relevant gameobjects
    public void Hide()
    {
        divorceSlider.gameObject.SetActive(false);
    }
    //reset activity progress
    public void Reset()
    {
        activityComplete = false;

        currentMatMom = msr.material;
        currentMatDad = dsr.material;
        currentMatBoth = fsr.material;

        msr.color = currentMatMom.color;
        dsr.color = currentMatDad.color;
        fsr.color = currentMatBoth.color;

        currentMatMom.color = new Color(msr.color.r, msr.color.g, msr.color.b, 0);
        currentMatDad.color = new Color(dsr.color.r, dsr.color.g, dsr.color.b, 0);
    }
}
