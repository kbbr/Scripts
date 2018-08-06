using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charactor : MonoBehaviour {

    /// <summary>
    /// Charactor HP
    /// </summary>
    public float CharactorHP { get; set; }
    /// <summary>
    /// Charactor Position
    /// </summary>
    public Vector3 CnaractorPosition { get; set; }

    public virtual void playerMove() { }

}
