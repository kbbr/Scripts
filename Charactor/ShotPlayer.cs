using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPlayer : MonoBehaviour {

    public GameObject explosion;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += this.transform.forward * Time.deltaTime * 100;
	}
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
