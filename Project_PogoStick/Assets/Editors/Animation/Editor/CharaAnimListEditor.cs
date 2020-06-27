using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[ExecuteInEditMode]
[CustomEditor(typeof(CharaAnimList))]
public class CharaAnimListEditor : Editor {

    private CharaAnimList charaAnimList;

    private ReorderableList reorderableList;

    private SerializedProperty serializedIndex;

    private SerializedProperty serializedCreateModeIndex;

    private List<string> animNames = new List<string>();

    private List<string> createModeNames = new List<string>();

    private static double waitTime = 0;

    private const int BODY_PARTS_LENGTH = 9;
    private bool[] isBodyEdits = new bool[BODY_PARTS_LENGTH];
    private string[] bodyNames = { "頭", "体", "腰", "左カタ", "右カタ", "左ウデ", "右ウデ", "左アシ", "右アシ" };

    private bool isShow = true;

    private void Awake() {
        charaAnimList = target as CharaAnimList;
    }

    private void OnEnable() {
        waitTime = EditorApplication.timeSinceStartup;
        serializedIndex = serializedObject.FindProperty("selectedIndex");
        serializedCreateModeIndex = serializedObject.FindProperty("createModeIndex");
        MakeReorderableMotions();
        RefreshAnimNames();
        CreateModeName();
    }

    private void OnValidate() {
        RefreshAnimNames();
        CreateModeName();
    }

    private void OnDisable() {
        EditorApplication.update -= EditorUpdate;
    }

