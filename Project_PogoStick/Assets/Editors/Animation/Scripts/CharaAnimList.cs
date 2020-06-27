using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
/// <summary>
/// キャラクターアニメーションデータ管理クラス
/// </summary>
public class CharaAnimList : SingletonMonoBehaviour<CharaAnimList> {

    /// <summary>
    /// アニメーション状態を登録する。
    /// </summary>
    public enum ANIM_STATE {
        NONE,
        MAX
    }

    /// <summary>
    /// キャラクターモデルを設定
    /// </summary>
    [SerializeField, Header("キャラクターモデルを設定")]
    public Character charaModel;

    /// <summary>
    /// アニメーションデータ
    /// </summary>
    [SerializeField, Header("アニメーションデータ")]
    public List<CharaAnimData> charaAnimDataList = new List<CharaAnimData>();

    /// <summary>
    /// 初期化
    /// </summary>
    public void Init() {
        if (charaAnimDataList.Count != (int)ANIM_STATE.MAX) {
            Debug.LogError("登録しているアニメーションデータ数と、アニメーション状態の数が一致しません。");
            Debug.LogError("AnimationListスクリプトとプレハブを修正してください");
            return;
        }
    }

#if UNITY_EDITOR

    /// <summary>
    /// データの新規作成
    /// </summary>
    public static CharaAnimData CreateAnimation(Character chara, string _poseName, string folderPath, float _commonAddSpeed) {
        // Transformを定義順に取得
        var tr = chara.transformList;
        return CharaAnimData.CreateBaseToAssets(tr, _poseName, folderPath, _commonAddSpeed);
    }

    public CharaAnimData GetAssetByName(string _name) {
        return AssetDatabase.LoadAssetAtPath<CharaAnimData>(folderPath + _name + ".asset");
    }

    public void SaveChangeSpeedValue() {
        // Transformを定義順に取得
        var dataBase = GetAssetByName(saveMotionName);
        dataBase.KeepChangingSpeedVectorValue(commonChangingSpeeds);

        // 変更の保存
        EditorUtility.SetDirty(dataBase);
        AssetDatabase.SaveAssets();
    }

    public void SaveChangeChangingValue()
    {
        // Transformを定義順に取得
        var tr = charaModel.transformList;
        var dataBase = GetAssetByName(saveMotionName);
        dataBase.KeepChangingTransformValue(tr);

        // 変更の保存
        EditorUtility.SetDirty(dataBase);
        AssetDatabase.SaveAssets();
    }

    public void SaveChangeBases()
    {
        // Transformを定義順に取得
        var tr = charaModel.transformList;
        var dataBase = GetAssetByName(saveMotionName);
        dataBase.KeepBaseTransformValue(tr, commonAddSpeed);

        // 変更の保存
        EditorUtility.SetDirty(dataBase);
        AssetDatabase.SaveAssets();
    }

    public void CreateAnimation()
    {
        var newAnime = CreateAnimation(charaModel, saveMotionName, folderPath, commonAddSpeed);
        charaAnimDataList.Add(newAnime);
    }

    public bool TryCreateNowAnimation()
    {
        if (GetAssetByName(saveMotionName))
        {
            Debug.Log("既に" + saveMotionName + "が存在します");
            return false;
        }
        else
        {
            return true;
        }
    }

    // ポーズを適用
    public void ApplyPose(int poseNo) {
        charaAnimDataList[poseNo].ApplyValues(charaModel);
    }

    [SerializeField, HideInInspector]
    public int selectedIndex = 0;

    [SerializeField, HideInInspector]
    public int createModeIndex = 0;

    [SerializeField, HideInInspector]
    public string folderPath = "Assets/Editors/Animation/AnimDatas/";

    [SerializeField, HideInInspector]
    public string saveMotionName = "NONE";

    [SerializeField, HideInInspector]
    public float commonAddSpeed;

    [SerializeField, HideInInspector]
    public Vector3[] commonChangingSpeeds = new Vector3[9];


#endif


}
