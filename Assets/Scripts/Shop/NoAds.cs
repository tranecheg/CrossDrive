using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoAds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       if(PlayerPrefs.GetString("NoAds") =="Yes")
        Destroy(gameObject);
    }

    
}
