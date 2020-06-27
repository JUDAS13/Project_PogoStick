using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour {

    void Start() {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM(AudioManager.BGM_TYPE.FIRST);
    }

    // Update is called once per frame
    void Update()
    {
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
            SceneLoadManager.Instance.MoveScene(SceneLoadManager.SceneType.Main);

    }
}
