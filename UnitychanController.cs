using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitychanController : MonoBehaviour {

    // GameObject
    public Text dbgText;
    public GameObject bomb;

    // Vector3
    private Vector3 downGrav = Vector3.zero;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 moveSpeed;

    private Vector3 targetSpeed = Vector3.zero;         // 目標速度
    private Vector3 addSpeed = Vector3.zero;            // 加算速度

    private Vector3 newSpeedX = Vector3.zero;
    private Vector3 newSpeedZ = Vector3.zero;
    private Vector3 newSpeedY = Vector3.zero;
    private Vector3 newSpeed = Vector3.zero;

    // const
    const float addNormalSpeed = 1f;        // 通常時加速度
    const float addBoostSpeed = 2f;         // ブースト時加算速度
    const float moveSpeedMax = 4f;         // 通常時最大速度
    const float boostSpeedMax = 20f;        // ブースト時最大速度
    const float hoverSpeed = 0.5f;

    // Animator
    private Animator anim;
    private CharacterController controller;

    // bool
    public bool isJumpDefence;
    public bool isJump = false;

    // float
    public float speed = 15f;
    public float gravity = 20f;
    private float timeCounter = 0f;

    // Use this for initialization
    void Start () {
        moveSpeed = Vector3.zero;
        isJump = false;
        isJumpDefence = false;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        UnitychanGuard();
        UnitychanRotate();
        UnitychanMove();
        dbgTextDraw();

	}


    public void UnitychanMove()
    {   
        // 接地時の重力移動なし
        if (controller.isGrounded)
        {
            moveDirection.y = 0;
            downGrav = Vector3.zero;
        }

        UnitychanVerticalMove();
        UnitychanHorizontalMove();
        UnitychanJump();

        Vector3 cameraRight = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
        Vector3 cameraForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        Transform cameraDummy = Camera.main.transform;
        cameraDummy.eulerAngles = new Vector3(0, cameraDummy.eulerAngles.y, cameraDummy.eulerAngles.z);
        cameraForward = cameraDummy.rotation * new Vector3(0, 0, 1f);
        newSpeedX = moveSpeed.x * cameraRight;
        newSpeedZ = moveSpeed.z * cameraForward;

        newSpeed = newSpeedX + newSpeedZ + newSpeedY;
        anim.SetFloat("speed", Mathf.Max(Mathf.Abs(moveSpeed.x), Mathf.Abs(moveSpeed.z)));



        // charactorControllerの制御
        controller.Move(newSpeed * Time.deltaTime);
    }

    private void UnitychanVerticalMove()
    {
        // 前後移動入力なし
        if (!InputController.IsVerticalMove)
        {
            targetSpeed.z = 0;
            anim.SetTrigger("Standing");
            // 減速値(targetSpeed = 0に向かってaddSpeed分減速する)
            if (controller.isGrounded)
                addSpeed.z = addNormalSpeed;
            else
                addSpeed.z = addNormalSpeed / 4;
        }
        // 前後移動入力あり
        else
        {
            if (InputController.IsBoostButton)
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
    }

    private void UnitychanHorizontalMove()
    {
        // 横移動入力なし
        if (!InputController.IsHorizontalMove)
        {
            targetSpeed = Vector3.zero;

            // 減速値
            if (controller.isGrounded)
                addSpeed.x = addNormalSpeed;
            else
                addSpeed.x = addNormalSpeed / 4;
        }
        // 横移動入力あり
        else
        {
            if (InputController.IsBoostButton)
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
    }

    private void UnitychanRotate()
    {
        // moveSpeedで回転
        if (Mathf.Abs(newSpeed.x) <= 0 && Mathf.Abs(newSpeed.z) <= 0) return;
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

    private void UnitychanJump()
    {
        float jumpSpeed = 4f;            // ジャンプスピード

        // Jump処理(上昇と落下）
        if (InputController.IsJumpButton)
        {
            if (controller.isGrounded)
            {
                isJump = true;
            }
            // hover
            else
            {
                if (InputController.IsBoostButton)
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
            if (InputController.IsBoostButton)
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
    }

    private void UnitychanGuard() {
        // ジャンプディフェンス開始
        if (!isJumpDefence && InputController.IsGuardButton)
        {
            isJumpDefence = true;
            anim.SetTrigger("jumpDefence");

            GameObject bombEff = Instantiate(bomb, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            bombEff.transform.parent = this.transform;
            Destroy(bombEff, 2f);

        }
    }

    void dbgTextDraw()
    {
        dbgText.text = "Pos[ " + this.transform.position + " ]\n";
        dbgText.text += "ViewPos[ " + RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position) + " ]\n";
        dbgText.text += "angle[ " + this.transform.eulerAngles + " ]\n";
        dbgText.text += "camForward[ " + this.transform.TransformDirection(Camera.main.transform.forward) + " ]\n";
        dbgText.text += "camRight[ " + this.transform.TransformDirection(Camera.main.transform.right) + " ]\n";
        dbgText.text += "jumpBool[ " + InputController.IsJumpButton + "]\n";
        AimRotate aimRotate;
        aimRotate = GameObject.Find("AimRotateOrigin").GetComponent<AimRotate>();
        if(aimRotate.target != null)
            dbgText.text += "dis[ " + (aimRotate.target.transform.position - this.transform.position).sqrMagnitude + "]\n";
    }

}
