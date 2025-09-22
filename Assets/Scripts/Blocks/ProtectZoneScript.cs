using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectZoneScript : MonoBehaviour
{
    public float NowLives, MaxLives;
    public GameObject cr1, cr2, cr3, cr4;
    public List<GameObject> cracks = new List<GameObject>();
    public LayerMask Player;
    public SpriteRenderer block, part, ray_sr;

    public AudioSource breaking;
    public AudioSource broked;

    public TMPro.TMP_Text text;
    public bool onmouse;

    public GameObject par;
    public Transform mask, ray;
    public Vector2 size;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || DangerTags.tags.Contains(coll.gameObject.tag))
        {
           NowLives--;
           Check();
           breaking.Play();
           Vector2 to = coll.gameObject.transform.position - transform.position;
            if (NowLives <= 0) GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (NowLives > 0)
        {
            mask.localScale = size*(NowLives/MaxLives);
            ray.Rotate(0, 0, -50f*Time.deltaTime);

            block.color = Color.Lerp(block.color, new Color(1, 1, 1, 1 - Random.Range(0f, 0.8f - NowLives/MaxLives)), 5*Time.deltaTime);
            part.color = Color.Lerp(part.color, new Color(1, 1, 1, 1 - Random.Range(0f, 0.8f - NowLives/MaxLives)), 5*Time.deltaTime);
            ray_sr.color = Color.Lerp(ray_sr.color, new Color(1, 1, 1, 1 - Random.Range(0f, 0.8f - NowLives/MaxLives)), 5*Time.deltaTime);
        }
        else
        {
            mask.localScale = size*1.1f;
            transform.localScale = Vector2.Lerp(transform.localScale, size*2, 3*Time.deltaTime);

            block.color = Color.Lerp(block.color, new Color(1, 1, 1, 0), 5*Time.deltaTime);
            part.color = Color.Lerp(part.color, new Color(1, 1, 1, 0), 5*Time.deltaTime);
            ray_sr.color = Color.Lerp(ray_sr.color, new Color(1, 1, 1, 0), 5*Time.deltaTime);
        }

        if (onmouse)
        {
            if (text.color.a < 0.2f)
            {
                text.color += new Color(0, 0, 0, 0.02f);
            }
            text.text = NowLives.ToString();
        }
        else
        {
            if (text.color.a >= 0f)
            {
                text.color -= new Color(0, 0, 0, 0.04f);
            }
        }

        transform.position = par.transform.position;
    }

    void Awake()
    {
        NowLives = MaxLives;
        Check();
        cracks.Add(cr1);
        cracks.Add(cr2);
        cracks.Add(cr3);
        cracks.Add(cr4);
    }

    void Check()
    {
        if (NowLives <= 0)
        {
            onmouse = false;
            broked.Play();
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
