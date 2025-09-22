using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruelEyeScript : MonoBehaviour
{
    GameObject[] player;
    Transform target_player;
    bool eyes_less, pulse_less, around_less;
    Vector3 eyes_need_size, eyes_start_size, eyes_start, hat_start;
    public Transform eyem, eyePartMask, aroundPartMask, hat, backhat, ligh;
    public Transform body;

    void FixedUpdate()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            if (target_player == null) target_player = player[i].transform;
            else if (Vector3.Distance(player[i].transform.position, transform.position) < Vector3.Distance(target_player.position, transform.position))
            {
                target_player = player[i].transform;
            }
        }
        body.position = Vector3.Lerp(body.position, (body.position - target_player.position).normalized * 0.5f, 1 * Time.deltaTime);
        transform.localPosition = Vector3.Lerp(transform.localPosition, eyes_start - (transform.position - target_player.position).normalized / 10, 10*Time.deltaTime);
        eyem.localPosition = Vector3.Lerp(eyem.localPosition, eyes_start - transform.up - (eyem.position - target_player.position).normalized / 8, 10*Time.deltaTime);
        hat.localPosition = Vector3.Lerp(hat.localPosition, hat_start + (hat.position - target_player.position).normalized / 8, 10*Time.deltaTime);
        backhat.localPosition = Vector3.Lerp(backhat.localPosition, hat_start - transform.up * 2.3f + (backhat.position - target_player.position).normalized / 7, 10*Time.deltaTime);
        if (eyes_need_size != Vector3.zero)
        {
            if (eyes_less)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, eyes_need_size, 14*Time.deltaTime);
                if (transform.localScale.x <= 0.4f)
                {
                    eyes_less = false;
                }
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, eyes_start_size, 18*Time.deltaTime);
                if (transform.localScale.x >= 0.9f)
                {
                    transform.localScale = eyes_start_size;
                    eyes_need_size = Vector3.zero;
                }
            }
        }

        if (pulse_less)
        {
            eyePartMask.localScale = Vector3.Lerp(eyePartMask.localScale, new Vector3(0.5f, 1f, 0.5f), 1f*Time.deltaTime);
            ligh.localScale = Vector3.Lerp(ligh.localScale, new Vector3(1.3f, 1.3f, 1.3f), 2f*Time.deltaTime);
            if (eyePartMask.localScale.x <= 0.6f)
            {
                pulse_less = false;
            }
        }
        else
        {
            eyePartMask.localScale = Vector3.Lerp(eyePartMask.localScale, new Vector3(1f, 2f, 1f), 2f*Time.deltaTime);
            ligh.localScale = Vector3.Lerp(ligh.localScale, new Vector3(0.7f, 0.7f, 0.7f), 1f*Time.deltaTime);
            if (eyePartMask.localScale.x >= 0.9f)
            {
                pulse_less = true;
            }
        }

        if (around_less)
        {
            aroundPartMask.localPosition = Vector3.Lerp(aroundPartMask.localPosition, new Vector3(0, -0.8f, 0), 3f*Time.deltaTime);
            if (aroundPartMask.localScale.y <= -0.6f)
            {
                around_less = false;
            }
        }
    }

    void Awake()
    {
        Invoke("Eye_blink", Random.Range(6, 12));
        Invoke("Around_blink", Random.Range(4, 10));
        eyes_start = transform.localPosition;
        eyes_start_size = transform.localScale;
        hat_start = hat.localPosition;
    }

    void Eye_blink()
    {
        eyes_less = true;
        eyes_need_size = new Vector3(0.3f, 1f, 1);
        Invoke("Eye_blink", Random.Range(6, 12));
    }

    void Around_blink()
    {
        around_less = true;
        aroundPartMask.localPosition = new Vector3(0, 0.8f, 0);
        Invoke("Around_blink", Random.Range(4, 10));
    }
}
