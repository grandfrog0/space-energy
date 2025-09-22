using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Destroy : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > 30)
        {
            Destroy(gameObject);
        }
    }
}
