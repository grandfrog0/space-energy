using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelsRange
{
    public static List<int> Range(int num, int j)
    {
        List<int> list = new List<int>();
        for (int i = num; i < j; i++)
        {
            list.Add(i);
        }
        Debug.Log(list);
        return list;
    }
}
