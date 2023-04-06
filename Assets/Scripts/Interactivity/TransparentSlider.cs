using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class TransparentSlider : MonoBehaviour
{
    //[SerializeField] private UnityEngine.UI.Slider slider;
    [SerializeField] private GameObject dadSquare;
    [SerializeField] private GameObject momSquare;
    private Material currentMatDad;
    private Material currentMatMom;

    private bool momSet = false;
    private bool dadSet = false;

    // Start is called before the first frame update
    void Start()
    {
        currentMatDad = dadSquare.GetComponent<Renderer>().material;
        currentMatMom = momSquare.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnSliderChanged(float alphaValue)
    {
       if (alphaValue < 0f && !momSet)
        {
            Color momOldColor = currentMatMom.color;
            Color momSquare = new Color(momOldColor.r, momOldColor.g, momOldColor.b, (alphaValue ) +1.1f);
            Debug.Log("Slider" + (alphaValue));
            Debug.Log("Alpha" + (alphaValue + 1f));
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
            Debug.Log("" + (alphaValue));
            currentMatDad.color = dadSquare;
            if (alphaValue >= 1f)
            {
                dadSet = true;
            }
        }
    }
    public void ChangeAlpha(UnityEngine.UI.Slider slider)
    {
        OnSliderChanged(slider.value);
    }
}
