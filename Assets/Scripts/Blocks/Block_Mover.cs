using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Mover : MonoBehaviour
{
    public Vector3 start, end, now;
    public float speed;
    public int axis;
    public int isHorizontal;
    public GameObject Barriers, Trail;
    Block_Default_Script def;
    public AudioSource up, down, big;

    void Start()
    {
        def = GetComponent<Block_Default_Script>();
        now = transform.position;
        Instantiate(Barriers, start, Quaternion.Euler(0, 0, isHorizontal*90));
        Instantiate(Barriers, end, Quaternion.Euler(0, 0, isHorizontal*90 + 180));
        for (int i = 0; i < Vector3.Distance(start, end)/0.28f; i++)
        {
            Instantiate(Trail, new Vector3(i*isHorizontal*0.28f+start.x+0.14f*isHorizontal, -i*Mathf.Abs(isHorizontal - 1)*0.28f+start.y-0.14f*Mathf.Abs(isHorizontal - 1), 0), Quaternion.Euler(0, 0, isHorizontal*90));
        }
        if (isHorizontal == 0) transform.Rotate(0, 0, 270);
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, now, speed*5*Time.deltaTime);
        def.start = now;
        if (axis == 1)
        {
            if (Vector3.Distance(now, end) < 0.1f)
            {
                axis = -1;
                up.pitch = Random.Range(0.9f, 1.3f);
                up.Play();
            }
            now = Vector3.Lerp(now, end, speed*3*Time.deltaTime);
        }
        if (axis == -1)
        {
                if (Vector3.Distance(now, start) < 0.1f)
                {
                    axis = 1;
                    if (transform.localScale.x + transform.localScale.y < 8)
                    {
                        down.pitch = Random.Range(0.9f, 1.1f);
                        down.Play();
                    }
                    else
                    {
                        big.pitch = Random.Range(0.85f, 1.05f);
                        big.Play();
                    }
                }
                now = Vector3.Lerp(now, start, speed*3*Time.deltaTime);
        }
    }
}
