using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// 定数定義
	private const int MAX_SCORE = 999999;   // スコア最大数
    private const string STAGE = "Stage";   // ステージ文字列

	public int stageNo;                 // ステージナンバー

    public GameObject textClear;		// 「クリア」テキスト
	public GameObject textScoreNumber;  // スコアテキスト
    public GameObject ButtonPause;

    //外部クラスの参照
    public StageClearDlgManager stageClearDlgManager;
    public GameOverDialogManager gameOverDialogManager;
    public StartDialogManager startDialogManager;
    public AdManager admobManager;

    public enum GAME_MODE{              // ゲーム状態定義
        PLAY,							// プレイ中
		CLEAR,							// クリア
		GAMEOVER,						// ゲームオーバー
	};
	public GAME_MODE gameMode = GAME_MODE.PLAY;	// 初期ゲーム状態

	private int score = 0;				// スコア
	private int displayScore = 0;		// 表示用スコア

    // 一定時間停止処理用の経過時間変数
    private float realDeltaTime = 0.0f;
    private float lastRealTime = 0.0f;

    // Use this for initialization
    void Start () {
        RefreshScore();
        startDialogManager.GetComponent<StartDialogManager>();
        StageStart();
        admobManager.GetComponent<AdManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (score > displayScore) {
			displayScore += 10;

			if (displayScore > score) {
				displayScore = score;
			}

			RefreshScore ();
		}
	}

    //各ステージ開始処理
    public void StageStart() {
        startDialogManager.EnableDialog();
    }

    // ゲームオーバー処理
    public void GameOver () {
        gameMode = GAME_MODE.GAMEOVER;
        //audioSource.PlayOneShot (gameoverSE);
        // (a)自動遷移の場合
        //TextGameOver.SetActive(true);
        //PauseSecond(3.0f);
        //Invoke("GoBackStageSelect", 0.0f);

        // (b)ダイアログ画面遷移の場合
        gameOverDialogManager.GetComponent<GameOverDialogManager>().EnableDialog();
    }

    // ステージクリア処理
    public void StageClear() {
        gameMode = GAME_MODE.CLEAR;
        stageClearDlgManager.GetComponent<StageClearDlgManager>().EnableDialog();

        // セーブデータ更新
        if (PlayerPrefs.GetInt ("CLEAR", 0) < stageNo) {
			PlayerPrefs.SetInt ("CLEAR", stageNo);
		}
	}

	// スコア加算
	public void AddScore (int val) {
		score += val;
		if (score > MAX_SCORE) {
			score = MAX_SCORE;
		}
	}

    // スコア表示を更新
    void RefreshScore () {
		textScoreNumber.GetComponent<Text> ().text = displayScore.ToString ();
	}

    // ステージセレクトシーンに戻る
    public void GoBackStageSelect () {
        SceneManager.LoadScene ("StageSelectScene");
    }

    // 一定時間停止処理
    void PauseSecond(float sec) {
        Time.timeScale = 0.0f;

        for (lastRealTime = 0f, realDeltaTime = 0f; sec > realDeltaTime; ) {
            CalcRealDeltaTime();
        }
        Time.timeScale = 1.0f;
    }

    //現実時間基準で経過時間を計算する
    void CalcRealDeltaTime()
    {
        if (lastRealTime == 0f)
        {
            lastRealTime = Time.realtimeSinceStartup;
        }
        realDeltaTime = Time.realtimeSinceStartup - lastRealTime;
    }

    /// <summary>
    /// ステージ番号をチェックして、広告表示判定を行う
    /// </summary>
    /// <returns>true: 広告を表示･false：広告を表示しない</returns>
    public void CheckStageNo()
    {
        if ( 0 == this.stageNo % 3 ) {
            if (admobManager == null)
            {
                Debug.Log("admobManager のインスタンスがNULL");
                // admobManager.GetComponent<AdManager>();
                Debug.Log("admobManager のインスタンス生成");
            }
            admobManager.GetComponent<AdManager>().ShowInterstitial();
        }    
    }
}
