using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Charactor {

    [SerializeField]
    protected float speed = 15f;
    [SerializeField]
    protected float gravity = 20f;
    [SerializeField]
    protected GameObject bomb;
    [SerializeField]
    protected bool isJumpDefence;

    protected Vector3 moveDirection = Vector3.zero;
    protected Vector3 moveSpeed;
    protected Animator anim;

    protected bool isBoost;                           // ブースト判定
    protected CharacterController controller;
    protected Vector3 downGrav = Vector3.zero;

    protected bool isJump = false;

    private float timeCounter = 0f;

    const float addNormalSpeed = 1f;        // 通常時加速度
    const float addBoostSpeed = 2f;         // ブースト時加算速度
    const float moveSpeedMax = 4f;         // 通常時最大速度
    const float boostSpeedMax = 20f;        // ブースト時最大速度
    float jumpSpeed = 4f;            // ジャンプスピード
    const float hoverSpeed = 0.5f;


    // Use this for initialization
    void Start()
    {
        moveSpeed = Vector3.zero;
        isBoost = false;
        isJump = false;
        isJumpDefence = false;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    private void Update () {
		// move 
        
        // animation

        // effect

        // damage

	}


    public void UnityChanMove()
    {

        // InputController
        // property: Boost
        // property: non-Axis
        // property: junpDefence
        // property: jump
        // property: lightning

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
            anim.SetTrigger("Standing");
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

        Vector3 cameraRight = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
        Vector3 cameraForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        Transform cameraDummy = Camera.main.transform;
        cameraDummy.eulerAngles = new Vector3(0, cameraDummy.eulerAngles.y, cameraDummy.eulerAngles.z);
        cameraForward = cameraDummy.rotation * new Vector3(0, 0, 1f);

        Vector3 newSpeedX = moveSpeed.x * cameraRight;
        Vector3 newSpeedZ = moveSpeed.z * cameraForward;
        Vector3 newSpeedY = Vector3.zero;

        // Jump処理(上昇と落下）
        if (Input.GetButton("Jump"))
        {
            if (controller.isGrounded)
            {
                isJump = true;
            }
            // hover
            else
            {
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
        // ジャンプ中なら
        if (isJump)
        {
            if (timeCounter == 0f)
            {
                // ジャンプアニメーション開始(トリガーをオン)
                anim.SetTrigger("jump");
                anim.speed = 2f;
                Debug.Log(timeCounter);
            }
            // タイムカウンターを引数に処理
            timeCounter += Time.deltaTime;
            if (timeCounter < 0.3f)
            {
                // シグモイド関数でジャンプ起動を模擬
                jumpSpeed = 8 / (1 + 0.1f * Mathf.Exp(-32f * timeCounter));
                newSpeedY = jumpSpeed * 1 * this.transform.TransformDirection(Vector3.up);
            }
            // ジャンプは0.3f秒だけ、過ぎたら終わり
            else
            {
                isJump = false;
                timeCounter = 0f;
                anim.speed = 1f;
            }
        }

        // ジャンプディフェンス開始
        if (!isJumpDefence && Input.GetButtonDown("JumpDefence"))
        {
            isJumpDefence = true;
            anim.SetTrigger("jumpDefence");

            GameObject bombEff = Instantiate(bomb, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            bombEff.transform.parent = this.transform;
            Destroy(bombEff, 2f);

        }

        Vector3 newSpeed = newSpeedX + newSpeedZ + newSpeedY;
        anim.SetFloat("speed", Mathf.Max(Mathf.Abs(moveSpeed.x), Mathf.Abs(moveSpeed.z)));

        // charactorControllerの制御
        controller.Move(newSpeed * Time.deltaTime);

        // moveSpeedで回転
        if (Mathf.Abs(newSpeed.x) <= 0 && Mathf.Abs(newSpeed.z) <= 0) return;
        //else if ((Mathf.Abs(newSpeed.x) > 0 || Mathf.Abs(newSpeed.z) > 0) && Mathf.Max(Mathf.Abs(newSpeed.x), Mathf.Abs(newSpeed.z)) < 1)
        else if (Mathf.Max(Mathf.Abs(newSpeed.x), Mathf.Abs(newSpeed.z)) < 1)
        {
            transform.rotation = Quaternion.LookRotation(newSpeedX + newSpeedZ);    // Y方向は抜くため
        }
        // 走っているときはゆっくり回転
        else
        {
            Vector3 smoothMoveSpeed = Vector3.RotateTowards(transform.forward, newSpeedX + newSpeedZ, 300 * Mathf.Deg2Rad * Time.deltaTime, 1000);
            //smoothMoveSpeed = smoothMoveSpeed.normalized;
            if (smoothMoveSpeed.magnitude > 0) transform.rotation = Quaternion.LookRotation(smoothMoveSpeed);
        }
    }






}
