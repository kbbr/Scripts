using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraDelayFollow : MonoBehaviour {
    // CameraのPositionのみ変更するスクリプト

    [SerializeField]
    private GameObject playerObj;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 0.8f, 0);
    public float moveSpeed = 8f;
    private Vector3 fixPosition;

    public Text vText;

    private void Awake()
    {
        
    }


    // Use this for initialization
    void Start () {
        // Playerのオブジェクトを探す
        playerObj = GameSystem.Instance.PlayerObject;
        // Playerオブジェクトと最初のカメラの位置関係をoffsetで保存
        this.transform.position = playerObj.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        // 現在のPositionを保存
        Vector3 nowPos = this.transform.position;
        // 目的位置のPositionにoffsetを足し込む
        Vector3 targetPos = playerObj.transform.position + offset;

        // Playerの向きを取得
        Vector3 pForward = playerObj.transform.forward;
        // カメラの向きはPlayer位置への向きにする
        Vector3 cameraForward = this.transform.position - playerObj.transform.position;

        // Playerの向きとカメラの向きの角度差を計算
        float angleVector = Vector3.Angle(pForward, cameraForward);
        // debugログ
        vText.text = angleVector.ToString();

        // Playerの向きとカメラの向きの角度差によってLerpの補間速度を変更(ヒューリスティック)
        // 角度差：55度以下
        if (angleVector < 55f)
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed * 4 * Time.deltaTime);
        }
        else if (angleVector < 57f)
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed * 3 * Time.deltaTime);
        }
        else if (angleVector < 70f) //60
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed * 2 * Time.deltaTime);
        }
        else if (angleVector < 90f) //65
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed  * 2 * Time.deltaTime);
        }
        // 角度差が90度以上
        else
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed * Time.deltaTime);
        }
        // 遅延追従をやめる
        this.transform.position = targetPos;
    }
}
