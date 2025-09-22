using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModButton : MonoBehaviour
{
    public int modifier;
    public Image image;

    GameObject ModInfoWindow;
    ModifiersManager info;

    void Awake()
    {        
        ModInfoWindow = GameObject.FindGameObjectWithTag("ModInfoWindow");
        info = ModInfoWindow.GetComponent<ModifiersManager>();
    }

    void Start()
    {
        image.sprite = Modifiers.GetImage(modifier);
    }

    public void Open()
    {
        info.Open(modifier);
    }
}
