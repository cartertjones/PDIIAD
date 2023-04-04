using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//this class manages the message object.
[System.Serializable]
public class Message
{
    [Header("Message Information")]
    public bool redFlag;
    public bool sent;
    public string content;
    
    //set by messages script
    private Vector3 localPos;
    private Vector3 size;

    public Vector3 LocalPos
    {
        get{return localPos;}
        set{
            this.localPos = (Vector3)value;
            var eh = ValueChanged; // avoid race condition.
            if (eh != null)
                eh(this, EventArgs.Empty);
        }
    }
    public Vector3 Size
    {
        get{return size;}
        set{
            this.size = (Vector3)value;
            var eh = ValueChanged; // avoid race condition.
            if (eh != null)
                eh(this, EventArgs.Empty);
        }
    }

    public event EventHandler ValueChanged;
}
