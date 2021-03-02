using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountCoins : MonoBehaviour
{
  
    void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt("coins").ToString();
    }

   
}
