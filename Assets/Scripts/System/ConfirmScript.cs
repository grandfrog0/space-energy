using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmScript : MonoBehaviour
{
    public bool levels_opening;
    public RectTransform Bg;
    public Menu_Manager menu_manager;

    string LastWindow;

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
            Open(LastWindow);
        }
    }
    
    public void Yes(string make)
    {
        Invoke(make, 0);
    }

    public void No()
    {
        if (levels_opening) Open(LastWindow);
    }

    void ResetLevelProgress()
    {
        PlayerPrefs.DeleteAll(); 
        SceneManager.LoadScene(0);
    }

    void FixedUpdate()
    {
        if (levels_opening)
        {
            Bg.localScale = Vector2.Lerp(Bg.localScale, new Vector2(1, 1), 20*Time.deltaTime);
        }
    }
    
    public void Open(string CurrentWindow)
    {
        if (menu_manager.CurrentWindow == CurrentWindow || menu_manager.CurrentWindow == "Confirm")
        {   
            levels_opening = !levels_opening;
            LastWindow = CurrentWindow;
            if (levels_opening)
            {
                Bg.localScale = new Vector2(0.9f, 0.9f);
                menu_manager.CurrentWindow = "Confirm";
                menu_manager.BgBack = true;
            }
            else 
            {
                Bg.localScale = Vector2.zero;
                menu_manager.CurrentWindow = CurrentWindow;
                menu_manager.BgBack = false;
            }
        }
    }

    void Awake()
    {
        Bg.localScale = Vector2.zero;
    }
}
