using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckMaps : MonoBehaviour
{
    private BuyMapCoins mapCoins;
    public Image[] maps;
    public Sprite selected, notSelected;
    private void Start()
    {
        whichMapSelected();
        mapCoins = GetComponent<BuyMapCoins>();
        if (PlayerPrefs.GetString("city") == "open")
        {
            mapCoins.coins1000.SetActive(false);
            mapCoins.money0_99.SetActive(false);
            mapCoins.city_btn.SetActive(true);
        }
        if (PlayerPrefs.GetString("megapolis") == "open")
        {
            mapCoins.coins5000.SetActive(false);
            mapCoins.money1_99.SetActive(false);
            mapCoins.mega_btn.SetActive(true);
        }

    }
    public void whichMapSelected()
    {
        switch (PlayerPrefs.GetInt("nowMap")) {
            case 2:
                maps[0].sprite = notSelected;
                maps[1].sprite = selected;
                maps[2].sprite = notSelected;
                break;
            case 3:
                maps[0].sprite = notSelected;
                maps[1].sprite = notSelected;
                maps[2].sprite = selected;
                break;
            default:
                maps[0].sprite = selected;
                maps[1].sprite = notSelected;
                maps[2].sprite = notSelected;
                break;
        }
            
    }
}
