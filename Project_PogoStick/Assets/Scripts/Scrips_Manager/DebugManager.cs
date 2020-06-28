using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// デバッグマネージャー
/// </summary>
public class DebugManager : SingletonMonoBehaviour<DebugManager> {

	/// <summary>
	/// デバッグモードのフラグ取得。デバッグ時は戻り値を直接変更。
	/// </summary>
	/// <returns></returns>
	public bool IsDebugMode(){
		return false;
	}

}
