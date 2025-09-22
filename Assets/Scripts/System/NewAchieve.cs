using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using YG;

public class NewAchieve : MonoBehaviour
{
    int DownStopUp = 1;
    RectTransform rect_transform;
    public float num;

    public List<bool> Achievements;
    public List<Sprite> Achieve_Image;
    public List<string> naming, description;

    public Image image;
    public TMPro.TMP_Text naming_text, description_text, textNewAchieve;

    public AudioSource au;

    GameObject camer;
    Translater translater;

    public void Achieve(int num)
    {
        PlayerPrefs.SetInt("AchieveNum", num+1);
        image.sprite = Achieve_Image[num];
        naming_text.text = naming[num];
        description_text.text = description[num];
        DownStopUp = 0;
        PlayerPrefs.SetInt("Achieve"+num, 1);
        au.Play();
        Achievements[num] = true;
    }

    void FixedUpdate()
    {
        if (DownStopUp == 0)
        {
            if (rect_transform.localPosition.y > 240*num)
            {
                rect_transform.Translate(Vector3.down*400*Time.deltaTime);
            }
            else 
            {
                DownStopUp = 1;
                PlayerPrefs.SetInt("AchieveNum", 0);
                Invoke("Up", 4);
            }
        }
        if (DownStopUp == 2)
        {
            if (rect_transform.localPosition.y < 440*num)
            {
                rect_transform.Translate(Vector3.up*400*Time.deltaTime);
            }
            else 
            {
                DownStopUp = 1;
            }
        }
    }

    void Up()
    {
        DownStopUp = 2;
    }

    public void LoadLanguage()
    {
        if (PlayerPrefs.GetString("Language") == "ru")
        {
            naming = translater.achieve_name_rus;
            description = translater.achieve_discription_rus;
            textNewAchieve.text = "Новое достижение";
        }
        else if (PlayerPrefs.GetString("Language") == "en")
        {
            naming = translater.achieve_name_eng;
            description = translater.achieve_discription_eng;
            textNewAchieve.text = "New achievement";
        }
        else
        {
            naming = translater.achieve_name_eng;
            description = translater.achieve_discription_eng;
            textNewAchieve.text = "New achievement";
        }
    }

    void Awake()
    {
        camer = GameObject.FindGameObjectWithTag("MainCamera");
        translater = camer.GetComponent<Translater>();
        LoadLanguage();
        rect_transform = GetComponent<RectTransform>();
        if (PlayerPrefs.GetInt("AchieveNum") != 0)
        {
            Achieve(PlayerPrefs.GetInt("AchieveNum")-1);
        }
        for (int j = 0; j < Achievements.Count; j++)
        {
            Achievements[j] = IntToBool(PlayerPrefs.GetInt("Achieve"+j));
        }
    }

    bool IntToBool(int i)
    {
        if (i == 0) return false;
        else return true;
    }
}
