using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RatioData", menuName = "MyScriptable/RatioData")]
public class RatioData : ScriptableObject
{
    [SerializeField] float[] ratios = new float[5];

    public float[] Ratios { get{return ratios;} }
}
