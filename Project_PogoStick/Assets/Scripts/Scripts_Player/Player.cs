using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの制御クラス
/// </summary>
public class Player : MonoBehaviour {

	/// <summary>
	/// 行動定義
	/// </summary>
	public enum Action {
		GROUND,  //着地時
		AIR,     //空中時
		CLASH,   //衝突時
		GOAL     //ゴール時
	}
	private Action action = Action.GROUND;

	/// <summary>
	/// 回転定義
	/// </summary>
	public enum RotateDir {
		LEFT,
		RIGHT
	}

	/// <summary>
	/// リジッドボディ
	/// </summary>
	[SerializeField, Header("リジッドボディ")]
	public Rigidbody rigidBody = null;

	/// <summary>
	/// コライダーオブジェクト
	/// </summary>
	[SerializeField, Header("コライダーオブジェクト")]
	private GameObject colliderObj = null;

	/// <summary>
	/// ホッピングクラス
	/// </summary>
	[SerializeField,Header("ホッピングクラス")]
	private PogoStick pogoStick;

	/// <summary>
	/// ゴールの座標
	/// </summary>
	[HideInInspector]
	public Vector3 goalPos = Vector3.zero;

	private float oldTime;

	public void Init() {
		
	}


	public void UpdateMethod() {
		if (Time.time > oldTime + 0.1f) {
			GroundJudge();
		}	
	}

	/// <summary>
	/// 設置判定
	/// </summary>
	public void GroundJudge() {

		//Rayの原点設定
		var origin = transform.position + transform.rotation * new Vector3(0.0f, pogoStick.rayOriginOffset, 0.0f);

		//Rayの射出方向設定
		var dir = transform.rotation * Vector3.down;

		//Rayが地面と衝突時
		if (Physics.Raycast(origin, dir, out RaycastHit hit, pogoStick.rayDistance) && hit.collider.tag == "Ground") {

			//重力を停止。
			rigidBody.useGravity = false;
			rigidBody.velocity = Vector3.zero;

			//自身の座標を地面と接触したポイントに設定。
			transform.position = hit.point + (transform.rotation * Vector3.down * pogoStick.rayPointOffset);

			//着地に判定。
			action = Action.GROUND;
		}
		
		else //接触していない場合は空中時と判定 
		{
			//空中に設定
			action = Action.AIR;
			
			//重力をON
			rigidBody.useGravity = true;
		}


		//デバッグ用
		Debug.DrawLine(origin, origin + dir * pogoStick.rayDistance, Color.red);
	}

	/// <summary>
	/// プレイヤーがゴールに吸い込まれる
	/// </summary>
	public void InhaleToGoal() {

		//衝突判定を全てオフ
		colliderObj.SetActive(false);
		rigidBody.isKinematic = true;
		rigidBody.useGravity = false;

		//回転させる
		float rotateSpeed = 5;
		transform.Rotate(0, 0, rotateSpeed);

		//ゴールに向かわせる。
		transform.position += (goalPos - transform.position) * Time.deltaTime;

		//小さくなっていく
		transform.localScale += new Vector3(-1, -1, -1) * Time.deltaTime;
		if (transform.localScale.x < 0.0f || transform.localScale.y < 0.0f) transform.localScale = Vector3.zero;
	}

	/// <summary>
	/// 回転
	/// </summary>
	/// <param name="_rotateDir"></param>
	public void Rotate(RotateDir _rotateDir) {
		if (_rotateDir == RotateDir.LEFT) {
			rigidBody.angularVelocity = Vector3.back * Time.deltaTime * 10;
		}
		else {
			rigidBody.angularVelocity = Vector3.forward * Time.deltaTime * 10;
		}
	}

	/// <summary>
	/// チャージ処理
	/// </summary>
	public void ChargePower() {

		////ジャンプパワー上限ならリターン
		//if ((pogoStick.chargePower * pogoStick.baseJumpPower).magnitude > pogoStick.jumpPowerLimit)
		//	return;

		//パワーをチャージ
		pogoStick.chargePower += transform.rotation * Vector3.up * Time.deltaTime;

	}

	/// <summary>
	/// ジャンプ処理
	/// </summary>
	public void Jump() {

		//重力をON
		rigidBody.useGravity = true;

		//上方向にジャンプ。
		rigidBody.AddForce(pogoStick.chargePower * pogoStick.baseJumpPower, ForceMode.Impulse);

		//チャージしたパワーを戻す。
		pogoStick.chargePower = Vector3.zero;

		//空中時に設定。
		action = Action.AIR;

		oldTime = Time.time;
	}
}