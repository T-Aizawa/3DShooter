using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // 敵を生成中か
    bool isSpawning;

    [SerializeField] GameObject player;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] RatioData[] ratioDataArray;

    [SerializeField] float spawnInterval;  // レベルアップで短く
    [SerializeField] float notSpawnDistance; // レベルアップで短く
    [SerializeField] float spawnMinPosX;
    [SerializeField] float spawnMaxPosX;
    [SerializeField] float spawnMinPosZ;
    [SerializeField] float spawnMaxPosZ;
    float spawnPosY = 0.5f;
    int spawnLevel = 1;
    int oldSpawnLevel = 0;
    float totalRatioWeight;
    GameObject spawnEnemy;
    RatioData spawnRatio;

    void OnEnable()
    {
        // アクティブ時に生成処理を再開
        isSpawning = false;
        oldSpawnLevel = 0;
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

        // 生成する敵の種類を取得
        spawnEnemy = GetSpawnEnemy();

        // プレイヤー位置情報の取得
        Transform playerTrans = player.GetComponent<Transform>();
        // 生成する座標を取得
        Vector3 spawnPos = GetSpawnPosition(playerTrans);
        // 生成時の向きを取得
        Quaternion spawnRot = GetSpawnRotation(spawnPos, playerTrans);

        // 敵を生成する
        Instantiate(spawnEnemy, spawnPos, spawnRot);
        // 生成再開
        isSpawning = false;
    }

    /// <summary>
    /// 生成する敵を抽選する
    /// </summary>
    /// <returns>敵のプレハブ</returns>
    GameObject GetSpawnEnemy()
    {
        // レベルが更新されている時
        if (spawnLevel != oldSpawnLevel){
            totalRatioWeight = 0;
            // 抽選テーブルを取得
            spawnRatio = ratioDataArray[spawnLevel - 1];
            // 各出現率の和を取得
            for (var i = 0; i < spawnRatio.Ratios.Length; i++) {
                totalRatioWeight += spawnRatio.Ratios[i];
            }
            oldSpawnLevel = spawnLevel;
        }
        var random = Random.Range(0, totalRatioWeight);
        var currentWeight = 0f;

        for (var i = 0; i < spawnRatio.Ratios.Length; i++)
        {
            // 現在要素までの重みの総和を求める
            currentWeight += spawnRatio.Ratios[i];

            // 乱数値が現在要素の範囲内かチェック
            if (random < currentWeight)
            {
                return enemyPrefabs[i];
            }
        }
        // 乱数値が重みの総和以上なら末尾要素とする
        return enemyPrefabs[spawnRatio.Ratios.Length - 1];
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

    /// <summary>
    /// スポナーにレベルを設定する
    /// </summary>
    /// <param name="level">現在のレベル</param>
    public void SetLevel(int level){
        spawnLevel = level;
    }
}