using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchieveButton : MonoBehaviour
{
    public int achieveCount;

    public RectTransform rect_transform;

    public bool levels_opening;
    public RectTransform Bg;

    public TMPro.TMP_Text achieve_texts, description_text;
    public Image Icon;
    public GameObject OpenedImage;
    public int curAchieve;

    public List<bool> AchieveOpened;
    public List<Sprite> Achieve_Image;
    public List<string> naming, description;

    public Menu_Manager menu_manager;
    public NewAchieve Achievement;

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
    
    bool IntToBool(int i)
    {
        if (i == 0) return false;
        else return true;
    }

    void FixedUpdate()
    {
        rect_transform.anchoredPosition = Vector2.Lerp(rect_transform.anchoredPosition, new Vector2(-125, 140), 2f*Time.deltaTime);
    
        if (levels_opening)
        {
            Bg.localScale = Vector2.Lerp(Bg.localScale, new Vector2(1, 1), 20*Time.deltaTime);
        }
    }

    void UpdateButtons()
    {
        achieve_texts.text = naming[curAchieve];
        description_text.text = description[curAchieve];
        Icon.sprite = Achieve_Image[curAchieve];

        if (AchieveOpened[curAchieve])
        {
            OpenedImage.SetActive(true);
        }
        else
        {
            OpenedImage.SetActive(false);
        }
        
        AchieveOpened.Clear();
        for (int j = 0; j < achieveCount; j++)
        {
            AchieveOpened.Add(IntToBool(PlayerPrefs.GetInt("Achieve"+j)));
        }
    }

    public void Next()
    {
        if (curAchieve < achieveCount-1)
        {
            curAchieve += 1;
        }
        UpdateButtons();
    }

    public void Back()
    {
        if (curAchieve > 0)
        {
            curAchieve -= 1;
        }
        UpdateButtons();
    }

    public void Open(bool Close)
    {
        UpdateButtons();
        
        if (menu_manager.CurrentWindow == "Nothing" || menu_manager.CurrentWindow == "Achievements")
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
                menu_manager.CurrentWindow = "Achievements";
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
        achieveCount = Achievement.Achievements.Count;
        for (int j = 0; j < achieveCount; j++)
        {
            AchieveOpened.Add(IntToBool(PlayerPrefs.GetInt("Achieve"+j)));
        }
        for (int i = 0; i < achieveCount; i++)
        {
            AchieveOpened = Achievement.Achievements;
            Achieve_Image = Achievement.Achieve_Image;
            naming = Achievement.naming;
            description = Achievement.description;
        }
        UpdateButtons();
    }
}
