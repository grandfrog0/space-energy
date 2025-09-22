using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Omicron_Script : MonoBehaviour
{
    public GameObject downSkull;
    public GameObject spikes;
    public GameObject omicron_part;
    public GameObject eat_point;
    public GameObject eyes, eyebrowes, damaged_eyes, damaged_eye1, damaged_eye2;

    public bool opened;
    public bool super_used;
    public bool damaged;
    bool part_used, omicron_part_less, eyes_less;

    public AudioSource open, fang, super, died;
    public BoxCollider2D coll1;
    public SpriteRenderer omicron_part_renderer;
    public BoxCollider2D damaged_collider;
    GameObject[] bullet;
    GameObject[] player;
    Transform target_player;
    CameraCenter camc;
    Vector3 eyes_start, eyebrowes_start, eyes_start_size, eyes_need_size;

    public void Fang()
    {
        if (opened)
        {
            opened = false;
            camc.Shake(true);
            Invoke("WaitShake", 0.2f);
            fang.Play();
            Invoke("Open", 0.35f);

            if (PlayerPrefs.GetInt("OmicronFed") < 50)
            {
                PlayerPrefs.SetInt("OmicronFed", PlayerPrefs.GetInt("OmicronFed")+1);
            }
            else if (PlayerPrefs.GetInt("Achieve15") == 0) 
            {
                GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(15);
            }
        }
    }

    public void Open()
    {
        if (!opened)
        {
            opened = true;
            open.Play();
        }
    }

    public void Super()
    {
        //not damaged
        damaged_collider.enabled = false;
        damaged = false;
        opened = true;
        damaged_eyes.SetActive(false);
        eyes.SetActive(true);
        eyebrowes.SetActive(true);
        eat_point.SetActive(false);

        super_used = true;
        part_used = true;
        omicron_part_less = false;
        spikes.SetActive(true);
        coll1.enabled = false;
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

    public void Damage()
    {
        if (!damaged && !super_used)
        {
            damaged_collider.enabled = true;
            damaged = true;
            opened = false;
            died.Play();
            damaged_eyes.SetActive(true);
            eyes.SetActive(false);
            eyebrowes.SetActive(false);
            eat_point.SetActive(false);

            part_used = true;
            omicron_part_less = false;

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

    void FixedUpdate()
    {
        bullet = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < bullet.Length; i++)
        {
            if (opened && !damaged && Vector3.Distance(bullet[i].transform.position, eat_point.transform.position) <= 0.75f)
            {
                if (bullet[i].GetComponent<ObjectTags>())
                {
                    if (!bullet[i].GetComponent<ObjectTags>().tags.Contains("delta_bullet"))
                    {
                        Fang();
                    }
                    else
                    {
                        Super();
                        
                        opened = false;
                        camc.Shake(true);
                        Invoke("WaitShake", 0.2f);
                        Invoke("Open", 0.5f);
                    }
                }
                else
                {
                    Fang();
                }
            }
        }
        player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            if (opened && !damaged && Vector3.Distance(player[i].transform.position, eat_point.transform.position) <= 1f)
            {
                Fang();
            }
            if (target_player == null) target_player = player[i].transform;
            else if (Vector3.Distance(player[i].transform.position, eat_point.transform.position) < Vector3.Distance(target_player.position, eat_point.transform.position))
            {
                target_player = player[i].transform;
            }
        }

        if (!damaged)
        {
            eyes.transform.localPosition = Vector3.Lerp(eyes.transform.localPosition, eyes_start - (eyes.transform.position - target_player.position).normalized / 10, 10*Time.deltaTime);
            eyebrowes.transform.localPosition = Vector3.Lerp(eyebrowes.transform.localPosition, eyebrowes_start - (eyebrowes.transform.position - target_player.position).normalized / 8, 10*Time.deltaTime);
            if (eyes_need_size != Vector3.zero)
            {
                if (eyes_less)
                {
                    eyes.transform.localScale = Vector3.Lerp(eyes.transform.localScale, eyes_need_size, 4*Time.deltaTime);
                    if (eyes.transform.localScale.y <= 0.3f)
                    {
                        eyes_less = false;
                    }
                }
                else
                {
                    eyes.transform.localScale = Vector3.Lerp(eyes.transform.localScale, eyes_start_size, 8*Time.deltaTime);
                    if (eyes.transform.localScale.y >= 0.9f)
                    {
                        eyes.transform.localScale = eyes_start_size;
                        eyes_need_size = Vector3.zero;
                    }
                }
            }
        }
        else
        {
            damaged_eye1.transform.Rotate(0, 0, 100*Time.deltaTime);
            damaged_eye2.transform.Rotate(0, 0, 100*Time.deltaTime);
            opened = false;
        }

        if (opened)
        {
            downSkull.transform.localPosition = Vector3.Lerp(downSkull.transform.localPosition, Vector3.up*-2.4f, 6*Time.deltaTime);
        }
        else
        {
            downSkull.transform.localPosition = Vector3.Lerp(downSkull.transform.localPosition, Vector3.up*-0.6f, 10*Time.deltaTime);
        }

        if (part_used)
        {
            if (!omicron_part_less)
            {
                omicron_part.transform.localScale += new Vector3(0.15f, 0.15f, 0.15f);
                omicron_part_renderer.color += new Color(0, 0, 0, 0.01f);
                if (omicron_part.transform.localScale.x >= 2 || omicron_part_renderer.color.a >= 1f)
                {
                    omicron_part_less = true;
                }
            }
            else
            {
                omicron_part.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
                omicron_part_renderer.color -= new Color(0, 0, 0, 0.002f);
                if (omicron_part.transform.localScale.x <= 0 || omicron_part_renderer.color.a <= 0)
                {
                    omicron_part.SetActive(false);
                    part_used = false;
                }
            }
        }
    }

    void Awake()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        camc = cam.GetComponent<CameraCenter>();
        eyes_start = eyes.transform.localPosition;
        eyebrowes_start = eyebrowes.transform.localPosition;
        eyes_start_size = eyes.transform.localScale;
        Invoke("Eye_blink", Random.Range(4, 10));
    }

    void Eye_blink()
    {
        eyes_less = true;
        eyes_need_size = new Vector3(1, 0.2f, 1);
        Invoke("Eye_blink", Random.Range(4, 10));
    }

    void WaitShake()
    {
        camc.Shake(false);
    }
}
