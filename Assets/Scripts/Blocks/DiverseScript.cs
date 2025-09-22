using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverseScript : MonoBehaviour
{
    public GameObject part;
    public Animator anim;
    public List<int> can_modifiers;
    public int curModifier = -1;
    This_Modifiers gms;
    
    public void Pop()
    {
        part.SetActive(true);
        if (curModifier >= 0)
        {
            if (!Modifiers.GetMod(can_modifiers[curModifier])) 
            {
                gms.modifier.Add(curModifier);
                gms.SpCheck(can_modifiers[curModifier]);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || DangerTags.tags.Contains(coll.gameObject.tag))
        {
            anim.Play("Pop");
        }
    }

    void Start()
    {
        if (can_modifiers.Count > 0 && curModifier == -1)
        {
            curModifier = Random.Range(0, can_modifiers.Count);
        }

        GameObject gm = GameObject.FindGameObjectWithTag("GameController");
        gms = gm.GetComponent<This_Modifiers>();
    }
}
