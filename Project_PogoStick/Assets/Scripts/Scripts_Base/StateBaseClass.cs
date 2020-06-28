using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// ステート管理ベースクラス
/// </summary>
public abstract class StateBaseClass : MonoBehaviour {

	/// <summary>
	/// メソッド設定用デリケート
	/// </summary>
	public delegate void StateMethod();

	/// <summary>
	/// 初期化メソッド設定用
	/// </summary>
	private StateMethod initMethod;

	/// <summary>
	/// アップデートメソッド設定用
	/// </summary>
	private StateMethod updateMethod;

	/// <summary>
	/// 初期化実行フラグ
	/// </summary>
	protected bool playInit;

	/// <summary>
	/// 初期化
	/// </summary>
	private void Awake() {
		Init();
	}

	/// <summary>
	/// 初期化（動作有効時）
	/// </summary>
	private void OnEnable() {
		Resume();
	}

	/// <summary>
	/// アップデート
	/// </summary>
	private void Update () {
		if (!playInit) {
			
			if (initMethod != null) {
				initMethod();
			}

			playInit = true;
		}
		else {
			if (updateMethod != null) {
				updateMethod();
			}
		}
		UpdateMethod();
	}

	/// <summary>
	/// カメラワーク用アップデート
	/// </summary>
	private void LateUpdate () {
		LateUpdateMethod();
	}

	/// <summary>
	/// ステートを設定する。
	/// </summary>
	/// <param name="_initMethod">初期化メソッド用</param>
	/// <param name="_updateMethod">アップデートメソッド用</param>
	protected void SetState(StateMethod _initMethod, StateMethod _updateMethod) {

		//初期化フラグの設定。
		playInit = false;

		//初期化メソッドの設定。
		initMethod = _initMethod;

		//アップデートメソッドの設定。
		updateMethod = _updateMethod;

	}

	/// <summary>
	/// 現在のステートの取得
	/// </summary>
	/// <returns></returns>
	protected StateMethod GetState(){
		return updateMethod;
	}

	/// <summary>
	/// 現在のステートが指定したステートか判定。
	/// </summary>
	/// <param name="_currentMethod"></param>
	/// <returns></returns>
	protected bool CheckState(StateMethod _currentMethod){
		if(updateMethod == _currentMethod){
			return true;
		}
		return false;
	}

	/// <summary>
	/// ステートのリセット
	/// </summary>
	protected void ResetState(){
		initMethod -= initMethod;
		updateMethod -= updateMethod;
		updateMethod = null;
		initMethod = null;
	}

	/// <summary>
	/// Awakeのオーバーライド用メソッド
	/// </summary>
	protected virtual void Init() { }

	/// <summary>
	/// OnEnableのオーバーライド用メソッド
	/// </summary>
	protected virtual void Resume() { }

	/// <summary>
	/// アップデートのオーバーライド用メソッド
	/// </summary>
	protected virtual void UpdateMethod() { }

	/// <summary>
	/// カメラワークのオーバーライド用メソッド
	/// </summary>
	protected virtual void LateUpdateMethod() { }

}