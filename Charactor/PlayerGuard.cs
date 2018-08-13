using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGuard : MonoBehaviour {

    TimeManager timeManager;
	
	// Update is called once per frame
	void Update () {

        // ターゲットを探す
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Attack");
        GameObject target = null;
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

        // Boostボタンが押されたら
        if (Input.GetButtonDown("Boost"))
        {
            // ターゲット存在時は、ターゲットの距離を返す、いなければ無限大の距離
            float distance = target ? (target.transform.position - this.transform.position).sqrMagnitude : Mathf.Infinity;
            // ターゲットがいて、距離が5(JumpDefenceのコライダーの半径)以下なら
            if (target != null && (target.transform.position - this.transform.position).sqrMagnitude < 5) {
                // ヒットストップ開始
                timeManager = Camera.main.gameObject.GetComponent<TimeManager>();
                timeManager.slowDown();
            }
        }
	}
}
