using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class handles the dev interface for message objects
public class Messages : MonoBehaviour
{

    [SerializeField]
    public Message[] messages;

    [SerializeField]
    private GameObject messagePrefab;
    [SerializeField]
    private GameObject parentObject;
    [SerializeField]
    private GameObject smartphone;

    [Header("Message Positions")]
    [SerializeField]
    private float SENT_X;
    [SerializeField]
    private float RECEIVED_X;

    [Header("Message Colors")]
    [SerializeField]
    private Color SENT_COLOR;
    [SerializeField]
    private Color RECEIVED_COLOR;

    [Header("Message Heights")]
    [SerializeField]
    private float maxMessageHeight;
    [SerializeField]
    private float minMessageHeight;

    private float boundsX, boundsY;
    private SpriteRenderer sr;


    //message attributes
    private Color messageColor;
    private float messageHeight;
    private float messageX;


    private float messageY = 0;
    private float prevMessageYSize = 0;
    private float messageWidth = 2f;
    
    [SerializeField]
    private float messageMargins;



    private void Awake()
    {
        //foreach item in list
        //  instantiate new message prefab as child of message group
        //  set color and x position based on if sent or received
        //  set y height to random value
        sr = messagePrefab.GetComponent<SpriteRenderer>();

        //set visible inside smartphone (spritemask)
        //sr.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        SENT_X += parentObject.transform.position.x;
        RECEIVED_X += parentObject.transform.position.x;


        //set first message y to a margin below top of phone screen
        SpriteRenderer smartphonesr = smartphone.GetComponent<SpriteRenderer>();
        messageY = (smartphonesr.bounds.size.y / 2 + smartphone.transform.position.y) - messageMargins;

        //used for naming gameobjects
        int messageCount = 0;

        foreach(Message m in messages)
        {
            messageHeight = Random.Range(minMessageHeight, maxMessageHeight);

            if(m.sent)
            {
                messageColor = SENT_COLOR;
                messageX = SENT_X;
            }
            else
            {
                messageColor = RECEIVED_COLOR;
                messageX = RECEIVED_X;
            }

            //attach values to prefab
            messagePrefab.transform.localScale = new Vector3(messageWidth, messageHeight, messagePrefab.transform.localScale.z);
            sr.color = messageColor;

            //adjust y position
            messageY = messageY - (sr.bounds.size.y / 2) - (prevMessageYSize / 2) - messageMargins;

            GameObject message = Instantiate(messagePrefab, new Vector3(messageX, messageY, 0), Quaternion.identity);
            message.transform.SetParent(parentObject.transform);

            message.name = "Element " + messageCount;

            prevMessageYSize = sr.bounds.size.y;
            messageCount++;
        }
    }

    public Color getSentColor()
    {
        return SENT_COLOR;
    }
    public Color getReceivedColor()
    {
        return RECEIVED_COLOR;
    }
}
