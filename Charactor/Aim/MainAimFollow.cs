using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAimFollow : MonoBehaviour {

    GameObject followTargetObj;
    private Vector3 offset;

    void Awake()
    {
        // 追従対象を探す: PlayerFollowTargetを探す
        followTargetObj = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerFollowTarget").gameObject;
    }

	// Use this for initialization
	void Start () {
        // カメラの最初の位置とfollowTargetの位置をoffsetにする
        offset = this.transform.position - followTargetObj.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // offset分離れて追従
        this.transform.position = followTargetObj.transform.position + offset;
	}
}
