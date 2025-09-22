using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using YG;

public class Shop_Manager : MonoBehaviour
{
    public List<SkinPackage> skinPackage;
    public List<Transform> pack;
    public List<int> num;
    public List<int> skin;
    public int axis;

    public List<SpriteRenderer> head1, head2, mouse1, mouse2, eyes1, eyes2, energy;
    public List<AudioSource> swipe, die;
    public List<TMPro.TMP_Text> naming_text, anim;

    // pack[num[0/1/2]] == упаковка слева/в центре/справа
    // skinPackage[skin[0/1/2]] == скин слева/в центре/справа

    public GameObject if_skin_opened, unless_skin_opened;
    public int CurSkin_adWatched;
    public TMPro.TMP_Text adWatched;

    GameObject cam;
    CameraCenter camc;

    void FixedUpdate()
    {
        pack[num[0]].position = Vector3.Lerp(pack[num[0]].position, new Vector3(-16, -3), 5*Time.deltaTime);
        pack[num[0]].localScale = Vector3.Lerp(pack[num[0]].localScale, new Vector3(0.5f, 0.5f), 5*Time.deltaTime);
    
        pack[num[1]].position = Vector3.Lerp(pack[num[1]].position, new Vector3(0, 3), 5*Time.deltaTime);
        pack[num[1]].localScale = Vector3.Lerp(pack[num[1]].localScale, new Vector3(1, 1), 5*Time.deltaTime);
    
        pack[num[2]].position = Vector3.Lerp(pack[num[2]].position, new Vector3(16, -3), 5*Time.deltaTime);
        pack[num[2]].localScale = Vector3.Lerp(pack[num[2]].localScale, new Vector3(0.5f, 0.5f), 5*Time.deltaTime);
    

        if (camc.fade.color.a >= 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GoLeft() //стрелка вправо
    {
        for (int i = 0; i < 3; i++)
        {
            num[i]++;
            skin[i]++;
        }
        CheckValues();
        
        pack[num[2]].position = new Vector3(16, -3);
        pack[num[2]].localScale = new Vector3(0.5f, 0.5f, 1);
        SetSkins();
    }

    public void GoRight() //стрелка влево
    {
        for (int i = 0; i < 3; i++)
        {
            num[i]--;
            skin[i]--;
        }
        CheckValues();
        
        pack[num[0]].position = new Vector3(-16, -3);
        pack[num[0]].localScale = new Vector3(0.5f, 0.5f, 1);
        SetSkins();
    }

    public void CheckValues()
    {
        for (int i = 0; i < 3; i++)
        {
            if (num[i] < 0) num[i] = 2;
            if (num[i] > 2) num[i] = 0;
            
            if (skin[i] < 0) skin[i] = skinPackage.Count-1;
            if (skin[i] > skinPackage.Count-1) skin[i] = 0;
        }
        CurSkin_adWatched = PlayerPrefs.GetInt("AdWatched_"+skinPackage[skin[1]].naming_eng);
        if (CurSkin_adWatched >= skinPackage[skin[1]].AdCount)
        {
            if_skin_opened.SetActive(true);
            unless_skin_opened.SetActive(false);
        }
        else
        {
            if_skin_opened.SetActive(false);
            unless_skin_opened.SetActive(true);
            adWatched.text = CurSkin_adWatched+"/"+skinPackage[skin[1]].AdCount;
        }
    }

    public void SetSkins()
    {
        for (int i = 0; i < 3; i++)
        {
            head1[num[i]].sprite = skinPackage[skin[i]].head;
            head2[num[i]].sprite = skinPackage[skin[i]].head;
            mouse1[num[i]].sprite = skinPackage[skin[i]].mouse;
            mouse2[num[i]].sprite = skinPackage[skin[i]].mouse;
            eyes1[num[i]].sprite = skinPackage[skin[i]].eyes;
            eyes2[num[i]].sprite = skinPackage[skin[i]].eyes;
            energy[num[i]].sprite = skinPackage[skin[i]].energy;

            swipe[num[i]].clip = skinPackage[skin[i]].swipe;
            die[num[i]].clip = skinPackage[skin[i]].die;

            if (PlayerPrefs.GetString("Language") == "ru")
            {
                naming_text[num[i]].text = skinPackage[skin[i]].naming_rus;
                anim[num[i]].text = "+особая анимация";
            }
            else if (PlayerPrefs.GetString("Language") == "en")
            {
                naming_text[num[i]].text = skinPackage[skin[i]].naming_eng;
                anim[num[i]].text = "+special animation";
            }
            else
            {
                naming_text[num[i]].text = skinPackage[skin[i]].naming_eng;
                anim[num[i]].text = "+special animation";
            }

            if (!skinPackage[skin[i]].hasAnim) anim[num[i]].text = "";
        }
        if (CurSkin_adWatched >= skinPackage[skin[1]].AdCount) PlayerPrefs.SetInt("CurSkin", skin[1]);
    }

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        camc = cam.GetComponent<CameraCenter>();

        CheckValues();
        pack[num[0]].position = new Vector3(-16, -3);
        pack[num[0]].localScale = new Vector3(0.5f, 0.5f, 1);

        pack[num[1]].position = new Vector3(0, 3);
        pack[num[1]].localScale = new Vector3(1, 1, 1);
        
        pack[num[2]].position = new Vector3(16, -3);
        pack[num[2]].localScale = new Vector3(0.5f, 0.5f, 1);

        CheckValues();
        SetSkins();
    }
}
