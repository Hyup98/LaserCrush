using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;

public class GoogleAdMob : MonoBehaviour
{
    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-2303943085632745/9263589806";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
  private string _adUnitId = "unused";
#endif

    BannerView _bannerView;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
        CreateBannerView();
        LoadAd();
    }

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            _bannerView.Destroy();
            _bannerView = null;
        }

        //���� ������ �ڵ� ���� 
        _bannerView = new BannerView(_adUnitId, AdSize.SmartBanner, AdPosition.Bottom);




        //���� ������ ����ڰ� ���� ����
        //_bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);

        //������ ���� coordinate (0,50)
        //_bannerView = new BannerView(_adUnitId, AdSize.Banner, 0, 50);

        //���� ������ ����
        //AdSize adSize = new AdSize(250, 250);
        //_bannerView = new BannerView(_adUnitId, adSize, AdPosition.Bottom);
    }


    /// <summary>
    /// Creates the banner view and loads a banner ad.
    /// </summary>
    public void LoadAd()
    {
        // create an instance of a banner view first.
        if (_bannerView == null)
        {
            CreateBannerView();
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }

    /// <summary>
    /// Destroys the banner view.
    /// </summary>
    public void DestroyBannerView()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }
}
