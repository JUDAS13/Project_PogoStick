﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン読み込み管理クラス
/// </summary>
public class SceneLoadManager : SingletonMonoBehaviour<SceneLoadManager> {

	/// <summary>
	/// シーン定義
	/// </summary>
	public enum SceneType{
		Title,
		Gacha,
		CharactorEdit,
		StageSelect,
		Main
	}

	/// <summary>
	/// シーン名前リスト
	/// </summary>
	private string[] sceneNamesList = {
		"Scene_Title",
		"Scene_Gacha",
		"Scene_CharactorEdit",
		"Scene_StageSelect",
		"Scene_Main"
	};

	/// <summary>
	/// フェード用メッシュレンダラー
	/// </summary>
	[SerializeField, Header("フェード用メッシュレンダラー")]
	private MeshRenderer fadeMeshrenderer = null;

	/// <summary>
	/// シーン遷移中フラグ
	/// </summary>
	private bool isMovingScene;

	/// <summary>
	/// フェード用マテリアル。
	/// </summary>
	private Material fadeMat;

	/// <summary>
	/// 初期化
	/// </summary>
	private new void Awake() {

		base.Awake();

		//シーン遷移時に削除されないように設定。
		DontDestroyOnLoad(this.gameObject);

		//マテリアルをコピー
		fadeMat = fadeMeshrenderer.material;

		//メッシュ非表示
		fadeMeshrenderer.enabled = false;
	}

	/// <summary>
	/// シーン遷移を実行する。
	/// </summary>
	/// <param name="_sceneType"></param>
	/// <param name="_interval"></param>
	public void MoveScene(SceneType _sceneType, float _interval = 1f) {

		//シーン遷移中は遷移させない。
		if (isMovingScene)
			return;

		//シーン遷移コルーチンの再生。
		StartCoroutine(_MoveScene(_sceneType, _interval));
	}

	/// <summary>
	/// シーン遷移コルーチン
	/// </summary>
	/// <param name="_sceneType"></param>
	/// <param name="_interval"></param>
	/// <returns></returns>
	private IEnumerator _MoveScene(SceneType _sceneType, float _interval) {

		//初期化----------------------------------------------------------------------------------------------------------------------------
		#region //初期化----------------------------------------------------------------------------------------------------------------------------

		//カラーの用意
		var tempColor = new Color();

		//カウント用タイマーの初期化。
		float timer = 0.0f;

		//インターバルの設定。
		float interval = _interval;

		//シーン遷移フラグの設定。
		isMovingScene = true;
		fadeMeshrenderer.enabled = isMovingScene;

		#endregion //初期化----------------------------------------------------------------------------------------------------------------------------

		yield return null;

		//フェードアウト--------------------------------------------------------------------------------------------------------------------
		#region //フェードアウト--------------------------------------------------------------------------------------------------------------------

		while (timer <= interval) {
			timer += Time.deltaTime;
			tempColor = new Color(0.0f, 0.0f, 0.0f, timer / interval);
			fadeMat.SetColor("_Color", tempColor);
			fadeMeshrenderer.material = fadeMat;
			yield return null;
		}

		//シーン切り替え。
		SceneManager.LoadScene(sceneNamesList[(int)_sceneType]);

		//タイマー初期化。
		timer = 0.0f;

		#endregion //フェードアウト--------------------------------------------------------------------------------------------------------------------

		yield return null;

		//フェードイン----------------------------------------------------------------------------------------------------------------------
		#region //フェードイン----------------------------------------------------------------------------------------------------------------------

		while (timer <= interval) {
			timer += Time.deltaTime;
			tempColor = new Color(0.0f, 0.0f, 0.0f, 1 - (timer / interval));
			fadeMat.SetColor("_Color", tempColor);
			fadeMeshrenderer.material = fadeMat;
			yield return null;
		}

		//シーン遷移フラグをOFF
		isMovingScene = false;
		fadeMeshrenderer.enabled = isMovingScene;

		#endregion //フェードイン----------------------------------------------------------------------------------------------------------------------

		//コルーチンの終了。
		yield break;

	}
}
