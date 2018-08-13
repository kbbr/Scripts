using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour {

    /// <summary> ショットのプレファブ /// </summary>
    [SerializeField]
    private GameObject shotPrefab;

    private GameObject tgt;
    private GameObject mainAimObj;

    // ショットを飛ばす距離
    private float rayDistance = 1000f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        // ターゲットオブジェクトを探す
        tgt = GameObject.Find("AimRotateOrigin").GetComponent<AimRotate>().target;
        // MainAimを探す
        mainAimObj = GameObject.Find("MainAim");

        // ショットボタンが押されたら
        if (Input.GetButtonDown("Fire1"))
        {
            // ショットのプレファブからインスタンス生成
            GameObject shot = Instantiate(shotPrefab, this.transform.position, transform.rotation);
            // ターゲットがいれば、ターゲット方向へショットを飛ばす
            if (tgt != null)
                shot.transform.forward = (tgt.transform.position - shot.transform.position).normalized;
            // ターゲット不在時
            else
            {
                // レイヤーマスク(Player:8 に当たらないよう除外)
                int layermask = ~(1 << 8);
                // メインAimとカメラの直線上にRayを飛ばす
                Ray ray = new Ray(Camera.main.transform.position, (mainAimObj.transform.position - Camera.main.transform.position));
                // 距離rayDistance以内でRayが衝突したらそこをショットの方向とする
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, rayDistance, layermask))
                {
                    shot.transform.forward = (hit.point - shot.transform.position).normalized;
                }
                // Rayが衝突しないときは、メインAimとカメラの直線上からrayDistanceを到達点としてショットの方向にして飛ばす
                else
                {
                    Vector3 noHitpoint = (mainAimObj.transform.position - Camera.main.transform.position).normalized * rayDistance;
                    shot.transform.forward = (noHitpoint - shot.transform.position).normalized;
                }
            }
        }
	}
}
