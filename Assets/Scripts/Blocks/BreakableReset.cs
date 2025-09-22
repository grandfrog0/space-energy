using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableReset : MonoBehaviour
{
    SpriteRenderer sr;
    public bool canR;
    public GameObject block;
    public Breakable_Block_Script bs;
    AudioSource au;

    public void Upd()
    {
        canR = true;
        sr.color = new Color(1, 1, 1, 0.3f);
    }

    void FixedUpdate()
    {
        if (transform.position != block.transform.position)
        {
            transform.position = block.transform.position;
        }
    }

    void OnMouseEnter()
    {
        sr.color = new Color(1, 1, 1, 1);
    }

    void OnMouseExit()
    {
        sr.color = new Color(1, 1, 1, 0.3f);
    }

    void OnMouseDown()
    {
        if (canR)
        {
            bs.Build();
            canR = false;
            au.Play();
        }
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.clear;
        canR = false;
        au = GetComponent<AudioSource>();
    }
}
