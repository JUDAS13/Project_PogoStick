using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクタークラス
/// </summary>
public class Character : MonoBehaviour {

    /// <summary>
    /// ボディーパーツリスト
    /// </summary>
    public enum BODY_PARTS_LIST {
        HEAD,
        BODY,
        HIP,
        SHOULDER_L,
        SHOULDER_R,
        ARM_L,
        ARM_R,
        LEG_L,
        LEG_R,
        MAX
    }

    /// <summary>
    /// トランスフォームリスト
    /// </summary>
    [SerializeField, Header("トランスフォームリスト")]
    public Transform[] transformList;

    /// <summary>
    /// キャラアニメーションプレイヤー
    /// </summary>
    [SerializeField, Header("キャラアニメーションプレイヤー")]
    public CharaAnimPlayer charaAnimPlayer;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Init() {
        SetAnimation(CharaAnimList.ANIM_STATE.NONE);
        PlayPose();
    }

    /// <summary>
    /// 再生したいアニメーションを指定できます。（引数に秒数を入れる場合は秒数以内にモーションが終了します。入れない場合は継続します。）
    /// </summary>
    /// <param name="_animState">指定アニメーション</param>
    /// <param name="_animEndTime">アニメーション終了時間</param>
    public void SetAnimation(CharaAnimList.ANIM_STATE _animState, float _animEndTime = 0) {
        charaAnimPlayer.SetAnimation(_animState, transformList, _animEndTime);
    }

    /// <summary>
    /// 指定したアニメーションを再生します。
    /// </summary>
    public void PlayAnimation() {
        charaAnimPlayer.PlayAnimation();
        for (int i = 0; i < transformList.Length; i++) transformList[i].localPosition = charaAnimPlayer.GetAnimationPositions(i);
        for (int i = 0; i < transformList.Length; i++) transformList[i].localRotation = charaAnimPlayer.GetAnimationRotations(i);
    }

    /// <summary>
    /// 指定したアニメーションの進行割合を０～１で取得できる
    /// </summary>
    /// <returns></returns>
    public float GetAnimationRate() {
        return charaAnimPlayer.GetAnimationRate();
    }

    /// <summary>
    /// 指定したアニメーションへ瞬時にポーズします。
    /// </summary>
    public void PlayPose() {
        charaAnimPlayer.PlayPose();
        for (int i = 0; i < transformList.Length; i++) transformList[i].localPosition = charaAnimPlayer.GetAnimationPositions(i);
        for (int i = 0; i < transformList.Length; i++) transformList[i].localRotation = charaAnimPlayer.GetAnimationRotations(i);
    }

    /// <summary>
    /// 指定したアニメーションが終了しているかを取得できます。
    /// </summary>
    /// <returns></returns>
    public bool IsDoneAnimation() {
        return charaAnimPlayer.IsDoneAnimation();
    }

}