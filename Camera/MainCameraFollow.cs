using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraFollow : MonoBehaviour {

    private GameObject cameraPositionTargetObj;
    private GameObject cameraLookTargetObj;
    private GameObject playerObj;
    private GameObject mainAimObj;
    public float cameraFollowSpeed = 4f;
    public float rotateSpeed = 6f;
    public Text dbgText;

    GameObject target;

    // Use this for initialization
    void Start () {
        // カメラの注視対象: CameraRotateOrigin
        cameraLookTargetObj = GameObject.Find("CameraRotateOrigin").gameObject;
        // Cemearaの追従先オブジェクトのPosition: CameraPosition
        cameraPositionTargetObj = cameraLookTargetObj.transform.Find("CameraPosition").gameObject;
        // Playerオブジェクト
        playerObj = GameSystem.Instance.PlayerObject;
        // mainAimObj
        mainAimObj = GameSystem.Instance.MainAim;
        dbgText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        // ロックオンボタンが押されたとき
        target = GameSystem.Instance.PlayerTarget;
        
        // カメラの現在位置を保存
        Vector3 nowPos = this.transform.position;
        // ターゲットのPositionをCameraPoistionTargetの位置に
        Vector3 targetPos = cameraPositionTargetObj.transform.position;

        // Playerの向き取得
        Vector3 pForward = playerObj.transform.forward;

        // カメラの向きはPlayer -> CameraRotateOriginの向き
        Vector3 cameraForward = Camera.main.transform.forward;// - playerObj.transform.position;

        // カメラの向きとPlayerの向きの角度差
        float angleVector = Vector3.Angle(pForward, cameraForward);

        // PlayerとMainカメラの距離
        float distanceVector = (playerObj.transform.position - this.transform.position).magnitude;
        
        // デバッグ用テキスト
        //dbgText.text = "";
        dbgText.text = "angleVector[ " + angleVector + " ]\n";

        // Playerの向きとCamera -> Playerのベクトル角度差によってカメラの追従速度を変更

        if (angleVector < 85f)
        {
            cameraFollowSpeed = 8f;
        }
        else
        {
            cameraFollowSpeed = 4f;
        }

        // Playerの向きとCamera -> Playerのベクトル角度差によってカメラの追従速度を変更(より細かく)
        if (angleVector < 85f)//57
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, cameraFollowSpeed * 1 * Time.deltaTime); //4
        }
        else if (angleVector < 60f)
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, cameraFollowSpeed * 1 * Time.deltaTime);//3
            //this.transform.position = Vector3.Lerp(nowPos, targetPos, cameraFollowSpeed * Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, cameraFollowSpeed * Mathf.MoveTowards(1f, Mathf.Abs(86-angleVector), 2f) * Time.deltaTime);
        }

        // 遅延追従をやめる
        //this.transform.position = targetPos;

        // カメラの現在位置の保存
        Vector3 thisPos = this.transform.position;
        // 注視ターゲットのPositionを取得
        Vector3 lookTargetPos = cameraLookTargetObj.transform.position;
        // 注視先へのベクトル取得
        Vector3 lookVector = lookTargetPos - thisPos;

        // Camera -> TargetのベクトルをlookVectorに足すことで、よりTarget方向へ向ける
        /*
        if (target != null)
        {
            //Vector3 crVec = (target.transform.position - thisPos);
            Vector3 crVec = new Vector3((target.transform.position.x - thisPos.x), target.transform.position.y - thisPos.y, target.transform.position.z - thisPos.z);
            lookVector = crVec * 0.8f + lookVector;
        }
        */
        // rotationをLerpで更新
        /*
        Quaternion thisRotate = this.transform.rotation;
        Quaternion newRotate = Quaternion.LookRotation(lookVector);
        this.transform.rotation = Quaternion.Lerp(thisRotate, newRotate, rotateSpeed * Time.deltaTime);
        */
        Vector3 lookVec = mainAimObj.transform.position - (playerObj.transform.position + new Vector3(0, 0.8f, 0));
        //this.transform.rotation = Quaternion.LookRotation(lookVec);

        Quaternion newRot = Quaternion.LookRotation(lookVec);
        Vector3 nowEulerAngle = newRot.eulerAngles;
        float clampAngle = 60f;
        float newX = nowEulerAngle.x;
        newX -= newX > 180 ? 360 : 0;
        newX = Mathf.Clamp(newX, -clampAngle, clampAngle);
        this.transform.eulerAngles = new Vector3(newX, nowEulerAngle.y, nowEulerAngle.z);

        dbgText.text += "CamaraRotation  [ " + this.transform.eulerAngles + " ]\n";
        dbgText.text += "eulerAngles [ " + newX + " ] \n";
        dbgText.text += "LookVec [ " + (mainAimObj.transform.position - (playerObj.transform.position + new Vector3(0, 0.8f, 0))) + " ]\n";
	}

}
