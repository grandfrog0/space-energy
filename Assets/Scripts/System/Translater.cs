using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translater : MonoBehaviour
{
    //перевод осуществляется в GameManager, MenuManager, ShopManager, CutSceneManager, ModifiersManager, NewAchieve, Texting, Info_Texting

    public TMPro.TMP_FontAsset eng, rus;

    public List<string> achieve_name_eng, achieve_discription_eng;
    public List<string> achieve_name_rus, achieve_discription_rus;
    
    public List<string> menu_texts_eng;
    public List<string> menu_texts_rus;

    public List<string> level_texty_eng;
    public List<string> level_texty_rus;

    public List<string> level_texty_info_eng;
    public List<string> level_texty_info_rus;

    public List<string> modifier_name_eng, modifier_name_rus;
    public List<string> modifier_description_eng, modifier_description_rus;
}
