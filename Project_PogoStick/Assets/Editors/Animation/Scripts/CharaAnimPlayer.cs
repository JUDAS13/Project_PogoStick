using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
/// <summary>
/// キャラクターアニメーション再生クラス。
/// </summary>
public class CharaAnimPlayer : MonoBehaviour　{

    /// <summary>
    /// キャラクターの体のパーツの数
    /// </summary>
    private const int BODY_PARTS_LENGTH = 9;

    /// <summary>
    /// キャラアニメーションリスト
    /// </summary>
    [Header("キャラアニメーションリスト")]
    public CharaAnimList charaAnimationList;

    //初期設定用
    private Quaternion[] oldRots = new Quaternion[BODY_PARTS_LENGTH];
    private Vector3[] oldPositions = new Vector3[BODY_PARTS_LENGTH];
    private Vector3[] oldEulers = new Vector3[BODY_PARTS_LENGTH];

    //加算用
    private Quaternion[] addRots = new Quaternion[BODY_PARTS_LENGTH];
    private Vector3[] addPositions = new Vector3[BODY_PARTS_LENGTH];
    private Vector3[] addEulers = new Vector3[BODY_PARTS_LENGTH];

    //アニメーション用レシオ
    private Vector3[] positionRatios = new Vector3[BODY_PARTS_LENGTH];
    private Vector3[] eulerRatios = new Vector3[BODY_PARTS_LENGTH];

    //返却用
    private Quaternion[] returnRots = new Quaternion[BODY_PARTS_LENGTH];
    private Vector3[] returnPositions = new Vector3[BODY_PARTS_LENGTH];

    //初期時間保存
    private float oldTime;

    //指定アニメーション番号
    private int animStateNo;

    //指定アニメーション終了時間
    private float animEndTime;

    /// <summary>
    /// 初期化時に登録ミスがないか検索。
    /// </summary>
    private void Start() {

        if (charaAnimationList == null) {
            Debug.LogError("CharaAnimPlayerにCharaAnimListプレハブを設定してください");
            return;
        }

        charaAnimationList.Init();
    }

    /// <summary>
    /// 再生したいアニメーションを指定できます。（引数に秒数を入れる場合は秒数以内にモーションが終了します。入れない場合は継続します。）
    /// </summary>
    /// <param name="_animState">指定アニメーション</param>
    /// <param name="_oldTransform">現在のレンダラー情報</param>
    /// <param name="_animEndTime">アニメーション終了時間</param>
    public void SetAnimation(CharaAnimList.ANIM_STATE _animState, Transform[] _oldTransform, float _animEndTime = 0) {

        //アニメーションを指定。
        animStateNo = (int)_animState;

        //アニメーション終了時間を設定。
        animEndTime = _animEndTime;

        //現在時間の初期化。
        oldTime = Time.time;

        //指定アニメーションのカウント用タイマーを初期化。
        charaAnimationList.charaAnimDataList[animStateNo].timer = 0;

        //アニメーション完了フラグを未完了に設定。
        charaAnimationList.charaAnimDataList[animStateNo].isDoneAnim = false;

        //初期化時のキャラの体の角度と座標を設定。
        for (int i = 0; i < _oldTransform.Length; i++) {
            oldEulers[i] = _oldTransform[i].localEulerAngles;
            oldPositions[i] = _oldTransform[i].localPosition;
            oldRots[i] = _oldTransform[i].localRotation;
        }
    }

