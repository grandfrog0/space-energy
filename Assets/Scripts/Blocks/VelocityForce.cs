using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityForce : MonoBehaviour
{
    public Vector2 Force;
    public float Gravity;
    public Rigidbody2D rb;
    public AudioSource swipe;
    public Pursuer_Script pursuer;
    
    public bool modifier1_used;
    public float modifier1_speed_multiplier;

    public bool outControl;

    public void SetOutControl(bool val)
    {
        outControl = val;
        if (outControl)
        {
            ResetVelocity();
        }
        else
        {
            Impulse(-Force);
        }
    }

    public void OutControl_Impulse(Vector3 to, float val)
    {
        if (rb.bodyType != RigidbodyType2D.Static && outControl) rb.velocity = to.normalized*val; //apply force
    }

    public void Impulse(Vector2 to)
    {
        if (GetComponent<P_Move>()) //if this == player
        {
            GetComponent<P_Move>().TouchVector = to;
            GetComponent<P_Collision>().par = null;
            if (pursuer != null)
            {
                pursuer.points.Add(transform.position);
            }
        } 
        if (swipe != null) //make sound
        {
            swipe.pitch = Random.Range(0.9f, 1.2f);
            swipe.Play();
        }

        SetAngle(to); //make angle for gameObject
        ResetVelocity(); //reset velocity
        Force = transform.up; //learn vector

        modifier1_used = Modifiers.GetMod(1); //random_speed modifier
        if (modifier1_used && rb.bodyType != RigidbodyType2D.Static)
        {
            modifier1_speed_multiplier = Random.Range(0.3f, 2f);
            rb.velocity = transform.up*Gravity*modifier1_speed_multiplier;
        }
        else if (rb.bodyType != RigidbodyType2D.Static && !outControl) rb.velocity = transform.up*Gravity; //apply force
        
        if (GetComponent<Anvil_Script>()) GetComponent<Anvil_Script>().Block = false;  //if anvil
    }

    public void SetAngle(Vector2 to)
    {
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

    public void ResetVelocity()
    {
        if (rb.bodyType != RigidbodyType2D.Static) rb.velocity = Vector2.zero;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
