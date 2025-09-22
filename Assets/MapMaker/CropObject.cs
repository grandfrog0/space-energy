using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropObject : MonoBehaviour
{
    public Camera cam;
    Vector3 tapPos;

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            tapPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            transform.position = new Vector3(Mathf.RoundToInt(tapPos.x/1.4f) * 1.4f, Mathf.RoundToInt(tapPos.y/1.4f) * 1.4f, 0);
        }
    }

    void Awake()
    {
        cam = Camera.main;
    }
}
