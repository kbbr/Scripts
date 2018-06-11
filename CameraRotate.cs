using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {

    public float sensitivityX = 2f;
    public float sensitivityY = 2f;
    public float clampAngle = 60f;
    public bool reverseX = true;
    public bool reverseY = false;
    public GameObject playerFollowTargetObj;
    public GameObject mainAimObj;

    GameObject target;
    float angleVector;

    // Use this for initialization
    void Start () {
        playerFollowTargetObj = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerFollowTarget").gameObject;
        mainAimObj = GameObject.Find("MainAim");
	}
	
	// Update is called once per frame
	void Update () {
        // ロックオンボタンが押されたとき
        if (Input.GetButtonDown("Lock"))
        {
            // ターゲットがいる(ロックオン中)なら、ロックを外す
            if (target != null)
            {
                target = null;
            }
            // ターゲットが不在(ロックオン中でない)なら、ロックオン対象のターゲットを探しターゲットにする
            else
            {
                GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
                float closestDistance = Mathf.Infinity;
                foreach (GameObject closest in targets)
                {
                    float distance = (closest.transform.position - this.transform.position).sqrMagnitude;
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        target = closest;
                    }
                }
                target = target.transform.Find("lockTarget").gameObject;

            }
        }

        // マウスの上下移動を取得
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityX; ;
        // 上下移動の値を反転させるかどうか
        mouseY *= reverseX ? -1 : 1;
        // 現在のRotationを保存
        Vector3 nowRot = this.transform.eulerAngles;
        // 更新後のRotationはマウスでの上下移動を足したものにする
        float newX = nowRot.x + mouseY;
        // 値は0-180の間に制限
        newX -= newX > 180 ? 360 : 0;

        // ターゲットがいるなら(ロックオン時)
        if (target != null)
        {
            // X軸だけclampAngleで制限(カメラが下になりすぎないようにclampAngleの5分の1)
            newX = Mathf.Clamp(newX, -clampAngle, -clampAngle / 5);
        }
        // ターゲットが不在なら(ロックオン解除時)
        else
        {
            // X軸だけclampAngleで制限
            newX = Mathf.Clamp(newX, -clampAngle, clampAngle);
        }
        /*
        // マウスの左右移動を取得
        float mouseX = Input.GetAxis("Mouse X") * sensitivityY;
        // 上下移動の値を反転させるかどうか
        mouseX *= reverseY ? -1 : 1;
        // 更新後のRotationはマウスでの左右移動を足したものにする
        float newY = nowRot.y + mouseX;
        */
        // カメラのRotationを更新
        //this.transform.eulerAngles = new Vector3(newX, playerFollowTargetObj.transform.eulerAngles.y, 0);
        //this.transform.eulerAngles = new Vector3(newX, nowRot.y, 0);

        // カメラのRotationを更新(MainAim -> Playerベクトル方向で)
        this.transform.rotation = Quaternion.LookRotation(mainAimObj.transform.position - playerFollowTargetObj.transform.position);

    }
}
