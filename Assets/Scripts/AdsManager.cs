using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    private string adUnitId = "ca-app-pub-3940256099942544/1033173712";
    private InterstitialAd interstitial;
    private int nowLoses;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        UpdateAds(true);
    }
    private void Update()
    {
        if(interstitial.IsLoaded() && GameController.countLoses % 3 == 0 
            && GameController.countLoses != 0 && GameController.countLoses!=nowLoses)
        {
            nowLoses = GameController.countLoses;
            interstitial.Show();
        }
    }
    
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        UpdateAds();
    }


    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        UpdateAds();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        UpdateAds();
    }
    void UpdateAds(bool isFirst = false)
    {
        if(!isFirst)
        interstitial.Destroy();


        interstitial = new InterstitialAd(adUnitId);
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
}
