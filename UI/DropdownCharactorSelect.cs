using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownCharactorSelect : MonoBehaviour {

    /// <summary> ドロップダウンメニュー /// </summary>
    [SerializeField]
    private Dropdown dropdown;
    /// <summary> デバッグ用テキスト /// </summary>
    [SerializeField]
    private Text text;
    /// <summary> PlayerObject /// </summary>
    [SerializeField]
    private GameObject SDunitychanPrefab;
    [SerializeField]
    private GameObject SDmisakichanPrefab;

    private void Awake()
    {
        // 初期値はSDunitychanで設定
        GameSystem.Instance.PlayerName = "SDunitychan";
    }

    /// <summary> ドロップダウンイベント時に呼び出される関数 /// </summary>
    /// <param name="result"></param>
    public void DropdownChanged(int result)
    {
        // デバッグ用
        text.text = dropdown.captionText.text;

        // ドロップダウンメニューで選択された文字列からプレイヤーオブジェクトのセット
        GameSystem.Instance.PlayerName = dropdown.captionText.text;
    }
}
