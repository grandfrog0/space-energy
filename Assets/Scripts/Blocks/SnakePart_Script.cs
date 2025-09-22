using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePart_Script : MonoBehaviour
{
    public SpriteRenderer sr;
    public Transform gm;
    public Sprite head, tail;
    public SnakeBlock_Script par;
    public float x, y;
    public GameObject spike;

    public void SetPart(string i)
    {
        if (i == "head")
        {
            sr.sprite = head;
            spike.SetActive(true);
        }
        else if (i == "tail")
        {
            sr.sprite = tail;
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y), 5*Time.deltaTime);
    }
}
