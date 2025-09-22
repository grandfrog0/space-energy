using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Sound_Script : MonoBehaviour
{
    public AudioSource bump;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            bump.Play();
        } 
    }
}
