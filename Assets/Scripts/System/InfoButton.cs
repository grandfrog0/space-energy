using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButton : MonoBehaviour
{
    public RectTransform rect_transform;

    public bool levels_opening;
    public RectTransform Bg;
    public Info_Texting text;

    public Menu_Manager menu_manager;
    public Settings_Button Settings;

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
        if (levels_opening)
        {
            Bg.localScale = Vector2.Lerp(Bg.localScale, new Vector2(1, 1), 20*Time.deltaTime);
        }
    }

    public void Open(bool Close)
    {
        if (menu_manager.CurrentWindow == "Settings" || menu_manager.CurrentWindow == "Info")
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
                menu_manager.CurrentWindow = "Info";
                menu_manager.BgBack = true;
                text.Blablabla = true;
                text.Invoke("Typing", 0.075f);

                Settings.Info();
            }
            else 
            {
                Bg.localScale = Vector2.zero;
                menu_manager.CurrentWindow = "Settings";
                menu_manager.BgBack = false;
                text.Blablabla = false;
                Settings.Open(false);
            }
        }
    }

    void Awake()
    {
        Bg.localScale = Vector2.zero;
    }
}
