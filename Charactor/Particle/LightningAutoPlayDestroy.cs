using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAutoPlayDestroy : MonoBehaviour {

    private Animator anim;
    [SerializeField]
    private GameObject lightning;
    [SerializeField]
    private GameObject energyLightning;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        // パーティクルは非アクティブに
        lightning.SetActive(false);
        energyLightning.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        // Animatorの状態を取得
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        // Animatorno次の遷移状態を取得
        AnimatorStateInfo nextInfo = anim.GetNextAnimatorStateInfo(0);
        
        // Animatorの状態名が詠唱終了状態なら
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.Lightning.ChantPattern02finish"))
        {
            // パーティクルは非アクティブに
            lightning.SetActive(false);
            energyLightning.SetActive(false);
        }
        // Animatorの状態名が詠唱開始状態なら
        else if(nextInfo.fullPathHash == Animator.StringToHash("Base Layer.Lightning.ChantPattern02in"))
        {
            // パーティクルをアクティブに
            lightning.SetActive(true);
            energyLightning.SetActive(true);
        }
	}
}
