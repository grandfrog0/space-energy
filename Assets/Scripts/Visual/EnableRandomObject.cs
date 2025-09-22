using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRandomObject : MonoBehaviour
{
    public List<GameObject> sets;
    public List<int> weights;

    void Awake()
    {
        On();
    }

    public void On()
    {
        foreach(GameObject set in sets)
        {
            set.SetActive(false);
        }
        if (sets.Count > 0)
        {
            sets[GetRandomWeightedIndex(weights)].SetActive(true);
        }
    }

    public int GetRandomWeightedIndex(List<int> weights)
    {
        if(weights == null || weights.Count == 0) return -1;
    
        int t = 0;
        for(int i = 0; i < weights.Count; i++)
        {
            if(weights[i] >= 0) t += weights[i];
        }
    
        float r = Random.value;
        float s = 0f;
    
        for(int i = 0; i < weights.Count; i++)
        {
            if (weights[i] <= 0f) continue;
        
            s += (float)weights[i] / t;
            if (s >= r) return i;
        }
    
        return -1;
    }
}