    /// <summary>
    /// 指定されたアニメーションを再生します。
    /// </summary>
    public void PlayAnimation() {

        //秒数指定した場合に実行します。
        if (animEndTime != 0) {
            if (charaAnimationList.charaAnimDataList[animStateNo].timer < animEndTime) {
                charaAnimationList.charaAnimDataList[animStateNo].timer += Time.deltaTime;
                if (charaAnimationList.charaAnimDataList[animStateNo].timer >= animEndTime) {
                    charaAnimationList.charaAnimDataList[animStateNo].timer = animEndTime;
                    charaAnimationList.charaAnimDataList[animStateNo].isDoneAnim = true;
                }
            }
        }

        //各体をアニメーションさせていきます。
        for (int i = 0; i < BODY_PARTS_LENGTH; i++) {

            if (animEndTime != 0) {

                if (addPositions[i].x != charaAnimationList.charaAnimDataList[animStateNo].posAddValues[i].x)
                    addPositions[i].x = Mathf.LerpAngle(oldPositions[i].x, charaAnimationList.charaAnimDataList[animStateNo].posAddValues[i].x, charaAnimationList.charaAnimDataList[animStateNo].timer / animEndTime);

                if (addPositions[i].y != charaAnimationList.charaAnimDataList[animStateNo].posAddValues[i].y)
                    addPositions[i].y = Mathf.LerpAngle(oldPositions[i].y, charaAnimationList.charaAnimDataList[animStateNo].posAddValues[i].y, charaAnimationList.charaAnimDataList[animStateNo].timer / animEndTime);

                if (addPositions[i].z != charaAnimationList.charaAnimDataList[animStateNo].posAddValues[i].z)
                    addPositions[i].z = Mathf.LerpAngle(oldPositions[i].z, charaAnimationList.charaAnimDataList[animStateNo].posAddValues[i].z, charaAnimationList.charaAnimDataList[animStateNo].timer / animEndTime);

                if (addRots[i] != Quaternion.Euler(
                    charaAnimationList.charaAnimDataList[animStateNo].eulerAddValues[i].x,
                    charaAnimationList.charaAnimDataList[animStateNo].eulerAddValues[i].y,
                    charaAnimationList.charaAnimDataList[animStateNo].eulerAddValues[i].z)
                    ) {
                    addRots[i] = Quaternion.Lerp(
                        oldRots[i],
                        Quaternion.Euler(charaAnimationList.charaAnimDataList[animStateNo].eulerAddValues[i].x,
                        charaAnimationList.charaAnimDataList[animStateNo].eulerAddValues[i].y,
                        charaAnimationList.charaAnimDataList[animStateNo].eulerAddValues[i].z),
                        charaAnimationList.charaAnimDataList[animStateNo].timer / animEndTime);
                }

            }
            else {

                //体各部に加算させていきます。(座標)
                if (addPositions[i].x != charaAnimationList.charaAnimDataList[animStateNo].posAddValues[i].x)
                    addPositions[i].x = Mathf.LerpAngle(addPositions[i].x, charaAnimationList.charaAnimDataList[animStateNo].posAddValues[i].x, charaAnimationList.charaAnimDataList[animStateNo].posAddSpeeds[i].x * Time.deltaTime);

                if (addPositions[i].y != charaAnimationList.charaAnimDataList[animStateNo].posAddValues[i].y)
                    addPositions[i].y = Mathf.LerpAngle(addPositions[i].y, charaAnimationList.charaAnimDataList[animStateNo].posAddValues[i].y, charaAnimationList.charaAnimDataList[animStateNo].posAddSpeeds[i].y * Time.deltaTime);

                if (addPositions[i].z != charaAnimationList.charaAnimDataList[animStateNo].posAddValues[i].z)
                    addPositions[i].z = Mathf.LerpAngle(addPositions[i].z, charaAnimationList.charaAnimDataList[animStateNo].posAddValues[i].z, charaAnimationList.charaAnimDataList[animStateNo].posAddSpeeds[i].z * Time.deltaTime);

                if (addRots[i] != Quaternion.Euler(
                        charaAnimationList.charaAnimDataList[animStateNo].eulerAddValues[i].x,
                        charaAnimationList.charaAnimDataList[animStateNo].eulerAddValues[i].y,
                        charaAnimationList.charaAnimDataList[animStateNo].eulerAddValues[i].z)
                        ) {

                    addRots[i] = Quaternion.Lerp(
                        addRots[i],
                        Quaternion.Euler(charaAnimationList.charaAnimDataList[animStateNo].eulerAddValues[i].x,
                        charaAnimationList.charaAnimDataList[animStateNo].eulerAddValues[i].y,
                        charaAnimationList.charaAnimDataList[animStateNo].eulerAddValues[i].z),
                        ((charaAnimationList.charaAnimDataList[animStateNo].eulerAddSpeeds[i].x + charaAnimationList.charaAnimDataList[animStateNo].eulerAddSpeeds[i].y + charaAnimationList.charaAnimDataList[animStateNo].eulerAddSpeeds[i].z) / 3) * Time.deltaTime);

                }
            }

            //加算値に対してアニメーションさせます(座標)
            positionRatios[i].x = Mathf.Sin((Time.time - oldTime) * (Mathf.PI * 2) * charaAnimationList.charaAnimDataList[animStateNo].posChangingSpeeds[i].x);
            positionRatios[i].y = Mathf.Sin((Time.time - oldTime) * (Mathf.PI * 2) * charaAnimationList.charaAnimDataList[animStateNo].posChangingSpeeds[i].y);
            positionRatios[i].z = Mathf.Sin((Time.time - oldTime) * (Mathf.PI * 2) * charaAnimationList.charaAnimDataList[animStateNo].posChangingSpeeds[i].z);

            //加算値に対してアニメーションさせます(角度)
            eulerRatios[i].x = Mathf.Sin((Time.time - oldTime) * (Mathf.PI * 2) * charaAnimationList.charaAnimDataList[animStateNo].eulerChangingSpeeds[i].x);
            eulerRatios[i].y = Mathf.Sin((Time.time - oldTime) * (Mathf.PI * 2) * charaAnimationList.charaAnimDataList[animStateNo].eulerChangingSpeeds[i].y);
            eulerRatios[i].z = Mathf.Sin((Time.time - oldTime) * (Mathf.PI * 2) * charaAnimationList.charaAnimDataList[animStateNo].eulerChangingSpeeds[i].z);

            //体各部へ上書きします(座標)
            returnPositions[i] = new Vector3(
                addPositions[i].x + (positionRatios[i].x * charaAnimationList.charaAnimDataList[animStateNo].posChangingValues[i].x),
                addPositions[i].y + (positionRatios[i].y * charaAnimationList.charaAnimDataList[animStateNo].posChangingValues[i].y),
                addPositions[i].z + (positionRatios[i].z * charaAnimationList.charaAnimDataList[animStateNo].posChangingValues[i].z));

            if(animEndTime != 0) {
                //体各部へ上書きします(角度)
                returnRots[i] = addRots[i];
            }
            else {

                //体各部へ上書きします(角度)
                returnRots[i] = Quaternion.Euler(
                    addRots[i].eulerAngles.x + (eulerRatios[i].x * charaAnimationList.charaAnimDataList[animStateNo].eulerChangingValues[i].x),
                    addRots[i].eulerAngles.y + (eulerRatios[i].y * charaAnimationList.charaAnimDataList[animStateNo].eulerChangingValues[i].y),
                    addRots[i].eulerAngles.z + (eulerRatios[i].z * charaAnimationList.charaAnimDataList[animStateNo].eulerChangingValues[i].z));

            }
        }
    }

