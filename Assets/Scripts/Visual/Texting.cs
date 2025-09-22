using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using YG;

public class Texting : MonoBehaviour
{
    public TMPro.TMP_Text TypingText;
    public string text;

    public int listNum;
    Translater translater;
    
    public int i;
    public AudioSource au;

    GameObject camer;
    CameraCenter cam;
    
    void Start()
    {
        camer = GameObject.FindGameObjectWithTag("MainCamera");
        if (camer != null) cam = camer.GetComponent<CameraCenter>();
        Invoke("Typing", 0.075f);
        InvokeRepeating("CheckDimension", 0.1f, 0.1f);
        translater = camer.GetComponent<Translater>();
        for (int i = 0; i < translater.level_texty_eng.Count; i++)
        {
            if (text == translater.level_texty_eng[i])
            {
                listNum = i;
            }
        }
        LoadLanguage();
    }

    void CheckDimension()
    {
        if (cam)
        {
            if (cam.thisDimension)
            {
                TypingText.color = Color.white;
            }
            else
            {
                TypingText.color = Color.black;
            }
        }
    }

    public void LoadLanguage()
    {
        if (PlayerPrefs.GetString("Language") == "ru")
        {
            text = translater.level_texty_rus[listNum];
        }
        else if (PlayerPrefs.GetString("Language") == "en")
        {
            text = translater.level_texty_eng[listNum];
        }
        else
        {
            text = translater.level_texty_eng[listNum];
        }
    }

    void Typing()
    {
        i++;
        if (i <= text.Length)
        {
            Invoke("Typing", 0.075f);
            TypingText.text = text.Substring(0, i);
            
            if (au != null)
            {
                au.pitch = Random.Range(0.8f, 1.5f);
                au.Play();
            }
        }
        else Invoke("Fade", 4);
    }

    void Fade()
    {
        i--;
        if (i >= 0)
        {
            Invoke("Fade", 0.05f);
            TypingText.text = text.Substring(0, i);
            if (au != null)
            {
                au.pitch = Random.Range(0.5f, 1.1f);
                au.Play();
            }
        }
    }
}
