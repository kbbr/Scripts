using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPDisplay : MonoBehaviour {

    /// <summary> PlayerのHPゲージのイメージオブジェクト /// </summary>
    [SerializeField]
    private Image playerGauge;

    private Color myGreen;
    private Color myYellow;
    private Color myRed;

    const float yellowThreshhold = 0.6f;
    const float redThereshhold = 0.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerHPUpdate(float HP, float MAXHP)
    {
        // HPの最大HPに対する割合を計算
        float HPproportion = HP / MAXHP;

        // HPの割合に応じてゲージカラーを変える
        if(HPproportion == 1f)
        {
            playerGauge.color = new Color(0f, 1f, 0f);
        }
        else if(HPproportion < redThereshhold)
        {
            playerGauge.color = new Color(1f, 0f, 0f);
        }
        else if (HPproportion < yellowThreshhold)
        {
            playerGauge.color = new Color(1f, 1f, 0f);
        }
        else
        {
            playerGauge.color = new Color(1f, 1f, 1f);
        }

        // HPゲージを更新する
        if (HP < 0)
        {
            playerGauge.transform.localScale = new Vector3(0f, playerGauge.transform.localScale.y, playerGauge.transform.localScale.z);
        }
        else
        {
            playerGauge.transform.localScale = new Vector3(HPproportion, playerGauge.transform.localScale.y, playerGauge.transform.localScale.z);
        }
    }

}
