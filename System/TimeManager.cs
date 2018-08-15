using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    private float slowTimeScale = 0.2f;
    private float slowTime = 0.5f;
    private float elapsedTime = 0f;
    private bool isSlowDown = false;
    //private bool nowSlowDown = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // スロー中のフラグがONなら
        // slowTimeの間、timeScaleをtimeScale(0.2)にする
        if (isSlowDown)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if(elapsedTime > slowTime)
            {
                setNormalTimeScale();
            }
        }
	}

    // スロー開始
    public void slowDown()
    {
        if (!isSlowDown)
        {
            elapsedTime = 0f;
            Time.timeScale = slowTimeScale;
            isSlowDown = true;
        }
    }

    // スロー解除
    public void setNormalTimeScale()
    {
        Time.timeScale = 1f;
        isSlowDown = false;
        StartCoroutine("delaySetSlowDown");
    }

    // スローかどうかのフラグを返す
    public bool returnEndSlowDown()
    {
        return isSlowDown;
    }

    // 0.5秒処理待ちするデリゲート
    private IEnumerator delaySetSlowDown()
    {
        yield return new WaitForSeconds(0.5f);
        isSlowDown = false;
    }

}
