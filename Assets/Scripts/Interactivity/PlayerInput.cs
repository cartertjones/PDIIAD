using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public LayerMask layer;

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            var position = GetMousePositionInWorldCoordinates();
            if(position != null)
            {
                MoveToPosition(position.Value);
            }
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

    private void MoveToPosition(Vector3 position)
    {
        transform.position = position;
    }
}
