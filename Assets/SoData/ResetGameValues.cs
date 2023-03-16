using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGameValues : MonoBehaviour
{
    [SerializeField]
    private FloatSO frameSO;

    private void Start()
    {
        frameSO.Value = 0;
    }
}
