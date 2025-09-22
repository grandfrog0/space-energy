using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet_Script : MonoBehaviour
{
    public float length;
    // public bool blocked;
    // public LayerMask Can_Stay;
    public LayerMask Player, Live;
    public VelocityForce velforce;
    public AudioSource drag, drop, dragging1;
    public float velforce_size;
    public Transform armor;
    public Vector3 armor_start;
    public GameObject sr;

    void FixedUpdate()
    {
        // RaycastHit2D stand = Physics2D.Raycast(transform.position - transform.right * 3, -transform.right, length, Can_Stay);
        RaycastHit2D player = Physics2D.Raycast(transform.position - transform.right * (0.7f + velforce_size), -transform.right, length, Player);
        RaycastHit2D live = Physics2D.Raycast(transform.position - transform.right * (0.7f + velforce_size), -transform.right, length, Live);
        if (player.collider != null)
        {
            if (player.collider.gameObject.GetComponent<VelocityForce>() != null)
            {
                if (velforce == null)
                {
                    drag.Play();
                }
                velforce = player.collider.gameObject.GetComponent<VelocityForce>();
                velforce.SetOutControl(true);
                velforce.OutControl_Impulse((transform.position - player.collider.gameObject.transform.position).normalized, 15f);
                //EFFECT OF GYPNO
            }
        }
        if (live.collider != null)
        {
            if (live.collider.gameObject.GetComponent<VelocityForce>() != null)
            {
                if (velforce == null)
                {
                    drag.Play();
                }
                velforce = live.collider.gameObject.GetComponent<VelocityForce>();
                velforce.SetOutControl(true);
                velforce.OutControl_Impulse((transform.position - live.collider.gameObject.transform.position).normalized, 15f);
                //MAKE SOUND
                //EFFECT OF GYPNO
            }
        }
        if (velforce != null)
        {
            velforce_size = (velforce.gameObject.transform.localScale.x + velforce.gameObject.transform.localScale.y) / 2f * 1.4f;
            armor.position = Vector3.Lerp(armor.position, armor_start + (armor_start - velforce.gameObject.transform.position).normalized/5f, 5*Time.deltaTime);
            if (!dragging1.isPlaying)
            {
                dragging1.Play();
            }
            sr.SetActive(true);
            sr.transform.position = velforce.gameObject.transform.position;
            sr.transform.localScale = velforce.gameObject.transform.localScale * 2f;
        }
        else
        {
            sr.SetActive(false);
            armor.position = Vector3.Lerp(armor.position, armor_start, 5*Time.deltaTime);
        }
        if (player.collider == null && live.collider == null && velforce != null)
        {
            velforce.SetOutControl(false);
            velforce = null;
            drop.Play();
        }
        // while (length < 50 && !blocked)
        // {
        //     length++;
        //     stand = Physics2D.Raycast(transform.position, -transform.right, length, Can_Stay);
        //     if (stand.collider != null)
        //     {
        //         Debug.Log(stand.collider.gameObject.name);
        //         blocked = true;
        //     }
        // }
        Debug.DrawRay(transform.position - transform.right * 2.1f, -transform.right * length, Color.green);
    }

    void Start()
    {
        armor_start = armor.position;
    }
}
