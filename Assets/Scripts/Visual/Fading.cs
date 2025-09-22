using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour
{
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        sr.color -= new Color(0, 0, 0, 3f*Time.deltaTime);
    }
}
