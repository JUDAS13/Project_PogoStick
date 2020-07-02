using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー操作クラス
/// </summary>
public class PlayerController : MonoBehaviour {

	/// <summary>
	/// プレイヤークラス
	/// </summary>
	private Player player = null;

	/// <summary>
	/// 左ボタン判定
	/// </summary>
	private bool isLeftPush;

	/// <summary>
	/// 右ボタン判定
	/// </summary>
	private bool isRightPush;

	/// <summary>
	/// 初期化処理
	/// </summary>
	public void Init(Player _player) {
		player = _player;
	}

	/// <summary>
	/// アップデート処理
	/// </summary>
	public void UpdateMethod(){

		//左押し時は左回転する。
		if (isLeftPush && !isRightPush || Input.GetKey(KeyCode.LeftArrow)) {
			player.Rotate(Player.RotateDir.LEFT);
		}

		//右押し時は右回転する。
		if (!isLeftPush && isRightPush || Input.GetKey(KeyCode.RightArrow)) {
			player.Rotate(Player.RotateDir.RIGHT);
		}

		//両押時は力をためる。
		if (isLeftPush && isRightPush || Input.GetKey(KeyCode.Space)) {
			player.ChargePower();
		}

		if(Input.GetKeyUp(KeyCode.Space)){
			player.Jump();
		}

		player.UpdateMethod();
	}

	/// <summary>
	/// ジャンプ操作処理
	/// </summary>
	public void Jump() {

		//両ボタンから指を離したらジャンプ
		if (!isLeftPush && !isRightPush) {
			player.Jump();
			Debug.Log("ジャンプ実行");
		}
	}

	/// <summary>
	/// 左ボタン接触
	/// </summary>
	public void PushingLeftButton() { 
		isLeftPush = true;
		Debug.Log("isLeftPush" + isLeftPush);
	}

	/// <summary>
	/// 右ボタン接触
	/// </summary>
	public void PushingRightButton() { 
		isRightPush = true;
		Debug.Log("isRightPush" + isRightPush);
	}

	/// <summary>
	/// 左ボタン非接触
	/// </summary>
	public void UpLeftButton() {
		isLeftPush = false;
		Jump();
		Debug.Log("isLeftPush" + isLeftPush);
	}

	/// <summary>
	/// 右ボタン非接触
	/// </summary>
	public void UpRightButton() {
		isRightPush = false;
		Jump();
		Debug.Log("isRightPush" + isRightPush);
	}
}
