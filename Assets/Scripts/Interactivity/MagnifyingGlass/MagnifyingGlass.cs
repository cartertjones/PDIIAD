using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlass : MonoBehaviour
{
    private MagnifyingGlassTargeter mgt;

    private GameObject bigSheet;
    private Lens lens;

    private bool activityComplete;
    public bool ActivityComplete{
        get{
            return activityComplete;
        }
        set{
            activityComplete = value;
        }
    }

    void Start()
    {
        lens = GameObject.Find("MagnifyingGlass/Lens").GetComponent<Lens>();
        bigSheet = lens.BigSheet.gameObject;
        bigSheet.SetActive(false);

        activityComplete = false;
        
        gameObject.SetActive(false);
        mgt = GetComponent<MagnifyingGlassTargeter>();
    }

    void Update()
    {
        if(activityComplete == false)
        {
            activityComplete = mgt.onTarget();
            if(activityComplete)
            {
                Debug.Log("Target found");
            }
        }
    }

    public void Show()
    {
        bigSheet.SetActive(true);
        gameObject.SetActive(true);
    }
}
