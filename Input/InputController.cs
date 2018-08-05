using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 入力制御
/// </summary>
public sealed class InputController : MonoBehaviour {

    /// <summary> コントローラーのインスタンス </summary>
    static private InputController _instance;

    /// <summary> インスタンスのアクセッサ </summary>
    static public InputController Instance
    {
        get
        {
            if(_instance != null)
                return _instance;
            var gameObject = new GameObject();
            _instance = gameObject.AddComponent<InputController>();
            gameObject.name = _instance.GetType().Name;
            return _instance;
        }
    }

    [SerializeField]
    //private UnitychanController listener = null;

    static public bool IsJumpButton = false;

    static public bool IsAttackButton = false;

    static public bool IsGuardButton = false;

    static public bool IsBoostButton = false;

    static public bool IsHorizontalMove = false;

    static public bool IsVerticalMove = false;

    private void Awake()
    {
        IsJumpButton = false;
        IsAttackButton = false;
        IsGuardButton = false;
        IsBoostButton = false;
        IsHorizontalMove = false;
        IsVerticalMove = false;
    }

    private void Update()
    {
        UpdateButtonOn();
        UpdateMove();
    }

    private void UpdateMove()
    {
        // 横移動(X軸移動)
        if (Input.GetAxis("Horizontal") == 0)
            IsHorizontalMove = false;
        else
            IsHorizontalMove = true;
        // 縦移動(Z軸移動)
        if (Input.GetAxis("Vertical") == 0)
            IsVerticalMove = false;
        else
            IsVerticalMove = true;
    }

    private void UpdateButtonOn()
    {
        // ブーストボタンが押されているか
        if (Input.GetButton("Boost"))
            IsBoostButton = true;
        else
            IsBoostButton = false;

        // ガードボタンが押されているか
        if (Input.GetButton("JumpDefence"))
            IsGuardButton = true;
        else
            IsGuardButton = false;

        // ジャンプボタンが押されているか
        if (Input.GetButton("Jump"))
            IsJumpButton = true;
        else
            IsJumpButton = false;
    }


}
