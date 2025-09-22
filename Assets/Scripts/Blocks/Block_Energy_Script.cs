using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Energy_Script : MonoBehaviour
{
    LayerMask Player;

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position+transform.up/2f, transform.up, transform.localScale.y-2f, Player);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                hit.collider.gameObject.GetComponent<P_Collision>().Energy();
            }
        }
    }

    void Start()
    {
        Player = LayerMask.GetMask("Player");
    }
}
