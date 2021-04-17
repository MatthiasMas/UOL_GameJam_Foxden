using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRotation : MonoBehaviour
{
    private Quaternion rotation;
    
    void Start()
    {
        this.rotation = this.transform.rotation;
    }

    void LateUpdate()
    {
        this.transform.rotation = this.rotation;
    }
}
