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
	/// プレイヤーコントローラー
	/// </summary>
	[SerializeField, Header("プレイヤーコントローラー")]
	private PlayerController playerController;

	/// <summary>
	/// プレイヤー
	/// </summary>
	[SerializeField,Header("プレイヤー")]
	public Player player;

	/// <summary>
	/// 初期化
	/// </summary>
	protected override void Resume() {

		//カメラ初期化
		mainCameraController.Init();

		//HUD初期化
		mainGameHud.Init();

		//コントローラーの初期化
		playerController.Init(player);
	}

    /// <summary>
    /// HUD表示初期化ステート
    /// </summary>
    private void StateShowHudInit() {
		mainCameraController.Reset();
	}

    /// <summary>
    /// HUD表示ステート
    /// </summary>
    private void StateShowHud() {
		playerController.UpdateMethod();
	}

    /// <summary>
    /// Scene_Mainクラスからゲームを実行する。
    /// </summary>
    public void StartPerformance() {
        ResetState();
        SetState(StateShowHudInit, StateShowHud);
    }
}
