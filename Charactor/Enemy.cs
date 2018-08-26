using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    // ターゲット用
    /// <summary> プレイヤーオブジェクト  /// </summary>
    private GameObject playerObj;
    /// <summary> ターゲットカーソルのプレファブ /// </summary>
    [SerializeField]
    private GameObject targetCursorPrefab;
    /// <summary> ターゲットカーソルのインスタンス /// </summary>
    private GameObject targetCursor;
    /// <summary> ターゲットカーソルの表示フラグ /// </summary>
    private bool isCursor;

    /// <summary> HPゲージ /// </summary>
    [SerializeField]
    private GameObject HPGaugeBase;
    private GameObject HPGaugeInstance;

    private float EnermyHP = 200;
    private float EnermyMAXHP = 200;

    TimeManager timeManager;

    private float cursorDistance = 20f;

    // Use this for initialization
    void Start () {
        playerObj = GameSystem.Instance.PlayerObject;
        targetCursor = Instantiate(targetCursorPrefab, transform.position, transform.rotation);
        //HPGaugeInstance = Instantiate(HPGaugeBase, RectTransformUtility.WorldToScreenPoint(Camera.main, HPGaugeInstance.transform.position), Quaternion.identity);
        HPGaugeInstance = Instantiate(HPGaugeBase, RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position), Quaternion.identity);

        HPGaugeInstance.transform.parent = GameObject.Find("UI/Canvas").transform;
        HPGaugeInstance.transform.localScale = HPGaugeBase.transform.localScale;
        isCursor = false;
        HPGaugeInstance.SetActive(isCursor);
        Destroy(this, 200);
    }
	
	// Update is called once per frame
	void Update () {
        // カーソル表示(一定距離以下なら表示しない)
        CursorDisplay();

        // 敵HPゲージ表示

	}

    // 敵カーソルを表示させる
    private void CursorDisplay()
    {
        float distance = (playerObj.transform.position - this.transform.position).magnitude;
        // 距離がcursorDistance以下ならカーソル表示
        if (distance < cursorDistance)
        {
            isCursor = true;
            targetCursor.SetActive(isCursor);
            HPGaugeInstance.SetActive(isCursor);
        }
        // 距離がcursorDistanceより遠ければ非表示
        else
        {
            isCursor = false;
            targetCursor.SetActive(isCursor);
            HPGaugeInstance.SetActive(isCursor);
        }
        // カーソル表示フラグがON
        if (isCursor)
        {
            // カーソルは自身の上に表示
            targetCursor.transform.position = this.transform.position + Vector3.up * 2.5f + Vector3.up * Mathf.PingPong(Time.time, 0.5f);
            targetCursor.transform.eulerAngles = new Vector3(0, Time.frameCount % 360f, 0);
            HPGaugeInstance.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position + new Vector3(1f, 2f, 0));
        }
    }

    private void HPGaugeDisplay(float HP, float MAXHP)
    {
        GameObject HPGauge = HPGaugeInstance.transform.Find("EnermyHPGauge").gameObject;
        float HPproportion = HP / MAXHP;
        HPGauge.transform.localScale = new Vector3(HPproportion, 1, 1);
    }

    // トリガー処理
    private void OnTriggerEnter(Collider other)
    {
        // バリアと衝突
        if (other.gameObject.tag == "Barrier")
        {
            // プレイヤーと逆方向へ飛ばす
            Vector3 forceBarrier = (this.transform.position - playerObj.transform.position).normalized;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = forceBarrier * 10;
            Debug.Log("barrier");
        }

    }

    // 衝突処理
    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーへダメージ処理
        if (collision.gameObject.tag == "Player")
        {
            playerObj.GetComponent<UnitychanController>().UnitychanDamage(100f);
        }

        // ショットのダメージ処理
        if(collision.gameObject.tag == "Shot")
        {
            Debug.Log("shot");
            if (EnermyHP > 20)
                EnermyHP -= 20f;
            HPGaugeDisplay(EnermyHP, EnermyMAXHP);
        }
    }
    
}
