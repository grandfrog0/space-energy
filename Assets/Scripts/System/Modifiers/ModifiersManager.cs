using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifiersManager : MonoBehaviour
{
    public bool levels_opening;
    public RectTransform Bg;

    bool outside;

    public GameObject cam;
    Translater translater;

    public TMPro.TMP_Text modifier_name, modifier_description;
    public Image image;

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
            Open(-1);
        }
    }

    public void LoadLanguage(int modifier)
    {
        if (PlayerPrefs.GetString("Language") == "ru")
        {
            modifier_name.text = translater.modifier_name_rus[modifier];
            modifier_description.text = translater.modifier_description_rus[modifier];
        }
        else if (PlayerPrefs.GetString("Language") == "en")
        {
            modifier_name.text = translater.modifier_name_eng[modifier];
            modifier_description.text = translater.modifier_description_eng[modifier];
        }
        else
        {
            modifier_name.text = translater.modifier_name_eng[modifier];
            modifier_description.text = translater.modifier_description_eng[modifier];
        }
        LoadImage(modifier);
    }

    public void LoadImage(int modifier)
    {
        image.sprite = Modifiers.GetImage(modifier);
    }

    void FixedUpdate()
    {
        if (levels_opening)
        {
            Bg.localScale = Vector2.Lerp(Bg.localScale, new Vector2(0.5f, 0.5f), 20*Time.deltaTime);
        }
    }
    
    public void Open(int modifier)
    {
        if (modifier >= 0)
        {
            LoadLanguage(modifier);
        }
        levels_opening = !levels_opening;
        if (levels_opening)
        {
            Bg.localScale = new Vector2(0.4f, 0.4f);
        }
        else 
        {
            Bg.localScale = Vector2.zero;
        }
    }

    void Awake()
    {
        Bg.localScale = Vector2.zero;

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        translater = cam.GetComponent<Translater>();
    }
}
