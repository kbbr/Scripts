using UnityEngine;
using UnityEngine.UI;

public class HPGaugeDisplay : MonoBehaviour {

    /// <summary> PlayerのHPゲージのイメージオブジェクト /// </summary>
    [SerializeField]
    private Image HPGauge;

    private Color myGreen;
    private Color myYellow;
    private Color myRed;

    const float yellowThreshhold = 0.6f;
    const float redThereshhold = 0.2f;

    // MAXHPに対するHPの色とゲージの長さの表示を更新する
    public void PlayerHPUpdate(float HP, float MAXHP)
    {
        // HPの最大HPに対する割合を計算
        float HPproportion = HP / MAXHP;

        // HPの割合に応じてゲージカラーを変える
        if(HPproportion == 1f)
        {
            HPGauge.color = new Color(0f, 1f, 0f);
        }
        else if(HPproportion < redThereshhold)
        {
            HPGauge.color = new Color(1f, 0f, 0f);
        }
        else if (HPproportion < yellowThreshhold)
        {
            HPGauge.color = new Color(1f, 1f, 0f);
        }
        else
        {
            HPGauge.color = new Color(1f, 1f, 1f);
        }

        // HPゲージを更新する
        if (HP < 0)
        {
            HPGauge.transform.localScale = new Vector3(0f, HPGauge.transform.localScale.y, HPGauge.transform.localScale.z);
        }
        else
        {
            HPGauge.transform.localScale = new Vector3(HPproportion, HPGauge.transform.localScale.y, HPGauge.transform.localScale.z);
        }
    }

}
