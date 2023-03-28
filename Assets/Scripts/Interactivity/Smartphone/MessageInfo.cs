using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageInfo : MonoBehaviour
{
    [Header("Message Information")]
    [SerializeField]
    private bool sent;
    [SerializeField]
    private bool redFlag;
    [SerializeField]
    private string content;

    [Header("Message Positions (DO NOT EDIT OUTSIDE OF PREFAB)")]
    [SerializeField]
    private float SENT_X;
    [SerializeField]
    private float RECEIVED_X;

    [Header("Message Colors (DO NOT EDIT OUTSIDE OF PREFAB)")]
    [SerializeField]
    private Color SENT_COLOR;
    [SerializeField]
    private Color RECEIVED_COLOR;

    [Header("Message Heights (DO NOT EDIT OUTSIDE OF PREFAB)")]
    [SerializeField]
    private float maxMessageHeight;
    [SerializeField]
    private float minMessageHeight;


    private float boundsX, boundsY;
    
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        RECEIVED_X += transform.parent.transform.position.x;
        SENT_X += transform.parent.transform.position.x;

        //set color and local x based on if sent or received
        if(sent)
        {
            sr.color = SENT_COLOR;
            transform.position = new Vector3(SENT_X, transform.position.y, transform.position.z);
        }
        else
        {
            sr.color = RECEIVED_COLOR;
            transform.position = new Vector3(RECEIVED_X, transform.position.y, transform.position.z);
        }

        //set message to random size within range (aesthetic purpose)
        float randomSizeY = Random.Range(minMessageHeight, maxMessageHeight);
        transform.localScale = new Vector3 (transform.localScale.x, randomSizeY, transform.localScale.z);
    }

    private void Update()
    {
        if(sent)
        {
            sr.color = SENT_COLOR;
            transform.position = new Vector3(SENT_X, transform.position.y, transform.position.z);
        }
        else
        {
            sr.color = RECEIVED_COLOR;
            transform.position = new Vector3(RECEIVED_X, transform.position.y, transform.position.z);
        }
    }

    public Vector3 getSize()
    {
        Vector3 size = sr.bounds.size;
        return size;
    }

    public string getContent()
    {
        return content;
    }

    public bool isSent()
    {
        return sent;
    }

    public bool isRedFlag()
    {
        return redFlag;
    }
}
