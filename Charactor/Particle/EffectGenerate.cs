using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGenerate : MonoBehaviour {

    public GameObject boostEffect;
    public GameObject magicSquare;
    GameObject moveMagicSquare;

    bool isBoost = false;
    //bool endBoost = false;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Boost"))
        {
            isBoost = true;
            //endBoost = false;
            generateBoostEffect();

        }
        if(isBoost && !Input.GetButton("Boost")){
            //endBoost = true;
            isBoost = false;
            destroyBoostEffect();
        }
	}

    void generateBoostEffect()
    {
        /*
        Vector3 pos = this.transform.position - new Vector3(0, 0f, 0);
        GameObject effectInstance = Instantiate(boostEffect, pos, transform.rotation);
        Destroy(effectInstance, 1f);
        */
        //Vector3 pos = this.transform.position - new Vector3(0, 0.1f, 0);
        moveMagicSquare = Instantiate(magicSquare, transform.position, transform.rotation);
        moveMagicSquare.transform.parent = this.transform;
        moveMagicSquare.transform.position += new Vector3(0, 0.1f, 0);

    }
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
