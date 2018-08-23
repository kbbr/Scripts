using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollowFixedUpdate : MonoBehaviour {
    private GameObject playerObj;
    private Vector3 offset;
    public float moveSpeed = 4f;
    private Vector3 fixPosition;

    public Text vText;
    // Use this for initialization
    private void Awake()
    {
        playerObj = GameSystem.Instance.PlayerObject;
    }


    // Use this for initialization
    void Start()
    {
        offset = this.transform.position - playerObj.transform.position;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void FixedUpdate () {
        Vector3 nowPos = this.transform.position;
        Vector3 targetPos = playerObj.transform.position + offset;
        //this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpped * Time.deltaTime);

        Vector3 pForward = playerObj.transform.forward;
        Vector3 cameraForward = this.transform.position - playerObj.transform.position;

        float angleVector = Vector3.Angle(pForward, cameraForward);
        vText.text = angleVector.ToString();
        /*
        if (angleVector < 60f)
        {
            //fixPosition = this.transform.position - playerObj.transform.position;
            //this.transform.position = playerObj.transform.position + fixPosition;
            this.transform.position = Vector3.Slerp(nowPos, targetPos, moveSpeed * 5 * Time.deltaTime);
            //this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed * Time.deltaTime);
            //this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed * Time.deltaTime);

        }
        
        else
        {
        */
            this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed * Time.deltaTime);
        //}

    }

}
