using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer_Script : MonoBehaviour
{
    public List<Vector3> points;
    public int currentPoint;
    Vector3 to;
    public float speed;
    Rigidbody2D rb;

    void FixedUpdate()
    {
        if (points.Count > 0 && currentPoint < points.Count)
        {
            if (Vector3.Distance(transform.position, points[currentPoint]) < 0.5f)
            {
                currentPoint++;
            }
            else to = points[currentPoint] - transform.position;
            rb.velocity = transform.up*speed;

            if (Mathf.Abs(to.x) >= Mathf.Abs(to.y))
            {
                if (to.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                if (to.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 270);
                }
            }
            else
            {
                if (to.y > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                }
                if (to.y < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
        else rb.velocity = Vector2.zero;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
