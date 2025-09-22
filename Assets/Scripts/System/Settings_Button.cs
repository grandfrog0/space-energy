using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings_Button : MonoBehaviour
{
    public RectTransform rect_transform;

    public bool levels_opening;
    public RectTransform Bg;

    public Menu_Manager menu_manager;
    
    bool outside;

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

    void FixedUpdate()
    {
        rect_transform.anchoredPosition = Vector2.Lerp(rect_transform.anchoredPosition, new Vector2(125, 340), 2f*Time.deltaTime);
    
        if (levels_opening)
        {
            Bg.localScale = Vector2.Lerp(Bg.localScale, new Vector2(1, 1), 20*Time.deltaTime);
        }
    }

    public void Language()
    {
        levels_opening = false;
        Bg.localScale = Vector2.zero;
        menu_manager.CurrentWindow = "Language";
        menu_manager.BgBack = true;
    }

    public void Info()
    {
        levels_opening = false;
        Bg.localScale = Vector2.zero;
        menu_manager.CurrentWindow = "Info";
        menu_manager.BgBack = true;
    }

    public void Open(bool Close)
    {
        if (menu_manager.CurrentWindow == "Nothing" || menu_manager.CurrentWindow == "Settings")
        {
            if (!Close)
            {
                levels_opening = !levels_opening;
            }
            else
            {
                levels_opening = false;
            }
            if (levels_opening)
            {
                Bg.localScale = new Vector2(0.9f, 0.9f);
                menu_manager.CurrentWindow = "Settings";
                menu_manager.BgBack = true;
            }
            else 
            {
                Bg.localScale = Vector2.zero;
                menu_manager.CurrentWindow = "Nothing";
                menu_manager.BgBack = false;
            }
        }
    }

    void Awake()
    {
        Bg.localScale = Vector2.zero;
    }
}
