using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Admobs : MonoBehaviour {
	
	BannerView bannerView;

	InterstitialAd interAd;
	AdRequest interRequest;

	int countForAd = 0;

	// Use this for initialization
	void Start () {
		//AdSize adSize = new AdSize(360, 33);
		bannerView = new BannerView("ca-app-pub-1236233386012084/1864937055", AdSize.Banner, AdPosition.Bottom);
		AdRequest request = new AdRequest.Builder().Build();
		bannerView.LoadAd(request);
		bannerView.Show ();

		interAd = new InterstitialAd("ca-app-pub-1236233386012084/5025335054");
		interRequest = new AdRequest.Builder().Build();

		interAd.LoadAd(interRequest);
		
	}
	
	void ReadyInterAd(){
		
		interAd.LoadAd(interRequest);
		
	}
	
	void ShowInterAd(){
		countForAd++;
		Debug.Log (countForAd);
		if(interAd.IsLoaded() && countForAd  > 1){
			interAd.Show();
			countForAd = 0;
		}
		
	}
	
	
	void ShowBannerView(){
		bannerView.Show();
		
	}
	
	void HideBannerView(){
		bannerView.Hide();
		
	}
	
	void Update () {
		
		if(!interAd.IsLoaded()){
			ReadyInterAd();
		}
		
	}

	/*public Button _BtnUnityAds;
	ShowOptions _ShowOpt = new ShowOptions();
	
	void Awake()
	{
		Advertisement.Initialize("68222", false);
		_ShowOpt.resultCallback = OnAdsShowResultCallBack;
	}
	
	void OnAdsShowResultCallBack(ShowResult result)
	{
		if (result == ShowResult.Finished) {
			//보상
		}
	}
	public void OnBtnUnityAds()
	{
		Advertisement.Show(null, _ShowOpt);
	} */   
	
	
	
}
