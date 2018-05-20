using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFollow : MonoBehaviour {

    private GameObject cameraPositionTargetObj;
    private GameObject cameraLookTargetObj;
    private GameObject playerObj;
    public float cameraFollowSpeed = 4f;
    public float rotateSpeed = 6f;

    GameObject target;

    // Use this for initialization
    void Start () {
        cameraLookTargetObj = GameObject.Find("CameraRotateOrigin").gameObject;
        cameraPositionTargetObj = cameraLookTargetObj.transform.Find("CameraPosition").gameObject;
        playerObj = GameObject.FindGameObjectWithTag("Player").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetButtonDown("Lock"))
        {
            if (target != null)
            {
                target = null;
                rotateSpeed = 6f;
                //cameraFollowSpeed = 4f;
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
                rotateSpeed = 6f;
                //cameraFollowSpeed = 16f;
            }
        }
        
        Vector3 nowPos = this.transform.position;
        Vector3 targetPos = cameraPositionTargetObj.transform.position;

        Vector3 pForward = playerObj.transform.forward;
        Vector3 cameraForward = cameraLookTargetObj.transform.position - playerObj.transform.position;
        float angleVector = Vector3.Angle(pForward, cameraForward);
        float distanceVector = (playerObj.transform.position - this.transform.position).magnitude;

        if(angleVector < 90f)
        {
            cameraFollowSpeed = 8f;
        }
        else
        {
            cameraFollowSpeed = 4f;
        }

        if (target != null && playerObj.transform.position.y - target.transform.position.y > 0)
        {
            if (playerObj.transform.position.y - target.transform.position.y < 12)
            {
                targetPos += new Vector3(0, Mathf.Clamp(playerObj.transform.position.y - target.transform.position.y, 0, 12), 0);
            }
            else
            {
                targetPos += new Vector3(0, Mathf.Clamp(playerObj.transform.position.y - target.transform.position.y, 22, 30) * 0.6f, 0);
            }
        }
        else
        {
            cameraFollowSpeed = 4f;
        }

        if (angleVector < 57f)
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, cameraFollowSpeed * 4 * Time.deltaTime);
        }
        else if (angleVector < 60f)
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, cameraFollowSpeed * 3 * Time.deltaTime);
            //this.transform.position = Vector3.Lerp(nowPos, targetPos, cameraFollowSpeed * Time.deltaTime);
            //this.transform.position = Vector3.Lerp(this.transform.position, targetPos, cameraFollowSpeed * Time.deltaTime);
            //this.transform.position = Vector3.Lerp(this.transform.position, targetPos, cameraFollowSpeed * Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, cameraFollowSpeed * Time.deltaTime);
        }
        

        Vector3 thisPos = this.transform.position;
        Vector3 lookTargetPos = cameraLookTargetObj.transform.position;
        Vector3 lookVector = lookTargetPos - thisPos;
        
        if (target)
        {
            //Vector3 crVec = (target.transform.position - thisPos);
            Vector3 crVec = new Vector3((target.transform.position.x - thisPos.x), target.transform.position.y - thisPos.y, target.transform.position.z - thisPos.z);
            lookVector = crVec * 0.8f + lookVector;
        }

        Quaternion thisRotate = this.transform.rotation;
        Quaternion newRotate = Quaternion.LookRotation(lookVector);
        this.transform.rotation = Quaternion.Lerp(thisRotate, newRotate, rotateSpeed * Time.deltaTime);

	}
    /*
    private void FixedUpdate()
    {
        Vector3 nowPos = this.transform.position;
        Vector3 targetPos = cameraPositionTargetObj.transform.position;

        Vector3 pForward = playerObj.transform.forward;
        Vector3 cameraForward = cameraLookTargetObj.transform.position - playerObj.transform.position;
        float angleVector = Vector3.Angle(pForward, cameraForward);
        Vector3 distanceVector = cameraPositionTargetObj.transform.position - this.transform.position;


        if (angleVector < 60f)
        {
            this.transform.position = Vector3.Slerp(nowPos, targetPos, cameraFollowSpeed * 3 * Time.deltaTime);
            //this.transform.position = Vector3.Lerp(this.transform.position, targetPos, cameraFollowSpeed * Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, cameraFollowSpeed * Time.deltaTime);
        }
    }
    */
}
