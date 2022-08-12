using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseDialogManager : MonoBehaviour {

    // エディタのインスペクタで定義されている Canvas名の変数を割り当てる
    public GameObject DialogPanel;
    public GameObject ButtonRestart;
    public GameObject ButtonSelectStage;
    public GameObject ButtonContinue;
    public GameObject ball;
    public GameObject ButtonPause;

    //外部クラスの参照
    public GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        ball = GameObject.Find("Ball");
    }

    // Continue ボタンと関連づけたイベントハンドラ関数
    public void onButtonContinue()
    {
        // Canvas を無効にする。(ダイアログを閉じる)
        DisableDialog();

        // ボールの移動を再開する
        ball.GetComponent<PlayerManager>().RestartPlayer();
    }

    // Restart ボタンと関連づけたイベントハンドラ関数
    public void onButtonRestart()
    {
        // Canvas を無効にする。(ダイアログを閉じる)
        DisableDialog();

        // 当該シーンを再読み込み
        SceneManager.LoadScene("GameScene" + gameManager.GetComponent<GameManager>().stageNo);	//ゲームシーンへ
    }

    // SelectStage ボタンと関連づけたイベントハンドラ関数
    public void onButtonSelectStage()
    {
        // Canvas を無効にする。(ダイアログを閉じる)
        DisableDialog();

        // 当該シーンを再読み込み
        gameManager.GoBackStageSelect();	//ゲームシーンへ
    }

    //ダイアログを表示する
    public void EnableDialog()
    {
        ButtonPause.SetActive(false);
        DialogPanel.SetActive(true);
        ButtonContinue.SetActive(true);
        ButtonRestart.SetActive(true);
        ButtonSelectStage.SetActive(true);
    }

    //ダイアログを非表示にする
    public void DisableDialog()
    {
        DialogPanel.SetActive(false);
        ButtonContinue.SetActive(false);
        ButtonRestart.SetActive(false);
        ButtonSelectStage.SetActive(false);
        ButtonPause.SetActive(true);
    }
}
