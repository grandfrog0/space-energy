using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour
{
    static List<bool> modifier = new List<bool>();
    static List<Sprite> image = new List<Sprite>();

    public static bool GetMod(int num)
    {
        if (modifier.Count > num)
        {
            return modifier[num];
        }
        else
        {
            return false;
        }
    }

    public static void LoadMod(int num) //добавит столько ячеек в modifier
    {
        modifier.Clear();
        for (int i = 0; i < num; i++)
        {
            modifier.Add(false);
        }
    }

    public static Sprite GetImage(int num)
    {
        return image[num];
    }

    public static void LoadImage(List<Sprite> i)
    {
        image = i;
    }

    public static void Adding(int num)
    {
        modifier[num] = true;
        Debug.Log(num + "Added");
    }

    public static void Erase(int num)
    {
        modifier[num] = false;
    }
}
