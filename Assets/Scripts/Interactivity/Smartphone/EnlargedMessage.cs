using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles most of the functions involved with the expanded message which appears when a user clicks on a message within the phone screen
public class EnlargedMessage : MonoBehaviour
{
    private MessageInteractivity mi;
    private Message messageInfo;

    private GameObject collision;
    public GameObject redFlagObject;

    private bool colliding;
    public bool Colliding{
        get{
            return colliding;
        }
        set{
            colliding = value;
        }
    }

    void Start()
    {
        mi = GameObject.Find("Screen").GetComponent<MessageInteractivity>();
        messageInfo = mi.MessageInfo;

        redFlagObject = GameObject.Find("RedFlagObject");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        collision = other.gameObject;
        colliding = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        colliding = false;
    }
    void Update()
    {
        if(colliding)
        {
            if(Input.GetMouseButtonUp(0))
            {
                if(collision.CompareTag("RedFlag") && messageInfo != null)
                {
                    if(messageInfo.redFlag)
                    {
                        mi.EnableRedFlagIcon(true);
                    }

                    List<int> discoveredRedFlags = mi.DiscoveredRedFlags;
                    List<int> redFlags = mi.RedFlags;
                    
                    if(discoveredRedFlags.Count < redFlags.Count)
                    {
                        collision.transform.position = mi.RedFlagObjectPos;
                    }  
                    else
                    {
                        collision.SetActive(false);
                    }
                }        
            }
        }
    }
}
