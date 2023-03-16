using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class contains all transitions panels can utilize */
public class PanelTransitions
{
    //increase scale on z axis each frame until target scale reached
    public float zoomIn(float zScale, float targetScale, int speed)
    {
        if(zScale != targetScale)
        {
            float retVal = zScale;
            retVal += targetScale * (1 / speed);
            return retVal;
        }
        else
        {
            return zScale;
        }
    }
    //decrease scale on z axis each frame until target scale reached
    public float zoomOut(float zScale, float targetScale, int speed)
    {
        if(zScale != targetScale)
        {
            float retVal = zScale;
            retVal -= targetScale * (1 / speed);
            return retVal;
        }
        else
        {
            return zScale;
        }
    }

    //increase alpha value each frame until max opacity reached
    public float fadeIn(float opacity, int speed)
    {
        if(opacity != 255f)
        {
            float retVal = opacity;
            retVal += 1 * speed;
            return retVal;
        }
        else
        {
            return opacity;
        }
    }
    //decrease alpha value each frame until minimum opacity reached
    public float fadeOut(float opacity, int speed)
    {
        if(opacity != 0f)
        {
            float retVal = opacity;
            retVal -= 1 * speed;
            return retVal;
        }
        else
        {
            return opacity;
        }
    }

    //TODO slider transitions
}
