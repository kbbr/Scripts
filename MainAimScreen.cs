﻿using System.Collections;
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
    Vector3 defaultMainAimPos;
    Quaternion defaultMainAimRot;

    // Use this for initialization
    void Start()
    {
        mainAim = this.transform.Find("MainAim").gameObject;
        defaultMainAimPos = mainAim.transform.localPosition;
        //Debug.Log(defaultMainAimPos.y);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Lock"))
        {
            if (target != null)
            {
                target = null;
                // mainAimのrotationとpositionをロック前に戻す
            }
            else
            {
                // 最近距離の敵をロック対象
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
        if (target != null)
        {
            // targetが画面のXY中央になるようにカメラポジション変更？
            // カメラのForwardもtargetが画面中央になるよう変更
            //Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - this.transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 50);
            //mainAim.transform.position = target.transform.position;
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
