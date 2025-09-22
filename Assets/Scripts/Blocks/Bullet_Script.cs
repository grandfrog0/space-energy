using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.layer == LayerMask.NameToLayer("Can_Stay") || coll.gameObject.layer == LayerMask.NameToLayer("Protect_Zone") || DangerTags.tags.Contains(coll.gameObject.tag))
        {
            Destroyy();
        }
    }
    void Destroyy()
    {
        Destroy(gameObject);
    }
}
