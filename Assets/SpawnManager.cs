using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemyPrefab;
    public float spawnIntarval;  // レベルアップで短く
    public float notSpawnDistance; // レベルアップで短く
    private float spawnTimer = 0f;
    [SerializeField] private float spawnMinPosX;
    [SerializeField] private float spawnMaxPosX;
    [SerializeField] private float spawnMinPosZ;
    [SerializeField] private float spawnMaxPosZ;
    private float spawnPosY = 0.5f;

    void Start()
    {
    }

    void Update()
    {
        // タイマー計測
        spawnTimer += Time.deltaTime;
        // 生成時間が経過したら
        if(spawnTimer > spawnIntarval) {
            // 敵を生成する
            spawnEnemy();
            // タイマー初期化
            spawnTimer = 0f;
        }
    }

    /// <summary>
    /// 敵を生成する
    /// </summary>
    void spawnEnemy()
    {
        // プレイヤー位置情報の取得
        Transform playerTrans = player.GetComponent<Transform>();

        // 生成する座標を取得
        Vector3 spawnPos = GetSpawnPosition(playerTrans);
        // 生成時の向きを取得
        Quaternion spawnRot = GetSpawnRotation(spawnPos, playerTrans);

        // 敵を生成する
        Instantiate(enemyPrefab, spawnPos, spawnRot);
    }

    /// <summary>
    /// 敵を生成するランダムな座標を取得する
    /// </summary>
    /// <param name="playerTrans">プレイヤーのTransform情報</param>
    /// <returns>敵を生成する座標</returns>
    private Vector3 GetSpawnPosition(Transform playerTrans)
    {
        Vector3 spawnPos = Vector3.zero;
        float spawnPosX;
        float spawnPosZ;

        // プレイヤーとの距離が指定の距離以上になるまでランダムな位置を生成
        do {
            spawnPosX = Random.Range(spawnMinPosX, spawnMaxPosX);
            spawnPosZ = Random.Range(spawnMinPosZ, spawnMaxPosZ);

            spawnPos.Set(spawnPosX, spawnPosY, spawnPosZ);
        }
        while(notSpawnDistance > Vector3.Distance(spawnPos, playerTrans.position));

        return spawnPos;
    }

    /// <summary>
    /// 敵を生成する時の向きを取得する
    /// </summary>
    /// <param name="spawnPos">敵を生成する座標</param>
    /// <param name="playerTrans">プレイヤーのTransform情報</param>
    /// <returns>生成する敵の向き</returns>
    private Quaternion GetSpawnRotation(Vector3 spawnPos, Transform playerTrans)
    {
        return Quaternion.LookRotation(playerTrans.position - spawnPos);
    }
}
