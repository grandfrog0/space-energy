using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_Move : MonoBehaviour
{
    public Vector2 axis;
    public float multiplier = 1f;
    public float x_r, y_r;
    float xr, yr;
    public EnableRandomObject rnd;

    void Awake()
    {
        xr = Random.Range(-x_r, x_r);
        yr = Random.Range(-y_r, y_r);
    }

    void FixedUpdate()
    {
        transform.Translate((axis.x + xr)*Time.deltaTime*multiplier, (axis.y + yr)*Time.deltaTime, 0);
        if (transform.position.x < -30)
        {
            transform.position = new Vector3(30, transform.position.y, 0);
            rnd.On();
        }
        if (transform.position.x > 30)
        {
            transform.position = new Vector3(-30, transform.position.y, 0);
            rnd.On();
        }
        if (transform.position.y > 15)
        {
            transform.position = new Vector3(transform.position.x, -15, 0);
            rnd.On();
        }
        if (transform.position.y < -15)
        {
            transform.position = new Vector3(transform.position.x, 15, 0);
            rnd.On();
        }
    }
}
