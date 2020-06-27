using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "PleaseMakeName", menuName = "CreateCharaAnimData")]
/// <summary>
/// キャラクターアニメーションデータクラス
/// </summary>
public class CharaAnimData : ScriptableObject {

    /// <summary>
    /// 体のパーツ数。
    /// </summary>
    private const int BODY_PARTS_LENGTH = 9;

    [HideInInspector]
    public float timer;

    [HideInInspector]
    public bool isDoneAnim;

    [Header("座標加算量")]
    public Vector3[] posAddValues = new Vector3[BODY_PARTS_LENGTH];
    [Header("角度加算量")]
    public Vector3[] eulerAddValues = new Vector3[BODY_PARTS_LENGTH];

    [Header("座標変化量")]
    public Vector3[] posChangingValues = new Vector3[BODY_PARTS_LENGTH];
    [Header("角度変化量")]
    public Vector3[] eulerChangingValues = new Vector3[BODY_PARTS_LENGTH];
    
    [Header("座標変化量スピード")]
    public Vector3[] posChangingSpeeds = new Vector3[BODY_PARTS_LENGTH];
    [Header("角度変化量スピード")]
    public Vector3[] eulerChangingSpeeds = new Vector3[BODY_PARTS_LENGTH];

    [Header("座標加算スピード")]
    public Vector3[] posAddSpeeds = new Vector3[BODY_PARTS_LENGTH];
    [Header("角度加算スピード")]
    public Vector3[] eulerAddSpeeds = new Vector3[BODY_PARTS_LENGTH];


#if UNITY_EDITOR

    /// <summary>
    /// 渡された基準ポーズの値を保持する。
    /// </summary>
    /// <param name="tr"></param>
    public void KeepBaseTransformValue(Transform[] tr, float _commonSpeed)
    {

        if (tr.Length > BODY_PARTS_LENGTH)
        {
            System.Array.Resize<Vector3>(ref posAddValues, tr.Length);
            System.Array.Resize<Vector3>(ref eulerAddValues, tr.Length);

            System.Array.Resize<Vector3>(ref posAddSpeeds, tr.Length);
            System.Array.Resize<Vector3>(ref eulerAddSpeeds, tr.Length);
        }

        for (int i = 0; i < tr.Length; i++)
        {
            posAddValues[i] = tr[i].localPosition;
            eulerAddValues[i] = tr[i].localEulerAngles;

            posAddSpeeds[i] = Vector3.one * _commonSpeed;
            eulerAddSpeeds[i] = Vector3.one * _commonSpeed;
        }

        for (int i = tr.Length; i < BODY_PARTS_LENGTH; i++)
        {
            posAddValues[i] = Vector3.zero;
            eulerAddValues[i] = Vector3.zero;

            posAddSpeeds[i] = Vector3.one;
            eulerAddSpeeds[i] = Vector3.one;
        }
    }

    /// <summary>
    /// 渡された変化量の値を保持する。
    /// </summary>
    /// <param name="tr"></param>
    public void KeepChangingTransformValue(Transform[] tr)
    {
        if (tr.Length > BODY_PARTS_LENGTH)
        {
            System.Array.Resize<Vector3>(ref posChangingValues, tr.Length);
            System.Array.Resize<Vector3>(ref eulerChangingValues, tr.Length);
        }

        for (int i = 0; i < tr.Length; i++)
        {

            posChangingValues[i] = new Vector3(
                Mathf.Abs(Mathf.Abs(posAddValues[i].x) - Mathf.Abs(tr[i].localPosition.x)),
                Mathf.Abs(Mathf.Abs(posAddValues[i].y) - Mathf.Abs(tr[i].localPosition.y)),
                Mathf.Abs(Mathf.Abs(posAddValues[i].z) - Mathf.Abs(tr[i].localPosition.z)));

            eulerChangingValues[i] = new Vector3(
               Mathf.DeltaAngle(eulerAddValues[i].x, tr[i].localEulerAngles.x),
               Mathf.DeltaAngle(eulerAddValues[i].y, tr[i].localEulerAngles.y),
               Mathf.DeltaAngle(eulerAddValues[i].z, tr[i].localEulerAngles.z));
        }

        for (int i = tr.Length; i < BODY_PARTS_LENGTH; i++)
        {
            posChangingValues[i] = Vector3.zero;
            eulerChangingValues[i] = Vector3.zero;
        }
    }

    public void KeepChangingSpeedVectorValue(Vector3[] _speedVec)  {

        if (_speedVec.Length > BODY_PARTS_LENGTH)
        {
            System.Array.Resize<Vector3>(ref posChangingSpeeds, _speedVec.Length);
            System.Array.Resize<Vector3>(ref eulerChangingSpeeds, _speedVec.Length);
        }

        for (int i = 0; i < _speedVec.Length; i++)
        {
            posChangingSpeeds[i] = _speedVec[i];
            eulerChangingSpeeds[i] = _speedVec[i];
        }

        for (int i = _speedVec.Length; i < BODY_PARTS_LENGTH; i++)
        {
            posChangingSpeeds[i] = Vector3.zero;
            eulerChangingSpeeds[i] = Vector3.zero;
        }
    }

    /// <summary>
    /// キャラクターの各パーツに適用する
    /// </summary>
    public void ApplyValues(Character chara) {
        Transform[] tr = chara.transformList;
        ApplyValues(tr);
    }

    /// <summary>
	/// Transformの配列に適用させる
	/// </summary>
	private void ApplyValues(Transform[] tr) {
        for (int i = 0; i < Mathf.Min(tr.Length, BODY_PARTS_LENGTH); i++) {
            tr[i].localPosition = posAddValues[i];
            tr[i].localEulerAngles = eulerAddValues[i];
        }
    }

    /// <summary>
    /// アセットとして保存する
    /// </summary>
    public static CharaAnimData CreateBaseToAssets(Transform[] tr, string _animName, string folderPath, float _commonSpeed) {

        string savePath = folderPath + _animName + ".asset";

        CharaAnimData newAnim = CreateInstance<CharaAnimData>();
        newAnim.KeepBaseTransformValue(tr, _commonSpeed);

        AssetDatabase.CreateAsset(newAnim, savePath);
        AssetDatabase.Refresh();

        // 生成完了
        return newAnim;
    }

#endif


}
