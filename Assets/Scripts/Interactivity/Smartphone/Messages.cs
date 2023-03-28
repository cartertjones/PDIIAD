using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    // Start is called before the first frame update

    private int numChildren;
    List<Message> messages;

    void Start()
    {
        //instantiate message object list
        messages = new List<Message>();

        //for each child get transform and spriterenderer size
        foreach(Transform child in transform)
        {
            //get info of child object
            MessageInfo mi = child.GetComponent<MessageInfo>();

            //set temporary values to info values from child
            Vector3 localPos = child.transform.position;

            Vector3 size = mi.getSize();

            bool sent = mi.isSent();
            bool redFlag = mi.isRedFlag();

            string content = mi.getContent();

            //create new message object and add to data list
            Message tempMessage = new Message(localPos, size, content, sent, redFlag);
            messages.Add(tempMessage);
        }


        //if message flagged & can be flagged, instantiate red flag object as child of message, delete flag that was dragged
        //else return flag to hotbar
    }

    void Update()
    {
        //if mouse within bounds of a message & mouse clicked, pull that message

        //if mouse scrolled, change y position of message group
    }
}
