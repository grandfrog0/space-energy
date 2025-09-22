using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using YG;

public class GameManager : MonoBehaviour
{
    [Header("Level")]
    public int level;
    public int level_text;
    public List<string> lvlName;
    public int Restart_Distance;

    public GameObject canvas;
    TMPro.TMP_Text textLvl;
    public GameObject canvthis, achthis;
    public GameObject Achieve;
    NewAchieve Achievement;

    public List<Color> colors;

    [Header("Debug")]
    public P_Collision p_coll;
    public P_Move p_move;

    //two players modifier
    public List<P_Collision> p_colls;
    public List<P_Move> p_moves;
    public bool modifier2_used;
    public int energies;

    [Header("Scene")]
    public GameObject LevelEndBg;
    public Transform playMask;
    public bool lvl_end_bg_fade, lvl_end_bg_faded;

    GameObject camer;
    CameraCenter cam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Screen.fullScreen = !Screen.fullScreen;
    }

    void Awake()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        if (level < 51) level_text = level;
        else level_text = level - 51;
        LevelEndBg = GameObject.FindGameObjectWithTag("Lvlendbg");
        playMask = GameObject.FindGameObjectWithTag("playMask").transform;
        canvthis = Instantiate(canvas, transform.position, transform.rotation);
        textLvl = canvthis.GetComponentInChildren<TMPro.TMP_Text>();
        LoadLanguage();
        if (level < colors.Count) textLvl.color = colors[level];
        camer = GameObject.FindGameObjectWithTag("MainCamera");
        cam = camer.GetComponent<CameraCenter>();
        Instantiate(Achieve, transform.position, transform.rotation);
        achthis = GameObject.FindGameObjectWithTag("AchieveWindow");
        Achievement = achthis.GetComponent<NewAchieve>();
    }

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            p_colls.Add(players[i].GetComponent<P_Collision>());
            p_moves.Add(players[i].GetComponent<P_Move>());
        }
    }

    public void LoadLanguage()
    {
        if (PlayerPrefs.GetString("Language") == "ru")
        {
            if (lvlName.Count > 1 && lvlName[1] != null)
            {
                textLvl.text = lvlName[1];
            }
            else
            {
                textLvl.text = "уровень "+level_text.ToString();
            }
        }
        else if (PlayerPrefs.GetString("Language") == "en")
        {
            if (lvlName.Count > 0 && lvlName[0] != null)
            {
                textLvl.text = lvlName[0];
            }
            else
            {
                textLvl.text = "level "+level_text.ToString();
            }
        }
        else
        {
            if (lvlName.Count > 0 && lvlName[0] != null)
            {
                textLvl.text = lvlName[0];
            }
            else
            {
                textLvl.text = "level "+level_text.ToString();
            }
            Debug.Log(PlayerPrefs.GetString("Language"));
        }
    }

    public void LevelCompleted()
    {
        if (!modifier2_used) lvl_end_bg_fade = true;
        else if (energies < p_moves.Count) energies++;
        if (energies >= p_moves.Count) lvl_end_bg_fade = true;
    }

    public void NextLevel()
    {
        cam.Right();
    }

    void FixedUpdate()
    {
        modifier2_used = Modifiers.GetMod(2); //two players modifier
        if (cam.fade.color.a >= 1)
        {
            if (cam.goLevel_num != -1)
            {
                SceneManager.LoadScene(cam.goLevel_num);
            }
        }
        if (lvl_end_bg_fade)
        {
            if (LevelEndBg.transform.localScale.y < 37) 
            {
                LevelEndBg.transform.localScale = Vector3.Lerp(LevelEndBg.transform.localScale, new Vector3(15, 40, 0), 5f*Time.deltaTime);
            }
            if (LevelEndBg.transform.localScale.y > 25) 
            {
                lvl_end_bg_faded = true;
            }
            if (cam.fade.color.a >= 1)
            {
                if (cam.goLevel_num == -1)
                {
                    if (level < 50 || level > 51 && level < SceneManager.sceneCountInBuildSettings-1) //levels without Shop scene
                    {
                        SceneManager.LoadScene(level+1);
                    }
                    if (level == 50)
                    {
                        if (PlayerPrefs.GetInt("AchieveNum") == 0 && PlayerPrefs.GetInt("Achieve11") == 0) SetAchievement(11);
                        SceneManager.LoadScene(0);
                    }

                    if (level >= SceneManager.sceneCountInBuildSettings-1) //another levels
                    {
                        if (PlayerPrefs.GetInt("AchieveNum") == 0 && PlayerPrefs.GetInt("Achieve0") == 0) SetAchievement(0);
                        SceneManager.LoadScene(0);
                    }
                }
            }
            playMask.localPosition = Vector3.Lerp(playMask.localPosition, Vector3.forward*10, 5*Time.deltaTime);
            playMask.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 10);
        }
        if (!modifier2_used)
        {
            if (Vector3.Distance(p_move.transform.position, Vector3.zero) > Restart_Distance && p_move.isLive)
            {
                p_move.isLive = false;
                p_move.ResetVelocity();
                p_move.Current_Animation = "Die";
                p_move.anim.Animation("Die");
                if (PlayerPrefs.GetInt("SpaceFalls") < 30)
                {
                    PlayerPrefs.SetInt("SpaceFalls", PlayerPrefs.GetInt("SpaceFalls")+1);
                }
                else if (PlayerPrefs.GetInt("SpaceFalls") == 30 && PlayerPrefs.GetInt("Achieve1") == 0) 
                {
                    SetAchievement(1);
                }
            }
        }
        else for (int i = 0; i < p_moves.Count; i++)
        {
            if (Vector3.Distance(p_moves[i].transform.position, Vector3.zero) > Restart_Distance && p_moves[i].isLive)
            {
                p_moves[i].isLive = false;
                p_moves[i].ResetVelocity();
                p_moves[i].Current_Animation = "Die";
                p_moves[i].anim.Animation("Die");
                if (PlayerPrefs.GetInt("SpaceFalls") < 30)
                {
                    PlayerPrefs.SetInt("SpaceFalls", PlayerPrefs.GetInt("SpaceFalls")+1);
                }
                else if (PlayerPrefs.GetInt("SpaceFalls") == 30 && PlayerPrefs.GetInt("Achieve1") == 0) 
                {
                    SetAchievement(1);
                }
            }
        }
    }

    public void SetAchievement(int num)
    {
        Achievement.Achieve(num);
    }

    int BoolToInt(bool i)
    {
        if (!i) return 0;
        else return 1;
    }
}