    /// <summary>
    /// 指定したアニメーションに瞬時にポーズします。
    /// </summary>
    public void PlayPose() {
        for (int i = 0; i < BODY_PARTS_LENGTH; i++) {

            addPositions[i] = charaAnimationList.charaAnimDataList[animStateNo].posAddValues[i];
            addEulers[i] = charaAnimationList.charaAnimDataList[animStateNo].eulerAddValues[i];
            addRots[i] = Quaternion.Euler(addEulers[i]);

            returnRots[i] = addRots[i];
            returnPositions[i] = addPositions[i];

            charaAnimationList.charaAnimDataList[animStateNo].isDoneAnim = true;
        }
    }

    /// <summary>
    /// 指定したアニメーションが終了しているか取得できます。（ただし秒数指定したアニメーションのみ）
    /// </summary>
    /// <returns></returns>
    public bool IsDoneAnimation() {
        return charaAnimationList.charaAnimDataList[animStateNo].isDoneAnim;
    }

    /// <summary>
    /// 指定したアニメーションの進行割合を０～１で取得できる
    /// </summary>
    /// <returns></returns>
    public float GetAnimationRate() {
        if (animEndTime != 0) {
            return charaAnimationList.charaAnimDataList[animStateNo].timer / animEndTime;
        }
        else {
            Debug.LogError("SetAnimation関数で秒数指定がされていませんので０を返します");
            return 0;
        }
    }

    /// <summary>
    /// アニメーションした各体部分のローカル座標を取得できる。
    /// </summary>
    /// <param name="_num">指定したパーツ番号</param>
    /// <returns></returns>
    public Vector3 GetAnimationPositions(int _num) {
        return returnPositions[_num];
    }

    /// <summary>
    /// アニメーションした各体部分のローカルクォータニオンを取得できる。
    /// </summary>
    /// <param name="_num"></param>
    /// <returns></returns>
    public Quaternion GetAnimationRotations(int _num) {
        return returnRots[_num];
    }
}