using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {

    // プレイヤーのオブジェクト
    public static GameObject PlayerObject {set; get;}

    // プレイヤーにとってのターゲット
    public static GameObject PlayerTarget { set; get; }

    // エネミーにとってのターゲット
    public static GameObject EnemyTarget { set; get; }

    public static GameObject MainAim { set; get; }
    public Vector3 defaultMainAimPos;

    private void Awake()
    {
        MainAim = GameObject.Find("World/AimRotateOrigin/MainAim").gameObject;
        defaultMainAimPos = MainAim.transform.localPosition;
        PlayerTarget = null;
        PlayerObject = GameObject.Find("World/Player");
    }

    private void Update()
    {
        TargetSearch();
    }

    private void TargetSearch()
    {
        // ロックオンボタンが押されたとき
        if (Input.GetButtonDown("Lock"))
        {
            // ターゲットがいる(ロックオン中)なら、ロックを外す
            if (PlayerTarget != null)
            {
                PlayerTarget = null;
                MainAim.transform.rotation = MainAim.transform.parent.transform.rotation;
                MainAim.transform.position = MainAim.transform.parent.transform.position + MainAim.transform.parent.transform.TransformDirection(defaultMainAimPos);

            }
            // ターゲットが不在(ロックオン中でない)なら、ロックオン対象のターゲットを探しターゲットにする
            else
            {
                GameObject[] PlayerTargets = GameObject.FindGameObjectsWithTag("Enemy");
                float closestDistance = Mathf.Infinity;
                foreach (GameObject closest in PlayerTargets)
                {
                    float distance = (closest.transform.position - this.transform.position).sqrMagnitude;
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        PlayerTarget = closest;
                    }
                }
                PlayerTarget = PlayerTarget.transform.Find("lockTarget").gameObject;

            }
        }
    }

}
