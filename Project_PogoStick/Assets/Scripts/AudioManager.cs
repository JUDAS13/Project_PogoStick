using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オーディオ管理クラス
/// </summary>
public class AudioManager : SingletonMonoBehaviour<AudioManager> {

	/// <summary>
	/// BGM定義
	/// </summary>
	public enum BGM_TYPE {
		FIRST
	}

	/// <summary>
	/// SE定義
	/// </summary>
	public enum SE_TYPE {
		FIRST
	}

	/// <summary>
	/// BGM用AudioSource
	/// </summary>
	private AudioSource bgmSource;

	/// <summary>
	/// SE用AudioSource
	/// </summary>
	private AudioSource seSource;

	/// <summary>
	/// 再生BGMファイル
	/// </summary>
	[SerializeField, Header("再生BGMファイル")]
	private AudioClip[] bgmClips = null;

	/// <summary>
	/// 再生SEファイル
	/// </summary>
	[SerializeField, Header("再生SEファイル")]
	private AudioClip[] seClips = null;

	/// <summary>
	/// 初期化
	/// </summary>
	private new void Awake() {

		base.Awake();

		//シーン遷移で消えないように設定。
		DontDestroyOnLoad(this);

		//BGMと効果音分のオーディオソースを追加。
		for (int i = 0; i < 2; i++) this.gameObject.AddComponent<AudioSource>();

		//オーディオソースの割り当ての設定。
		var audioSources = GetComponents<AudioSource>();
		bgmSource = audioSources[0];
		bgmSource.playOnAwake = false;
		seSource = audioSources[1];
		seSource.playOnAwake = false;
	}


	//BGMの再生・停止に関して-------------------------------------------------------------------------------------------------------------------
	#region //BGMの再生・停止に関して-------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// 指定したBGMを再生する。
	/// </summary>
	/// <param name="_bgmType"></param>
	/// <param name="_vol"></param>
	/// <param name="_delay"></param>
	/// <param name="_loop"></param>
	public void PlayBGM(BGM_TYPE _bgmType, float _vol = 1, float _delay = 0, bool _loop = true) {
		//BGM再生コルーチンの再生。
		StartCoroutine(_PlayBGM(_bgmType, _vol, _delay, _loop));
	}

	/// <summary>
	/// BGM再生コルーチン
	/// </summary>
	/// <param name="_bgmType"></param>
	/// <param name="_vol"></param>
	/// <param name="_delay"></param>
	/// <param name="_loop"></param>
	/// <returns></returns>
	private IEnumerator _PlayBGM(BGM_TYPE _bgmType, float _vol, float _delay, bool _loop) {

		//ディレイの設定。
		yield return new WaitForSeconds(_delay);

		//BGMの設定。
		bgmSource.clip = bgmClips[(int)_bgmType];

		//ボリュームの設定。
		bgmSource.volume = _vol;

		//ループの設定。
		bgmSource.loop = _loop;

		//BGMの再生。
		bgmSource.Play();

		//コルーチンの終了。
		yield break;
	}

	/// <summary>
	/// BGMの停止
	/// </summary>
	public void StopBGM() {
		bgmSource.Stop();
	}

	#endregion //BGMの再生・停止に関して-------------------------------------------------------------------------------------------------------------------


	//SEの再生・停止に関して--------------------------------------------------------------------------------------------------------------------
	#region //SEの再生・停止に関して--------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// 指定したSEを再生する。
	/// </summary>
	/// <param name="_seType"></param>
	/// <param name="_vol"></param>
	/// <param name="_delay"></param>
	/// <param name="_loop"></param>
	public void PlaySE(SE_TYPE _seType, float _vol = 1, float _delay = 0, bool _loop = false) {
		//SE再生コルーチンの再生。
		StartCoroutine(_PlaySE(_seType, _vol, _delay, _loop));
	}

	/// <summary>
	/// SE再生コルーチン
	/// </summary>
	/// <param name="_seType"></param>
	/// <param name="_vol"></param>
	/// <param name="_delay"></param>
	/// <param name="_loop"></param>
	/// <returns></returns>
	private IEnumerator _PlaySE(SE_TYPE _seType, float _vol, float _delay, bool _loop) {

		//ディレイの設定。
		yield return new WaitForSeconds(_delay);

		//ループ設定の場合
		if (_loop)
		{

			//SEの設定。
			seSource.clip = seClips[(int)_seType];

			//ボリュームの設定。
			seSource.volume = _vol;

			//ループの設定。
			seSource.loop = _loop;

			//BGMの再生。
			seSource.Play();

			//コルーチンの終了。
			yield break;

		}
		else //単発再生の場合 
		{
			seSource.PlayOneShot(seClips[(int)_seType], _vol);

			//コルーチンの終了。
			yield break;
		}
	}

	/// <summary>
	/// SEの停止。
	/// </summary>
	public void StopSE() {
		seSource.Stop();
	}

	#endregion //SEの再生・停止に関して--------------------------------------------------------------------------------------------------------------------


}
