using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シングルトンクラス
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

	/// <summary>
	/// 可変インスタンス
	/// </summary>
	private static T _instance;

	/// <summary>
	/// インスタンス取得プロパティ
	/// </summary>
	public static T Instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<T>();
				if (_instance == null) {
					Debug.LogError(typeof(T) + "のインスタンスが見つかりません");
				}
			}
			return _instance;
		}
	}

	/// <summary>
	/// 初期化
	/// </summary>
	protected void Awake() {
		CheckInstance();
	}

	/// <summary>
	/// インスタンスの確認
	/// </summary>
	/// <returns></returns>
	protected bool CheckInstance() {
		if (this == Instance){
			return true;
		} else{
			Destroy(gameObject);
			return false;
		}
	}

}