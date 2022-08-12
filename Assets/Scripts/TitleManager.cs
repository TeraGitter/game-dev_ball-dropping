using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //クリアステージの初期化
        PlayerPrefs.GetInt("CLEAR", 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	//スタートボタンを押した
	public void PushStartButton () {
		SceneManager.LoadScene ("StageSelectScene");	//ステージ選択シーンへ
	}

    //設定ボタンを押した
    public void PushSettingButton()
    {
        //設定画面へ遷移
        SceneManager.LoadScene("SettingScene");
    }
}
