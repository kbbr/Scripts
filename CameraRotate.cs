using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {

    public float sensitivityX = 1f;
    public float clampAngle = 60f;
    public bool reverseX = true;
    private GameObject playerFollowTargetObj;

    GameObject target;
    float angleVector;

    // Use this for initialization
    void Start () {
        playerFollowTargetObj = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerFollowTarget").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Lock"))
        {
            if (target != null)
            {
                target = null;
            }
            else
            {
                GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
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
                target = target.transform.Find("lockTarget").gameObject;

            }
        }

        ///*
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityX; ;
        mouseY *= reverseX ? -1 : 1;
        Vector3 nowRot = this.transform.eulerAngles;
        float newX = nowRot.x + mouseY;
        newX -= newX > 180 ? 360 : 0;
        if (target != null)
        {
            newX = Mathf.Clamp(newX, -clampAngle, -clampAngle / 5);
        }
        else
        {
            newX = Mathf.Clamp(newX, -clampAngle, clampAngle);
        }
        this.transform.eulerAngles = new Vector3(newX, playerFollowTargetObj.transform.eulerAngles.y, 0);
        //*/
	}
}
