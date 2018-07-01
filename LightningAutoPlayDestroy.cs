using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAutoPlayDestroy : MonoBehaviour {

    private Animator anim;
    public GameObject lightning;
    public GameObject energyLightning;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        lightning.SetActive(false);
        energyLightning.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = anim.GetNextAnimatorStateInfo(0);
        if(stateInfo.fullPathHash == Animator.StringToHash("Base Layer.Lightning.ChantPattern02finish"))
        {
            lightning.SetActive(false);
            energyLightning.SetActive(false);
        }
        else if(nextInfo.fullPathHash == Animator.StringToHash("Base Layer.Lightning.ChantPattern02in"))
        {
            lightning.SetActive(true);
            energyLightning.SetActive(true);
        }
	}
}
