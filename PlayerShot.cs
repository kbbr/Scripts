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
            {
                Ray ray = new Ray(Camera.main.transform.position, (mainAimObj.transform.position - Camera.main.transform.position));
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, 1000f))
                {
                    shotPrefab.transform.forward = (hit.point - shotPrefab.transform.position).normalized;
                }
                else
                {
                    Vector3 noHitpoint = (mainAimObj.transform.position - Camera.main.transform.position).normalized * 2000;
                    //noHitpoint = mainAimObj.transform.position;
                    shotPrefab.transform.forward = (noHitpoint - shotPrefab.transform.position).normalized;
                }
                //shotPrefab.transform.forward = (mainAimObj.transform.position - Camera.main.transform.position + new Vector3(0, 0f, 0)).normalized;
                //shotPrefab.transform.forward = (mainAimObj.transform.position - shotPrefab.transform.position).normalized;
            }
        }
	}
}
