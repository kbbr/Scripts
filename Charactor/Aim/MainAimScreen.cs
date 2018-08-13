using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAimScreen : MonoBehaviour {

    private GameObject mainAim;
    public float sensitivityX = 1f;
    public float sensitivityY = 1f;

    public bool reverseX = false;
    public bool reverseY = true;

    public float clampAngle = 60f;

    public GameObject target = null;
    private GameObject playerObj;
    Vector3 defaultMainAimPos;
    Quaternion defaultMainAimRot;

    // Use this for initialization
    void Start()
    {
        mainAim = this.transform.Find("MainAim").gameObject;
        defaultMainAimPos = mainAim.transform.localPosition;
        playerObj = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerFollowTarget").gameObject ;
        //Debug.Log(defaultMainAimPos.y);
    }

    // Update is called once per frame
    void Update()
    {

        target = GameSystem.PlayerTarget;
        if (target != null)
        {
            // targetが画面のXY中央になるようにカメラポジション変更？
            // カメラのForwardもtargetが画面中央になるよう変更
            Vector3 lookVector = (target.transform.position - playerObj.transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(lookVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 50);
            mainAim.transform.position = target.transform.position;
        }
        else
        {
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
            this.transform.localEulerAngles = new Vector3(newX, nowRot.y + mouseX, 0);

        }

        Vector3 mainAimPos = mainAim.transform.position;
    }
}
