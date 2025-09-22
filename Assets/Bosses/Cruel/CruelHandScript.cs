using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruelHandScript : MonoBehaviour
{
    public GameObject FallConductor, FallMod, FallBlackhole;
    public GameObject fallGm;
    public Vector3 fallPos;
    public LayerMask ignore;
    public SpriteRenderer filter;
    public Transform target_player, crop;
    Vector3 dir, startPos;
    float angle;
    public Camera cam;
    int rnd;
    public bool watchTo;
    public Animator anim;

    void Rnd()
    {
        Invoke("Rnd", Random.Range(3, 5));
        rnd = Random.Range(0, 5);
        RandomAttack();
    }

    public void Impulse()
    {
        fallGm.transform.parent = null;
        fallPos = target_player.position + new Vector3(Random.Range(-1.4f, 1.4f), Random.Range(-1.4f, 1.4f));
    }
    
    void RandomAttack()
    {
        if (rnd == 0)
        {
            fallGm = Instantiate(FallConductor, crop);
            anim.Play("DropSome");
        }
        else if (rnd == 1)
        {
            fallGm = Instantiate(FallMod, crop);
            anim.Play("DropSome");
        }
        else if (rnd == 2)
        {
            fallGm = Instantiate(FallBlackhole, crop);
            anim.Play("DropSome");
        }
        else if (rnd == 3)
        {
            
            
            fallGm = Instantiate(FallBlackhole, crop);
            anim.Play("DropSome");
        }
        else if (rnd == 4)
        {
            
            
            fallGm = Instantiate(FallBlackhole, crop);
            anim.Play("DropSome");
        }
        else if (rnd == 5)
        {
            
            
            fallGm = Instantiate(FallBlackhole, crop);
            anim.Play("DropSome");
        }
    }

    void Awake()
    {
        Invoke("Rnd", Random.Range(7, 10));
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        dir = target_player.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        if (watchTo)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 5*Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, startPos + transform.up * 3, 1 * Time.deltaTime);
        }
        if (fallGm && fallGm.transform.parent == null && Vector3.Distance(fallGm.transform.position, new Vector3(Mathf.RoundToInt(fallPos.x / 1.4f) * 1.4f, Mathf.RoundToInt(fallPos.y / 1.4f) * 1.4f)) > 0.1f)
        {
            fallGm.transform.position = Vector3.Lerp(fallGm.transform.position, new Vector3(Mathf.RoundToInt(fallPos.x / 1.4f) * 1.4f, Mathf.RoundToInt(fallPos.y / 1.4f) * 1.4f), 5*Time.deltaTime);
            fallGm.transform.localScale = Vector3.Lerp(fallGm.transform.localScale, new Vector3(1f, 1f, 1f), 5*Time.deltaTime);
        }
    }
}
