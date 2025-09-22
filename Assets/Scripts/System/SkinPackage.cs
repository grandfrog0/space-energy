using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinPackage : MonoBehaviour
{
    public int AdCount;

    public Sprite head, eyes, mouse, energy;
    public AudioClip swipe, die;
    public string naming_eng, naming_rus;
    public bool hasAnim;

    [Header("Animations")]
    public List<Sprite> animed_head, animed_eyes, animed_mouse, animed_energy;
    //Time, Random, Tick, Twice (float float float bool)
    // public string TRTT_head, TRTT_eyes, TRTT_mouse, TRTT_energy;
    public float time_head, time_eyes, time_mouse, time_energy;
    public float tick_head, tick_eyes, tick_mouse, tick_energy;
}
