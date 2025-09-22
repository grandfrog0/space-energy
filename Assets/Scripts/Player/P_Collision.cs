using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P_Collision : MonoBehaviour
{
    GameObject camer;
    public CameraCenter cam;

    public GameObject Energy_Mask;
    SpriteMask Energy_Mask_sr;
    public bool Energy_Charged;
    P_Move move;
    P_Animator anim;
    VelocityForce velforce;
    LayerMask layer;
    public AudioSource Energied;
    public bool DiedOnce;

    public Transform par;
    
    GameManager game_manager;
    bool lvl_comp;
    RaycastHit2D hit, hitp;

    void Update()
    {
        if (Energy_Charged)
        {
            Energy_Mask.transform.position = Vector3.Lerp(Energy_Mask.transform.position, transform.position, 0.01f);
            if (Vector3.Distance(Energy_Mask.transform.position, transform.position) < 0.02f && !lvl_comp)
            {
                lvl_comp = true;
            }
        }
    }
    void FixedUpdate()
    {
        if (move.isLive)
        {
            if (par!=null) transform.parent = par;
            if (transform.parent != null) transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 0.5f*Time.deltaTime);
        }
        if (par==null || !move.isLive) transform.parent = null;


        hit = Physics2D.Raycast(transform.position, velforce.Force*-1, transform.localScale.y, layer);
        Debug.DrawRay(transform.position, velforce.Force*-1);
        if (hit.collider != null && move.isLive)
        {
            move.SetGrounded(true);
            par = hit.collider.gameObject.transform;
            
            if (hit.collider.gameObject.CompareTag("Rotator"))
            {
                transform.position = Vector3.Lerp(transform.position, par.position, 0.02f);
            }
        }
    }

    public void Energy()
    {
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Finish")
            {
                if (!Energy_Charged)
                {
                    game_manager.LevelCompleted();
                    if (PlayerPrefs.GetInt("EnergyCharged") < 75)
                    {
                        PlayerPrefs.SetInt("EnergyCharged", PlayerPrefs.GetInt("EnergyCharged")+1);
                    }
                    else if (PlayerPrefs.GetInt("Achieve5") == 0) 
                    {
                        GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(5);
                    }
                    if (!DiedOnce)
                    {
                        if (PlayerPrefs.GetInt("WithoutDieLvls") < 15)
                        {
                            PlayerPrefs.SetInt("WithoutDieLvls", PlayerPrefs.GetInt("WithoutDieLvls")+1);
                        }
                        else if (PlayerPrefs.GetInt("Achieve8") == 0) 
                        {
                            GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(8);
                        }
                    }
                    anim.Animation("Energied");
                    Energied.Play();
                }
                Energy_Mask_sr.enabled = true;
                Energy_Charged = true;
                PlayerPrefs.SetInt("LvlOpened"+(game_manager.level+1), 1);
                move.enabled = false;
                cam.SetDimension(true);
            }
        }
    }

    void Start()
    {
        Energy_Mask_sr = Energy_Mask.GetComponent<SpriteMask>();
        move = GetComponent<P_Move>();
        anim = GetComponent<P_Animator>();
        layer = LayerMask.GetMask("Can_Stay");
        game_manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        velforce = GetComponent<VelocityForce>();
        camer = GameObject.FindGameObjectWithTag("MainCamera");
        cam = camer.GetComponent<CameraCenter>();
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        par = null;
        move.SetGrounded(false);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (move.rb.bodyType != RigidbodyType2D.Static)
        {
            if (coll.gameObject.CompareTag("Bullet") || coll.gameObject.CompareTag("Spike"))
            {
                move.RestartPos();
            }

            //achievement 10
            if (SceneManager.GetActiveScene().buildIndex == 15)
            {
                if (coll.gameObject.CompareTag("Mover") && transform.position.x > -9.8f && transform.position.x < -7f)
                {
                    if (PlayerPrefs.GetInt("Achieve10") == 0) 
                    {
                        GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(10);
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Conductor"))
        {
            cam.SwipeDimension();
        }
    }
}
