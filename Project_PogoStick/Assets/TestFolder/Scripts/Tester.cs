using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM(AudioManager.BGM_TYPE.FIRST);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            AudioManager.Instance.PlaySE(AudioManager.SE_TYPE.FIRST);

        if (Input.GetMouseButtonDown(1))
            FadeManager.Instance.MoveScene("TestScene");
    }
}
