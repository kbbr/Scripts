using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDbgTextPos : MonoBehaviour {

    GameObject playerObj;
    RectTransform thisRectTransform;
    Vector2 offset = new Vector2(-50f, 0);

	// Use this for initialization
	void Start () {
        thisRectTransform = GetComponent<RectTransform>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        thisRectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, playerObj.transform.position) + offset;
    }
}
