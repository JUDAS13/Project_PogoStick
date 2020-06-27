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
    private MainGameManager mainGameManager;

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Resume() {
        //ステートを初期化へ移行。
        SetState(StateResetMainGameInit, StateResetMainGame);
    }

    private void StateResetMainGameInit(){
        Debug.Log("StateResetMainGameInit");
    }

    private void StateResetMainGame(){
        Debug.Log("StateResetMainGame");

        ResetState();
        mainGameManager.StartPerformance();
    }
}