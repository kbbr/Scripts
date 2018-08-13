using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDefenceEvents : MonoBehaviour {

    //GameObject playerObj;
    //unitychanMove unitychanMove;

    private void Awake()
    {
        //playerObj = GameObject.Find("Player");
        //unitychanMove = playerObj.GetComponent<unitychanMove>();
    }
    
    public void resetTrigger()
    {
        Animator anim = GetComponent<Animator>();
        anim.ResetTrigger("jumpDefence");
    }

    public void setIsJumpDefence()
    {
        UnitychanController unitychanMv = GetComponent<UnitychanController>();
        unitychanMv.isJumpDefence = false;
    }

    public void resetJumpTrigger()
    {
        Animator anim = GetComponent<Animator>();
        anim.ResetTrigger("jump");
    }


    public void OnCallChangeFace()
    {
        return;
    }
}
