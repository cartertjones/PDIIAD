using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class MessageInteractivity : MonoBehaviour
{
    /*
    if mouse clicked
    get mouse world position

    for each child object of messages, see if sprite contains mouse position
        if so, get attached message from index given in object name
        break loop

    instantiate zoomed message prefab, set values given from found message
        color
        text
        redflag
   */     
    public LayerMask layer;
    private Messages m;

    [SerializeField]
    private GameObject enlargedMessage;
    [SerializeField]
    private float enlargedX, enlargedY;

    [SerializeField]
    private GameObject screen;

    [SerializeField]
    private float scrollSpeed;

    void Awake()
    {
        m = GetComponent<Messages>();
    }

    void Update()
    {
        //left click
        if(Input.GetMouseButtonDown(0))
        {
            var pos = GetMousePositionInWorldCoordinates();
            if(pos != null)
            {
                Vector3 mousePosition = pos.Value;
                Debug.Log(mousePosition);

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
                    //do stuff with the message pulled
                    Debug.Log(messageClicked.name + " clicked");

                    var result = Regex.Match(messageClicked.name, @"\d+$").Value;
                    Message messageInfo = m.messages[Convert.ToInt32(result)];

                    //instantiate enlarged prefab with information, could be subbed out for a switch with specific images
                    SpriteRenderer sr = enlargedMessage.GetComponent<SpriteRenderer>();
                    Text text = enlargedMessage.GetComponent<Text>();
                    if(messageInfo.sent)
                    {
                        sr.color = m.getSentColor();
                    }
                    else
                    {
                        sr.color = m.getReceivedColor();
                    }

                    text.text = messageInfo.content;
                    
                    GameObject em = Instantiate(enlargedMessage, new Vector3(enlargedX, enlargedY, 0), Quaternion.identity);
                }   
            }  
        }

        //scroll
        else if(Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            float y = transform.position.y + (Input.GetAxisRaw("Mouse ScrollWheel") * scrollSpeed);
            if(y < 0) {y = 0;}
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
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
}
