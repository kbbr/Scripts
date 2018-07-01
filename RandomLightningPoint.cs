using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLightningPoint : MonoBehaviour {

    private float timeCounter = 0f;
    private const float ChangeTime = 0.5f;
    private Vector3 defaultPos;

    private void Start()
    {
        defaultPos = this.transform.position;
    }

    // Update is called once per frame
    void Update () {
        // 0.5秒ごとに元のポジションを基軸にポジション変更
        timeCounter += Time.deltaTime;
        if(timeCounter > ChangeTime)
        {
            timeCounter = 0f;
            Vector3 newPos = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            this.transform.localPosition = defaultPos + newPos;
        }
	}
}
