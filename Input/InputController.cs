using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 入力制御
/// </summary>
public sealed class InputController : MonoBehaviour {

    /// <summary> コントローラーのインスタンス </summary>
    static private InputController _instance;

    /// <summary> インスタンスのアクセッサ </summary>
    public static InputController Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            var gameObject = new GameObject();
            _instance = gameObject.AddComponent<InputController>();
            gameObject.name = _instance.GetType().Name;
            return _instance;
        }
    }

    [SerializeField]
    //private UnitychanController listener = null;

    public static bool IsJumpButton = false;

    public static bool IsAttackButton = false;

    public static bool IsGuardButton = false;

    public static bool IsBoostButton = false;

    public static bool IsBoostButtonDown = false;

    public static bool IsHorizontalMove = false;

    public static bool IsVerticalMove = false;

    public static bool IsLocked = false;

    private void Awake()
    {
        IsJumpButton = false;
        IsAttackButton = false;
        IsGuardButton = false;
        IsBoostButton = false;
        IsBoostButtonDown = false;
        IsHorizontalMove = false;
        IsVerticalMove = false;
        IsLocked = false;
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

        // ブーストボタンが押された瞬間か
        if (Input.GetButtonDown("Boost"))
            IsBoostButtonDown = true;
        else
            IsBoostButtonDown = false;

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

        // Lockボタンが押されているか
        if (Input.GetButton("Jump"))
            IsLocked = true;
        else
            IsLocked = false;
        }

}
