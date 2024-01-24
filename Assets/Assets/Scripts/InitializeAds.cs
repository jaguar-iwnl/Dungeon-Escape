using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Rendering;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string androidGameId;
    [SerializeField] private string iosGameId;
    [SerializeField] private bool isTesting;

    private string gameId;


    private void Awake()
    {
        #if UNITY_IOS
            gameId = iosGameId;
        #elif UNITY_ANDROID
            gameId = androidGameId;
        #elif UNITY_EDITOR
            gameId = androidGameId;
        #endif

        if(!Advertisement.isInitialized && Advertisement.isSupported )
        {
            Advertisement.Initialize(gameId, isTesting, this);
        }
    }
    public void OnInitializationComplete()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Ads initialized");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        //throw new System.NotImplementedException();
        Debug.LogError("Ads not initialized");
    }
}
