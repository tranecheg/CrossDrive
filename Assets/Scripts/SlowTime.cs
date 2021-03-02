using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowTime : MonoBehaviour
{

    public Text slowTimeText;
    public Sprite timeSlow, noTimeSlow;
    private int slowTimeCount;


    private void Update()
    {
        slowTimeCount = PlayerPrefs.GetInt("slowTime");
        slowTimeText.text = slowTimeCount.ToString();
        if (slowTimeCount > 0)
            GetComponent<Image>().sprite = timeSlow;
        else
            GetComponent<Image>().sprite = noTimeSlow;
        
    }
    public void slowTime()
    {
        if (slowTimeCount > 0 && Time.timeScale==1)
        {
            StartCoroutine(slow());
            slowTimeCount--;
            PlayerPrefs.SetInt("slowTime", slowTimeCount);
           
            GetComponent<AudioSource>().Play();
        }

    }
    IEnumerator slow()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(3f);
        Time.timeScale = 1f;
    }
}
