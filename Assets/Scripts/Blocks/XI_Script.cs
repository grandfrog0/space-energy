using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XI_Script : MonoBehaviour
{
    //add die
    public bool see_player;
    public bool super_used;
    public bool damaged;
    public bool angry_used;
    bool part_used, xi_part_less;

    public GameObject bullet;
    public GameObject super_eye, super_back, xi_part, damaged_eyes, eyes;
    public GameObject player;
    public GameObject[] players;
    public SpriteRenderer xi_part_renderer;
    public Animator anim;
    public AudioSource died, super, angry, pew;

    void FixedUpdate()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (player == null) player = players[i];
            else if (Vector3.Distance(players[i].transform.position, transform.position) < Vector3.Distance(player.transform.position, transform.position))
            {
                player = players[i];
            }
        }

        if (see_player && !damaged)
        {
            anim.Play("Pew");
            angry_used = false;
        }
        else
        {
            anim.Play("Wait");
            if (!angry_used && !damaged)
            {
                angry_used = true;
                angry.Play();
            }
        }

        if (damaged)
        {
            damaged_eyes.transform.Rotate(0, 0, -100*Time.deltaTime);
        }

        if (Vector3.Distance(player.transform.position, transform.position) < 8)
        {
            see_player = true;
        }
        else
        {
            see_player = false;
        }

        if (part_used)
        {
            if (!xi_part_less)
            {
                xi_part.transform.localScale += new Vector3(0.15f, 0.15f, 0.15f);
                xi_part_renderer.color += new Color(0, 0, 0, 0.01f);
                if (xi_part.transform.localScale.x >= 2 || xi_part_renderer.color.a >= 1f)
                {
                    xi_part_less = true;
                }
            }
            else
            {
                xi_part.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
                xi_part_renderer.color -= new Color(0, 0, 0, 0.002f);
                if (xi_part.transform.localScale.x <= 0 || xi_part_renderer.color.a <= 0)
                {
                    xi_part.SetActive(false);
                    part_used = false;
                }
            }
        }
    }

    public void Damage()
    {
        if (!damaged)
        {
            damaged = true;
            died.Play();
            damaged_eyes.SetActive(true);
            eyes.SetActive(false);

            part_used = true;
            xi_part_less = false;

            if (PlayerPrefs.GetInt("GreensKills") < 20)
            {
                PlayerPrefs.SetInt("GreensKills", PlayerPrefs.GetInt("GreensKills")+1);
            }
            else if (PlayerPrefs.GetInt("Achieve16") == 0) 
            {
                GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(16);
            }
        }
    }

    public void Super()
    {
        if (!super_used)
        {
            super_used = true;
            super_eye.SetActive(true);
            super_back.SetActive(true);
            anim.speed = 2f;
            
            part_used = true;
            xi_part_less = false;

            super.Play();

            if (PlayerPrefs.GetInt("DeltaUpgraded") < 15)
            {
                PlayerPrefs.SetInt("DeltaUpgraded", PlayerPrefs.GetInt("DeltaUpgraded")+1);
            }
            else if (PlayerPrefs.GetInt("Achieve14") == 0) 
            {
                GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(14);
            }
        }
    }

    public void Pew()
    {
        GameObject bullet1 = Instantiate(bullet, transform.position + transform.right*1.5f, Quaternion.identity);
        VelocityForce velforce = bullet1.GetComponent<VelocityForce>();
        velforce.Impulse(transform.right);
        pew.Play();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || (DangerTags.tags.Contains(coll.gameObject.tag) && coll.gameObject.tag != "Spike"))
        {
            if (coll.gameObject.GetComponent<ObjectTags>())
            {
                ObjectTags tags = coll.gameObject.GetComponent<ObjectTags>();
                if (!tags.tags.Contains("delta_bullet"))
                {
                    Damage();
                }
            }
            else
            {
                Damage();
            }
        }
    }
}
