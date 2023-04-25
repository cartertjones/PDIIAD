using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingDan : MonoBehaviour
{
    [SerializeField] Slider slider;
    public ProgressTracker progressTracker;
    [SerializeField] private bool isActivated;
    [SerializeField] private AnimationCurve alphaCurve;

    private bool isMovingBack;
    private Animator anim;
    private Vector3 newDanPosition;

    public float fadeDuration = 3f; // Fade duration in seconds
    private float elapsedTime; // Time elapsed since fading started
    
    private float startTime;
    private bool hasFadedIn = false;
    private float alphaValue;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (isActivated && (gameObject.transform.position.x < 5))
        {
            isMovingBack = false;
            anim.SetBool("MovingBack", isMovingBack);
        }
        if (isActivated && !progressTracker.movingForward/*(gameObject.transform.position.x > 490)*/)
        {
            isMovingBack = true;
            anim.SetBool("MovingBack", isMovingBack);
        }
        if (isActivated && !hasFadedIn)
        {
            FadeObject();
            startTime = Time.time;
        }
        /*if (alphaValue == 255f)
        {
            hasFadedIn = true;
        }*/
    }

    public void onSliderChanged(float sliderValue)
    {

        MoveDan(sliderValue);
    }

    void MoveDan(float value)
    {
        if (isActivated)
        {
            newDanPosition.x = value;
            gameObject.transform.position = newDanPosition;
        }

    }
    public void ActivateDan()
    {
        isActivated = true;
    }

    void FadeObject()
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / fadeDuration);
        float curveNum = Mathf.Lerp(0f, 3f, t);
        alphaValue = alphaCurve.Evaluate(curveNum);
        GetComponent<SpriteRenderer>().color = new Color (255, 255, 255, alphaValue); // Apply the new color to the gameObject
    }
}
