using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Default_Animation : MonoBehaviour
{
    public GameObject center, heart;
    public float speed;

    void FixedUpdate()
    {
        center.transform.Rotate(0, 0, 250f*Time.deltaTime*speed);
        heart.transform.Rotate(0, 0, 125f*Time.deltaTime*speed);
    }
}
