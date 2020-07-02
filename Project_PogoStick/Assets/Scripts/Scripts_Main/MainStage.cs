using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインステージ情報クラス
/// </summary>
public class MainStage : MonoBehaviour {

	/// <summary>
	/// カメラ初期位置
	/// </summary>
	[SerializeField, Header("カメラ初期位置")]
	public Transform cameraFirstAnchor;

	/// <summary>
	/// プレイヤー初期位置
	/// </summary>
	[SerializeField, Header("プレイヤー初期位置")]
	public Transform playerFirstAnchor;

}
