using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
	/// <summary>
	/// カメラの状態、またはターゲット
	/// </summary>
	public enum CAMERA_STATE {
		MAX,
	}

	/// <summary>
	/// カメラ本体
	/// </summary>
	[SerializeField, Header("カメラ本体")]
	private Camera camera3D = null;

	/// <summary>
	/// 最初のカメラの揺れの速さ
	/// </summary>
	[SerializeField, Header("最初のカメラの揺れの速さ")]
	private Vector3 shakeFrequency = Vector3.zero;

	/// <summary>
	/// 最初のカメラの揺れの量
	/// </summary>
	[SerializeField, Header("最初のカメラの揺れの量")]
	private Vector3 shakeValue = Vector3.zero;

	/// <summary>
	/// カメラの目標
	/// </summary>
	[SerializeField, Header("カメラの目標を設定")]
	private CameraTarget[] cameraTarget = null;
	[System.Serializable]
	private class CameraTarget {

		[SerializeField, Header("目標のオブジェクトを登録するか、カメラの最終目標を登録する")]
		public GameObject target = null;

		[SerializeField, Range(0, 10), Header("目標までのカメラのスピード")]
		public float toTargetTransformSpeed = 0.0f;

		[SerializeField, Header("目標調整オフセット")]
		public Vector3 offset = Vector3.zero;
	}

	/// <summary>
	/// カメラが目標との距離をキープする距離
	/// </summary>
	[SerializeField, Header("カメラが目標との距離をキープする距離")]
	public float keepDistance;

	/// <summary>
	/// 自転車を追尾座標調整ベクトル        
	/// /// </summary>
	[SerializeField, Header("自転車追尾座標調整ベクトル")]
	private Vector3 followBikerOffset;

	/// <summary>
	/// カメラが目標をキープし続けるスピード
	/// </summary>
	[SerializeField, Header("カメラが目標をキープし続けるスピード")]
	private float keepSpeed = 0;

	/// <summary>
	/// 目標までのレート
	/// </summary>
	private float rate;

	/// <summary>
	/// 位置更新前の座標保存用
	/// </summary>
	private Vector3 oldPosition;

	/// <summary>
	/// 位置更新前の角度保存用
	/// </summary>
	private Quaternion oldRot;

	/// <summary>
	/// 初期角度保存用
	/// </summary>
	private Vector3 tempEuler;

	/// <summary>
	/// 時間設定用
	/// </summary>
	private float oldTime;

	/// <summary>
	/// カメラの初期化。
	/// </summary>
	public void Init()
	{

		//設定ミスがないか確認
		if ((int)CAMERA_STATE.MAX != cameraTarget.Length)
		{
			Debug.LogError("カメラステート分のカメラターゲットを登録してください");
			return;
		}

		//初期カメラ角度の保存
		tempEuler = camera3D.transform.eulerAngles;

		//時間保存
		oldTime = Time.time;

		//カメラ設定値リセット
		Reset();

	}

	/// <summary>
	/// カメラ設定値リセット
	/// </summary>
	/// <param name="_gachaCameraMode"></param>
	public void Reset()
	{
		rate = 0;
		oldPosition = camera3D.transform.position;
		oldRot = camera3D.transform.rotation;
	}

	/// <summary>
	/// カメラを指定した目標のトランスフォームまで変化させます。
	/// </summary>
	/// <param name="_isFirst"></param>
	public void UpdateCameraTransform(CAMERA_STATE _state, bool _isFirst = false)
	{

		if (rate < 1)
		{
			if (_isFirst)
				rate += Time.deltaTime * cameraTarget[(int)_state].toTargetTransformSpeed;
			else
				rate += Time.deltaTime * cameraTarget[(int)_state].toTargetTransformSpeed;

			if (rate >= 1)
				rate = 1;
		}

		if (_isFirst)
		{
			camera3D.transform.position = Vector3.Lerp(oldPosition, cameraTarget[(int)_state].target.transform.position, rate);
			var rotX = Mathf.LerpAngle(oldRot.eulerAngles.x, cameraTarget[(int)_state].target.transform.eulerAngles.x, rate);
			var rotY = Mathf.LerpAngle(oldRot.eulerAngles.y, cameraTarget[(int)_state].target.transform.eulerAngles.y, rate);
			var rotZ = Mathf.LerpAngle(oldRot.eulerAngles.z, cameraTarget[(int)_state].target.transform.eulerAngles.z, rate);
			camera3D.transform.eulerAngles = new Vector3(rotX, rotY, rotZ);
		}
		else
		{
			camera3D.transform.position = Vector3.Lerp(camera3D.transform.position, cameraTarget[(int)_state].target.transform.position, rate);
			var rotX = Mathf.LerpAngle(camera3D.transform.eulerAngles.x, cameraTarget[(int)_state].target.transform.eulerAngles.x, rate);
			var rotY = Mathf.LerpAngle(camera3D.transform.eulerAngles.y, cameraTarget[(int)_state].target.transform.eulerAngles.y, rate);
			var rotZ = Mathf.LerpAngle(camera3D.transform.eulerAngles.z, cameraTarget[(int)_state].target.transform.eulerAngles.z, rate);
			camera3D.transform.eulerAngles = new Vector3(rotX, rotY, rotZ);
		}
	}

	/// <summary>
	/// カメラを指定した目標の座標まで変化させます。
	/// </summary>
	/// <param name="_state"></param>
	/// <param name="_isFirst"></param>
	public void UpdateCameraPosition(CAMERA_STATE _state, bool _isFirst = false)
	{

		if (rate < 1)
		{

			if (_isFirst)
				rate += Time.deltaTime * cameraTarget[(int)_state].toTargetTransformSpeed;

			else
				rate += Time.deltaTime * cameraTarget[(int)_state].toTargetTransformSpeed;

			if (rate >= 1)
				rate = 1;
		}

		if (_isFirst)
			camera3D.transform.position = Vector3.Lerp(oldPosition, cameraTarget[(int)_state].target.transform.position, rate);
		else
			camera3D.transform.position = Vector3.Lerp(camera3D.transform.position, cameraTarget[(int)_state].target.transform.position, rate);

	}

	/// <summary>
	/// カメラが指定した目標を捉え続けます。
	/// </summary>
	/// <param name="_state"></param>
	public void LookTarget(CAMERA_STATE _state)
	{
		Vector3 dir = (cameraTarget[(int)_state].target.transform.position - camera3D.transform.position).normalized;
		Quaternion targetRot = Quaternion.LookRotation(dir);
		camera3D.transform.rotation = Quaternion.Lerp(camera3D.transform.rotation, targetRot, cameraTarget[(int)_state].toTargetTransformSpeed);
	}

	/// <summary>
	/// カメラが指定した目標を捉え続ける。
	/// </summary>
	/// <param name="_state"></param>
	public void LookTarget(CAMERA_STATE _state, bool _offset)
	{
		if (_offset)
		{
			var target = cameraTarget[(int)_state].target.transform.position + cameraTarget[(int)_state].offset;
			Vector3 dir = (target - camera3D.transform.position).normalized;
			Quaternion targetRot = Quaternion.LookRotation(dir);
			camera3D.transform.rotation = Quaternion.Lerp(camera3D.transform.rotation, targetRot, cameraTarget[(int)_state].toTargetTransformSpeed);
		}
	}

	/// <summary>
	/// カメラが指定した目標を追い続けます。
	/// </summary>
	/// <param name="_state"></param>
	public void KeepDistance(CAMERA_STATE _state)
	{
		var target = cameraTarget[(int)_state].target.transform.position + (cameraTarget[(int)_state].target.transform.rotation * (followBikerOffset.normalized * keepDistance));
		camera3D.transform.position = Vector3.Lerp(camera3D.transform.position, target, Time.deltaTime * keepSpeed);
	}

	/// <summary>
	/// カメラが指定した目標を捉え続ける
	/// </summary>
	/// <param name="_state"></param>
	/// <param name="y_Only"></param>
	public void KeepDistance(CAMERA_STATE _state, bool y_Only)
	{
		Vector3 target = new Vector3();
		Quaternion rot;
		if (y_Only)
		{
			rot = Quaternion.Euler(0, cameraTarget[(int)_state].target.transform.eulerAngles.y, 0);
			target = cameraTarget[(int)_state].target.transform.position + (rot * (followBikerOffset.normalized * keepDistance));
		}
		camera3D.transform.position = Vector3.Lerp(camera3D.transform.position, target, Time.deltaTime * keepSpeed);
	}

	/// <summary>
	/// カメラの揺れをさせる関数。
	/// </summary>
	public void ShakeEuler()
	{
		var valueX = Mathf.Sin((Time.time - oldTime) * (Mathf.PI * 2) * shakeFrequency.x) * shakeValue.x;
		var valueY = Mathf.Sin((Time.time - oldTime) * (Mathf.PI * 2) * shakeFrequency.y) * shakeValue.y;
		var valueZ = Mathf.Sin((Time.time - oldTime) * (Mathf.PI * 2) * shakeFrequency.z) * shakeValue.z;
		camera3D.transform.eulerAngles = new Vector3(tempEuler.x + valueX, tempEuler.y + valueY, tempEuler.z + valueZ);
	}

	/// <summary>
	/// カメラコンポーネント取得
	/// </summary>
	/// <returns></returns>
	public Camera GetCamera()
	{
		return camera3D;
	}

	/// <summary>
	/// カメラの目標までのレートを取得
	/// </summary>
	/// <returns></returns>
	public float GetRate()
	{
		return rate;
	}

	/// <summary>
	/// カメラが目標に到着したかを返す関数。
	/// </summary>
	/// <returns></returns>
	public bool IsDone()
	{
		if (rate == 1)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
