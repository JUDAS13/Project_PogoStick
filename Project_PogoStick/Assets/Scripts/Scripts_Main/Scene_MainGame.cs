using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;

/// <summary>
/// メインシーン管理クラス
/// </summary>
public class Scene_MainGame : StateBaseClass {

    /// <summary>
    /// MainGameManager
    /// </summary>
    [SerializeField, Header("MainGameManager")]
    private MainGameModerator mainGameModerator = null;

    /// <summary>
    /// ステージ生成アンカー
    /// </summary>
    [SerializeField, Header("ステージ生成アンカー")]
    private Transform stageGenerateAnchor = null;

    /// <summary>
    /// 初期化
    /// </summary>
    protected override void Resume() {

        //ステージ生成アンカーにステージを生成。
        StageListManager.Instance.GenerateStagePrefab(GameDataParams.StageParam.StageType.Stage_0, stageGenerateAnchor.position, Quaternion.identity, stageGenerateAnchor);

        //ステージ情報に合わせてプレイヤー位置を設定。
        mainGameModerator.player.transform.position = StageListManager.Instance.GetMainStage().playerFirstAnchor.position;
        mainGameModerator.player.transform.rotation = StageListManager.Instance.GetMainStage().playerFirstAnchor.rotation;

        //ステージ情報に合わせてカメラ位置を設定。
        mainGameModerator.mainCameraController.GetCamera().transform.position = StageListManager.Instance.GetMainStage().cameraFirstAnchor.position;
        mainGameModerator.mainCameraController.GetCamera().transform.rotation = StageListManager.Instance.GetMainStage().cameraFirstAnchor.rotation;

        //ステートを初期化へ移行。
        SetState(StateResetMainGameInit, StateResetMainGame);

    }

    /// <summary>
    /// 初期ステート初期化
    /// </summary>
	private void StateResetMainGameInit(){

    }

    /// <summary>
    /// 初期ステート
    /// </summary>
    private void StateResetMainGame(){
        ResetState();
        mainGameModerator.StartPerformance();
    }
}