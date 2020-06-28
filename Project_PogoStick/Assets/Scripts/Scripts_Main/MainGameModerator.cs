using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインゲームの進行役クラス
/// </summary>
public class MainGameModerator : StateBaseClass {

	/// <summary>
	/// メインカメラマネージャー
	/// </summary>
	[SerializeField, Header("メインカメラマネージャー")]
	public MainCameraController mainCameraController = null;

	/// <summary>
	/// メインゲームHUD
	/// </summary>
	[SerializeField, Header("メインゲームHUD")]
	private MainGaneHUD mainGameHud = null;

	/// <summary>
	/// 初期化
	/// </summary>
	protected override void Resume() {

		//カメラ初期化
		mainCameraController.Init();

		//HUD初期化
		mainGameHud.Init();

		////コントローラーの初期化。
		//controllerManager.Init();
	}

	/// <summary>
	/// コントローラー用アップデート
	/// </summary>
	protected override void UpdateMethod() {

		base.UpdateMethod();

		////コントローラーから入力ベクトルを取得する。
		//controllerManager.UpdateMethod();
	}

	/// <summary>
	/// カメラワーク用のレイトアップデート
	/// </summary>
	protected override void LateUpdateMethod() {

		base.LateUpdateMethod();

		//HUD表示時のカメラワーク
		//if (IsState(StateShowHud))
		//{

		//	FIRSTに登録されているカメラのトランスフォームに合わせる。
		//	miniGameCamera.UpdateCameraTransform(MiniGameCamera.CAMERA_STATE.FIRST, true);
		//}

	}

    /// <summary>
    /// HUD表示初期化ステート
    /// </summary>
    private void StateShowHudInit() {
		//カメラ初期化
		mainCameraController.Reset();

		Debug.Log("StateShowHudInit");
	}

    /// <summary>
    /// HUD表示ステート
    /// </summary>
    private void StateShowHud() {


		////スタート演出の表示完了したら、レース開始。
		//if (miniGameHud.IsDone()) {
		//    SetState(StateRaceInit, StateRace);
		//    return;
		//}

		////お楽しみのポイントが半減する場合の説明を表記する。
		//if (!miniGameHud.IsShowedExpanation())
		//{
		//    miniGameHud.ShowExplanation();
		//}
		//else
		//{
		//    //お楽しみのポイントが表記し終わったら、カウントする。
		//    miniGameHud.ShowCount();
		//}


		Debug.Log("StateShowHud");
	}

    /// <summary>
    /// Scene_Mainクラスからゲームを実行する。
    /// </summary>
    public void StartPerformance() {
        ResetState();
        SetState(StateShowHudInit, StateShowHud);
    }
}
