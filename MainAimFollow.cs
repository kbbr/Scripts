using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAimFollow : MonoBehaviour {

    GameObject followTargetObj;
    private Vector3 offset;

    void Awake()
    {
        followTargetObj = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerFollowTarget").gameObject;
    }

	// Use this for initialization
	void Start () {
        offset = this.transform.position - followTargetObj.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
        this.transform.position = followTargetObj.transform.position + offset;
	}
}
