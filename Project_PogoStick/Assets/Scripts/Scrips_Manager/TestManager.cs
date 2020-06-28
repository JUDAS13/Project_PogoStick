using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テスト用マネージャークラス
/// </summary>
public class TestManager : SingletonMonoBehaviour<TestManager> {

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start() {

        if (AudioManager.Instance == null)
            return;

        DontDestroyOnLoad(this);
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM(AudioManager.BGM_TYPE.FIRST);
    }

    /// <summary>
    /// アップデート
    /// </summary>
    private void Update() {

        if (AudioManager.Instance == null)
            return;


        if (Input.GetMouseButtonDown(0))
            AudioManager.Instance.PlaySE(AudioManager.SE_TYPE.FIRST);

        if (Input.GetKeyDown(KeyCode.Alpha0))
            SceneLoadManager.Instance.MoveScene(SceneLoadManager.SceneType.Title);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SceneLoadManager.Instance.MoveScene(SceneLoadManager.SceneType.Gacha);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SceneLoadManager.Instance.MoveScene(SceneLoadManager.SceneType.CharactorEdit);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SceneLoadManager.Instance.MoveScene(SceneLoadManager.SceneType.StageSelect);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            SceneLoadManager.Instance.MoveScene(SceneLoadManager.SceneType.MainGame);

    }
}
