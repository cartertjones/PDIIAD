using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlass : MonoBehaviour
{
    private MagnifyingGlassTargeter mgt;

    [SerializeField] private GameObject bigSheet;
    private Lens lens;

    [SerializeField] private Sprite finalImage;

    private SlideCam slideCam;

    private bool activityComplete;
    public bool ActivityComplete{
        get{
            return activityComplete;
        }
        set{
            activityComplete = value;
        }
    }

    [SerializeField] private GameObject instructions;
    private bool instructionsActive;
    public bool InstructionsActive{
        get{return instructionsActive;}
        set{instructionsActive = value;}
    }
    private bool activityActive;
    public bool ActivityActive{
        get{return activityActive;}
        set{activityActive = value;}
    }
    void Start()
    {
        lens = GameObject.Find("MagnifyingGlass/Lens").GetComponent<Lens>();
        bigSheet.SetActive(false);

        activityComplete = false;
        
        gameObject.SetActive(false);
        mgt = GetComponent<MagnifyingGlassTargeter>();

        slideCam = GameObject.Find("Main Camera").GetComponent<SlideCam>();

        instructionsActive = false;
        instructions.SetActive(false);

        activityActive = false;
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

        StartCoroutine(ShowInstructions());
    }

    private IEnumerator ShowInstructions()
    {
        yield return new WaitForSeconds(2);
        if(slideCam.onAPanel && !slideCam.OnInteractivePanel)
        {
            yield break;
        }
        else
        {
            instructions.SetActive(true);
            instructionsActive = true;
            activityActive = true;
        }
    }

    public void HideInstructions()
    {
        instructions.SetActive(false);
        Debug.Log("hiding instructions");
        instructionsActive = false;
    }
}
