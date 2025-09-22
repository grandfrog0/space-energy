using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class This_Modifiers : MonoBehaviour
{
    [Header("All image for each icon in order")]
    public List<Sprite> image;

    [Header("In each cell, the selected modifiers")]
    public List<int> modifier;

    [Header("System")]
    public GameObject obj;
    public List<GameObject> modButtons;
    public List<ModButton> modButton;
    GameManager gm;

    void Awake()
    {
        Modifiers.LoadImage(image);
        Modifiers.LoadMod(image.Count);

        gm = GetComponent<GameManager>();
    }

    public void Check(int i)
    {
        Modifiers.Adding(modifier[i]);
        modButtons.Add(Instantiate(obj, gm.canvthis.transform));
        modButtons[i].transform.position = new Vector3(234 + i*150, 990);
        modButton.Add(modButtons[i].GetComponent<ModButton>());
        modButton[i].modifier = modifier[i];
    }

    public void SpCheck(int i)
    {
        Modifiers.Adding(i);
        modButtons.Add(Instantiate(obj, gm.canvthis.transform));
        modButtons[modButtons.Count - 1].transform.position = new Vector3(234 + (modButtons.Count - 1)*150, 990);
        modButton.Add(modButtons[modButtons.Count - 1].GetComponent<ModButton>());
        modButton[modButtons.Count - 1].modifier = i;
    }

    void Start()
    {
        for (int i = 0; i < modifier.Count; i++)
        {
            Check(i);
        }
    }
}
