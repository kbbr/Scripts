using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotate : MonoBehaviour {

    private GameObject mainAim;
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;

    public bool reverseX = false;
    public bool reverseY = true;

    public float clampAngle = 60f;

    public GameObject target = null;
    Vector3 defaultMainAimPos;
    Quaternion defaultMainAimRot;

    // Use this for initialization
    void Start () {
        mainAim = this.transform.Find("MainAim").gameObject;
        defaultMainAimPos = mainAim.transform.localPosition;
        //Debug.Log(defaultMainAimPos.y);
    }
	
	// Update is called once per frame
	void Update () {

        // ロックオン時
        if (Input.GetButtonDown("Lock"))
        {
            // ターゲットがいる(ロックオン中)なら、ロックを外す
            if (target != null)
            {
                target = null;
                mainAim.transform.rotation = this.transform.rotation;
                mainAim.transform.position = this.transform.position + transform.TransformDirection(defaultMainAimPos);

            }
            // ターゲットが不在(ロックオン中でない)なら、ロックオン対象のターゲットを探しターゲットにする
            else
            {
                GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
                float closestDistance = Mathf.Infinity;
                foreach (GameObject closest in targets)
                {
                    float distance = (closest.transform.position - this.transform.position).sqrMagnitude;
                    if (distance < closestDistance){
                        closestDistance = distance;
                        target = closest;
                    }
                }
                target = target.transform.Find("lockTarget").gameObject;

            }
        }
        // ターゲットがいるなら(ロックオン時)
        if (target != null)
        {
            // ターゲットへのRotationをQuaternionで算出
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - this.transform.position);
            // ターゲット方向へAimRotateの回転を更新
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * 50);
            // MainAimのPositionをターゲットへ移動
            mainAim.transform.position = target.transform.position;
        }
        // ターゲットが不在なら(ロックオン解除時)
        else {

            // マウス移動取得
            float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

            // 反転
            mouseX *= reverseX ? -1 : 1;
            mouseY *= reverseY ? -1 : 1;

            // X軸での回転は制限を掛ける
            Vector3 nowRot = this.transform.localEulerAngles;
            float newX = this.transform.localEulerAngles.x + mouseY;
            newX -= newX > 180 ? 360 : 0;
            newX = Mathf.Abs(newX) > clampAngle ? Mathf.Sign(newX) * clampAngle : newX;
            // 回転を更新
            this.transform.localEulerAngles = new Vector3(newX, nowRot.y + mouseX, 0);

        }
    }
}
