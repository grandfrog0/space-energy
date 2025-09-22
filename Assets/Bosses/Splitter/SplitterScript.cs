using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterScript : MonoBehaviour
{
    public Animator anim;
    public string state;

    public Vector3 ToPosition;
    public int axis;

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            ToPosition = new Vector3(-5, -2);
            if (state != "FlyLeft_Between")
            {
                Animate("FlyLeft_Between");
            }
        }
        transform.position = Vector3.Lerp(transform.position, ToPosition, 0.01f);
        if (Vector3.Distance(transform.position, ToPosition) < 0.5f)
        {
            if (state != "Stay")
            {
                if (axis == -1)
                {
                    Animate("FlyLeft_ToStay");
                    axis = 0;
                }
                else if (axis == 1)
                {
                    Animate("FlyRight_ToStay");
                    axis = 0;
                }
                else if (axis == 0 && state != "FlyLeft_ToStay" && state != "FlyRight_Between")
                {
                    Animate("Stay");
                }
            }
        }
        else if (ToPosition.x >= transform.position.x)
        {
            if (state != "FlyRight_Between")
            {
                Animate("FlyRight_Between");
                axis = 1;
            }
        }
        else
        {
            if (state != "FlyLeft_Between")
            {
                Animate("FlyLeft_Between");
                axis = -1;
            }
        }
    }

    public void Animate(string name)
    {
        state = name;
        anim.Play(name, 0);
    }

    void Awake()
    {
        anim.enabled = true;
    }
}
