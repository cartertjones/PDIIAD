using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Message
{
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

    public string Content
    {
        get{return content;}
        set{
            this.content = value;
            var eh = ValueChanged; // avoid race condition.
            if (eh != null)
                eh(this, EventArgs.Empty);
        }
    }
    public bool RedFlag
    {
        get{return redFlag;}
        set{
            this.redFlag = value;
            var eh = ValueChanged; // avoid race condition.
            if (eh != null)
                eh(this, EventArgs.Empty);
        }
    }
    public bool Sent
    {
        get{return sent;}
        set{
            this.sent = value;
            var eh = ValueChanged; // avoid race condition.
            if (eh != null)
                eh(this, EventArgs.Empty);
        }
    }
    private Vector3 localPos;
    private Vector3 size;
    private string content;
    private bool redFlag, sent;

    public event EventHandler ValueChanged;

    public Message(Vector3 localPos, Vector3 size, string content, bool sent, bool redFlag)
    {
        this.localPos = localPos;
        this.size = size;

        this.content = content;

        this.sent = sent;
        this.redFlag = redFlag;
        

    }
}
