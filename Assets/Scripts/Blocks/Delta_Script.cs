using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delta_Script : MonoBehaviour
{
    Transform player;
    public Transform eye, pupil;
    public Vector3 eye_start, pupil_start;
    public GameObject bullet;
    public VelocityForce velforce;

    //break block part
    public GameObject block, ligh;
    public float NowLives, MaxLives;
    public GameObject cr1, cr2, cr3, cr4;
    public List<GameObject> cracks = new List<GameObject>();
    public List<SpriteRenderer> renders;
    LayerMask Player;
    public int cracklvl;
    bool lightless, lightmore;
    SpriteRenderer lightr;
    public Vector3 to, to1;

    public AudioSource breaking;
    public AudioSource broked;

    [Header("Particles")]
    public GameObject destroyParticle;
    public int particlecount;

    void OnCollisionExit2D(Collision2D coll)
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

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || DangerTags.tags.Contains(coll.gameObject.tag))
        {
           to1 = coll.gameObject.transform.position - transform.position;
        }
    }

    void FixedUpdate()
    {
        if (NowLives > 0)
        {
            eye.localPosition = Vector3.Lerp(eye.localPosition, eye_start - (eye.position - player.position).normalized / 10, 10*Time.deltaTime);
            pupil.localPosition = Vector3.Lerp(pupil.localPosition, pupil_start - (pupil.position - player.position).normalized / 8, 10*Time.deltaTime);
        }
        else
        {
            eye.position = bullet.transform.position;
            eye.localScale = bullet.transform.localScale;
        }
        
        //break block part
        if (lightless) lightr.color = new Color(lightr.color.r, lightr.color.g, lightr.color.b, lightr.color.a-0.01f);
        if (lightmore) lightr.color = new Color(lightr.color.r, lightr.color.g, lightr.color.b, lightr.color.a+0.03f);
        if (lightr.color.a > 0.7f)
        {
            lightmore = false;
            lightless = true;
        }
        if (!lightmore && !lightless)
        {
            if (cracklvl == 1) renders[0].color = new Color(1, 1, 1, Mathf.Lerp(renders[0].color.a, 255, 0.02f));
            if (cracklvl == 2) renders[1].color = new Color(1, 1, 1, Mathf.Lerp(renders[1].color.a, 255, 0.02f));
            if (cracklvl == 3) renders[2].color = new Color(1, 1, 1, Mathf.Lerp(renders[2].color.a, 255, 0.02f));
            if (cracklvl == 4) renders[3].color = new Color(1, 1, 1, Mathf.Lerp(renders[3].color.a, 255, 0.02f));
        }
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        eye_start = eye.localPosition;
        pupil_start = pupil.localPosition;

        //break block part
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
            // restart.Invoke("Upd", 3);
            broked.Play();
            lightmore = true;
            block.GetComponent<SpriteRenderer>().color = Color.clear;
            bullet.SetActive(true);
            bullet.transform.parent = null;
            velforce.Impulse(to1);
            for (int c = 0; c < cracks.Count; c++)
            {
                renders[c].color = Color.clear;
            }
            for (int k = 0; k < particlecount; k++)
            {
                GameObject dp = Instantiate(destroyParticle, transform.position, transform.rotation);
                dp.transform.rotation = Quaternion.Euler(0, 0, k*360/particlecount);
                dp.GetComponent<Rigidbody2D>().velocity = dp.transform.up*10;
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
}
