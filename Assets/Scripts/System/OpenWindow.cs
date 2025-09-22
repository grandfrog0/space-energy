using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWindow : MonoBehaviour
{
    public string naming;
    public Menu_Manager menu_manager;
    bool outside;
    bool levels_opening;
    public RectTransform Bg;

    public void Open(bool Close)
    {
        if (menu_manager.CurrentWindow == "Levels" || menu_manager.CurrentWindow == naming)
        {   
            levels_opening = !Close;
            if (levels_opening)
            {
                Bg.localScale = new Vector2(0.9f, 0.9f);
                menu_manager.CurrentWindow = naming;
                menu_manager.BgBack = true;
            }
            else 
            {
                Bg.localScale = Vector2.zero;
                menu_manager.CurrentWindow = "Levels";
                menu_manager.BgBack = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (levels_opening)
        {
            Bg.localScale = Vector2.Lerp(Bg.localScale, new Vector2(1, 1), 20*Time.deltaTime);
        }
    }

    public void Outside()
    {
        outside = true;
    }

    public void Inside()
    {
        outside = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && outside && levels_opening)
        {
            Open(true);
        }
    }

    void Awake()
    {
        Bg.localScale = Vector2.zero;
    }
}
