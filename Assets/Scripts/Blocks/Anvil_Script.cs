using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil_Script : MonoBehaviour
{
    public bool Block;
    public bool BlockSeted;
    VelocityForce velforce;
    public int cans, live;
    LayerMask Can_Stay, player;
    Rigidbody2D rb;

    //super
    public int super_state = 0;

    void Start()
    {
        velforce = GetComponent<VelocityForce>();
        cans = LayerMask.NameToLayer("Can_Stay");
        live = LayerMask.NameToLayer("Live");
        Can_Stay = LayerMask.GetMask("Can_Stay");
        player = LayerMask.GetMask("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    public void Super()
    {
        super_state = 1;
        Block = false;
        BlockSeted = true;
        velforce.Impulse(transform.up);
        Invoke("SupImp", 0.2f);
        if (PlayerPrefs.GetInt("DeltaUpgraded") < 15)
        {
            PlayerPrefs.SetInt("DeltaUpgraded", PlayerPrefs.GetInt("DeltaUpgraded")+1);
        }
        else if (PlayerPrefs.GetInt("Achieve14") == 0) 
        {
            GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(14);
        }
    }

    void SupImp()
    {
        Block = false;
        BlockSeted = true;
        super_state = 2;
        if (!Block) velforce.Impulse(transform.up);
    }

    void FixedUpdate()
    {
        if (Block)
        {
            if (!BlockSeted)
            {
                gameObject.layer = cans;
                velforce.enabled = false;
                BlockSeted = true;
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
        if (!Block)
        {
            if (BlockSeted)
            {
                gameObject.layer = live;
                velforce.enabled = true;
                BlockSeted = false;
                rb.bodyType = RigidbodyType2D.Dynamic;

                velforce.Impulse(transform.up*-1);
            }
        }
        RaycastHit2D stand = Physics2D.Raycast(transform.position-transform.up, transform.up*-1, transform.localScale.y/5, Can_Stay);
        Debug.DrawRay(transform.position-transform.up, transform.up*-1);
        if (stand.collider != null)
        {
            if (stand.collider.gameObject.CompareTag("Portal"))
            {

            }
            else if (!BlockSeted) Block = true;
            
            if (stand.collider.gameObject.CompareTag("Rotator"))
            {
                transform.parent = stand.collider.gameObject.transform;
                transform.position = Vector3.Lerp(transform.position, transform.parent.position, 0.02f);
            }
            else transform.parent = null;
        }
        else
        {
            if (BlockSeted) Block = false;
        }
        RaycastHit2D p = Physics2D.Raycast(transform.position-transform.up, transform.up*-1, transform.localScale.y/2, player);
        if (p.collider != null)
        {
            if (!Block)
            {
                p.collider.gameObject.GetComponent<P_Move>().RestartPos();
                if (PlayerPrefs.GetInt("DiedByAnvil") < 10)
                {
                    PlayerPrefs.SetInt("DiedByAnvil", PlayerPrefs.GetInt("DiedByAnvil")+1);
                }
                else if (PlayerPrefs.GetInt("Achieve9") == 0) 
                {
                    GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(9);
                }
            }
        }
    }
}
