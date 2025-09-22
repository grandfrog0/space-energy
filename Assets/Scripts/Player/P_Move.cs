using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Move : MonoBehaviour
{
    public P_Animator anim;
    P_Collision coll;
    VelocityForce velforce;

    [Header("Debug Parameters")]
    public Rigidbody2D rb;
    public string Current_Animation;
    Vector2 StartTouch, EndTouch;
    public Vector2 TouchVector;
    public bool IsGrounded;

    public bool isLive;
    public Vector3 SpawnPoint;
    public int DeathCount;

    public bool HasQueue;
    public Vector2 QueueVector;

    public GameObject destroyParticle;
    public float particlecount;
    public AudioSource die;

    public GameObject eyes, head, mouse;

    public bool use_fallpart;
    public List<GameObject> fall_particles;
    public List<SpriteRenderer> fall_srs;
    int cur_fallpart;

    public bool modifier2_used; //two players modifier
    public int playerNum;

    void Update()
    {
        modifier2_used = Modifiers.GetMod(2);

        if (isLive)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartPos();
            }
            if (IsGrounded)
            {
                velforce.SetAngle(TouchVector);

                if (!modifier2_used)
                {
                    if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
                    {
                        velforce.Impulse(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
                    }
                }
                else if (playerNum == 0)
                {
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
                    {
                        velforce.Impulse(new Vector2(-BoolToInt(Input.GetKeyDown(KeyCode.A)) + BoolToInt(Input.GetKeyDown(KeyCode.D)), -BoolToInt(Input.GetKeyDown(KeyCode.S)) + BoolToInt(Input.GetKeyDown(KeyCode.W))));
                    }
                }
                else if (playerNum == 1)
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        velforce.Impulse(new Vector2(-BoolToInt(Input.GetKeyDown(KeyCode.LeftArrow)) + BoolToInt(Input.GetKeyDown(KeyCode.RightArrow)), -BoolToInt(Input.GetKeyDown(KeyCode.DownArrow)) + BoolToInt(Input.GetKeyDown(KeyCode.UpArrow))));
                    }
                }
            }
            if (HasQueue)
            {
                if (IsGrounded)
                {
                    velforce.Impulse(QueueVector);
                    HasQueue = false;
                    QueueVector = Vector2.zero;
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (!modifier2_used) StartTouch = Input.mousePosition;
                else if (Input.mousePosition.x < 960)
                {
                    if (playerNum == 0)
                    {
                        StartTouch = Input.mousePosition;
                    }
                }
                else
                {
                    if (playerNum == 1)
                    {
                        StartTouch = Input.mousePosition;
                    }
                }
            }
            if (Input.GetButtonUp("Fire1") && rb.bodyType == RigidbodyType2D.Dynamic)
            {
                if (IsGrounded)
                {
                    if (!modifier2_used)
                    {
                        fEndTouch();
                    }
                    else if (Input.mousePosition.x < 960)
                    {
                        if (playerNum == 0)
                        {
                            fEndTouch();
                        }
                    }
                    else
                    {
                        if (playerNum == 1)
                        {
                            fEndTouch();
                        }
                    }
                }
                else
                {
                    HasQueue = true;
                    if (Vector2.Distance(TouchVector, Vector2.zero) < 40)
                    {
                        QueueVector = Vector2.zero;
                    }
                    else if (!modifier2_used) QueueVector = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - StartTouch;
                    else if (Input.mousePosition.x < 960)
                    {
                        if (playerNum == 0)
                        {
                            QueueVector = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - StartTouch;
                        }
                    }
                    else
                    {
                        if (playerNum == 1)
                        {
                            QueueVector = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - StartTouch;
                        }
                    }
                }
            }

            if (!IsGrounded)
            {
                if (Current_Animation != "Fall")
                {
                    Current_Animation = "Fall";
                    anim.Animation(Current_Animation);
                }
            }
            if (IsGrounded && Current_Animation == "Fall") 
            {
                Current_Animation = "Land";
                anim.Animation(Current_Animation);
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<P_Animator>();
        coll = GetComponent<P_Collision>();
        velforce = GetComponent<VelocityForce>();
        isLive = true;
        SpawnPoint = transform.position;

        if (TouchVector == Vector2.zero) TouchVector = new Vector2(0, -1);
        
        ResetVelocity();
        if (velforce.Force == Vector2.zero) velforce.Force = Vector2.up;
        rb.AddForce(velforce.Force*velforce.Gravity, ForceMode2D.Impulse);

        if (use_fallpart)
        foreach (GameObject gm in fall_particles)
        {
            fall_srs.Add(gm.GetComponent<SpriteRenderer>());
        }
    }

    public void SetGrounded(bool value)
    {
        IsGrounded = value;
        if (value) velforce.SetAngle(TouchVector);
    }

    void SpawnDP()
    {
        for (int k = 0; k < particlecount; k++)
        {
            GameObject dp = Instantiate(destroyParticle, transform.position, transform.rotation);
            dp.transform.rotation = Quaternion.Euler(0, 0, k*360/particlecount+Random.Range(-5f, 5f));
            dp.GetComponent<Rigidbody2D>().velocity = dp.transform.up*10;
        }
    }

    public void RestartPos()
    {
        coll.par = null;
        transform.parent = null;
        SpawnDP();
        die.Play();
        coll.cam.SetDimension(true);
        coll.DiedOnce = true;
        DeathCount++;
        PlayerPrefs.SetInt("WithoutDieLvls", 0);
        isLive = false;
        TouchVector = Vector2.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = SpawnPoint;
        ResetVelocity();
        HasQueue = false;
        QueueVector = Vector2.zero;
        coll.Energy_Charged = false;
        coll.Energy_Mask.transform.localPosition = Vector3.down * 2;
        head.GetComponent<SpriteRenderer>().color = Color.clear;
        eyes.GetComponent<SpriteRenderer>().color = Color.clear;
        mouse.GetComponent<SpriteRenderer>().color = Color.clear;
        GetComponent<BoxCollider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        Invoke("ResP", 1.5f);
        IsGrounded = false;
        velforce.outControl = false;
        velforce.enabled = false;
        anim.DieSeted = false;
    }

    void ResP()
    {
        isLive = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.angularVelocity = 0;
        velforce.enabled = true;
        velforce.Force = Vector2.up;
        rb.AddForce(velforce.Force*velforce.Gravity, ForceMode2D.Impulse);
        head.GetComponent<SpriteRenderer>().color = Color.white;
        eyes.GetComponent<SpriteRenderer>().color = Color.white;
        mouse.GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<BoxCollider2D>().enabled = true;
        
        Current_Animation = "Land";
        anim.Animation(Current_Animation);
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector2.zero;
    }

    void fEndTouch()
    {
        EndTouch = Input.mousePosition;
        TouchVector = EndTouch - StartTouch;
        if (Vector2.Distance(TouchVector, Vector2.zero) < 40)
        {
            TouchVector = Vector2.zero;
        }
        else velforce.Impulse(TouchVector);

        if (PlayerPrefs.GetInt("Swipes") < 750)
        {
            PlayerPrefs.SetInt("Swipes", PlayerPrefs.GetInt("Swipes")+1);
        }
        else if (PlayerPrefs.GetInt("Achieve6") == 0) 
        {
            GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(6);
        }
    }

    int BoolToInt(bool val)
    {
        if (val) return 1;
        else return -1;
    }

    void FixedUpdate()
    {
        if (use_fallpart)
        {
            cur_fallpart = cur_fallpart + 1;
            cur_fallpart %= fall_particles.Count;
            fall_particles[cur_fallpart].transform.parent = null;
            fall_particles[cur_fallpart].transform.position = transform.position;
            fall_particles[cur_fallpart].transform.localScale = new Vector3(1, 1, 1);
            fall_srs[cur_fallpart].color += new Color(0, 0, 0, 0.7f);
        }
    }
}
