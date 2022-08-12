using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClearDlgManager : MonoBehaviour {

    // エディタのインスペクタで定義されている Canvas名の変数を割り当てる
    public GameObject DialogPanel;
    public GameObject TextStageClear;
    public GameObject ButtonSelectStage;
    public GameObject ButtonNextStage;
    public GameObject ButtonPause;
    public GameManager gameManager;
    public AdManager admobManager;

    // Use this for initialization
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        admobManager = GetComponent<AdManager>();
    }
    // Restart ボタンと関連づけたイベントハンドラ関数
    public void onButtonNextStage()
    {
        // Canvas を無効にする。(ダイアログを閉じる)
        DisableDialog();

        // 次のシーンを読み込み
        SceneManager.LoadScene("GameScene" + (gameManager.GetComponent<GameManager>().stageNo + 1).ToString());
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
        TextStageClear.SetActive(true);
        ButtonNextStage.SetActive(true);
        ButtonSelectStage.SetActive(true);
    }

    //ダイアログを非表示にする
    public void DisableDialog()
    {
        // ステージ番号をチェックして広告表示などを判定
        // gameManager.CheckStageNo(admobManager);
        gameManager.CheckStageNo();

        DialogPanel.SetActive(false);
        TextStageClear.SetActive(false);
        ButtonNextStage.SetActive(false);
        ButtonSelectStage.SetActive(false);
        ButtonPause.SetActive(true);
    }
}
