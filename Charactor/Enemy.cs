using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    TimeManager timeManager;

    private float cursorDistance = 20f;

    // Use this for initialization
    void Start () {
        playerObj = GameObject.Find("Player");
        targetCursor = Instantiate(targetCursorPrefab, transform.position, transform.rotation);
        isCursor = false;
        Destroy(this, 200);
    }
	
	// Update is called once per frame
	void Update () {
        // カーソル表示(一定距離以下なら表示しない)
        CursorDisplay();

	}

    private void CursorDisplay()
    {
        float distance = (playerObj.transform.position - this.transform.position).magnitude;
        // 距離がcursorDistance以下ならカーソル表示
        if (distance < cursorDistance)
        {
            isCursor = true;
            targetCursor.SetActive(isCursor);
        }
        // 距離がcursorDistanceより遠ければ非表示
        else
        {
            isCursor = false;
            targetCursor.SetActive(isCursor);
        }
        // カーソル表示フラグがON
        if (isCursor)
        {
            // カーソルは自身の上に表示
            targetCursor.transform.position = this.transform.position + Vector3.up * 2.5f + Vector3.up * Mathf.PingPong(Time.time, 0.5f);
            targetCursor.transform.eulerAngles = new Vector3(0, Time.frameCount % 360f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Barrier")
        {
            Vector3 forceBarrier = (this.transform.position - playerObj.transform.position).normalized;
            Rigidbody rb = GetComponent<Rigidbody>();
            //rb.AddForce(forceBarrier * 1000);
            rb.velocity = forceBarrier * 10;
            Debug.Log("barrier");
        }

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerObj.GetComponent<UnitychanController>().UnitychanDamage(100f);
        }
    }
    
}
