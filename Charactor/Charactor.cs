using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charactor : MonoBehaviour {

    /// <summary> Charactorの現在のHP /// </summary>
    public float HP { get; set; }
    /// <summary> CharactorのMAXHP /// </summary>
    public float MAXHP { get; set; }

    /// <summary> Charactorの現在のPosition /// </summary>
    public Vector3 CharactorPosition { get; set; }


}
