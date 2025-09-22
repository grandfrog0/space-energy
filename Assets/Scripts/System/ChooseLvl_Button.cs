using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLvl_Button : MonoBehaviour
{
    public bool levels_opening;
    public RectTransform Bg;

    public List<TMPro.TMP_Text> level_texts;
    public List<GameObject> BlockedImage;
    public List<bool> LvlOpened;

    public Menu_Manager menu_manager;
    
    public List<LevelsButton> lb;

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
        if (levels_opening)
        {
            Bg.localScale = Vector2.Lerp(Bg.localScale, new Vector2(1, 1), 20*Time.deltaTime);
        }
    }

    void UpdateButtons()
    {
        for (int i = 0; i < BlockedImage.Count; i++)
        {
            // level_texts[i].text = (curLvl-4+i).ToString(); сделать перевод
            if (LvlOpened[i])
            {
                BlockedImage[i].SetActive(false);
            }
            else
            {
                BlockedImage[i].SetActive(true);
            }
        }
        
        LvlOpened.Clear();
        for (int j = 0; j < 3; j++)
        {
            LvlOpened.Add(IntToBool(PlayerPrefs.GetInt("LvlChooseOpened"+j)));
        }
        LvlOpened[0] = true;
    }

    public void OpenSub(int num)
    {
        if (LvlOpened[num])
        {
            lb[num].Open(false);
        }
    }

    public void Open(bool Close)
    {
        UpdateButtons();
        
        if (menu_manager.CurrentWindow == "Nothing" || menu_manager.CurrentWindow == "Levels")
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
                menu_manager.CurrentWindow = "Levels";
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
        if (PlayerPrefs.GetInt("Achieve11") == 1) PlayerPrefs.SetInt("LvlChooseOpened1", 1);
        PlayerPrefs.SetInt("LvlChooseOpened0", 1);
        for (int j = 0; j < 3; j++)
        {
            LvlOpened.Add(IntToBool(PlayerPrefs.GetInt("LvlChooseOpened"+j)));
        }
        UpdateButtons();
    }
}
