using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlassTargeter : MonoBehaviour
{  
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float toleranceRadius;

    //if magnifying glass is within radius of target AND mouse is released  

    public bool onTarget()
    {
        bool toReturn = false;

        if(Vector3.Distance(transform.position, target.position) < toleranceRadius && !Input.GetMouseButton(0))
        {
            toReturn = true;
        }
        else
        {
            toReturn = false;
        }

        Debug.Log("onTarget: " + toReturn);
        return toReturn;
    }
}
