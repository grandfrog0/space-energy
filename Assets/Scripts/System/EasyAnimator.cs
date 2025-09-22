using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyAnimator : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> sprites;
    public float time = 5f;
    public float randomCD = 1f;
    public float tick = 0.03f;
    public bool twice = true;

    int cur_sprite;
    bool back;

    void Awake()
    {
        Invoke("PlaySprites", time + Random.Range(-randomCD, randomCD));
    }

    public void PlaySprites()
    {
        Invoke("Next", tick);
        Invoke("PlaySprites", time + Random.Range(-randomCD, randomCD));
    }

    void Next()
    {
        if (sprites.Count <= 0) return;

        if (cur_sprite >= sprites.Count)
        {
            if (!twice)
            {   
                cur_sprite = 0;
                sr.sprite = sprites[cur_sprite];
                return;
            }
            else
            {
                back = true;
                cur_sprite -= 1;
            }
        }
        if (cur_sprite < 0)
        {
            if (!twice)
            {   
                cur_sprite = 0;
                sr.sprite = sprites[cur_sprite];
            }
            else
            {
                back = false;
                cur_sprite += 1;
                return;
            }
        }
        sr.sprite = sprites[cur_sprite];
        if (!back) cur_sprite += 1;
        else cur_sprite -= 1;
        Invoke("Next", tick);
    }
}
