using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsButton : MonoBehaviour
{
    public int lvlCount;
    public int dontUse_lvlCont;
    public string naming;
    public int num;

    public RectTransform rect_transform;
    public bool rect;

    public bool levels_opening;
    public RectTransform Bg;
    public List<Image> colors;

    public List<TMPro.TMP_Text> level_texts;
    public List<GameObject> levelObj, BlockedImage;
    public List<bool> LvlOpened;
    public int curLvl;

    public Menu_Manager menu_manager;
    public ChooseLvl_Button Levels;

    public GameManager gm;

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
        if (rect) rect_transform.anchoredPosition = Vector2.Lerp(rect_transform.anchoredPosition, new Vector2(-125, 340), 2f*Time.deltaTime);
    
        if (levels_opening)
        {
            Bg.localScale = Vector2.Lerp(Bg.localScale, new Vector2(1, 1), 20*Time.deltaTime);
        }

        if (levels_opening)
        {
            for (int i = 0; i < colors.Count; i++)
            {
                if (curLvl+dontUse_lvlCont-1 < gm.colors.Count) 
                {
                    colors[i].color = Color.Lerp(colors[i].color, gm.colors[curLvl+dontUse_lvlCont-1], 4*Time.deltaTime);
                }
            }
        }
    }

    public void PleaseGoLevel(int buttonNum)
    {
        if (LvlOpened[curLvl-num+buttonNum])
        {
            SceneManager.LoadScene(curLvl-num+buttonNum+dontUse_lvlCont);
        }
    }

    void UpdateButtons()
    {
        for (int i = 0; i < num; i++)
        {
            level_texts[i].text = (curLvl-num+i+1).ToString();
            if (curLvl-num+i+dontUse_lvlCont >= lvlCount+dontUse_lvlCont)
            {
                levelObj[i].SetActive(false);
                BlockedImage[i].SetActive(false);
            }
            else
            {
                levelObj[i].SetActive(true);

                if (LvlOpened[curLvl-num+i])
                {
                    BlockedImage[i].SetActive(false);
                }
                else
                {
                    BlockedImage[i].SetActive(true);
                }
            }
        }
        
        LvlOpened.Clear();
        for (int j = 0; j < lvlCount; j++)
        {
            LvlOpened.Add(IntToBool(PlayerPrefs.GetInt("LvlOpened"+(j+dontUse_lvlCont))));
        }
        LvlOpened[0] = true;
    }

    public void Next()
    {
        if (curLvl < lvlCount-2)
        {
            curLvl += 5;
            UpdateButtons();
        }
    }

    public void Back()
    {
        if (curLvl > 5)
        {
            curLvl -= 5;
            UpdateButtons();
        }
    }

    public void Open(bool Close)
    {
        UpdateButtons();
        
        if (menu_manager.CurrentWindow == "Levels" || menu_manager.CurrentWindow == naming)
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
                menu_manager.CurrentWindow = naming;
                menu_manager.BgBack = true;
            }
            else 
            {
                Bg.localScale = Vector2.zero;
                menu_manager.CurrentWindow = "Levels";
                menu_manager.BgBack = false;
                Levels.Open(false);
            }
        }
    }

    void Awake()
    {
        Bg.localScale = Vector2.zero;
        for (int j = 0; j < lvlCount; j++)
        {
            LvlOpened.Add(IntToBool(PlayerPrefs.GetInt("LvlOpened"+(j+dontUse_lvlCont))));
        }
        LvlOpened[0] = true;
        UpdateButtons();
    }
}
