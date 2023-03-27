using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlass : MonoBehaviour
{
    private MagnifyingGlassTargeter mgt;

    void Start()
    {
        mgt = GetComponent<MagnifyingGlassTargeter>();
    }

    void Update()
    {
        mgt.onTarget();
    }
}
