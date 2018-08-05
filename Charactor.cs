using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charactor : MonoBehaviour {

    /// <summary>
    /// Player HP
    /// </summary>
    public float _playerHP = 0f;
    /// <summary>
    /// Player Position
    /// </summary>
    public Vector3 _playerPos = Vector3.zero;

    public virtual void playerMove() { }

}
