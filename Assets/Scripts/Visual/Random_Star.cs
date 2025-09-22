using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Star : MonoBehaviour
{
    public SpriteRenderer form;
    public List<Sprite> forms;

    public List<SpriteRenderer> add;
    public List<Sprite> adds;

    public List<Transform> rnd_rotate;

    void Awake()
    {
        form.sprite = forms[Random.Range(0, forms.Count)];
        for (int i = 0; i < add.Count; i++)
        {
            if (Random.Range(0, 5) > 3)
            {
                add[i].sprite = adds[Random.Range(0, adds.Count)];
            }
        }
        for (int i = 0; i < rnd_rotate.Count; i++)
        {
            rnd_rotate[i].Rotate(0, 0, Random.Range(0, 360));
        }
    }
}
