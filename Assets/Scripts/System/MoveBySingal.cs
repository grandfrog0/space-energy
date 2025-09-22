using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBySingal : MonoBehaviour
{
    public Vector3 end;
    bool started;
    public float speed = 3f;

    public void Go()
    {
        started = true;
    }

    void FixedUpdate()
    {
        if (started) transform.position = Vector3.Lerp(transform.position, end, speed*Time.deltaTime);
    }
}
