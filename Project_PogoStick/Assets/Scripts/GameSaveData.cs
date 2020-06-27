using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveData : MonoBehaviour {

	/// <summary>
	/// 初回起動キー
	/// </summary>
	private const string FIRST_BOOT = "FIRST_BOOT";

	/// <summary>
	/// クリア済みエリア番号キー
	/// </summary>
	private const string CLEAR_AREA_NO = "CLEAR_AREA_NO";

	/// <summary>
	/// 所持金額キー
	/// </summary>
	private const string HAVE_MONEY = "HAVE_MONEY";

	/// <summary>
	/// 初回起動しているか取得
	/// </summary>
	/// <returns></returns>
	public bool Get_FirstBoot(){

		if (PlayerPrefs.GetInt(FIRST_BOOT, -1) == -1) 
			return false;

		return true;
	}

	/// <summary>
	/// 初回起動フラグの設定
	/// </summary>
	/// <param name="_value"></param>
	public void Set_FirstBoot(bool _value) {
		var no = _value ? 1 : -1;
		PlayerPrefs.SetInt(FIRST_BOOT, no);
	}

	/// <summary>
	/// クリア済みエリア番号を取得。
	/// </summary>
	/// <returns></returns>
	public int Get_ClearAreaNo() {
		var no = PlayerPrefs.GetInt(CLEAR_AREA_NO, 0);
		return no;
	}

	/// <summary>
	/// クリア済みエリア番号の設定。
	/// </summary>
	/// <param name="_no"></param>
	public void Set_ClearAreaNo(int _no) {
		PlayerPrefs.SetInt(CLEAR_AREA_NO, _no);
	}

	/// <summary>
	/// 所持金の取得
	/// </summary>
	/// <returns></returns>
	public int Get_HaveMoney(){
		var money = PlayerPrefs.GetInt(HAVE_MONEY, GameDataParams.money.DEFAULT_MONEY);
		return money;
	}

	/// <summary>
	/// 所持金の設定。
	/// </summary>
	/// <param name="_value"></param>
	public void Set_HaveMoney(int _value){
		PlayerPrefs.GetInt(HAVE_MONEY, _value);
	}

}
