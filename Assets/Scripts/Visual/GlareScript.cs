using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlareScript : MonoBehaviour
{
    RectTransform rect_transform;

    void Start()
    {
        rect_transform = GetComponent<RectTransform>();
        Invoke("Glare", Random.Range(5, 45));
    }

    void FixedUpdate()
    {
        rect_transform.Translate(Vector2.right*10000*Time.deltaTime, 0);
    }

    void Glare()
    {
        Invoke("Glare", Random.Range(5, 45));
        rect_transform.anchoredPosition = Vector2.right * -1100;
    }
}
