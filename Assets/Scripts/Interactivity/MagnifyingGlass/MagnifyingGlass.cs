using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlass : MonoBehaviour
{
    private MagnifyingGlassTargeter mgt;

    private GameObject bigSheet;
    private Lens lens;

    [SerializeField] private Sprite finalImage;

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
                AudioManager.Instance.PlaySFX("complete");

                SpriteMask lsm = lens.GetComponent<SpriteMask>();
                Destroy(lsm);

                SpriteRenderer lsr = lens.GetComponent<SpriteRenderer>();
                lsr.sprite = finalImage;
                lsr.color = new Color(lsr.color.r, lsr.color.g, lsr.color.b, 1);

                Drag ld = lens.GetComponent<Drag>();
                Destroy(ld);

                lens.transform.localScale = new Vector3(0.04f, 0.04f, 1);
            }
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
        bigSheet.SetActive(true);
    }
}
