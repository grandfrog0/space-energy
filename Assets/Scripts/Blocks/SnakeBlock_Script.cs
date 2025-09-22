using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBlock_Script : MonoBehaviour
{
    //анимация съедания яблока, взаимодействие со змеей, смерть змеи от касания с змееблоком, super, audio
    //spawner
    public int spawn_count;
    public bool cant_spawn;
    public Vector3 to, spawn_to;
    public GameObject spawn_object;
    public List<GameObject> gm;
    public List<SnakePart_Script> part;

    public List<GameObject> targets;
    public Transform target;

    //block
    public bool damaged;

    //Visual
    Transform player;
    public bool see_player;
    public Transform eye, pupil, l;
    public Vector3 eye_start, pupil_start, l_start;

    void FixedUpdate()
    {
        if (!damaged)
        {
            eye.localPosition = Vector3.Lerp(eye.localPosition, eye_start - (eye.position - player.position).normalized / 10, 10*Time.deltaTime);
            pupil.localPosition = Vector3.Lerp(pupil.localPosition, pupil_start - (pupil.position - player.position).normalized / 8, 10*Time.deltaTime);
            l.localPosition = Vector3.Lerp(l.localPosition, l_start - (l.position - player.position).normalized / 15, 8*Time.deltaTime);
        }
        else
        {
            targets.Clear();
            target = transform;
        }

        if (target == null)
        {
            target = null;
            UpdateTargets();
        }

        if (Vector3.Distance(player.transform.position, transform.position) < 8)
        {
            if (!see_player)
            {
                UpdateTargets();
                InvokeRepeating("Spawn", 0.5f, 0.5f);
                see_player = true;
            }
        }
    }

    public void UpdateTargets()
    {
        GameObject[] j = GameObject.FindGameObjectsWithTag("Apple");
        targets.Clear();
        for (int i = 0; i < j.Length; i++)
        {
            targets.Add(j[i]);
        }
        
        for (int i = 0; i < targets.Count; i++)
        {
            if (target == null)
            {
                target = targets[i].transform;
            }
            else if (part.Count > 0)
            {
                if (Vector3.Distance(targets[i].transform.position, part[0].transform.position) < Vector3.Distance(target.position, part[0].transform.position))
                {
                    target = targets[i].transform;
                }
            }
            else
            {
                if (Vector3.Distance(targets[i].transform.position, transform.position) < Vector3.Distance(target.position, transform.position))
                {
                    target = targets[i].transform;
                }
                spawn_to = AbsVector3(target.position - transform.position);
            }
        }
    }

    void Spawn()
    {
        UpdateTargets();

        for (int i = gm.Count - 1; i > 0; i--) 
        {
            if (part[i])
            {
                part[i].x = part[i-1].x;
                part[i].y = part[i-1].y;
                part[0].gm.rotation = Quaternion.LookRotation(Vector3.forward, -to);
                part[i].gm.rotation = part[i-1].gm.rotation;
            }
        }
        if (gm.Count > 0)
        {
            if (part[0])
            {
                part[0].x = gm[0].transform.position.x + to.x*1.4f;
                part[0].y = gm[0].transform.position.y + to.y*1.4f;
                part[0].gm.rotation = Quaternion.LookRotation(Vector3.forward, -to);
            }
            
        }

        if (!cant_spawn)
        {
            if (spawn_count <= 0)
            {
                cant_spawn = true;
            }
            else
            {
                gm.Add(Instantiate(spawn_object, transform.position, Quaternion.identity));
                part.Add(gm[gm.Count-1].GetComponent<SnakePart_Script>());
                part[gm.Count-1].par = this;
                part[gm.Count-1].x = gm[gm.Count-1].transform.position.x + spawn_to.x*1.4f;
                part[gm.Count-1].y = gm[gm.Count-1].transform.position.y + spawn_to.y*1.4f;

                spawn_count--;

                if (spawn_count == 0)
                {
                    part[gm.Count-1].SetPart("tail");
                }

                if (gm.Count-1 == 0)
                {
                    part[0].SetPart("head");
                }
            }
        }

        if (target != null)
        {
            to = AbsVector3(target.position - part[0].transform.position);
        }
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        eye_start = eye.localPosition;
        pupil_start = pupil.localPosition;
        l_start = l.localPosition;
    }

    Vector3 AbsVector3(Vector3 i)
    {
        if (Mathf.Abs(i.x) >= Mathf.Abs(i.y))
        {
            if (i.x > 0)
            {
                return Vector3.right; 
            }
            else
            {
                return Vector3.left; 
            }
        }
        else
        {
            if (i.y > 0)
            {
                return Vector3.up;
            }
            else
            {
                return Vector3.down; 
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Ouch!"); //DELETE IT
        if (damaged)
        {
            if (targets.Contains(coll.gameObject))
            {
                Destroy(coll.gameObject);  
            }
        }
        else
        {
            damaged = true;
        }
    }
}
