using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour {

    public GameObject[] stageButtons;   //ステージ選択ボタン配列

    //クリア済のステージNo.
    int clearStageNo = 0;
    //ページ切り換え
    public GameObject[] pages;
    
    //現在のページのインデクス
    public int curPageIdx = 0;

    //ページ切り換えボタン
    public GameObject buttonPageLeft;
    public GameObject buttonPageRight;

    // Use this for initialization
    void Start () {
        //どのステージまでクリアしているのかをロード（セーブされていなければ「０」）
        clearStageNo = PlayerPrefs.GetInt ("CLEAR", 0);
        
        //ステージボタンを有効化
        for (int i = 0; i <= stageButtons.GetUpperBound(0); i++) {
			bool buttonEnable;

			if (clearStageNo < i) {
				buttonEnable = false;	//前ステージをクリアしていなければ無効
			}else{
				buttonEnable = true;	//前ステージをクリアしていれば有効
			}

            stageButtons[i].GetComponent<Button>().interactable = buttonEnable; //ボタンの有効/無効を設定
        }

        //ページ切り換えボタン生成
        buttonPageLeft = GameObject.Find("ButtonPageLeft");
        buttonPageRight = GameObject.Find("ButtonPageRight");

        //各ページオブジェクト生成
        for (int i = 0; i <= pages.GetUpperBound(0); i++) {
            pages[i] = GameObject.Find("Page" + (i + 1).ToString());
            pages[i].SetActive(false);
        }

        //現在クリアしたステージのあるページを表示
        curPageIdx = clearStageNo / 6;
        SetCurrentPage();
    }

    //ステージ選択ボタンを押した
    public void PushStageSelectButton (int stageNo) {
		SceneManager.LoadScene ("GameScene" + stageNo);	//ゲームシーンへ
	}

    //設定ボタンを押した
    public void PushSettingButton() {
        //設定画面へ遷移
        SceneManager.LoadScene("SettingScene");
    }

    //→（次の）ページ移動ボタン押下イベント処理
    public void MoveNextPage() {

        if (curPageIdx < pages.GetUpperBound(0)) {
            pages[curPageIdx].SetActive(false);
            curPageIdx++;
            SetCurrentPage();
        }
    }

    //←（前の）ページ移動ボタン押下イベント処理
    public void MovePrevPage()
    {
        if (0 < curPageIdx) {
            pages[curPageIdx].SetActive(false);
            curPageIdx--;
            SetCurrentPage();
        }
    }

    private void SetCurrentPage() {

        pages[curPageIdx].SetActive(true);
        buttonPageRight.GetComponent<Button>().interactable = false;

        // 左矢印ボタン：選択中のページが、1ページ目のみOFFにする
        buttonPageLeft.GetComponent<Button>().interactable = (curPageIdx <= 0) ? false : true;
        
        // 右矢印ボタン：末尾ページ以外、AND 当該ページ末尾ステージクリアの場合、ONにする
        buttonPageRight.GetComponent<Button>().interactable =
            ( curPageIdx < pages.GetUpperBound(0) || 
             (curPageIdx == pages.GetUpperBound(0) && clearStageNo > 0 && clearStageNo % 6 == 0) ) ?
            true : false;
    }
}
