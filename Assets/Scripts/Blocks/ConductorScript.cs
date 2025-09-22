using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductorScript : MonoBehaviour
{
    public Transform energy;
    public float rot_speed = 60f;

    void FixedUpdate()
    {
        transform.Rotate(0, 0, rot_speed*Time.deltaTime);
        if (energy != null) energy.Rotate(0, 0, -100*Time.deltaTime);
    }
}
