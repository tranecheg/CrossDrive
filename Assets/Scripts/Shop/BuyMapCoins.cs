using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyMapCoins : MonoBehaviour
{
    public Animation coinsText;
    public GameObject coins1000, coins5000, money0_99, money1_99, city_btn, mega_btn;
    public Text coinsCount, slowTimeCount;
    public AudioClip success, fail;
    public void buyNew(int needCoins)
    {


        int coins = PlayerPrefs.GetInt("coins");
        if (coins < needCoins)
        {
            if (PlayerPrefs.GetString("music") != "No") {
                GetComponent<AudioSource>().clip = fail;
                GetComponent<AudioSource>().Play();
            }
            coinsText.Play();

        }
        
        else
        {
            switch (needCoins)
            {
                case 1000:
                    PlayerPrefs.SetString("city", "open");
                    PlayerPrefs.SetInt("nowMap", 2);
                    coins1000.SetActive(false);
                    money0_99.SetActive(false);
                    city_btn.SetActive(true);
                    GetComponent<CheckMaps>().whichMapSelected();
                    break;
                case 5000:
                    PlayerPrefs.SetString("megapolis", "open");
                    PlayerPrefs.SetInt("nowMap", 3);
                    coins5000.SetActive(false);
                    money1_99.SetActive(false);
                    mega_btn.SetActive(true);
                    GetComponent<CheckMaps>().whichMapSelected();
                    break;
                case 100:
                    PlayerPrefs.SetInt("slowTime", PlayerPrefs.GetInt("slowTime")+1);
                    break;


            }
            int nowCoins = coins - needCoins;
            coinsCount.text = nowCoins.ToString();
            PlayerPrefs.SetInt("coins", nowCoins);
            slowTimeCount.text = PlayerPrefs.GetInt("slowTime").ToString();
            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = success;
                GetComponent<AudioSource>().Play();
            }
        }

        

    }
}
