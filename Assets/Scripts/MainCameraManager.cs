using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : MonoBehaviour {

    private const float CAMERA_Y_OFFSET = 2.0f;
	public GameObject player;		//プレイヤーオブジェクト
    //下橋の透明オブジェクト
    public GameObject OutZone;

	// Update is called once per frame
	void Update () {
		//プレイヤーキャラをカメラが追いかけていくようにする
		if (player != null) {			//プレイヤーキャラは存在しているか？
			//存在していればカメラポジションを設定

			//カメラポジションを決定
            //現在のプレイヤーのY座標をカメラのY座標に設定
            Vector3 cameraPos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);

            //カメラが上端を越えて動かないように
            if (cameraPos.y > 0.0f){
				cameraPos.y = 0.0f;
			} else if (cameraPos.y < OutZone.transform.position.y + CAMERA_Y_OFFSET) {
                cameraPos.y = OutZone.transform.position.y + CAMERA_Y_OFFSET;
            }
            //カメラポジションを変更
            transform.position = cameraPos;
		}
	}
}
