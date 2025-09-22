using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaBullet_Script : MonoBehaviour
{
    public GameObject delta_part;
    public SpriteRenderer delta_part_renderer;
    public Rigidbody2D rb;
    bool destroyed, delta_part_less;
    public AudioSource au;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || (coll.gameObject.layer == LayerMask.NameToLayer("Can_Stay")))
        {
            if (coll.gameObject.GetComponent<ObjectTags>())
            {
                ObjectTags tags = coll.gameObject.GetComponent<ObjectTags>();
                if (tags.tags.Contains("anvil"))
                {
                    if (coll.gameObject.GetComponent<Anvil_Script>())
                    {
                        coll.gameObject.GetComponent<Anvil_Script>().Super();
                    }
                }
                else if (tags.tags.Contains("omicron"))
                {
                    if (coll.gameObject.GetComponent<Omicron_Script>())
                    {
                        coll.gameObject.GetComponent<Omicron_Script>().Super();
                    }
                }
                else if (tags.tags.Contains("omicron_eatpoint"))
                {
                    if (coll.gameObject.GetComponent<Omicron_Eatpoint_Script>())
                    {
                        coll.gameObject.GetComponent<Omicron_Eatpoint_Script>().par.Super();
                    }
                }
                else if (tags.tags.Contains("xi"))
                {
                    if (coll.gameObject.GetComponent<XI_Script>())
                    {
                        coll.gameObject.GetComponent<XI_Script>().Super();
                    }
                }
            }
            Destroyy();
        }
    }

    void FixedUpdate()
    {
        if (destroyed)
        {
            if (!delta_part_less)
            {
                delta_part.transform.localScale += new Vector3(0.15f, 0.15f, 0.15f);
                delta_part_renderer.color += new Color(0, 0, 0, 0.01f);
                if (delta_part.transform.localScale.x >= 2 || delta_part_renderer.color.a >= 1f)
                {
                    delta_part_less = true;
                }
            }
            else
            {
                delta_part.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
                delta_part_renderer.color -= new Color(0, 0, 0, 0.002f);
                if (delta_part.transform.localScale.x <= 0 || delta_part_renderer.color.a <= 0)
                {
                    delta_part.SetActive(false);
                }
            }
        }
        if (!destroyed && Vector2.Distance(Vector2.zero, rb.velocity) < 4)
        {
            Destroyy();
        }
    }

    public void Destroyy()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        delta_part.transform.parent = null;
        delta_part.transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.zero;
        destroyed = true;
        au.Play();
        gameObject.SetActive(false);
    }
}
