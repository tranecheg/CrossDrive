using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasButtons : MonoBehaviour
{
    public Sprite btn, btnPressed, musicOn, musicOff;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        if (gameObject.name == "Music Button") { 
        if (PlayerPrefs.GetString("music") == "No")
        {
           transform.GetChild(0).GetComponent<Image>().sprite = musicOff;
        }
        }

    }
    
    public void SetPressedButton()
    {
        image.sprite = btnPressed;
        transform.GetChild(0).localPosition -= new Vector3(0, 5f, 0);
    }
    public void SetDefaultButton()
    {
        image.sprite = btn;
        transform.GetChild(0).localPosition += new Vector3(0, 5f, 0);
    }

    public void PlayGame()
    {
        if (PlayerPrefs.GetString("firstGame") == "No")
        {
            StartCoroutine(loadScene("Game"));
            Time.timeScale = 1;
        }
     
        else
        {
            StartCoroutine(loadScene("Study"));
            PlayerPrefs.SetInt("slowTime", 3);
            PlayerPrefs.SetString("firstGame", "No");
        }
        PlayButtonSound();


    }
    public void Shop()
    {
        StartCoroutine(loadScene("Shop"));
        PlayButtonSound();
    }
    public void ExitShop()
    {
        StartCoroutine(loadScene("Main"));
        PlayButtonSound();
    }

    public void MusicButton()
    {
        if(PlayerPrefs.GetString("music") == "No"){
            PlayerPrefs.SetString("music", "Yes");
            transform.GetChild(0).GetComponent<Image>().sprite = musicOn;
        }
        else
        {
            PlayerPrefs.SetString("music", "No");
            transform.GetChild(0).GetComponent<Image>().sprite = musicOff;
        }
        PlayButtonSound();
    }

    IEnumerator loadScene(string name)
    {
        float fadeTime = Camera.main.GetComponent<Fading>().Fade(1f);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(name);
    }

    private void PlayButtonSound()
    {
        if (PlayerPrefs.GetString("music") != "No")
            GetComponent<AudioSource>().Play();
        
    }
   
}
