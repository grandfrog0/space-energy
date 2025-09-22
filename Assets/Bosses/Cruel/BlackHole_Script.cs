using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole_Script : MonoBehaviour
{
    bool destr;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!destr && coll.gameObject.CompareTag("Player"))
        {
            if (coll.gameObject.GetComponent<P_Move>())
            {
                P_Move move = coll.gameObject.GetComponent<P_Move>();
                move.isLive = false;
                move.ResetVelocity();
                move.Current_Animation = "Die";
                move.anim.Animation("Die");

                destr = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (destr)
        {
            transform.localScale -= new Vector3(0.5f, 0.5f, 0) * Time.deltaTime;
            if (transform.localScale.x < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        Invoke("Destroyy", Random.Range(3f, 7f));
    }

    void Destroyy()
    {
        destr = true;
    }
}
