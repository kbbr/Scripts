using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFollowFixedUpdate : MonoBehaviour
{

    private GameObject cameraPositionTargetObj;
    private GameObject cameraLookTargetObj;
    private GameObject playerObj;
    public float cameraFollowSpeed = 4f;
    public float rotateSpeed = 6f;


    // Use this for initialization
    void Start()
    {
        cameraLookTargetObj = GameObject.Find("CameraRotateOrigin").gameObject;
        cameraPositionTargetObj = cameraLookTargetObj.transform.Find("CameraPosition").gameObject;
        playerObj = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 nowPos = this.transform.position;
        Vector3 targetPos = cameraPositionTargetObj.transform.position;

        Vector3 pForward = playerObj.transform.forward;
        Vector3 cameraForward = cameraLookTargetObj.transform.position - playerObj.transform.position;
        float angleVector = Vector3.Angle(pForward, cameraForward);
        float distanceVector = (playerObj.transform.position - this.transform.position).magnitude;

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
        */

        Vector3 thisPos = this.transform.position;
        Vector3 lookTargetPos = cameraLookTargetObj.transform.position;
        Vector3 lookVector = lookTargetPos - thisPos;
        Quaternion thisRotate = this.transform.rotation;
        Quaternion newRotate = Quaternion.LookRotation(lookVector);
        this.transform.rotation = Quaternion.Lerp(thisRotate, newRotate, rotateSpeed * Time.deltaTime);

    }

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

}