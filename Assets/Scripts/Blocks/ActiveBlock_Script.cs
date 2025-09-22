using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBlock_Script : MonoBehaviour
{
    Animator anim;
    public bool solid;
    public AudioSource au;

    public Transform Mask;

    void FixedUpdate()
    {
        Mask.Rotate(0, 0, 300*Time.deltaTime);
        Mask.localPosition = Mask.up;
    }

    void OnEnable()
    {
        if (solid)
        {
            anim.Play("EnableSolid");
        }
    }

    public void DisableSolid()
    {
        solid = false;
        anim.Play("DisableSolid", 0);
    }

    public void EnableSolid()
    {
        solid = true;
        anim.Play("EnableSolid", 0);
        au.Play();
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        DisableSolid();
    }
}
