using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGenerate : MonoBehaviour {

    public GameObject boostEffect;
    public GameObject magicSquare;
    GameObject moveMagicSquare;

    bool isBoost = false;
	
	// Update is called once per frame
	void Update () {
        // ブーストボタンが押されたら
        if (InputController.IsBoostButton)
        {
            isBoost = true;
            generateBoostEffect();

        }
        if(isBoost && !InputController.IsBoostButton)
        {
            isBoost = false;
            destroyBoostEffect();
        }
	}

    // Boostのエフェクト作成
    void generateBoostEffect()
    {
        moveMagicSquare = Instantiate(magicSquare, transform.position, transform.rotation);
        moveMagicSquare.transform.parent = this.transform;
        moveMagicSquare.transform.position += new Vector3(0, 0.1f, 0);

    }

    // Boostのエフェクトを削除
    void destroyBoostEffect()
    {
        ParticleSystem prtSys = moveMagicSquare.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        //prtSys.Simulate(300f, true, false);
        //prtSys.time = 290f;
        var simMain= prtSys.main;
        simMain.simulationSpeed = 1200f;
        for(int i = 0; i < moveMagicSquare.transform.GetChild(0).childCount; i++)
        {
            ParticleSystem tmp_part = moveMagicSquare.transform.GetChild(0).GetChild(i).GetComponent<ParticleSystem>();
            var tmpMain = tmp_part.main;
            tmpMain.simulationSpeed = 1200f;
        }
        Destroy(moveMagicSquare, 0.3f);
    }
}
