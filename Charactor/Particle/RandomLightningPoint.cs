using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLightningPoint : MonoBehaviour {

    private float timeCounter = 0f;
    private const float ChangeTime = 0.5f;
    private Vector3 defaultPos;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        // 0.5秒ごとに元のポジションを基軸にポジション変更
        timeCounter += Time.deltaTime;
        if(timeCounter > ChangeTime)
        {
            defaultPos = this.transform.localPosition;
            timeCounter = 0f;
            Vector3 newPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            this.transform.localPosition = defaultPos + newPos;
        }
	}
}
