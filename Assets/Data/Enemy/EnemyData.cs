using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "MyScriptable/Create EnemyData")]
public class EnemyData : ScriptableObject
{
    // status
    [SerializeField] string enemyName;
    [SerializeField] int maxHP;
    [SerializeField] float speed;
    [SerializeField] float radSpeed;
    [SerializeField] float shootInterval;
    [SerializeField] int score;

    // control
    [SerializeField] bool enableMoving;
    [SerializeField] bool enableShooting;
    [SerializeField] bool enableTurning;
    [SerializeField] bool faceToPlayer;

    // access
    public int Hp { get {return maxHP;} }
    public float Speed { get {return speed;} }
    public float RadSpeed { get {return radSpeed;} }
    public float ShootInterval { get {return shootInterval;} }
    public int Score { get {return score;} }
    public bool EnableMoving { get {return enableMoving;} }
    public bool EnableShooting { get {return enableShooting;} }
    public bool EnableTurning { get {return enableTurning;} }
    public bool FaceToPlayer { get {return faceToPlayer;} }
}