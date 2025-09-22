using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Portal : MonoBehaviour
{
    int layer, player, live, def;
    public GameObject friend, Mask;
    public SpriteRenderer onmouse_sr, light_sr;
    Color light_sr_start;
    bool UpMask, onmouse;
    public bool friend_onmouse;
    AudioSource au;

    void Start()
    {
        layer = LayerMask.NameToLayer("Can_Stay");
        player = LayerMask.GetMask("Player");
        live = LayerMask.GetMask("Live");
        def = LayerMask.GetMask("Default");
        au = GetComponent<AudioSource>();
        light_sr_start = light_sr.color;
    }

    void FixedUpdate()
    {
        if (UpMask) Mask.transform.Translate(0, 5*Time.deltaTime, 0);
        if (Mask.transform.localPosition.y >= 0.7f) UpMask = false;
        if (!UpMask) Mask.transform.Translate(0, -5*Time.deltaTime, 0);
        if (Mask.transform.localPosition.y <= -0.925) UpMask = true;
        if (onmouse || friend_onmouse)
        {
            onmouse_sr.color = Color.Lerp(onmouse_sr.color, new Color(1, 1, 1, 1), 4 * Time.deltaTime);
            light_sr.color = Color.Lerp(light_sr.color, new Color(0.64f, 0f, 0.57f, 0.75f), 4 * Time.deltaTime);
        }
        else
        {
            onmouse_sr.color = Color.Lerp(onmouse_sr.color, new Color(1, 1, 1, 0), 6 * Time.deltaTime);
            light_sr.color = Color.Lerp(light_sr.color, light_sr_start, 6 * Time.deltaTime);
        }
        RaycastHit2D waitp = Physics2D.Raycast(transform.position, transform.up, transform.localScale.y-1.1f, player);
        RaycastHit2D waitlive = Physics2D.Raycast(transform.position, transform.up, transform.localScale.y-1.1f, live);
        if (waitp.collider != null)
        {
            Tp(friend, waitp.collider.gameObject);
        }
        if (waitlive.collider != null)
        {
            Tp(friend, waitlive.collider.gameObject);
        }
        Debug.DrawRay(transform.position, transform.up, Color.red);
    }

    void Tp(GameObject fr, GameObject who)
    {
        who.transform.position = fr.transform.position;
        fr.GetComponent<Block_Portal>().enabled = false;
        fr.layer = def;
        Invoke("FriendCD", 0.5f);
        who.GetComponent<VelocityForce>().Impulse(fr.transform.up);
        au.Play();
        if (PlayerPrefs.GetInt("Teleports") < 100)
        {
            PlayerPrefs.SetInt("Teleports", PlayerPrefs.GetInt("Teleports")+1);
        }
        else if (PlayerPrefs.GetInt("Achieve7") == 0) 
        {
            GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(7);
        }
    }

    void FriendCD()
    {  
        friend.GetComponent<Block_Portal>().enabled = true;
        friend.layer = layer;
    }

    void OnMouseEnter()
    {
        onmouse = true;
        friend.GetComponent<Block_Portal>().friend_onmouse = true;
    }

    void OnMouseExit()
    {
        onmouse = false;
        friend.GetComponent<Block_Portal>().friend_onmouse = false;
    }
}
