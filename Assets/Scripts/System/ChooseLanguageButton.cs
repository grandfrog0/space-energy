using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLanguageButton : MonoBehaviour
{
    public string Language;

    public bool levels_opening;
    public RectTransform Bg;
    public Menu_Manager menu_manager;
    public Settings_Button Settings;

    public NewAchieve Achieve;

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
            Open();
        }
    }

    public void LoadLanguage(string Language)
    {
        PlayerPrefs.SetString("Language", Language);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SiteLang(int num)
    {
        PlayerPrefs.SetInt("SiteLang", num);
    }

    void FixedUpdate()
    {
        if (levels_opening)
        {
            Bg.localScale = Vector2.Lerp(Bg.localScale, new Vector2(1, 1), 20*Time.deltaTime);
        }
    }
    
    public void Open()
    {
        if (menu_manager.CurrentWindow == "Settings" || menu_manager.CurrentWindow == "Language")
        {   
            levels_opening = !levels_opening;
            if (levels_opening)
            {
                Bg.localScale = new Vector2(0.9f, 0.9f);
                menu_manager.CurrentWindow = "Language";
                menu_manager.BgBack = true;
                Settings.Language();
            }
            else 
            {
                Bg.localScale = Vector2.zero;
                menu_manager.CurrentWindow = "Settings";
                menu_manager.BgBack = false;
                Settings.Open(false);
            }
        }
        else if (menu_manager.CurrentWindow == "Nothing")
        {
            levels_opening = !levels_opening;
            if (levels_opening)
            {
                Bg.localScale = new Vector2(0.9f, 0.9f);
                menu_manager.CurrentWindow = "Language";
                menu_manager.BgBack = true;
                Settings.Language();
            }
        }
    }

    void Awake()
    {
        Bg.localScale = Vector2.zero;
        Language = PlayerPrefs.GetString("Language");
    }
}
