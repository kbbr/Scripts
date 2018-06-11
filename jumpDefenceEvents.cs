using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpDefenceEvents : MonoBehaviour {

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
        unitychanMove unitychanMv = GetComponent<unitychanMove>();
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
