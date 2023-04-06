using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingDan : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] private bool isActivated;
    private bool isMovingBack;
    private Animator anim;
    private Vector3 newDanPosition;
    // Start is called before the first frame update
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
        if (isActivated && (gameObject.transform.position.x > 490))
        {
            isMovingBack = true;
            anim.SetBool("MovingBack", isMovingBack);
        }
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
}
