using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Default_Script : MonoBehaviour
{
    public Vector3 start;
    Rigidbody2D rb;

    void FixedUpdate()
    {
        if (transform.position != start)
        {
            transform.position = Vector3.Lerp(transform.position, start, 0.05f);
            rb.velocity = Vector2.zero;
            if (Vector3.Distance(transform.position, start) < 0.05f)
            {
                transform.position = start;
            }
        }
    }

    void Start()
    {
        start = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }
}
