using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public PauseDialogManager pauseDialogManager;
    public GameObject ball;

    //ボタン
    public GameObject ButtonPause;
    public GameObject ButtonVolume;

    //ボリュームON/OFFボタン表示切替
    public Sprite volumeOn;
    public Sprite volumeOff;

    //ブロック表示切替
    public SpriteRenderer spRenderBlock;
    public Sprite blockOn;
    public Sprite blockOff;

    // Use this for initialization
    void Start()
    {
        this.spRenderBlock = GetComponent<SpriteRenderer>();
        ball = GameObject.Find("Ball");
        //Soundボリュームの画像設定
        SetSoundButtonImage();
    }

    // Update is called once per frame
    //void FixedUpdate() {
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            //ダイアログ画面表示中はブロッククリックのイベントは無効
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
                return;
            }
            OnClickBlock();
        }
    }

    // マウスクリック、またはタップ
    public void OnClickBlock()
    {

        //スクリーンから見たマウスの座標を得る
        Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //コライダーを持つオブジェクト＝クリックされた場所の座標
        Collider2D tapCollider = Physics2D.OverlapPoint(tapPoint);


        //取得されたcolliderのgameObjectと、このスクリプトがアタッチされたオブジェクトが一致した場合
        if (tapCollider != null && this.gameObject.Equals(tapCollider.gameObject))
        {
            //ブロックの表示・透過切り換えを実行
            SwitchBlockDisplay(tapCollider);
        }
    }

    /// <summary>
    /// ブロックの表示・透過切り換え
    /// </summary>
    /// <param name="collider"></param>
    public void SwitchBlockDisplay(Collider2D collider)
    {
        //表示切替
        if (!collider.isTrigger)
        {
            // 透過していない場合 → 透過用のブロックに
            this.spRenderBlock.sprite = blockOff;
        }
        else
        {
            // 透過している場合 → 不透過用のブロックに
            this.spRenderBlock.sprite = blockOn;
        }
        //透過の切り換え
        collider.isTrigger = !collider.isTrigger;
    }

    /// <summary>
    /// ポースボタン押下イベント処理
    /// </summary>
    public void OnPauseButtonClick()
    {
        //ボールをとめる
        ball.GetComponent<PlayerManager>().StopPlayer();

        //ポーズダイアログ表示
        pauseDialogManager.EnableDialog();
    }

    /// <summary>
    /// サウンドON/OFFボタン押下イベント処理
    /// </summary>
    public void OnSoundButtonClick() {

        if (0.0f != AudioListener.volume) {
            // 音量ON/OFF切り換え
            AudioListener.volume = 0.0f;
        } else {
            // 音量ON/OFF切り換え
            AudioListener.volume = 1.0f;
        }
        SetSoundButtonImage();
    }

    /// <summary>
    /// サウンドON/OFFボタン画像設定処理
    /// </summary>
    private void SetSoundButtonImage() {
        if (0.0f == AudioListener.volume) {
            ButtonVolume.GetComponent<Image>().sprite = volumeOff;
        } else {
            ButtonVolume.GetComponent<Image>().sprite = volumeOn;
        }
    }
}
