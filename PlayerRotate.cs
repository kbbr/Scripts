using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRotate : MonoBehaviour {

    private GameObject mainAimObj;
    private GameObject playerUpperBodyObj;
    public float rotateSpeed = 4f;

    public bool constrainX = true;
    public bool constrainY = false;
    public bool constrainZ = true;

    public float clampAngleX = 25f;
    public float clampAngleZ = 0f;

    public Text dbgText;
    public float dbgValue = -90f;

    // Use this for initialization
    void Start () {
        mainAimObj = GameObject.Find("MainAim").gameObject;
        playerUpperBodyObj = GameObject.Find("Character1_Spine");
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 thisPos = this.transform.position;
        Vector3 targetPos = mainAimObj.transform.position;
        Quaternion targetVector = Quaternion.LookRotation(targetPos - thisPos);

        Quaternion nowRotate = this.transform.rotation;
        Vector3 newRot = Quaternion.Lerp(nowRotate, targetVector, rotateSpeed * Time.deltaTime).eulerAngles;

        newRot.x *= constrainX ? 0 : 1;
        newRot.y *= constrainY ? 0 : 1;
        newRot.z *= constrainZ ? 0 : 1;


        Vector3 nowPlayerUpperRot = playerUpperBodyObj.transform.localEulerAngles;


        targetVector = Quaternion.LookRotation(targetPos - playerUpperBodyObj.transform.position);
        Quaternion offsetRotate = Quaternion.Euler(90f, 0, 0);
        Vector3 newRotPU = Quaternion.Lerp(playerUpperBodyObj.transform.rotation * offsetRotate, targetVector, 2f * Time.deltaTime).eulerAngles;

        
        newRotPU.x -= newRotPU.x > 180 ? 360 : 0;
        newRotPU.z -= newRotPU.z > 180 ? 360 : 0; 

        newRotPU.x = Mathf.Abs(newRotPU.x) > clampAngleX ? Mathf.Sign(newRotPU.x) * clampAngleX : newRotPU.x;
        newRotPU.z = Mathf.Abs(newRotPU.z) > clampAngleZ ? Mathf.Sign(newRotPU.z) * clampAngleZ : newRotPU.z;
        /**/

        playerUpperBodyObj.transform.localEulerAngles = new Vector3(newRotPU.x, nowPlayerUpperRot.y, 0);

        this.transform.eulerAngles = new Vector3(newRot.x, newRot.y, newRot.z); 
	}

    void dbgTextPush(string txt)
    {
        dbgText.text += txt;
        dbgText.text += "\n";
    }

}
