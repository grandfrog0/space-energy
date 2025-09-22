using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_NextLvl_Script : MonoBehaviour
{
    public bool Lvlend;
    GameObject gmObj;
    GameManager game_manager;
    Menu_Manager menu_manager;
    bool gm;
    AudioSource au;

    void OnMouseDown()
    {
        if (gm && game_manager.lvl_end_bg_faded) 
        {
            game_manager.NextLevel();
            au.Play();
        }
        if (!gm && menu_manager.lvl_end_bg_faded) 
        {
            menu_manager.NextLevel();
            au.Play();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (gm && game_manager.lvl_end_bg_faded) 
            {
                game_manager.NextLevel();
                au.Play();
            }
            if (!gm && menu_manager.lvl_end_bg_faded) 
            {
                menu_manager.NextLevel();
                au.Play();
            }
        }
    }

    void Awake()
    {
        gmObj = GameObject.FindGameObjectWithTag("GameController");
        if (gmObj != null)
        {
            if (gmObj.GetComponent<GameManager>())
            {
                game_manager = gmObj.GetComponent<GameManager>();
                gm = true;
            }
            else if (gmObj.GetComponent<Menu_Manager>())
            {
                menu_manager = gmObj.GetComponent<Menu_Manager>();
                gm = false;
            }
        }
        
        au = GetComponent<AudioSource>();
    }
}
