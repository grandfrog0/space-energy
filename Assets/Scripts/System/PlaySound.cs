using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource au;

    void OnMouseDown()
    {
        au.Play();
    }
}
