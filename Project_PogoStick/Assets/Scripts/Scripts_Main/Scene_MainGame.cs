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

    [SerializeField, Header("ステージ生成アンカー")]
    private Transform stageGenerateAnchor = null;

    /// <summary>
    /// 初期化
    /// </summary>
    protected override void Resume() {
        //ステートを初期化へ移行。
        SetState(StateResetMainGameInit, StateResetMainGame);
    }

    protected override void UpdateMethod()
	{
		base.UpdateMethod();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StageListManager.Instance.GenerateStagePrefab(GameDataParams.StageParam.StageType.Stage_0, stageGenerateAnchor.position, Quaternion.identity, stageGenerateAnchor);
            mainGameModerator.mainCameraController.GetCamera().transform.position = StageListManager.Instance.GetMainStage().cameraFirstAnchor.position;
            mainGameModerator.mainCameraController.GetCamera().transform.rotation = StageListManager.Instance.GetMainStage().cameraFirstAnchor.rotation;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StageListManager.Instance.GenerateStagePrefab(GameDataParams.StageParam.StageType.Stage_1, stageGenerateAnchor.position, Quaternion.identity, stageGenerateAnchor);
            mainGameModerator.mainCameraController.GetCamera().transform.position = StageListManager.Instance.GetMainStage().cameraFirstAnchor.position;
            mainGameModerator.mainCameraController.GetCamera().transform.rotation = StageListManager.Instance.GetMainStage().cameraFirstAnchor.rotation;
        }
    }

	private void StateResetMainGameInit(){
        Debug.Log("StateResetMainGameInit");
    }

    private void StateResetMainGame(){
        Debug.Log("StateResetMainGame");

        ResetState();
        mainGameModerator.StartPerformance();
    }
}