using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Can_Stay"))
        {
            if (coll.gameObject.GetComponent<ObjectTags>())
            {
                ObjectTags tags = coll.gameObject.GetComponent<ObjectTags>();
                if (tags.tags.Contains("snake_part"))
                {
                    if (coll.gameObject.GetComponent<SnakePart_Script>())
                    {
                        coll.gameObject.GetComponent<SnakePart_Script>().par.UpdateTargets();
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
