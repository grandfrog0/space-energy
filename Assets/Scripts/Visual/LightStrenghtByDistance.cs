using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStrenghtByDistance : MonoBehaviour
{
    public List<Transform> targets;
    Transform target;
    public SpriteRenderer sr;
    float start_alpha;

    void Awake()
    {
        if (targets.Count <= 0)
        {
            GameObject[] gms = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < gms.Length; i++)
            {
                targets.Add(gms[i].transform);
            }
        }
        start_alpha = sr.color.a;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (Vector2.Distance(transform.position, targets[i].position) < Vector2.Distance(transform.position, target.position))
                {
                    target = targets[i];
                }
            }
        }
        else target = targets[0];
        
        if (Vector2.Distance(transform.position, target.position) < 1.5f)
        {
            sr.color = Color.Lerp(sr.color, new Color(sr.color.r, sr.color.g, sr.color.b, start_alpha + 0.5f), 20f*Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 2, 1), 20f*Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, target.position) < 3f)
        {
            sr.color = Color.Lerp(sr.color, new Color(sr.color.r, sr.color.g, sr.color.b, start_alpha + 0.2f), 20f*Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1.5f, 1), 20f*Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, target.position) < 7f)
        {
            sr.color = Color.Lerp(sr.color, new Color(sr.color.r, sr.color.g, sr.color.b, start_alpha + 0.1f), 20f*Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1.25f, 1), 20f*Time.deltaTime);
        }
        else
        {
            sr.color = Color.Lerp(sr.color, new Color(sr.color.r, sr.color.g, sr.color.b, start_alpha), 20f*Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), 20f*Time.deltaTime);
        }
    }
}
