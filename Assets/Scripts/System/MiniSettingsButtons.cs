using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniSettingsButtons : MonoBehaviour
{
    int lvlCount;
    int achieveCount;

    GameObject cam;
    CameraCenter camc;

    void Start()
    {
        lvlCount = SceneManager.sceneCountInBuildSettings;
        GameObject Achievement = GameObject.FindGameObjectWithTag("AchieveWindow");
        NewAchieve achthis = Achievement.GetComponent<NewAchieve>();
        achieveCount = achthis.Achievements.Count;

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        camc = cam.GetComponent<CameraCenter>();
    }

    public void GoMenu()
    {
        camc.goLevel_num = 0;
        camc.Right();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ResetLevelProgress()
    {
        PlayerPrefs.DeleteAll(); 
        SceneManager.LoadScene(0);
    }
}
