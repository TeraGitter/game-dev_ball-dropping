using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class PlayerManager : MonoBehaviour {
	public GameObject gameManager;		// ゲームマネージャー
    public LayerMask blockLayer;        // ブロックレイヤー
    public Rigidbody2D rbody;		    // プレイヤー制御用rigidbody2D

	private const float MOVE_SPEED = 3;	// 移動速度固定値

    // 効果音用
    private AudioSource audioSource;
    
    // 効果音：ボールの着地
    public AudioClip ballLandingSE;
    // 効果音：ボールのスター取得
    public AudioClip getStarSE;
    // 効果音：ボールのボム衝突
    public AudioClip collisionBombSE;
    // 効果音：ボールのゴール(フラグ)到着
    public AudioClip arrivedGoalSE;

    //前回のフレームでボールが空中かどうかの判定用
    bool isBallITAir;


    // Use this for initialization
    void Start () {
        audioSource = gameManager.GetComponent<AudioSource> ();
        rbody = GetComponent<Rigidbody2D> ();
        // 空中状態から開始
        isBallITAir = true;
    }
 
    // 衝突処理
    private void OnTriggerEnter2D(Collider2D col)
    {
        // プレイ中でなければ衝突判定は行わない
        if (gameManager.GetComponent<GameManager>().gameMode != GameManager.GAME_MODE.PLAY) {
            return;
        }

        //ボールが落ちてブロックに衝突した場合
        if ("Block".Equals(col.gameObject.tag)) {

            // 空中フラグがONの場合 → OFFへ ＆ 効果音
            if (isBallITAir) {
                //ボールを回転させる
                this.RestartPlayer();
                //効果音
                audioSource.PlayOneShot(ballLandingSE);
 
                isBallITAir = !isBallITAir;
                Debug.Log("空中フラグ：ON → OFF");
            }
        }
        
        //停止したBombに衝突時、又は移動範囲外に出た場合
        if ("Bomb".Equals(col.gameObject.tag) || "Trap".Equals(col.gameObject.tag))
        {
            //ボール停止処理
            StopPlayer();

            //効果音
            audioSource.PlayOneShot(collisionBombSE);
            gameManager.GetComponent<GameManager>().GameOver();
            DestroyPlayer();
        }

        if ("Goal".Equals(col.gameObject.tag))
        {
            //ボール移動停止および重力無効化
            rbody.velocity = Vector2.zero;
            rbody.isKinematic = true;

            //効果音
            audioSource.PlayOneShot(arrivedGoalSE);
            gameManager.GetComponent<GameManager>().StageClear();
        }

        //動くBombに衝突時
        if ("Enemy".Equals(col.gameObject.tag))
        {
            // ボールのミスイベント（ボールの停止・消す）
            StopPlayer();
            gameManager.GetComponent<GameManager>().GameOver();
            DestroyPlayer();
        }

        if ("Star".Equals(col.gameObject.tag))
        {
            //効果音
            audioSource.PlayOneShot(getStarSE);
            col.gameObject.GetComponent<StarManager>().GetStar();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        // 空中フラグがOFFの場合 → ONへ
        if (!isBallITAir) {
            isBallITAir = !isBallITAir;
            // 02.17 Add
            this.RestartPlayer();
            Debug.Log("空中フラグ：OFF → ON");
        }
    }

    //ボール移動・回転停止および重力無効化処理
    public void StopPlayer() {
        rbody.velocity = Vector2.zero;
        rbody.freezeRotation = true;
        rbody.isKinematic = true;
    }

    //ボール移動・回転停止および重力有効化処理
    public void RestartPlayer()
    {
        Debug.Log("RestartPlayer called!");
        rbody.AddForce(new Vector2(1f, 0f));
        rbody.freezeRotation = false;
        rbody.isKinematic = false;
    }

    // プレイヤーオブジェクト削除処理
    public void DestroyPlayer () {
		gameManager.GetComponent<GameManager> ().gameMode = GameManager.GAME_MODE.GAMEOVER;
 		//  コライダーを削除
		CircleCollider2D circleCollider = GetComponent<CircleCollider2D> ();
		Destroy (circleCollider);
		Destroy (this.gameObject , 3.0f);
	}
}
