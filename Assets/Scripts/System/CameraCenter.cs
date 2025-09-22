using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour
{
    public bool HasPlayer;

    public Vector3 cameraPos;
    public Vector2 cameraVel;

    Camera cam;
    public float camScale;
    public float speed;
    public float goSpeed;

    public bool shake;

    public Transform arSt1, arSt2;
    public GameObject St1, St2;

    bool right;
    public SpriteRenderer fade;
    public int goLevel_num;

    public bool muted;

    public AudioSource game_over;
    public bool game_over_played;
    
    //dimensions
    public bool thisDimension;
    public AudioSource dimension;
    public GameObject[] dimension_block;

    //two players modifier
    public List<Transform> players;
    public List<Rigidbody2D> rbs;

    public bool modifier2_used, modifier4_used;
    public bool modifier4_invoked;

    void FixedUpdate()
    {
        modifier2_used = Modifiers.GetMod(2); //two players modifier
        modifier4_used = Modifiers.GetMod(4); //dim trail modifier

        if (modifier4_used)
        {
            if (!modifier4_invoked)
            {
                Invoke("Modifier4_swipeDim", Random.Range(5, 25));
                modifier4_invoked = true;
            }
        }

        if (thisDimension)
        {
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, Color.black, 10*Time.deltaTime);
        }
        else
        {
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, new Color(0.7f, 0, 0.7f, 1), 10*Time.deltaTime);
        }

        if (!right)
        {
            if (shake) transform.Translate(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);

            if (HasPlayer)
            {
                Vector3 vectors = new Vector3();
                Vector2 val = new Vector2();
                for (int i = 0; i < players.Count; i++)
                {
                    vectors += players[i].position;
                    val += rbs[i].velocity;
                }
                cameraPos = vectors / players.Count;
                cameraVel = val / rbs.Count;

                if (Vector3.Distance(cameraPos, Vector3.zero) > 4f)
                {
                    cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Vector3.Distance(cameraPos, Vector3.zero)/20+camScale, speed*Time.deltaTime);
                }
                else cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camScale, 5*Time.deltaTime);
                transform.position = Vector3.Lerp(transform.position, cameraPos/5+Vector3.forward*-10, goSpeed*Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, cameraVel.normalized.x), 5*Time.deltaTime);   
            }
            else cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camScale, 1*Time.deltaTime);
            if (fade.color.a > 0) fade.color = new Color(0, 0, 0, fade.color.a-0.04f);
            else fade.color = Color.clear;
        }
        if (right)
        {
            fade.color = new Color(0, 0, 0, fade.color.a+0.04f);
        }
    }

    public void Game_over_play()
    {
        if (!game_over_played)
        {
            game_over.Play();
            game_over_played = true;
        }
    }

    void Modifier4_swipeDim()
    {
        Invoke("Modifier4_swipeDim", Random.Range(5, 25));
        SwipeDimension();
    }

    public void SwipeDimension()
    {
        thisDimension = !thisDimension;
        dimension.Play();
        if (thisDimension)
        {
            for (int i = 0; i < dimension_block.Length; i++)
            {
               dimension_block[i].SetActive(false);
            }

        }
        else
        {
            for (int i = 0; i < dimension_block.Length; i++)
            {
               dimension_block[i].SetActive(true);
            }
        }

        if (PlayerPrefs.GetInt("Dimension_Tp") < 125)
        {
            PlayerPrefs.SetInt("Dimension_Tp", PlayerPrefs.GetInt("Dimension_Tp")+1);
        }
        else if (PlayerPrefs.GetInt("Achieve12") == 0) 
        {
            GameObject.FindGameObjectWithTag("AchieveWindow").GetComponent<NewAchieve>().Achieve(12);
        }
    }

    public void SetDimension(bool dim)
    {
        thisDimension = dim;
        for (int i = 0; i < dimension_block.Length; i++)
        {
           dimension_block[i].SetActive(!dim);
        }
    }

    public void Shake(bool i)
    {
        shake = i;
    }

    public void MuteUnmute()
    {
        muted = !muted;
        if (muted) 
        {
            PlayerPrefs.SetInt("Muted", 1);
            
            AudioListener.volume = 0;
        }
        else 
        {
            PlayerPrefs.SetInt("Muted", 0);
            
            AudioListener.volume = 1;
        }
    }

    public void MuteSoundWhileAd()
    {
        AudioListener.volume = 0;
    }

    public void UnmuteSoundWhileAdEnd()
    {
        if (muted) 
        {
            AudioListener.volume = 0;
        }
        else 
        {
            AudioListener.volume = 1;
        }
    }

    public void Right()
    {
        right = true;
    }

    void Awake()
    {
        cam = GetComponent<Camera>();
        if (PlayerPrefs.GetInt("Muted") == 0) 
        {
            muted = false;
            
            AudioListener.volume = 1;
        }
        else
        {
            muted = true;

            AudioListener.volume = 0;
        }
        if (GameObject.FindGameObjectWithTag("Player")) HasPlayer = true;
        if (HasPlayer)
        {
            GameObject[] gm = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < gm.Length; i++)
            {
                players.Add(gm[i].transform);
                rbs.Add(gm[i].GetComponent<Rigidbody2D>());
            }
            
            fade.color = Color.black;
        } 

        for (int a = 0; a < 28f*camScale; a++)
        {
            GameObject s = Instantiate(St1, arSt1);
            s.transform.position = new Vector3(Random.Range(-30f, 30f), Random.Range(-15f, 15f), 0);
        }
        for (int a = 0; a < 28f*camScale; a++)
        {
            GameObject s = Instantiate(St2, arSt2);
            s.transform.position = new Vector3(Random.Range(-30f, 30f), Random.Range(-15f, 15f), 0);
        }

        dimension_block = GameObject.FindGameObjectsWithTag("AnotherDimension");
        for (int i = 0; i < dimension_block.Length; i++)
        {
            dimension_block[i].SetActive(false);
        }
    }
}
