using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour {

    public GameObject shot;
    GameObject tgt;
    GameObject mainAimObj;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        tgt = GameObject.Find("AimRotateOrigin").GetComponent<AimRotate>().target;
        mainAimObj = GameObject.Find("MainAim");
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject shotPrefab = Instantiate(shot, this.transform.position, transform.rotation);
            if (tgt != null)
                shotPrefab.transform.forward = (tgt.transform.position - shotPrefab.transform.position).normalized;
            else
                shotPrefab.transform.forward = (mainAimObj.transform.position - shotPrefab.transform.position).normalized;
        }
	}
}
