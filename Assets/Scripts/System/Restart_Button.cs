using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart_Button : MonoBehaviour
{
    GameObject cam;
    CameraCenter camc;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        camc = cam.GetComponent<CameraCenter>();
    }

    public void click()
    {
        camc.goLevel_num = SceneManager.GetActiveScene().buildIndex;
        camc.Right();
    }
}
