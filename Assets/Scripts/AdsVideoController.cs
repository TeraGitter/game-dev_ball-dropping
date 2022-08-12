//using UnityEngine;
//using UnityEngine.Advertisements;

//public class AdsVideoController : MonoBehaviour {

//    private string m_result = string.Empty;

//    /// <summary>
//    /// OnGUI
//    /// ボタン押下テスト用メソッド
//    /// </summary>
//    /*
//    private void OnGUI()
//    {
//        var options = new[]
//        {
//            GUILayout.Width( Screen.width ),
//            GUILayout.Height( Screen.height / 2 ),
//        };

//        if (GUILayout.Button("Unity Ads", options))
//        {
//            ShowRewardedAd();
//        }

//        GUILayout.Label(m_result);
//    }
//*/
//    /// <summary>
//    /// ShowRewardedAd
//    /// 動画広告表示
//    /// </summary>
//    //private void ShowRewardedAd()
//    public void ShowRewardedAd()
//    {
//        if (!Advertisement.IsReady()) return;

//        var options = new ShowOptions
//        {
//            resultCallback = OnResult,
//        };
//        Advertisement.Show(options);
//    }

//    /// <summary>
//    /// OnResult
//    /// 広告表示結果を戻す
//    /// </summary>
//    /// <param name="result"></param>
//    private void OnResult(ShowResult result)
//    {
//        switch (result)
//        {
//            case ShowResult.Finished:
//                m_result = "Finished";
//                break;
//            case ShowResult.Skipped:
//                m_result = "Skipped";
//                break;
//            case ShowResult.Failed:
//                m_result = "Failed";
//                break;
//        }
//    }
//}
