using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    public YandexGame sdk;
    public int coins;
    public Text coinsTxt;

    public Shop_Manager shop;
    public string skin;

    public void AdButton()
    {
        sdk._RewardedShow(1);
    }

    public void AdButtonCul()
    {
        skin = shop.skinPackage[shop.skin[1]].naming_eng;
        PlayerPrefs.SetInt("AdWatched_"+skin, PlayerPrefs.GetInt("AdWatched_"+skin)+1);
    }
}
