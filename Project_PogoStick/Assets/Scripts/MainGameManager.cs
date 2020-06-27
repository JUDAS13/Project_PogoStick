using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : StateBaseClass {

    /// <summary>
    /// Scene_Mainクラス
    /// </summary>
    [SerializeField, Header("Scene_Mainクラス")]
    private Scene_Main sceneMainGame;

	///// <summary>
	///// コントローラークラス
	///// </summary>
	//[SerializeField, Header("コントローラークラス")]
	//private UnicycleRaceControllerManager controllerManager;

	///// <summary>
	///// MiniGameCoinGeneraterクラス
	///// </summary>
	//[SerializeField, Header("MiniGameCoinGenerater")]
	//private MiniGameCoinGenerater miniGameCoinGenerater;


	///// <summary>
	///// お楽しみ用HUDクラス
	///// </summary>
	//[SerializeField, Header("お楽しみ用HUDクラス")]
	//public MiniGameHUD miniGameHud;
	//[System.Serializable]
	//public class MiniGameHUD
	//{

	//    /// <summary>
	//    /// ポイント表示
	//    /// </summary>
	//    [SerializeField, Header("ポイント表示")]
	//    private GameObject getPoint;

	//    /// <summary>
	//    /// ポイントテキスト
	//    /// </summary>
	//    [SerializeField, Header("ポイントテキスト")]
	//    private TextMeshPro getPointText;

	//    /// <summary>
	//    /// 逆走表記
	//    /// </summary>
	//    [SerializeField, Header("逆走表記")]
	//    private tk2dSprite reverse;

	//    /// <summary>
	//    /// レースに負けるとポイントが半減する説明
	//    /// </summary>
	//    [SerializeField, Header("レースに負けるとポイントが半減する説明")]
	//    private GameObject explanation;

	//    /// <summary>
	//    /// カウント用の画像
	//    /// </summary>
	//    [SerializeField, Header("カウント用の画像")]
	//    private tk2dSprite[] countSprites;

	//    /// <summary>
	//    /// カウント表示スピード
	//    /// </summary>
	//    [SerializeField, Header("カウント表示スピード")]
	//    private float countFadeSpeed;
	//    private float countfadeTimer;

	//    /// <summary>
	//    /// カウント画像の、カウント用。
	//    /// </summary>
	//    private int countNo;

	//    /// <summary>
	//    /// スタート演出
	//    /// </summary>
	//    [SerializeField, Header("スタート！")]
	//    private GameObject startEffect;

	//    /// <summary>
	//    /// ゴール演出
	//    /// </summary>
	//    [SerializeField, Header("ゴール！")]
	//    private GameObject goalEffect;

	//    /// <summary>
	//    /// 説明文表示フラグ
	//    /// </summary>
	//    private bool isExplanation;

	//    /// <summary>
	//    /// 説明文表示用レート
	//    /// </summary>
	//    private float explanationRate;

	//    /// <summary>
	//    /// 説明文を読む時間のカウント用タイマー
	//    /// </summary>
	//    private float explanationTimer;

	//    /// <summary>
	//    /// カウント画像の保存用。
	//    /// </summary>
	//    private Color countSpriteColor = Color.white;

	//    /// <summary>
	//    /// 逆走時の色の保存用。
	//    /// </summary>
	//    private Color reverseTempColor = Color.white;

	//    /// <summary>
	//    /// 逆走時の逆走時間保存用。
	//    /// </summary>
	//    private float reverseOldTime;

	//    /// <summary>
	//    /// HUDの表示完了フラグ
	//    /// </summary>
	//    private bool isDone;

	//    /// <summary>
	//    /// レース開始時の説明文の表示
	//    /// </summary>
	//    public void ShowExplanation()
	//    {

	//        //説明を読む時間の設定。
	//        const float explanationTime = 2;

	//        //説明文回転スピード
	//        const float explanationRotSpeed = 2;

	//        //表示していない場合
	//        if (!isExplanation)
	//        {

	//            //説明文を広げていく。
	//            if (explanationRate < 1)
	//            {

	//                //説明文が表示される。
	//                explanationRate += Time.deltaTime * explanationRotSpeed;
	//                var add = Mathf.LerpAngle(90, 0, explanationRate);
	//                explanation.transform.eulerAngles = new Vector3(add, 0, 0);

	//                //説明文の完全表示。
	//                if (explanationRate >= 1)
	//                {
	//                    explanation.transform.eulerAngles = new Vector3(0, 0, 0);
	//                    explanationRate = 0;
	//                    isExplanation = true;
	//                }
	//            }

	//        }

	//        //説明文表示時。
	//        if (isExplanation)
	//        {

	//            //読む時間分のディレイ。
	//            if (explanationTimer < explanationTime)
	//            {
	//                explanationTimer += Time.deltaTime;
	//                return;
	//            }

	//            //説明文を畳んでいく。
	//            if (explanationRate < 1)
	//            {
	//                explanationRate += Time.deltaTime * explanationRotSpeed;
	//                if (explanationRate >= 1)
	//                {
	//                    explanationRate = 1;
	//                }
	//            }

	//            //説明文の表示
	//            var add = Mathf.LerpAngle(0, 90, explanationRate);
	//            explanation.transform.eulerAngles = new Vector3(add, 0, 0);
	//        }
	//    }

	//    /// <summary>
	//    /// 説明文が表示し終わったかを取得。
	//    /// </summary>
	//    /// <returns></returns>
	//    public bool IsShowedExpanation()
	//    {

	//        if (isExplanation && explanationRate == 1)
	//        {
	//            return true;
	//        }

	//        return false;
	//    }

	//    /// <summary>
	//    /// カウントの表示
	//    /// </summary>
	//    public void ShowCount()
	//    {

	//        //カウント画像が全て表示されていない場合に実行される。
	//        if (countNo < countSprites.Length)
	//        {

	//            //カウント画像をフェードで表示していく
	//            countfadeTimer += Time.deltaTime * countFadeSpeed;
	//            var _sinF = Mathf.Sin(countfadeTimer);
	//            countSpriteColor.a = _sinF;
	//            countSprites[countNo].color = countSpriteColor;

	//            //完全にフェードが終了した場合。
	//            if (_sinF < 0)
	//            {
	//                countfadeTimer = 0;
	//                countNo++;
	//            }
	//        }
	//        else
	//        {
	//            if (startEffect != null)
	//                startEffect.SetActive(true);

	//            isDone = true;
	//        }
	//    }

	//    /// <summary>
	//    /// 逆走表記
	//    /// </summary>
	//    /// <param name="_flg"></param>
	//    public void ShowReverse(bool _flg)
	//    {
	//        if (_flg)
	//        {
	//            reverseTempColor.a = Mathf.Sin((Time.time - reverseOldTime) * (Mathf.PI * 2) * 1f) * 1;
	//        }
	//        else
	//        {
	//            reverseTempColor.a = 0;
	//            reverseOldTime = Time.time;
	//        }
	//        reverse.color = reverseTempColor;
	//    }

	//    /// <summary>
	//    /// ポイントのUIの表示
	//    /// </summary>
	//    public void ShowGetPoint()
	//    {
	//        getPoint.SetActive(true);
	//    }

	//    /// <summary>
	//    /// ポイントのUIの非表示
	//    /// </summary>
	//    public void HideGetPoint()
	//    {
	//        getPoint.SetActive(false);
	//    }

	//    /// <summary>
	//    /// ポイントを更新表示。
	//    /// </summary>
	//    /// <param name="_value"></param>
	//    public void UpdatePoint(int _value)
	//    {
	//        getPointText.text = _value.ToString();
	//    }

	//    /// <summary>
	//    /// 演出表示フラグ
	//    /// </summary>
	//    /// <returns></returns>
	//    public bool IsDone()
	//    {
	//        return isDone;
	//    }

	//    /// <summary>
	//    /// ゴール演出
	//    /// </summary>
	//    public void PlayGoalEffect()
	//    {
	//        goalEffect.SetActive(true);
	//    }
	//}


	/// <summary>
	/// 初期化
	/// </summary>
	public override void Resume()
	{

		////タイトルロゴ初期化
		//miniGameTitleLogo.Init();

		////カメラ初期化
		//miniGameCamera.Init();

		////プレイヤーの初期化。プレイヤーフラグの設定。
		//miniGameBiker.Init(true);

		////敵の初期化、敵フラグを設定。
		//miniGameEnemy.Init(false);

		////コントローラーの初期化。
		//controllerManager.Init();

		////プレイヤーと敵のスタートポジションをランダムに設定する。
		//Set_RandomStartPositon();
	}

	/// <summary>
	/// コントローラー用アップデート
	/// </summary>
	public override void UpdateMethod()
	{

		//base.UpdateMethod();

		////コントローラーから入力ベクトルを取得する。
		//controllerManager.UpdateMethod();
	}

	/// <summary>
	/// カメラワーク用のレイトアップデート
	/// </summary>
	public override void LateUpdateMethod()
    {

		//base.LateUpdateMethod();

		//HUD表示時のカメラワーク
		//if (IsState(StateShowHud))
		//{

		//	FIRSTに登録されているカメラのトランスフォームに合わせる。
		//	miniGameCamera.UpdateCameraTransform(MiniGameCamera.CAMERA_STATE.FIRST, true);
		//}

		//レース時のカメラワーク
		//if (IsState(StateRace))
		//{

		//	プレイヤーと一定距離を保って、カメラをプレイヤーに向ける。
		//	miniGameCamera.KeepDistance(MiniGameCamera.CAMERA_STATE.FOLLOW_CHARA, true);
		//	miniGameCamera.LookTarget(MiniGameCamera.CAMERA_STATE.FOLLOW_CHARA, true);
		//}

		//リザルト時のカメラワーク
		//if (IsState(StateResult))
		//{

		//	ゴールしてから数秒間だけ追尾する。
		//	if (goaledCameraFollowTimer < goaledCameraFollowTime)
		//	{
		//		goaledCameraFollowTimer += Time.deltaTime;
		//		if (goaledCameraFollowTimer >= goaledCameraFollowTime)
		//		{
		//			goaledCameraFollowTimer = goaledCameraFollowTime;
		//		}

		//		プレイヤーと一定距離を保って、カメラをプレイヤーに向ける。
		//		miniGameCamera.KeepDistance(MiniGameCamera.CAMERA_STATE.FOLLOW_CHARA, true);
		//		miniGameCamera.LookTarget(MiniGameCamera.CAMERA_STATE.FOLLOW_CHARA, true);

		//	}
		//}
	}

    /// <summary>
    /// HUD表示初期化ステート
    /// </summary>
    private void StateShowHudInit()
    {
        ////カメラ初期化
        //miniGameCamera.Reset();
    }

    /// <summary>
    /// HUD表示ステート
    /// </summary>
    private void StateShowHud() {

        ////スタート演出の表示完了したら、レース開始。
        //if (miniGameHud.IsDone()) {
        //    SetState(StateRaceInit, StateRace);
        //    return;
        //}

        ////お楽しみのポイントが半減する場合の説明を表記する。
        //if (!miniGameHud.IsShowedExpanation())
        //{
        //    miniGameHud.ShowExplanation();
        //}
        //else
        //{
        //    //お楽しみのポイントが表記し終わったら、カウントする。
        //    miniGameHud.ShowCount();
        //}

    }

    /// <summary>
    /// Scene_MiniGameクラスから演出を実行する。
    /// </summary>
    public void StartPerformance() {
        //ステートを切り替えてレースを開始させる。
        ResetState();
        SetState(StateShowHudInit, StateShowHud);
    }
}
