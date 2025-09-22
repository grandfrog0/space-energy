using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopButton : MonoBehaviour
{
    public RectTransform rect_transform;

    GameObject cam;
    CameraCenter camc;

    void FixedUpdate()
    {
        rect_transform.anchoredPosition = Vector2.Lerp(rect_transform.anchoredPosition, new Vector2(-125, -60), 2f*Time.deltaTime);
    }

    public void GoShop()
    {
        camc.goLevel_num = 51;
        camc.Right();
    }

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        camc = cam.GetComponent<CameraCenter>();
    }
}
