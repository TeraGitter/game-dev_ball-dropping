using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    private RewardBasedVideoAd rewardBasedVideo;
    private InterstitialAd interstitial;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
#if UNITY_ANDROID
        // これはテスト用
        string appId = "ca-app-pub-3940256099942544~3347511713";
#elif UNITY_IPHONE
        string appId = "unexpected_platform";
#else
            string appId = "unexpected_platform";
#endif

        MobileAds.Initialize(appId);
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        //rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;

        RequestInterstitial();
        RequestRewardBasedVideo();
    }

    // Interstitial
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        //これはテスト用
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        string adUnitId = "xxxxx";
#else
        string adUnitId = "unexpected_platform";
#endif
        interstitial = new InterstitialAd(adUnitId);

        //interstitial.OnAdFailedToLoad += OnAdFailedToLoad;
        interstitial.OnAdOpening += HandleOnAdOpened;
        interstitial.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        //広告が表示されたらポーズする(AudioManager→使用中のAudoSource名に変更)
        //AudioManager.audioSource.Pause();
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //広告を閉じたらポーズを解除する(AudioManager→使用中のAudoSource名に変更)
        //AudioManager.audioSource.UnPause();

        interstitial.Destroy();
        RequestInterstitial();
    }

    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }


    // RewardedVideo

    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "xxxxxxxxxx";
#else
            string adUnitId = "unexpected_platform";
#endif

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        //広告が表示されたらポーズする
        //AudioManager.audioSource.Pause();
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        //広告を閉じたらポーズを解除する
        //AudioManager.audioSource.UnPause();
        this.RequestRewardBasedVideo();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;

        /*
        リワード付与をここに記述
        */
    }

    public void ShowRewardedVideo(string rewardType)
    {
        if (rewardBasedVideo.IsLoaded())
        {
            //ロードが完了している時、動画を表示する
            rewardBasedVideo.Show();
        }
        else
        {
            /*
            完了していない場合、「読み込み中」のテキストを表示
            */
        }
    }
}