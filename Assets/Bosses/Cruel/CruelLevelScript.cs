using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CruelLevelScript : MonoBehaviour
{
    List<P_Move> move = new List<P_Move>();
    int DeathCount;
    public CameraCenter cam;

    public int timeLeft = 10;
    int timeStart = 0;
    public Transform bossbar1, bossbar2;
    public AudioSource au;
    public Animator anim, hand1, hand2;
    public GameObject hand1s, hand2s;
    public Animator hand1a, hand2a;
    public MoveBySingal energies;

    void FixedUpdate()
    {
        DeathCount = 0;
        for (int i = 0; i < move.Count; i++)
        {
            DeathCount += move[i].DeathCount;
        }
        if (DeathCount >= 5)
        {
            cam.goLevel_num = SceneManager.GetActiveScene().buildIndex;
            cam.Game_over_play();
            cam.Right();
        }
    }

    public void End()
    {
        energies.Go();
        hand1s.SetActive(false);
        hand2s.SetActive(false);
    }

    void Timer()
    {
        timeLeft -= 1;
        bossbar1.Translate(14f / timeStart, 0, 0);
        bossbar2.localScale += new Vector3(3.5f / timeStart, 0, 0);
        bossbar2.Translate(7f / timeStart, 0, 0);
        if (timeLeft <= 0)
        {
            au.Play();
            hand1.Play("HandDied");
            hand2.Play("HandDied");
            anim.Play("Died");
            hand1a.enabled = false;
            hand2a.enabled = false;
        }
        else Invoke("Timer", 1);
    }

    void Awake()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            move.Add(player[i].GetComponent<P_Move>());
        }
        Invoke("Timer", 1);
        timeStart = timeLeft;
    }
}
