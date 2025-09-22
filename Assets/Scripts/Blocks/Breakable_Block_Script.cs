using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_Block_Script : MonoBehaviour
{
    public GameObject heart, motor, circle, block, ligh;
    public float NowLives, MaxLives;
    public GameObject cr1, cr2, cr3, cr4;
    public List<GameObject> cracks = new List<GameObject>();
    public List<SpriteRenderer> renders;
    LayerMask Player;
    public int cracklvl;
    bool lightless, lightmore;
    SpriteRenderer lightr;
    bool md, cd, hd;

    public GameObject rest_origin;
    GameObject rest;
    BreakableReset restart;

    Vector3 ht, mt, ct;
    Vector2 to;

    public AudioSource breaking;
    public AudioSource broked;

    public TMPro.TMP_Text text;
    public bool onmouse;

    public List<Rigidbody2D> breaks_rb;
    public GameObject breaks_gm;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || DangerTags.tags.Contains(coll.gameObject.tag))
        {
           NowLives--;
           Check();
           breaking.Play();
           to = coll.gameObject.transform.position - transform.position;
            if (cracklvl > 0)
            {
                if (Mathf.Abs(to.x) >= Mathf.Abs(to.y))
                {
                    if (to.x <= 0)
                    {
                        cracks[cracklvl-1].transform.localPosition = new Vector3(-1.16f, 1.18f, 0);
                        cracks[cracklvl-1].transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        cracks[cracklvl-1].transform.localPosition = new Vector3(1.16f, -1.18f, 0);
                        cracks[cracklvl-1].transform.rotation = Quaternion.Euler(0, 0, 180);
                    }
                }
                else
                {
                    if (to.y > 0)
                    {
                        cracks[cracklvl-1].transform.localPosition = new Vector3(1.18f, 1.16f, 0);
                        cracks[cracklvl-1].transform.rotation = Quaternion.Euler(0, 0, 270);
                    }
                    else
                    {
                        cracks[cracklvl-1].transform.localPosition = new Vector3(-1.18f, -1.16f, 0);
                        cracks[cracklvl-1].transform.rotation = Quaternion.Euler(0, 0, 90);
                    }
                }
                if (NowLives <= 0) GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (lightless) lightr.color = new Color(lightr.color.r, lightr.color.g, lightr.color.b, lightr.color.a-0.01f);
        if (lightmore) lightr.color = new Color(lightr.color.r, lightr.color.g, lightr.color.b, lightr.color.a+0.03f);
        if (lightr.color.a > 0.7f)
        {
            lightmore = false;
            lightless = true;
        }
        if (!md) motor.transform.Rotate(0, 0, -150f*Time.deltaTime);
        if (!lightmore && !lightless)
        {
            if (cracklvl == 1) renders[0].color = new Color(1, 1, 1, Mathf.Lerp(renders[0].color.a, 255, 0.02f));
            if (cracklvl == 2) renders[1].color = new Color(1, 1, 1, Mathf.Lerp(renders[1].color.a, 255, 0.02f));
            if (cracklvl == 3) renders[2].color = new Color(1, 1, 1, Mathf.Lerp(renders[2].color.a, 255, 0.02f));
            if (cracklvl == 4) renders[3].color = new Color(1, 1, 1, Mathf.Lerp(renders[3].color.a, 255, 0.02f));
        }

        if (onmouse)
        {
            if (text.color.a < 0.3f)
            {
                text.color += new Color(0, 0, 0, 0.02f);
            }
            if (lightr.color.a < 0.6f)
            {
                lightr.color += new Color(0, 0, 0, 0.02f);
            }
            text.text = NowLives.ToString();
        }
        else
        {
            if (text.color.a >= 0f)
            {
                text.color -= new Color(0, 0, 0, 0.04f);
            }
            if (lightr.color.a > 0.4f)
            {
                lightr.color -= new Color(0, 0, 0, 0.04f);
            }
        }
    }

    void Awake()
    {
        InvokeRepeating("HeartBeat", 0.2f, 0.2f);
        NowLives = MaxLives;
        Check();
        Player = LayerMask.GetMask("Player");
        cracks.Add(cr1);
        cracks.Add(cr2);
        cracks.Add(cr3);
        cracks.Add(cr4);
        for (int j = 0; j < cracks.Count; j++)
        {
            renders[j] = cracks[j].GetComponent<SpriteRenderer>();
        }
        lightr = ligh.GetComponent<SpriteRenderer>();
        mt = motor.transform.position;
        ht = heart.transform.position;
        ct = circle.transform.position;

        // rest = Instantiate(rest_origin, transform.position, Quaternion.identity);
        // restart = rest.GetComponent<BreakableReset>();
        // restart.block = gameObject;
        // restart.bs = this;
    }

    void HeartBeat()
    {
        if (!hd) heart.transform.Rotate(0, 0, 30);
    }

    public void Build()
    {
        NowLives = MaxLives;
        cracklvl = 0;
        motor = Instantiate(motor, mt, Quaternion.identity);
        heart = Instantiate(heart, ht, Quaternion.identity);
        circle = Instantiate(circle, ct, Quaternion.identity);
        lightmore = false;
        lightless = false;
        motor.transform.parent = transform;
        heart.transform.parent = transform;
        circle.transform.parent = transform;
        block.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void Check()
    {
        if (NowLives/MaxLives <= 0.8f) 
        {
            cracklvl = 1;
        }
        if (NowLives/MaxLives <= 0.6f) 
        {
            cracklvl = 2;
        }
        if (NowLives/MaxLives <= 0.4f) 
        {
            cracklvl = 3;
        }
        if (NowLives/MaxLives <= 0.2f) 
        {
            cracklvl = 4;
        }
        if (NowLives <= 0)
        {
            onmouse = false;
            // restart.Invoke("Upd", 3);
            breaks_gm.SetActive(true);
            if (Mathf.Abs(to.x) >= Mathf.Abs(to.y))
            {
                if (to.x <= 0)
                {
                    breaks_gm.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    breaks_gm.transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            }
            else
            {
                if (to.y > 0)
                {
                    breaks_gm.transform.rotation = Quaternion.Euler(0, 0, 270);
                }
                else
                {
                    breaks_gm.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
            }
            for (int i = 0; i < breaks_rb.Count; i++)
            {
                if (to != Vector2.zero) breaks_rb[i].AddForce((breaks_rb[i].transform.position - transform.position) * 2, ForceMode2D.Impulse);
                else breaks_rb[i].AddForce((breaks_rb[i].transform.position - transform.position) * 2, ForceMode2D.Impulse);
            }

            broked.Play();
            motor.GetComponent<Rigidbody2D>().velocity = (motor.transform.position - transform.position)*2;
            heart.GetComponent<Rigidbody2D>().velocity = (heart.transform.position - transform.position)*2;
            circle.GetComponent<Rigidbody2D>().velocity = transform.up+transform.right*-1;
            lightmore = true;
            motor.transform.parent = null;
            heart.transform.parent = null;
            circle.transform.parent = null;
            block.GetComponent<SpriteRenderer>().color = Color.clear;
            for (int c = 0; c < cracks.Count; c++)
            {
                renders[c].color = Color.clear;
            }
            if (PlayerPrefs.GetInt("BreakableBrocked") < 75)
            {
                PlayerPrefs.SetInt("BreakableBrocked", PlayerPrefs.GetInt("BreakableBrocked")+1);
            }
            else if (PlayerPrefs.GetInt("Achieve3") == 0) 
            {
                GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(3);
            }
        }
    }

    void OnMouseEnter()
    {
        onmouse = true;
    }

    void OnMouseExit()
    {
        onmouse = false;
    }
}
