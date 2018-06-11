using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JummingAim : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    
	// Update is called once per frame
	void Update () {
        float t = Time.time * 2;
        /*
        // 1 - 2
        this.transform.localScale = new Vector3(1 + Mathf.Pow(Mathf.PingPong(t, 1f), 4), 1 + Mathf.Pow(Mathf.PingPong(t, 1f), 4), 1);
        // -0.75 - -1.5
        this.transform.localPosition = new Vector3(-0.75f - Mathf.Pow(Mathf.PingPong(t, 1f), 4) * 0.75f, 0, 0);
        */
        // 1 - 2
        this.transform.localScale = new Vector3(0.5f + Mathf.Pow(Mathf.PingPong(t, 1f), 4) * 1.5f, 0.5f + Mathf.Pow(Mathf.PingPong(t, 1f), 4) * 1.5f, 1);
        // -0.75 - -1.5
        this.transform.localPosition = new Vector3(-0.4f - Mathf.Pow(Mathf.PingPong(t, 1f), 4)  * 1.1f, 0, 0);
    }
}
