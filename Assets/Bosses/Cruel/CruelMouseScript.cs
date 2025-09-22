using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruelMouseScript : MonoBehaviour
{
    public Transform rwisker, lwisker, drop;
    bool rwisker_less, lwisker_less, drop_down;
    Vector3 rwisker_start, lwisker_start, drop_start;

    void FixedUpdate()
    {
        if (rwisker_less)
        {
            rwisker.localPosition = Vector3.Lerp(rwisker.localPosition, rwisker_start + transform.up * 2, 3*Time.deltaTime);
            rwisker.rotation = Quaternion.Lerp(rwisker.rotation, Quaternion.Euler(0, 0, 15), 10*Time.deltaTime);
            if (rwisker.localPosition.y >= rwisker_start.y + 0.2f)
            {
                rwisker_less = false;
            }
        }
        else
        {
            rwisker.localPosition = Vector3.Lerp(rwisker.localPosition, rwisker_start, 7*Time.deltaTime);
            rwisker.rotation = Quaternion.Lerp(rwisker.rotation, Quaternion.Euler(0, 0, 0), 5*Time.deltaTime);
            if (rwisker.localPosition.y < rwisker_start.y - 0.2f)
            {
                rwisker.localPosition = rwisker_start;
            }
        }

        if (lwisker_less)
        {
            lwisker.localPosition = Vector3.Lerp(lwisker.localPosition, lwisker_start + transform.up * 0.8f, 0.2f*Time.deltaTime);
            lwisker.rotation = Quaternion.Lerp(lwisker.rotation, Quaternion.Euler(0, 0, 10), 3*Time.deltaTime);
            if (lwisker.localPosition.y >= lwisker_start.y + transform.up.y * 0.1f)
            {
                lwisker_less = false;
            }
        }
        else
        {
            lwisker.localPosition = Vector3.Lerp(lwisker.localPosition, lwisker_start - transform.up * 0.8f, 0.1f*Time.deltaTime);
            lwisker.rotation = Quaternion.Lerp(lwisker.rotation, Quaternion.Euler(0, 0, -4), 1*Time.deltaTime);
            if (lwisker.localPosition.y < lwisker_start.y - transform.up.y * 0.1f)
            {
                lwisker_less = true;
            }
        }

        if (drop_down)
        {
            drop.Translate(0, -20*Time.deltaTime, 0);
            if (Vector3.Distance(drop.position, drop_start) < 4)
            {
                drop.localScale = Vector3.Lerp(drop.localScale, new Vector3(0.5f, 1.7f, 1f), 15f*Time.deltaTime);
            }
            else
            {
                drop.localScale = Vector3.Lerp(drop.localScale, new Vector3(2f, 0f, 1f), 2f*Time.deltaTime);
            }
            if (Vector3.Distance(drop.position, drop_start) > 20)
            {
                drop_down = false;
            }
        }
        else
        {
            drop.localPosition = drop_start;
            drop.localScale = Vector3.Lerp(drop.localScale, new Vector3(1.4f, 0.7f, 1f), 4f*Time.deltaTime);
            drop.rotation = Quaternion.Lerp(lwisker.rotation, Quaternion.Euler(0, 0, Random.Range(-8f, 8f)), 5*Time.deltaTime);
        }
    }

    void Awake()
    {
        rwisker_start = rwisker.localPosition;
        Invoke("Rwisk", Random.Range(2, 8));
        lwisker_start = lwisker.localPosition;
        drop_start = drop.localPosition;
        Invoke("Drop", Random.Range(4, 12));
    }

    void Rwisk()
    {
        rwisker_less = true;
        Invoke("Rwisk", Random.Range(2, 8));
    }

    void Drop()
    {
        drop_down = true;
        Invoke("Drop", Random.Range(4, 12));
    }
}
