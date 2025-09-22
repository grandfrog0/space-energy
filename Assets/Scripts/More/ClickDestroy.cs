using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDestroy : MonoBehaviour
{
    public GameObject parts;

    void OnMouseDown()
    {
        if (parts != null)
        {
            parts.SetActive(true);
            parts.GetComponent<ParticleSystem>().Play();
            parts.transform.parent = null;
        }
        gameObject.transform.Translate(1000, 0, 0);
    }
}
