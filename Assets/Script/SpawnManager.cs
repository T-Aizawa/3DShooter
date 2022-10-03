using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // 敵を生成中か
    bool isSpawning;

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnInterval;  // レベルアップで短く
    [SerializeField] float notSpawnDistance; // レベルアップで短く
    [SerializeField] float spawnMinPosX;
    [SerializeField] float spawnMaxPosX;
    [SerializeField] float spawnMinPosZ;
    [SerializeField] float spawnMaxPosZ;
    float spawnPosY = 0.5f;

    void OnEnable()
    {
        // アクティブ時に生成処理を再開
        isSpawning = false;
    }
    void Update()
    {
        if (!isSpawning) {
            // 敵を生成する
            StartCoroutine(SpawnEnemy());
        }
    }

    /// <summary>
    /// 敵を生成する
    /// </summary>
    IEnumerator SpawnEnemy()
    {
        // 生成中は次の処理を止める
        isSpawning = true;

        // 指定の秒数待機する
        yield return new WaitForSeconds(spawnInterval);

        // プレイヤー位置情報の取得
        Transform playerTrans = player.GetComponent<Transform>();

        // 生成する座標を取得
        Vector3 spawnPos = GetSpawnPosition(playerTrans);
        // 生成時の向きを取得
        Quaternion spawnRot = GetSpawnRotation(spawnPos, playerTrans);

        // 敵を生成する
        Instantiate(enemyPrefab, spawnPos, spawnRot);
        // 生成再開
        isSpawning = false;
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
