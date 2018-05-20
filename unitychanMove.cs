using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unitychanMove : MonoBehaviour {

    public float speed = 15f;
    public float gravity = 20f;

    public Text dbgText;
    public GameObject bomb;

    private Vector3 moveDirection = Vector3.zero;
    Vector3 moveSpeed;

    const float addNormalSpeed = 1f;        // 通常時加速度
    const float addBoostSpeed = 2f;         // ブースト時加算速度
    const float moveSpeedMax = 5f;         // 通常時最大速度
    const float boostSpeedMax = 20f;        // ブースト時最大速度
    float jumpSpeed = 4f;            // ジャンプスピード
    const float hoverSpeed = 0.5f;

    private Animator anim;

    bool isBoost;                           // ブースト判定
    public bool isJumpDefence;
    CharacterController controller;
    Vector3 downGrav = Vector3.zero;

    float timeCounter = 0f;
    bool isJump = false;

    // Use this for initialization
    void Start () {
        moveSpeed = Vector3.zero;
        isBoost = false;
        isJump = false;
        isJumpDefence = false;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();
        
	}
	
	// Update is called once per frame
	void Update () {
        // 接地時の重力移動なし
        if (controller.isGrounded)
        {
            moveDirection.y = 0;
            downGrav = Vector3.zero;
        }

        if (Input.GetButton("Boost"))
        {
            isBoost = true;
        }
        else
        {
            isBoost = false;
        }

        Vector3 targetSpeed = Vector3.zero;         // 目標速度
        Vector3 addSpeed = Vector3.zero;            // 加算速度

        // 横移動入力なし
        if (Input.GetAxis("Horizontal") == 0)
        {
            targetSpeed = Vector3.zero;

            // 減速値
            if (controller.isGrounded)
            {
                addSpeed.x = addNormalSpeed;
            }
            else
            {
                addSpeed.x = addNormalSpeed / 4;
            }
        }
        // 横移動入力あり
        else
        {
            if (isBoost)
            {
                targetSpeed.x = boostSpeedMax;
                addSpeed.x = addBoostSpeed;
            }
            else
            {
                targetSpeed.x = moveSpeedMax;
                addSpeed.x = addNormalSpeed;
            }
            // 横の入力方向によってベクトルの向きを真逆にする
            targetSpeed.x *= Mathf.Sign(Input.GetAxis("Horizontal"));
        }
        // 左右移動速度
        moveSpeed.x = Mathf.MoveTowards(moveSpeed.x, targetSpeed.x, addSpeed.x);

        // 前後移動入力なし
        if (Input.GetAxis("Vertical") == 0)
        {
            targetSpeed.z = 0;

            if (controller.isGrounded)
            {
                // 減速値(targetSpeed = 0に向かってaddSpeed分減速する)
                addSpeed.z = addNormalSpeed;
            }
            else
            {
                addSpeed.z = addNormalSpeed / 4;
            }
        }
        // 前後移動入力あり
        else
        {
            if (isBoost)
            {
                targetSpeed.z = boostSpeedMax;
                addSpeed.z = addBoostSpeed;
            }
            else
            {
                targetSpeed.z = moveSpeedMax;
                addSpeed.z = addNormalSpeed;
            }
            // 前後の入力方向によってベクトルの向きを真逆にする
            targetSpeed.z *= Mathf.Sign(Input.GetAxis("Vertical"));
        }

        // 前後移動速度
        moveSpeed.z = Mathf.MoveTowards(moveSpeed.z, targetSpeed.z, addSpeed.z);
        // ローカルからワールド座標のベクトルへ変換
        //moveSpeed = transform.TransformDirection(moveSpeed);

        Vector3 newSpeedX = moveSpeed.x * this.transform.TransformDirection(Vector3.right);
        Vector3 newSpeedZ = moveSpeed.z * this.transform.TransformDirection(Vector3.forward);
        Vector3 newSpeedY = Vector3.zero;

        dbgText.text = "";
        //dbgText.text = "newSpeedX.x: " + newSpeedX.x + "\nnewSpeedX.y: " + newSpeedX.y + "\nnewSpeedX.z: " + newSpeedX.z + "\n\n";
        //dbgText.text += "newSpeedZ.x: " + newSpeedZ.x + "\nnewSpeedZ.y: " + newSpeedZ.y + "\nnewSpeedZ.z: " + newSpeedZ.z + "\n\n";
        dbgText.text += "jumpSpeed: " + jumpSpeed + "\n";

        if (Input.GetButton("Jump"))
        {
            if (controller.isGrounded)
            {
                dbgText.text += "jump";
                isJump = true;
                //newSpeedY = jumpSpeed * this.transform.TransformDirection(Vector3.up);
            }
            // hover
            else
            {
                //isJump = false;
                //dbgText.text += "hover";
                if (isBoost)
                {
                    moveSpeed.y = gravity * Mathf.MoveTowards(moveSpeed.y, boostSpeedMax / 2, addBoostSpeed / 2) / 10;
                    newSpeedY = this.transform.TransformDirection(Vector3.up) * moveSpeed.y;
                }
                else
                {
                    newSpeedY = hoverSpeed * gravity * this.transform.TransformDirection(Vector3.up);
                }
                downGrav = -gravity * this.transform.TransformDirection(Vector3.up) * Time.deltaTime;
            }
        }
        // down to Geometry
        else
        {
            if (isBoost)
            {
                downGrav = -gravity * this.transform.TransformDirection(Vector3.up) * Time.deltaTime * Time.deltaTime;
                newSpeedY = downGrav;
            }
            else
            {
                downGrav += -gravity * this.transform.TransformDirection(Vector3.up) * Time.deltaTime;
                newSpeedY = downGrav;
            }
        }

        if (isJump)
        {
            if (timeCounter == 0f)
            {
                anim.SetTrigger("jump");
                anim.speed = 2f;
                Debug.Log(timeCounter);
            }
            timeCounter += Time.deltaTime;
            if (timeCounter < 0.3f)
            {
                jumpSpeed = 8 / (1 + 0.1f * Mathf.Exp(-32f * timeCounter));
                newSpeedY = jumpSpeed * 1 * this.transform.TransformDirection(Vector3.up);
            }
            else
            {
                isJump = false;
                timeCounter = 0f;
                anim.speed = 1f;
            }
        }
        
        if (!isJumpDefence && Input.GetButtonDown("JumpDefence"))
        {
            isJumpDefence = true;
            anim.SetTrigger("jumpDefence");
            
            GameObject bombEff = Instantiate(bomb, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            bombEff.transform.parent = this.transform;
            Destroy(bombEff, 2f);
            
        }

        //moveSpeed = newSpeedX + newSpeedZ;
        Vector3 newSpeed = newSpeedX + newSpeedZ + newSpeedY;

        //dbgText.text += "moveSpeed.x: " + moveSpeed.x + "\nmoveSpeed.y: " + moveSpeed.y + "\nmoveSpeed.z: " + moveSpeed.z + "\n";

        // charactorControllerの制御
        controller.Move(newSpeed * Time.deltaTime);
        //this.transform.localPosition += newSpeed * Time.deltaTime;
        //rb.AddForce(moveSpeed * 100 * Time.deltaTime);
	}
    
    

}
