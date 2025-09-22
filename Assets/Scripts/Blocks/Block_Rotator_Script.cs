using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Rotator_Script : MonoBehaviour
{
    public Transform spinner;
    public float spinspeed;
    public float RotateSpeed;
    public bool needRotate;
    public float needAngle;

    void FixedUpdate()
    {
        spinner.Rotate(0, 0, spinspeed*Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, needAngle), 0.04f);
        if (Mathf.Abs(transform.rotation.z - needAngle) < 25) 
        {
            transform.rotation = Quaternion.Euler(0, 0, needAngle);
        }
    }

    void Start()
    {
        InvokeRepeating("Rotate", RotateSpeed, RotateSpeed);
    }

    void Rotate()
    {
        needAngle += 90;
    }
}