using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualParticles : MonoBehaviour
{
    public List<GameObject> particles;
    public List<GameObject> cur_particles;
    public List<SpriteRenderer> cur_srs;

    void FixedUpdate()
    {
        if (Random.Range(0, 400) <= 1)
        {
            GameObject gm = Instantiate(particles[Random.Range(0, particles.Count)], new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f)), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            float randomizer = Random.Range(1f, 2f);
            gm.transform.localScale = new Vector3(randomizer, randomizer, 1f);
            cur_particles.Add(gm);
            SpriteRenderer sr = gm.GetComponent<SpriteRenderer>();
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
            cur_srs.Add(sr);
        }
        for (int i = 0; i < cur_particles.Count; i++)
        {
            GameObject part = cur_particles[i];
            SpriteRenderer sr = cur_srs[i];

            part.transform.Translate(0, 1f*Time.deltaTime, 0);
            part.transform.Rotate(0, 0, 10f*Time.deltaTime);
            
            if (sr.color.a < 0.2f) sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + 3f*Time.deltaTime);

            if (Vector3.Distance(part.transform.position, Vector3.zero) > 30)
            {
                Destroy(part);
                particles.RemoveAt(i);
                cur_srs.RemoveAt(i);
            }
        }
    }
}
