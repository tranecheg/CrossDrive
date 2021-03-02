using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [NonSerialized] public static int countLoses;
    public bool isMainScene;
    public GameObject[] cars, maps;
    public float spawnTimeFrom = 3, spawnTimeTo = 5;
    private int countCars;
    private Coroutine bottomCars, leftCars, topCars, rightCars;
    private bool isLoseOnce;
    public GameObject canvasLosePanel, canvasPanel, horn, adsManager;
    public AudioSource turnSignal;
    public Text nowScore, bestScore, coinsCount;
    private static bool isAdd;
    private void Start()
    {
        if (!isAdd && PlayerPrefs.GetString("NoAds") !="Yes")
        {
            Instantiate(adsManager, Vector3.zero, Quaternion.identity);
            isAdd = true;
        }
        if (PlayerPrefs.GetInt("nowMap") == 2)
        {
            Destroy(maps[0]);
            maps[1].SetActive(true);
            Destroy(maps[2]);

        }
        else if (PlayerPrefs.GetInt("nowMap") == 3)
        {
            Destroy(maps[0]);
            Destroy(maps[1]);
            maps[2].SetActive(true);

        }
        else
        {
            maps[0].SetActive(true);
            Destroy(maps[1]);
            Destroy(maps[2]);
        }

        CarController.isLose = false;
        CarController.countCars = 0;

        bottomCars = StartCoroutine(BottomCars());
        topCars = StartCoroutine(TopCars());
        leftCars = StartCoroutine(LeftCars());
        rightCars = StartCoroutine(RightCars());
        
    }

    private void Update()
    {
        if (CarController.isLose && !isLoseOnce)
        {
            countLoses++;
            StopCoroutine(bottomCars);
            StopCoroutine(topCars);
            StopCoroutine(leftCars);
            StopCoroutine(rightCars);
            nowScore.text = "<color=#D94242>Score:</color> " + CarController.countCars;
            if (PlayerPrefs.GetInt("score") < CarController.countCars)
                PlayerPrefs.SetInt("score", CarController.countCars);

            bestScore.text = "<color=#D94242>Best score:</color> " + PlayerPrefs.GetInt("score");
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + CarController.countCars);
            coinsCount.text = "" + PlayerPrefs.GetInt("coins");
            canvasLosePanel.SetActive(true);
            canvasPanel.SetActive(false);
            isLoseOnce = true;
        }
    }
    IEnumerator BottomCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-1,0,-25), 180);
            yield return new WaitForSeconds(Random.Range(spawnTimeFrom, spawnTimeTo));
        }
    }
    IEnumerator TopCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-8, 0, 66), 0, true);
            yield return new WaitForSeconds(Random.Range(spawnTimeFrom, spawnTimeTo));
        }
    }
    IEnumerator LeftCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-71.4f, 0, 2.5f), 270);
            yield return new WaitForSeconds(Random.Range(spawnTimeFrom, spawnTimeTo));
        }
    }
    IEnumerator RightCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(24, 0, 11), 90);
            yield return new WaitForSeconds(Random.Range(spawnTimeFrom, spawnTimeTo));
        }
    }

    void SpawnCar(Vector3 pos, float rotY, bool isMoveFromUp = false)
    {
        GameObject newCar = Instantiate(cars[Random.Range(0,2)], pos, Quaternion.Euler(0, rotY, 0));
        
        newCar.name = "Car" + ++countCars;
        int random = isMainScene ? 1: Random.Range(1, 4);
        switch (random)
        {
            case 1:
                newCar.GetComponent<CarController>().rightTurn = true;
                if (PlayerPrefs.GetString("music") != "No" && !turnSignal.isPlaying)
                {
                    turnSignal.Play();
                    Invoke("StopSound", 2f);
                }
                    
                break;
            case 2:
                newCar.GetComponent<CarController>().turnLeft = true;
                if(isMoveFromUp)
                    newCar.GetComponent<CarController>().moveFromUp = true;
                if (PlayerPrefs.GetString("music") != "No" && !turnSignal.isPlaying)
                {
                    turnSignal.Play();
                    Invoke("StopSound", 2f);
                }
                break;
        }
    }
    void StopSound()
    {
        turnSignal.Stop();
    }

    IEnumerator CreateHorn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            if (PlayerPrefs.GetString("music") != "No")
                Instantiate(horn, Vector3.zero, Quaternion.identity);
            
        }
    }

}

