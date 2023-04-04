using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this class just sets the red flag object back to its position in the event it is dragged out
public class RedFlag : MonoBehaviour
{
    MessageInteractivity mi;

    void Start()
    {
        mi = GameObject.Find("Screen").GetComponent<MessageInteractivity>();
    }

    void Update()
    {
            if(Input.GetMouseButtonUp(0) && transform.position != mi.RedFlagObjectPos)
            {
                transform.position = mi.RedFlagObjectPos;
            }
    }
}
