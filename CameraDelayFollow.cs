using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraDelayFollow : MonoBehaviour {

    private GameObject playerObj;
    private Vector3 offset;
    public float moveSpeed = 8f;
    private Vector3 fixPosition;

    public Text vText;

    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player").gameObject;
    }


    // Use this for initialization
    void Start () {
        offset = this.transform.position - playerObj.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 nowPos = this.transform.position;
        Vector3 targetPos = playerObj.transform.position + offset;
        //this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpped * Time.deltaTime);

        Vector3 pForward = playerObj.transform.forward;
        Vector3 cameraForward = this.transform.position - playerObj.transform.position;

        float angleVector = Vector3.Angle(pForward, cameraForward);
        vText.text = angleVector.ToString();

        if (angleVector < 55f)
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed * 4 * Time.deltaTime);
        }
        else if (angleVector < 57f)
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed * 3 * Time.deltaTime);
        }
        else if (angleVector < 70f) //60
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed * 2 * Time.deltaTime);
        }
        else if (angleVector < 90f) //65
        {
            //fixPosition = this.transform.position - playerObj.transform.position;
            //this.transform.position = playerObj.transform.position + fixPosition;
            this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed  * 2 * Time.deltaTime);
            //this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed * Time.deltaTime);
            //this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed * Time.deltaTime);

        }
        else
        {
            this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed * Time.deltaTime);
        }
    }

    /*
    void FixedUpdate () {
        Vector3 nowPos = this.transform.position;
        Vector3 targetPos = playerObj.transform.position + offset;
        //this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpped * Time.deltaTime);
        
        Vector3 pForward = playerObj.transform.forward;
        Vector3 cameraForward = this.transform.position - playerObj.transform.position;

        float angleVector = Vector3.Angle(pForward, cameraForward);
        vText.text = angleVector.ToString();

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
            this.transform.position = Vector3.Lerp(nowPos, targetPos, moveSpeed * Time.deltaTime);
        }
        
    }
    */
}
