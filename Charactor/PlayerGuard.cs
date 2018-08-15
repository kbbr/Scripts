using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGuard : MonoBehaviour {

    TimeManager timeManager;

    private void Start()
    {
        timeManager = Camera.main.gameObject.GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update () {

        // Boostボタンが押されたら
        if (InputController.IsBoostButtonDown) {
            // Attackタグを持つオブジェクトを探す
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Attack");
            GameObject target = null;
            float closestDistance = Mathf.Infinity;

            // 最近傍のオブジェクトを探す
            foreach (GameObject closest in targets)
            {
                float dis = (closest.transform.position - this.transform.position).sqrMagnitude;
                if (dis < closestDistance)
                {
                    closestDistance = dis;
                    target = closest;
                }
            }

            // ターゲット存在時は、ターゲットの距離を返す、いなければ無限大の距離
            float distance = target ? (target.transform.position - this.transform.position).sqrMagnitude : Mathf.Infinity;
            
            // ターゲットがいて、距離が5(JumpDefenceのコライダーの半径)以下なら
            if (!timeManager.returnEndSlowDown() && target != null && distance < 5) {
                // ヒットストップ開始
                timeManager.slowDown();
            }
        }
	}
}