    /// <summary>
    /// レイアウト
    /// </summary>
    public override void OnInspectorGUI() {

        if (isShow = EditorGUILayout.Foldout(isShow, "設定項目"))
            base.OnInspectorGUI();

        serializedObject.Update();

        reorderableList.DoLayoutList();

        EditorGUILayout.HelpBox(
                "　★★★出来ることについて★★★　" + System.Environment.NewLine + System.Environment.NewLine +
                "１、アニメーションの基準となるポーズを作成できます。" + System.Environment.NewLine +
                "２、作成したポーズを確認できます。" + System.Environment.NewLine +
                "３、作成した基準のポーズに「揺らぎ」を加えることで" + System.Environment.NewLine +
                "より細かいアニメーションが作成できます。", MessageType.Warning);

        EditorGUILayout.HelpBox(
                "　　★★★使い方について★★★　" + System.Environment.NewLine + System.Environment.NewLine +
                "１、アニメーションの基準のポーズを作成する場合、" + System.Environment.NewLine +
                "　　配置してあるキャラを作成したいポーズに、調整してください。" + System.Environment.NewLine +
                "　　完成したら「保存したいファイル名」に名前を英語で入力し、" + System.Environment.NewLine +
                "　　「セーブする」を押してアセットへ保存できます。" + System.Environment.NewLine + System.Environment.NewLine +
                "２、作成したポーズを確認したい場合、" + System.Environment.NewLine +
                "　　「確認したいアニメーション」からポーズを選択し、" + System.Environment.NewLine +
                "　　レビューで確認することが出来ます。", MessageType.Warning);

        GUILayout.Space(15f);
        serializedCreateModeIndex.intValue = EditorGUILayout.Popup("アニメーション作成モードを選択", serializedCreateModeIndex.intValue, createModeNames.ToArray());

        if (serializedCreateModeIndex.intValue == 0) {

            GUILayout.Space(10f);
            EditorGUILayout.LabelField("1.シーン上のキャラのポーズを調整してください");

            GUILayout.Space(10f);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("2.基準ポーズへのスピードを設定してください");
            charaAnimList.commonAddSpeed = EditorGUILayout.Slider(charaAnimList.commonAddSpeed, 1, 10);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10f);
            EditorGUILayout.BeginHorizontal();
            charaAnimList.saveMotionName = EditorGUILayout.TextField("3.保存するポーズ名を入力してください→", charaAnimList.saveMotionName);
            if (GUILayout.Button("4.基準ポーズを保存する")) {
                if (charaAnimList.saveMotionName.Length == 0) {
                    EditorUtility.DisplayDialog("ポーズ名が未入力です", "ポーズ名を入力してください", "OK");
                    return;
                }
                if (charaAnimList.TryCreateNowAnimation()) {
                    charaAnimList.CreateAnimation();
                    RefreshAnimNames();
                    CreateModeName();
                    EditorUtility.DisplayDialog("基準ポーズの保存が完了しました", "揺らぎを設定する場合は「アニメーション作成モード」を「揺れ幅作成モード」に設定してください", "OK");
                }
                else {
                    bool result = EditorUtility.DisplayDialog(
                            "上書き確認",
                            "同名ファイルがすでに存在します。上書きしますか？",
                            "OK",
                            "キャンセル"
                            );

                    if (result) {
                        charaAnimList.SaveChangeBases();
                        EditorUtility.DisplayDialog("基準ポーズの保存が完了しました", "揺れ幅を設定する場合は「アニメーション作成モード」を「揺れ幅作成モード」に設定してください", "OK");
                    }
                    else {
                        EditorUtility.DisplayDialog("基準ポーズの保存をキャンセル", "基準ポーズの保存をキャンセルしました", "OK");
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        else if (serializedCreateModeIndex.intValue == 1) {

            GUILayout.Space(10f);
            int selectAnimNum = EditorGUILayout.Popup("1.揺れ幅を調整したいポーズを指定してください", serializedIndex.intValue, animNames.ToArray());
            charaAnimList.saveMotionName = charaAnimList.charaAnimDataList[selectAnimNum].name;

            if (serializedIndex.intValue != selectAnimNum) {
                serializedIndex.intValue = selectAnimNum;
                charaAnimList.charaModel.SetAnimation((CharaAnimList.ANIM_STATE)serializedIndex.intValue);
                charaAnimList.charaModel.PlayPose();
            }

            GUILayout.Space(10f);
            EditorGUILayout.LabelField("2.シーン上のキャラのポーズを揺れ幅最大として調整してください");

            GUILayout.Space(10f);
            if (GUILayout.Button("3.揺れ幅を上書き保存する")) {
                if (charaAnimList.saveMotionName.Length == 0) {
                    EditorUtility.DisplayDialog("予期しないエラーです", "ポーズ名が入力されていません", "OK");
                    return;
                }
                if (!charaAnimList.TryCreateNowAnimation()) {
                    charaAnimList.SaveChangeChangingValue();
                    RefreshAnimNames();
                    CreateModeName();
                    EditorUtility.DisplayDialog("揺れ幅の上書き保存が完了しました", "揺れスピードを設定する場合は「アニメーション作成モード」を「揺れスピード作成モード」に設定してください", "OK");
                }
                else {
                    EditorUtility.DisplayDialog("予期しないエラーです。", "選択しているポーズは存在しない事になっています", "OK");
                }
            }
        }
        else if (serializedCreateModeIndex.intValue == 2) {

            GUILayout.Space(10f);
            int selectAnimNum = EditorGUILayout.Popup("1.揺れスピードを調整したいアニメーションを指定してください", serializedIndex.intValue, animNames.ToArray());

            if (serializedIndex.intValue != selectAnimNum) {
                EditorApplication.update = null;
                charaAnimList.saveMotionName = charaAnimList.charaAnimDataList[serializedIndex.intValue].name;
                charaAnimList.charaModel.SetAnimation((CharaAnimList.ANIM_STATE)serializedIndex.intValue);
                charaAnimList.commonChangingSpeeds = charaAnimList.charaAnimDataList[serializedIndex.intValue].eulerChangingSpeeds;
            }   

            GUILayout.Space(10f);
            EditorGUILayout.LabelField("2.各体のXYZにスピードを割り当ててください");

            for (int i = 0; i < BODY_PARTS_LENGTH; i++) {
                GUILayout.Space(10f);
                EditorGUILayout.BeginHorizontal();
                if (isBodyEdits[i] = EditorGUILayout.Foldout(isBodyEdits[i], bodyNames[i])) {
                    charaAnimList.commonChangingSpeeds[i].x = EditorGUILayout.Slider(charaAnimList.commonChangingSpeeds[i].x, -1, 1);
                    charaAnimList.commonChangingSpeeds[i].y = EditorGUILayout.Slider(charaAnimList.commonChangingSpeeds[i].y, -1, 1);
                    charaAnimList.commonChangingSpeeds[i].z = EditorGUILayout.Slider(charaAnimList.commonChangingSpeeds[i].z, -1, 1);
                }
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(10f);
            if (GUILayout.Button("揺れスピードを上書き保存＆再生テスト")) {
                if (charaAnimList.saveMotionName.Length == 0) {
                    EditorUtility.DisplayDialog("予期しないエラーです", "ポーズ名が入力されていません", "OK");
                    return;
                }

                if (!charaAnimList.TryCreateNowAnimation()) {
                    charaAnimList.SaveChangeSpeedValue();
                    RefreshAnimNames();
                    CreateModeName();
                    EditorUtility.DisplayDialog("揺れスピードの上書き保存が完了しました", "アニメーションを確認したい場合は「アニメーション作成モード」を「アニメーション確認モード」に設定してください", "OK");
                    EditorApplication.update = null;
                    charaAnimList.charaModel.SetAnimation((CharaAnimList.ANIM_STATE)serializedIndex.intValue);
                    EditorApplication.update += EditorUpdate;
                }
                else {
                    EditorUtility.DisplayDialog("予期しないエラーです。", "選択しているポーズは存在しない事になっています", "OK");
                }
            }
        }
        else if (serializedCreateModeIndex.intValue == 3) {
            GUILayout.Space(10f);
            EditorGUILayout.BeginHorizontal();
            int selectAnimNum = EditorGUILayout.Popup("確認したいアニメーションを指定してください", serializedIndex.intValue, animNames.ToArray());
            if (serializedIndex.intValue != selectAnimNum)
            {
                serializedIndex.intValue = selectAnimNum;
                EditorApplication.update = null;
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10f);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            if (GUILayout.Button("単発再生"))
            {
                EditorApplication.update = null;
                charaAnimList.charaModel.SetAnimation((CharaAnimList.ANIM_STATE)serializedIndex.intValue, 1);
                EditorApplication.update += EditorUpdate;
            }

            GUILayout.Space(10f);
            if (GUILayout.Button("継続再生"))
            {
                EditorApplication.update = null;
                charaAnimList.charaModel.SetAnimation((CharaAnimList.ANIM_STATE)serializedIndex.intValue);
                EditorApplication.update += EditorUpdate;
            }

            GUILayout.Space(10f);
            if (GUILayout.Button("ポーズ再生"))
            {
                EditorApplication.update = null;
                charaAnimList.charaModel.SetAnimation((CharaAnimList.ANIM_STATE)serializedIndex.intValue);
                charaAnimList.charaModel.PlayPose();
                //gachaCharaAnimList.ApplyPose(serializedIndex.intValue);
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10f);
            if (GUILayout.Button("再生停止"))
            {
                EditorApplication.update = null;
            }

            GUILayout.Space(10f);
            if (GUILayout.Button("（その他）データ再検索ボタン"))
            {
                SearchDataBase_ForAssets();
            }
        }

        bool isAplly = serializedObject.ApplyModifiedProperties();

        // 変更を保存
        if (isAplly) {
            RefreshAnimNames();
            CreateModeName();
        }
    }

    /// <summary>
    /// アセットからデータベースを検索
    /// </summary>
    public void SearchDataBase_ForAssets() {

        //CharaAnimData型を持つデータベースを検索。
        var datas = UnityEditor.AssetDatabase.FindAssets("t:CharaAnimData");

        //ファイルが見つからない場合エラー表示
        if (datas.Length == 0) {
            throw new System.IO.FileNotFoundException("CharaAnimData does not found");
        }
        
        //データ全検索
        for (int i = 0; i < datas.Length; i++) {

            //データをパスに変更
            var pathName = AssetDatabase.GUIDToAssetPath(datas[i]);

            //パスからデータを取り出し
            var animData = AssetDatabase.LoadAssetAtPath<CharaAnimData>(pathName);

            //データが一致する場合は追加。
            if (!charaAnimList.charaAnimDataList.Contains(animData))　{
                charaAnimList.charaAnimDataList.Add(animData);
            }
        }
    }

    /// <summary>
    /// エディターを疑似的に更新します。
    /// </summary>
    private void EditorUpdate() {
        // １／６０秒に１回更新
        if ((EditorApplication.timeSinceStartup - waitTime) >= 0.01666f) {
            if (!charaAnimList.charaModel.IsDoneAnimation()) {
                charaAnimList.charaModel.PlayAnimation();
            }
            SceneView.RepaintAll();
            waitTime = EditorApplication.timeSinceStartup;
        }
    }

    /// <summary>
    /// ポーズを取得して並べ替え可能なフィールドにする
    /// </summary>
    private void MakeReorderableMotions() {
        var serializedPoses = serializedObject.FindProperty("charaAnimDataList");
        reorderableList = new ReorderableList(serializedObject, serializedPoses);

        reorderableList.elementHeight = EditorGUIUtility.singleLineHeight;
        reorderableList.drawHeaderCallback = (rect) => {
            EditorGUI.LabelField(rect, "モーションリスト");
        };

        reorderableList.drawElementCallback = (rect, index, isActive, isFocused) => {
            var element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, element);
        };

        reorderableList.onChangedCallback += ReordableCallBack;
    }

    private void ReordableCallBack(ReorderableList _reorderableList) {
        RefreshAnimNames();
    }

    private void RefreshAnimNames() {
        animNames.Clear();
        for (int i = 0; i < charaAnimList.charaAnimDataList.Count; i++) {
            if (charaAnimList.charaAnimDataList[i] != null){
                animNames.Add(charaAnimList.charaAnimDataList[i].name);
            }
        }
    }

    private void CreateModeName() {
        createModeNames.Clear();
        createModeNames.Add("基準ポーズ作成モード");
        createModeNames.Add("揺れ幅作成モード");
        createModeNames.Add("揺れスピード作成モード");
        createModeNames.Add("アニメーション確認モード");
    }
}
