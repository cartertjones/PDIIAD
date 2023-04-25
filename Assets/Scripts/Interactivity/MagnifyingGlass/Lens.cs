using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lens : MonoBehaviour
{
    [SerializeField]
    private Transform smallSheet, bigSheet;
    public Transform BigSheet{
        get{return bigSheet;}
        set{bigSheet = value;}
    }
    
    private void Update() {
        bigSheet.position = smallSheet.position * 2 - transform.position;
    }
}
