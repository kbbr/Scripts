using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    private float timeScale = 0.2f;
    private float slowTime = 0.5f;
    private float elapsedTime = 0f;
    private bool isSlowDown = false;
    //private bool nowSlowDown = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isSlowDown)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if(elapsedTime > slowTime)
            {
                setNormalTimeScale();
            }
        }
	}

    public void slowDown()
    {
        if (!isSlowDown)
        {
            elapsedTime = 0f;
            Time.timeScale = timeScale;
            isSlowDown = true;
        }
    }

    public void setNormalTimeScale()
    {
        Time.timeScale = 1f;
        isSlowDown = false;
        StartCoroutine("delaySetSlowDown");
    }

    public bool returnEndSlowDown()
    {
        return isSlowDown;
    }

    private IEnumerator delaySetSlowDown()
    {
        yield return new WaitForSeconds(0.5f);
        isSlowDown = false;
    }

}
