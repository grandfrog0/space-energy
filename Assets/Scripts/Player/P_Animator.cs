using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Animator : MonoBehaviour
{
    Animator anim;
    public bool DieSeted;

    public List<SkinPackage> skinPackage;
    public int curSkin;

    public SpriteRenderer head, eyes, mouse, energy;
    public EasyAnimator ea_head, ea_eyes, ea_mouse, ea_energy;
    public AudioSource swipe, die;

    void Awake()
    {
        anim = GetComponent<Animator>();

        curSkin = PlayerPrefs.GetInt("CurSkin");
        SetSkin();
    }

    void SetSkin()
    {
        head.sprite = skinPackage[curSkin].head;
        mouse.sprite = skinPackage[curSkin].mouse;
        eyes.sprite = skinPackage[curSkin].eyes;
        energy.sprite = skinPackage[curSkin].energy;

        if (skinPackage[curSkin].animed_head != null) ea_head.sprites = skinPackage[curSkin].animed_head;
        if (skinPackage[curSkin].animed_mouse != null) ea_mouse.sprites = skinPackage[curSkin].animed_mouse;
        if (skinPackage[curSkin].animed_eyes != null) ea_eyes.sprites = skinPackage[curSkin].animed_eyes;
        if (skinPackage[curSkin].animed_energy != null) ea_energy.sprites = skinPackage[curSkin].animed_energy;

        if (skinPackage[curSkin].time_head != 0) ea_head.time = skinPackage[curSkin].time_head;
        if (skinPackage[curSkin].time_mouse != 0) ea_mouse.time = skinPackage[curSkin].time_mouse;
        if (skinPackage[curSkin].time_eyes != 0) ea_eyes.time = skinPackage[curSkin].time_eyes;
        if (skinPackage[curSkin].time_energy != 0) ea_energy.time = skinPackage[curSkin].time_energy;

        if (skinPackage[curSkin].tick_head != 0) ea_head.tick = skinPackage[curSkin].tick_head;
        if (skinPackage[curSkin].tick_mouse != 0) ea_mouse.tick = skinPackage[curSkin].tick_mouse;
        if (skinPackage[curSkin].tick_eyes != 0) ea_eyes.tick = skinPackage[curSkin].tick_eyes;
        if (skinPackage[curSkin].tick_energy != 0) ea_energy.tick = skinPackage[curSkin].tick_energy;

        swipe.clip = skinPackage[curSkin].swipe;
        die.clip = skinPackage[curSkin].die;
    }
    
    public void Animation(string name)
    {
        if (name != "Die")
        {
            anim.Play(name, 0, 0);
        }
        else if (!DieSeted)
        {
            anim.Play(name, 0, 0);
            DieSeted = true;
        }
    }

    public void MaybeAnim()
    {
        if (Random.Range(0, 10) == 0)
        {   
            if (Random.Range(0, 2) == 0)
            {
                if (skinPackage[curSkin].hasAnim)
                {
                    anim.Play("Random_"+skinPackage[curSkin].naming_eng, 0, 0);
                }
                else
                {
                    anim.Play("Random_Default", 0, 0);
                }
            }
            else
            {
                anim.Play("Random1_Default", 0, 0);
            }
        }
    }
}
