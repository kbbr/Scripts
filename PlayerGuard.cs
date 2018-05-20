using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGuard : MonoBehaviour {

    TimeManager timeManager;
	
	// Update is called once per frame
	void Update () {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Attack");
        GameObject target = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject closest in targets)
        {
            float distance = (closest.transform.position - this.transform.position).sqrMagnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = closest;
            }
        }

        if (Input.GetButtonDown("Boost"))
        {
            //Debug.Log(closestObject ? closestObject.gameObject.name : "");
            float distance = target ? (target.transform.position - this.transform.position).sqrMagnitude : Mathf.Infinity;
            if (target != null) Debug.Log(distance);
            if (target != null && (target.transform.position - this.transform.position).sqrMagnitude < 5) {
                timeManager = Camera.main.gameObject.GetComponent<TimeManager>();
                timeManager.slowDown();
            }
        }
	}
}
