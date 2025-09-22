using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullCrusher_Anim : MonoBehaviour
{
    public Vector2 campos;
    public Transform camer;

    public List<AudioSource> au;
    public CameraCenter cam;
    
    public Transform block;
    public GameObject destroyParticle;
    public int particlecount;

    void FixedUpdate()
    {
        camer.position = Vector3.Lerp(camer.position, campos, 1*Time.deltaTime);
        camer.position = new Vector3(camer.position.x, camer.position.y, -10);
    }

    public void Shake()
    {
        cam.Shake(!cam.shake);
    }

    public void SetPosX(float num)
    {
        campos.x = num;
    }
    
    public void SetPosY(float num)
    {
        campos.y = num;
    }

    public void PlaySound(int num)
    {
        au[num].Play();
    }

    public void SetCameraSize(int num)
    {
        cam.camScale = num;
    }

    public void Break()
    {
        for (int k = 0; k < particlecount; k++)
        {
            GameObject dp = Instantiate(destroyParticle, block.position, block.rotation);
            dp.transform.rotation = Quaternion.Euler(0, 0, k*360/particlecount);
            dp.GetComponent<Rigidbody2D>().velocity = dp.transform.up*10;
        }
    }
}
