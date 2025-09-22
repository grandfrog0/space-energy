using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoPlayersButtonScript : MonoBehaviour
{
    public string naming;
    public List<GameObject> BlockedImage;
    public List<Image> circles;
    public List<int> endLvl_circle;
    public Color colorActive, colorDisactive;

    public int startLvl, endLvl, curLvl;
    public List<int> levels;
    public List<int> LvlOpened;

    public Menu_Manager menu_manager;
    bool outside;
    bool levels_opening;
    public RectTransform Bg;

    public void LetsStart(int val)
    {
        if (startLvl + val < endLvl && LvlOpened[val] == 1) SceneManager.LoadScene(startLvl + val);
    }

    void Awake()
    {
        Bg.localScale = Vector2.zero;
        levels = LevelsRange.Range(startLvl, endLvl);
        curLvl = startLvl;
        UpdateButtons();
    }

    void UpdateButtons()
    {   
        LvlOpened.Clear();
        for (int i = 0; i < levels.Count; i++)
        {
            LvlOpened.Add(PlayerPrefs.GetInt("LvlOpened"+(levels[i])));
            if (LvlOpened[i] != 0)
            {
                curLvl = levels[i];
            }
        }
        LvlOpened[0] = 1;
        
        for (int i = 0; i < LvlOpened.Count; i++)
        {
            if (LvlOpened[i] == 1)
            {
                BlockedImage[i].SetActive(false);
            }
            else
            {
                BlockedImage[i].SetActive(true);
            }
        }

        int val = 0;
        for (int i = 0; i < endLvl_circle.Count; i++)
        {
            if (i < endLvl - startLvl && LvlOpened[i] == 1)
            {
                for (int j = val; j < endLvl_circle[i]; j++)
                {
                    circles[j].color = colorActive;
                }
            }
            else
            {
                for (int j = val; j < endLvl_circle[i]; j++)
                {
                    circles[j].color = colorDisactive;
                }
            }
            val = endLvl_circle[i];
        }
    }

    public void Open(bool Close)
    {
        UpdateButtons();
        
        if (menu_manager.CurrentWindow == "ChallengeLevels" || menu_manager.CurrentWindow == naming)
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
                menu_manager.CurrentWindow = "ChallengeLevels";
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
}
