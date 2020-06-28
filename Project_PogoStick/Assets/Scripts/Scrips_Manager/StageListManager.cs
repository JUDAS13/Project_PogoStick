using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージリストマネージャークラス
/// </summary>
public class StageListManager : SingletonMonoBehaviour<StageListManager> {

	/// <summary>
	/// ステージプレハブ
	/// </summary>
	[SerializeField, Header("ステージプレハブ")]
	public MainStage[] stagePrefabs = null;

	/// <summary>
	/// 生成ステージ
	/// </summary>
	private GameObject generateStage = null;

	/// <summary>
	/// 初期化
	/// </summary>
	private void Start() {
		DontDestroyOnLoad(this);
	}

	/// <summary>
	/// ステージプレハブを生成する。
	/// </summary>
	/// <param name="_stageType"></param>
	/// <param name="_pos"></param>
	/// <param name="_rot"></param>
	/// <param name="_parent"></param>
	public void GenerateStagePrefab(GameDataParams.StageParam.StageType _stageType,Vector3 _pos,Quaternion _rot,Transform _parent){

		if (generateStage != null) {
			Destroy(generateStage);
			generateStage = null;
		}
		
		generateStage = Instantiate(stagePrefabs[(int)_stageType].gameObject, _pos, _rot, _parent);
	}

	/// <summary>
	/// 生成したステージを削除する。
	/// </summary>
	public void DestroyGenerateStage() {
		if (generateStage != null) {
			Destroy(generateStage);
			generateStage = null;
		} else {
			Debug.LogError("生成したステージが存在しません");
		}
	}

	public MainStage GetMainStage(){
		return generateStage.GetComponent<MainStage>();
	}
}
