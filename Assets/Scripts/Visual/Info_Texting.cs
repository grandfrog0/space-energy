using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using YG;

public class Info_Texting : MonoBehaviour
{
    public TMPro.TMP_Text TypingText;
    public List<string> text;

    public int num; 
    public int listNum;
    GameObject camer;
    Translater translater;
    
    public int i;
    public AudioSource au;

    public bool Blablabla;

    void Start()
    {
        camer = GameObject.FindGameObjectWithTag("MainCamera");
        translater = camer.GetComponent<Translater>();
        text = translater.level_texty_info_eng;
        LoadLanguage();
    }

    public void LoadLanguage()
    {
        if (PlayerPrefs.GetString("Language") == "ru")
        {
            text = translater.level_texty_info_rus;
        }
        else if (PlayerPrefs.GetString("Language") == "en")
        {
            text = translater.level_texty_info_eng;
        }
        else
        {
            text = translater.level_texty_info_eng;
        }
    }

    public void Typing()
    {
        if (Blablabla)
        {
            i++;
            if (i <= text[num].Length)
            {
                Invoke("Typing", 0.075f);
                TypingText.text = text[num].Substring(0, i);
                au.pitch = Random.Range(0.8f, 1.5f);
                au.Play();
            }
            else Invoke("Next", 2);
        }
    }

    void Next()
    {
        if (num < text.Count-1) 
        {
            num++;
            i = 0;
            Typing();
        }
        else
        {
            if (PlayerPrefs.GetInt("Achieve13") == 0) 
            {
                GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(13);
            }
        }
    }
}
