using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;
using TMPro;

//This class is the "driver" class for the message interactivity section. it works in tandem with and implements methods from Messages.cs, Message.cs, EnlargedMessage.cs, and RedFlag.cs
public class MessageInteractivity : MonoBehaviour
{    
    public LayerMask layer;
    private Messages m;

    [SerializeField]
    private GameObject enlargedMessage;
    private GameObject enlargedText;
    private GameObject enlargedImage;
    private GameObject enlargedRedFlag;

    [SerializeField] private GameObject activityActivator;
    private float enlargedX, enlargedY;

    [SerializeField]
    private GameObject screen;

    [SerializeField]
    private GameObject smartphone;

    [SerializeField]
    private float scrollSpeed;

    private Message messageInfo;
    public Message MessageInfo{
        get{
            return messageInfo;
        }
        set{
            messageInfo = value;
        }
    }

    private int activeIndex;

    private List<int> redFlags;
    public List<int> RedFlags{
        get{
            return redFlags;
        }
        set{
            redFlags = value;
        }
    }
    private List<int> discoveredRedFlags;
    public List<int> DiscoveredRedFlags{
        get{
            return discoveredRedFlags;
        }
        set
        {
            discoveredRedFlags = value;
        }
    }

    private GameObject em = null;

    private GameObject redFlagObject;
    private Vector3 redFlagObjectPos;
    public Vector3 RedFlagObjectPos{
        get{
            return redFlagObjectPos;
        }
        set{
            redFlagObjectPos = value;
        }
    }

    private bool activityComplete;
    public bool ActivityComplete{
        get{
            return activityComplete;
        }
        set{
            activityComplete = value;
        }
    }

    [SerializeField] private CameraManager camMan;
    [SerializeField] private ProgressTracker progressTracker;

    void Awake()
    {
        activityComplete = false;

        m = GetComponent<Messages>();
        redFlags = new List<int>();
        discoveredRedFlags = new List<int>();

        redFlagObject = GameObject.Find("RedFlagObject");
        redFlagObjectPos = redFlagObject.transform.position;

        //note this requires gameobject children at hardcoded indices
        enlargedText = enlargedMessage.transform.GetChild(0).gameObject;
        enlargedImage = enlargedMessage.transform.GetChild(1).gameObject;
        enlargedRedFlag = enlargedMessage.transform.GetChild(2).gameObject;

        //hardcoded im sorry
        enlargedX = this.gameObject.transform.position.x + -25;
        enlargedY = this.gameObject.transform.position.y + 3;

        enlargedRedFlag.SetActive(false);

        //get index of all red flag messages and add them to a list
        int index = -1;
        foreach(Message message in m.messages)
        {
            index++;
            if(message.redFlag)
            {
                redFlags.Add(index);
            }
        }
    }

    void Update()
    {
        enlargedRedFlag.SetActive(false);

        if(activityComplete || !progressTracker.interactivityActive || !progressTracker.movingForward)
        {
            activityActivator.SetActive(false);
        }
        else if(!activityComplete && progressTracker.interactivityActive)
        {
            activityActivator.SetActive(true);
        }

        //left click
        if(Input.GetMouseButtonDown(0))
        {
            var pos = GetMousePositionInWorldCoordinates();
            if(pos != null)
            {
                Vector3 mousePosition = pos.Value;

                Transform messageClicked = null;

                foreach(Transform message in screen.transform)
                {
                    SpriteRenderer sr = message.GetComponent<SpriteRenderer>();

                    bool isInBounds = sr.bounds.Contains(mousePosition);
                    if(isInBounds)
                    {
                        messageClicked = message;
                        break;
                    }
                }

                if(messageClicked != null)
                {
                    //delete old message
                    if(em != null)
                    {
                        Destroy(em);
                    }

                    //do stuff with the message pulled
                    var result = Regex.Match(messageClicked.name, @"\d+$").Value;
                    activeIndex = Convert.ToInt32(result);
                    messageInfo = m.messages[activeIndex];

                    //instantiate enlarged prefab with information, could be subbed out for a switch with specific images
                    SpriteRenderer sr = enlargedImage.GetComponent<SpriteRenderer>();
                    TextMeshPro text = enlargedText.GetComponent<TextMeshPro>();

                    text.fontSize = 2;

                    if(messageInfo.sent)
                    {
                        sr.color = m.getSentColor();
                    }
                    else
                    {
                        sr.color = m.getReceivedColor();
                    }

                    text.text = messageInfo.content;
                    
                    //if this red flag has been discovered previously, enable red flag icon
                    foreach(int value in discoveredRedFlags)
                    {
                        if(activeIndex == value)
                        {
                            enlargedRedFlag.SetActive(true);
                        }
                    }
                    

                    em = Instantiate(enlargedMessage, new Vector3(enlargedX, enlargedY, 0), Quaternion.identity);
                    
                    em.transform.localScale = new Vector3(7,7,0);
                }   
            }  
        }

        //scroll
        else if(Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            float y = transform.position.y + (Input.GetAxisRaw("Mouse ScrollWheel") * scrollSpeed);
            if(y < smartphone.transform.position.y) {y = smartphone.transform.position.y;}

            this.transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }


        //check if all red flags found, if so print
        if(discoveredRedFlags.Count == redFlags.Count && !activityComplete)
        {
            activityComplete = true;

            AudioManager.Instance.PlaySFX("complete");

            camMan.ReturnToBreakupPage();
        }
    }

    private Vector3? GetMousePositionInWorldCoordinates()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = new RaycastHit2D[1];
        if(Physics2D.GetRayIntersectionNonAlloc(ray, hits) > 0 && layer.value != (1 << hits[0].collider.gameObject.layer))
        {
            return hits[0].point;
        }
        return null;
    }

    public void EnableRedFlagIcon(bool value)
    {
        em.transform.GetChild(2).gameObject.SetActive(value);
        discoveredRedFlags.Add(activeIndex);
    }

     private bool IsBetween(double testValue, double bound1, double bound2)
    {
        if (bound1 > bound2)
            return testValue >= bound2 && testValue <= bound1;
        return testValue >= bound1 && testValue <= bound2;
    }
}
