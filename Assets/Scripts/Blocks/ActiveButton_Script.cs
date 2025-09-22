using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveButton_Script : MonoBehaviour
{
    public Transform Mask;
    bool UpMask;
    public ActiveBlock_Script friend;
    LayerMask Player, Live;

    void FixedUpdate()
    {
        if (UpMask) Mask.transform.Translate(0, 1*Time.deltaTime, 0);
        if (Mask.transform.localPosition.y >= 0.5f) UpMask = false;
        if (!UpMask) Mask.transform.Translate(0, -1*Time.deltaTime, 0);
        if (Mask.transform.localPosition.y <= -1.25f) UpMask = true;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, transform.localScale.y/3f, Player);
        Debug.DrawRay(transform.position, transform.up);
        if (hit.collider != null)
        {
            Pressed();
        }
        RaycastHit2D live = Physics2D.Raycast(transform.position, transform.up, transform.localScale.y/3f, Live);
        if (live.collider != null)
        {
            Pressed();
        }
    }

    void Awake()
    {
        Player = LayerMask.GetMask("Player");
        Live = LayerMask.GetMask("Live");
    }

    public void Pressed()
    {
        friend.EnableSolid();

        if (PlayerPrefs.GetInt("ButtonActivated") < 40)
        {
            PlayerPrefs.SetInt("ButtonActivated", PlayerPrefs.GetInt("ButtonActivated")+1);
        }
        else if (PlayerPrefs.GetInt("Achieve4") == 0) 
        {
            GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(4);
        }
        Destroyy();
    }

    void Destroyy()
    {
        Destroy(gameObject);
    }
}
