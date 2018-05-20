using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    GameObject playerObj;
    public GameObject targetCursorPrefab;
    GameObject targetCursor;
    bool isCursor;
    TimeManager timeManager;

    // Use this for initialization
    void Start () {
        playerObj = GameObject.Find("Player");
        targetCursor = Instantiate(targetCursorPrefab, transform.position, transform.rotation);
        isCursor = false;
    }
	
	// Update is called once per frame
	void Update () {
        float distance = (playerObj.transform.position - this.transform.position).magnitude;
        if(distance < 20)
        {
            isCursor = true;
            targetCursor.SetActive(isCursor);
        }
        else
        {
            isCursor = false;
            targetCursor.SetActive(isCursor);
        }
        if (isCursor)
        {
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

}
