using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexMobileAdsInterstitialDemoScript : MonoBehaviour
{
    public Interstitial interstitial;

    public void RequestInterstitial()
    {
        string adUnitId = "R-M-DEMO-interstitial";
        interstitial = new Interstitial(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
        interstitial.OnInterstitialLoaded += HandleInterstitialLoaded;
        interstitial.OnInterstitialFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.OnReturnedToApplication += HandleReturnedToApplication;
        interstitial.OnLeftApplication += HandleLeftApplication;
        interstitial.OnAdClicked += HandleAdClicked;
        interstitial.OnInterstitialShown += HandleInterstitialShown;
        interstitial.OnInterstitialDismissed += HandleInterstitialDismissed;
        interstitial.OnImpression += HandleImpression;
        interstitial.OnInterstitialFailedToShow += HandleInterstitialFailedToShow;
    }
    private void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            Debug.Log("Interstitial is not ready yet");
        }
    }
    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailureEventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleReturnedToApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleReturnedToApplication event received");
    }

    public void HandleLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleLeftApplication event received");
    }

    public void HandleAdClicked(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClicked event received");
    }

    public void HandleInterstitialShown(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialShown event received");
    }

    public void HandleInterstitialDismissed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialDismissed event received");
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        MonoBehaviour.print("HandleImpression event received with data: " + data);
    }

    public void HandleInterstitialFailedToShow(object sender, AdFailureEventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialFailedToShow event received with message: " + args.Message);
    }
}
