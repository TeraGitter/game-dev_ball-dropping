using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverDialogManager : MonoBehaviour {

    // エディタのインスペクタで定義されている Canvas名の変数を割り当てる
    public GameObject DialogPanel;
    public GameObject textGameOver;
    public GameObject ButtonRestart;
    public GameObject ButtonSelectStage;
    public GameObject ButtonPause;
    public GameManager gameManager;

    // Use this for initialization
    void Start () {
        // ダイアログを表示するときまで部品を無効にしておく。

        gameManager = GetComponent<GameManager>();

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

    //ダイアログを非表示にする
    public void EnableDialog()
    {
        ButtonPause.SetActive(false);
        DialogPanel.SetActive(true);
        textGameOver.SetActive(true);
        ButtonRestart.SetActive(true);
        ButtonSelectStage.SetActive(true);
    }

    //ダイアログを非表示にする
    public void DisableDialog() {
        textGameOver.SetActive(false);
        ButtonRestart.SetActive(false);
        ButtonSelectStage.SetActive(false);
        DialogPanel.SetActive(false);
        ButtonPause.SetActive(true);
    }
}
