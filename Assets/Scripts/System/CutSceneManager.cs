using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using YG;

public class CutSceneManager : MonoBehaviour
{
    GameManager game_manager;
    Animator finger;
    public GameObject Player;

    public TMPro.TMP_Text swipe;

    void Awake()
    {
        game_manager = GetComponent<GameManager>();
        if (GameObject.FindGameObjectWithTag("Finger")) finger = GameObject.FindGameObjectWithTag("Finger").GetComponent<Animator>();
        LoadLanguage();
    }

    void FixedUpdate()
    {
        if (game_manager.level == 1)
        {
            if (Player.transform.position.x < -6.5f && Player.transform.position.y < 2.5f)
            {
                finger.Play("Lvl1_2");
            }
            if (Player.transform.position.y > 2.5f)
            {
                finger.Play("Lvl1_3");
            }
        }
    }
    
    public void LoadLanguage()
    {
        if (PlayerPrefs.GetString("Language") == "ru")
        {
            swipe.text = "Проведи по экрану";
        }
        else if (PlayerPrefs.GetString("Language") == "en")
        {
            swipe.text = "Swipe";
        }
        else
        {
            swipe.text = "Swipe";
        }
    }
}
