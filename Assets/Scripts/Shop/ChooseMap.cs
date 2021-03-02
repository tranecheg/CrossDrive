using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMap : MonoBehaviour
{
    public AudioClip btnClick;
   public void chooseNewMap(int map)
    {
        PlayerPrefs.SetInt("nowMap", map);
        GetComponent<CheckMaps>().whichMapSelected();

        if (PlayerPrefs.GetString("music") != "No")
        {
            GetComponent<AudioSource>().clip = btnClick;
            GetComponent<AudioSource>().Play();
        }
    }
}
