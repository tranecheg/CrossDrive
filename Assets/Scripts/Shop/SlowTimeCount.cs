using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowTimeCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("firstGame") != "No")
        {
            PlayerPrefs.SetInt("slowTime", 3);
        }
        
        GetComponent<Text>().text = PlayerPrefs.GetInt("slowTime").ToString();
    }

   
}
