using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パラメーター管理クラス
/// </summary>
public class GameDataParams : MonoBehaviour {

	public static CharaParam charactor;
    public class CharaParam {

		public float weight;

		public float height;

		public float power;

	}

	public static PogoParam pogoStick;
	public class PogoParam {

		public float weight;

		public float height;

		public float power;

	}

	public static MoneyParam money;
	public class MoneyParam {
		public int DEFAULT_MONEY = 4000;
	}


	public static StageParam stage;
	public class StageParam {
		
		public enum StageType{
			Stage_0,
			Stage_1,
			Max,
		}
	}
}
