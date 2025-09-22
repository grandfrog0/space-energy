using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher_Script : MonoBehaviour
{
    public Transform block;
    Animator anim;
    public AudioSource bump;

    public int touchLeft, touchMax;
    public bool solid;

    int def, Can_Stay;

    public TMPro.TMP_Text text;
    public bool onmouse;

    void FixedUpdate()
    {
        block.Rotate(0, 0, 100*Time.deltaTime);
        if (onmouse)
        {
            if (text.color.a < 0.3f)
            {
                text.color += new Color(0, 0, 0, 0.02f);
            }
            if (!solid) text.text = touchLeft.ToString();
            else onmouse = false;
        }
        else
        {
            if (text.color.a >= 0f)
            {
                text.color -= new Color(0, 0, 0, 0.04f);
            }
        }
    }

    void DisableSolid()
    {
        solid = false;
        anim.Play("DisableSolid", 0);
        gameObject.layer = def;
    }

    void EnableSolid()
    {
        solid = true;
        anim.Play("EnableSolid", 0);
        bump.Play();
        gameObject.layer = Can_Stay;
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        touchLeft = touchMax;
        def = LayerMask.NameToLayer("Default");
        Can_Stay = LayerMask.NameToLayer("Can_Stay");
        DisableSolid();
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || DangerTags.tags.Contains(coll.gameObject.tag))
        {
            if (touchLeft > 1)
            {
                touchLeft--;
            }
            else
            {
                EnableSolid();
                if (PlayerPrefs.GetInt("PusherEnables") < 50)
                {
                    PlayerPrefs.SetInt("PusherEnables", PlayerPrefs.GetInt("PusherEnables")+1);
                }
                else if (PlayerPrefs.GetInt("Achieve2") == 0) 
                {
                    GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(2);
                }
            }
        }
    }

    void OnMouseEnter()
    {
        if (!solid) onmouse = true;
    }

    void OnMouseExit()
    {
        onmouse = false;
    }
}
