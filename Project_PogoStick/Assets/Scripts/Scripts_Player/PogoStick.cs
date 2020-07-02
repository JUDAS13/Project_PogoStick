using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングクラス
/// </summary>
public class PogoStick : MonoBehaviour {

	/// <summary>
	/// ジャンプ力の基準パワー
	/// </summary>
	[SerializeField, Header("ジャンプ力の基準パワー")]
	public float baseJumpPower;

	/// <summary>
	/// ジャンプ力の上限
	/// </summary>
	[SerializeField, Header("ジャンプ力の上限")]
	public float jumpPowerLimit;

	/// <summary>
	/// RAYの原点調整
	/// </summary>
	[SerializeField, Header("RAYの原点調整")]
	public float rayOriginOffset;

	/// <summary>
	/// RAYの長さ
	/// </summary>
	[SerializeField, Header("RAYの長さ")]
	public float rayDistance;

	/// <summary>
	/// RAYを地面に突き刺す長さ
	/// </summary>
	[SerializeField, Header("RAYを地面に突き刺す長さ")]
	public float rayPointOffset;

	[HideInInspector]
	public Vector3 chargePower;

}
