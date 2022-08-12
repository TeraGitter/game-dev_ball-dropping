using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class StartDialogManager : MonoBehaviour {

    //効果音：ステージスタート
    private AudioSource audioSource;
    public AudioClip stageStartSE;

    //StartDialog ui部品
    public GameObject ButtonStart;
    public GameObject TextStageStart;

    //ゲーム一時停止・再開処理用
    public GameObject ball;

    //ゲーム・ステータス遷移用
    public GameManager gameManager;

    //ポーズボタン表示切り換え
    public GameObject ButtonPause;

    void Start () {

        gameManager = GetComponent<GameManager>();
        ball = GameObject.Find("Ball");
        ButtonPause = GameObject.Find("ButtonPause");
        audioSource = gameManager.GetComponent<AudioSource>();
    }

    //非表示・有効のボタンを押下（タップ）イベント処理
    public void onButtonStart() {

        gameManager.GetComponent<GameManager>().gameMode = GameManager.GAME_MODE.PLAY;
        //効果音
        audioSource.PlayOneShot(stageStartSE);

        DisableDialog();

    }

    public void EnableDialog() {
        ball.GetComponent<PlayerManager>().StopPlayer();
        ButtonPause.SetActive(false);
        TextStageStart.SetActive(true);
        ButtonStart.SetActive(true);
    }

    public void DisableDialog() {
        TextStageStart.SetActive(false);
        ButtonStart.SetActive(false);
        ButtonPause.SetActive(true);
        //止まっていたボールを動かす
        ball.GetComponent<PlayerManager>().RestartPlayer();

    }
}
