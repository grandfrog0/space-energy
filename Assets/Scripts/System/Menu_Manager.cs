using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using YG;

public class Menu_Manager : MonoBehaviour
{
    public Camera cam;
    public Translater translater;
    public GameObject LevelEndBg;

    public CameraCenter camc;
    public List<TMPro.TMP_Text> menu_texts;

    public List<GameObject> VisualItems;
    public List<GameObject> vi;
    public List<Rigidbody2D> rb;
    public List<Transform> tr;

    public bool lvl_end_bg_faded;
    public AudioSource au;

    public bool BgBack;
    public string CurrentWindow;

    public LevelsButton levelsButton;
    public int LvlOpened_Last, lvlCount;
    public List<int> LvlOpened;

    public Image muteImage;
    public Color muted, unmuted;
    bool Muted;

    public ChooseLanguageButton Language;

    // public TMPro.TMP_Text lang;

    public void OnOff()
    {
        Muted = !Muted;
        if (Muted) 
        {
            muteImage.color = muted;
        }
        if (!Muted) 
        {
            muteImage.color = unmuted;
        }
    }

    public void NextLevel()
    {
        camc.Right();
    }

    void FixedUpdate()
    {
        if (LevelEndBg.transform.localScale.y > 25) 
        {
            if (!lvl_end_bg_faded) au.Play();
            lvl_end_bg_faded = true;
        }
        else
        {
            lvl_end_bg_faded = false;
        }
        if (Mathf.Abs(cam.orthographicSize - camc.camScale) < 0.4f && !BgBack)
        {
            if (LevelEndBg.transform.localScale.y < 37) 
            {
                LevelEndBg.transform.localScale = Vector3.Lerp(LevelEndBg.transform.localScale, new Vector3(12, 40, 0), 4f*Time.deltaTime);
            } 
        }
        if (BgBack)
        {
            LevelEndBg.transform.localScale = Vector3.Lerp(LevelEndBg.transform.localScale, Vector3.zero, 10f*Time.deltaTime);
        }

        if (camc.fade.color.a >= 1)
        {
            if (camc.goLevel_num == -1)
            {
                for (int i = 0; i < lvlCount; i++)
                {
                    if (LvlOpened[i] == 1 && i != 51)
                    {
                        LvlOpened_Last = i;
                    }
                }
                SceneManager.LoadScene(LvlOpened_Last);
            }
            else SceneManager.LoadScene(camc.goLevel_num);
        }
    }

    public void LoadLanguage()
    {
        if (PlayerPrefs.GetInt("SiteLang") == 0) //0 - можно менять языком сайта, 1 - статичный язык
        {
            // Language.Open();
            PlayerPrefs.SetString("Language", YandexGame.EnvironmentData.language);
        }
        
        if (PlayerPrefs.GetString("Language") == "ru")
        {
            for (int j = 0; j < menu_texts.Count; j++)
            {
                menu_texts[j].text = translater.menu_texts_rus[j];
            }
        }
        else if (PlayerPrefs.GetString("Language") == "en")
        {
            for (int j = 0; j < menu_texts.Count; j++)
            {
                menu_texts[j].text = translater.menu_texts_eng[j];
            }
        }
        else
        {
            for (int j = 0; j < menu_texts.Count; j++)
            {
                menu_texts[j].text = translater.menu_texts_eng[j];
            }
        }
    }

    void Start()
    {
        for (int i = 0; i < VisualItems.Count; i++)
        {
            vi[i] = Instantiate(VisualItems[i], new Vector3(-50, -50, 0), Quaternion.identity);
            rb[i] = vi[i].GetComponent<Rigidbody2D>();
        }
        InvokeRepeating("FallSth", 7f, 7f);
        if (PlayerPrefs.GetInt("Muted") == 0) 
        {
            Muted = false;
            
            muteImage.color = unmuted;
        }
        else
        {
            Muted = true;

            muteImage.color = muted;
        }
        
        LoadLanguage();
        // lang.text = YandexGame.EnvironmentData.language;
    }

    void Awake()
    {
        lvlCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < lvlCount; i++)
        {
            LvlOpened.Add(PlayerPrefs.GetInt("LvlOpened"+(i)));
        }
    }

    void FallSth()
    {
        int r = Random.Range(0, vi.Count);
        int g = Random.Range(0, tr.Count);
        float s = Random.Range(0.75f, 2.25f);
        vi[r].transform.position = tr[g].position;
        vi[r].transform.rotation = tr[g].rotation;
        vi[r].transform.localScale = new Vector3(s, s, 1);
        rb[r].velocity = Vector2.zero;
        rb[r].AddForce(tr[g].transform.up*-20*vi[r].transform.localScale.x + tr[g].transform.right*Random.Range(-2f, 2f), ForceMode2D.Impulse);
        rb[r].angularVelocity = Random.Range(-720, 720);
    }

    void Update()
    {
        if (lvl_end_bg_faded && Input.GetKeyDown(KeyCode.Return)) NextLevel();
        if (Input.GetKeyDown(KeyCode.Escape)) Screen.fullScreen = !Screen.fullScreen;
    }
}
