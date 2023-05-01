using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Vector2 difference = Vector2.zero;

    private bool mouseDown;
    private bool locked;

    private void OnMouseDown()
    {
        mouseDown = false;
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        AudioManager.Instance.PlaySFX("click");

        if(transform.parent.name == "MagnifyingGlass")
        {
            MagnifyingGlass mg = transform.GetComponent<MagnifyingGlass>();
            locked = mg.InstructionsActive;
        }
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

    private void OnMouseUp()
    {
        mouseDown = false;
    }

    public bool GetMouseDown()
    {
        return mouseDown;
    }
}
