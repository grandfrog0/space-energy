using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkullCrusher : MonoBehaviour
{
    public GameObject Player;
    P_Move move;
    SkullCrusher_Anim anim;

    void FixedUpdate()
    {
        if (move.DeathCount >= 3)
        {
            anim.cam.goLevel_num = SceneManager.GetActiveScene().buildIndex;
            anim.cam.Right(); 
        }
    }

    void Awake()
    {
        move = Player.GetComponent<P_Move>();
        anim = GetComponent<SkullCrusher_Anim>();
    }
}
