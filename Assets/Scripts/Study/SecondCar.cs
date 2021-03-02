using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondCar : MonoBehaviour
{
    private void OnDestroy()
    {
        PlayerPrefs.GetString("firstGame", "No");
        SceneManager.LoadScene("Game");
    }
}
