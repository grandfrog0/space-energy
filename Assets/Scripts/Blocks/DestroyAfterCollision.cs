using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterCollision : MonoBehaviour
{
    public bool destroy;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (destroy) Destroy(gameObject);
        else gameObject.SetActive(false);
    }
}
