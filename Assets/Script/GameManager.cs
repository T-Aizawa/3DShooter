using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] Canvas gameStartUI;
    [SerializeField] Canvas gameOverUI;
    [SerializeField] Canvas gameUI;

    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;
    int oldScore = 0;

    [SerializeField] Vector3 playerIniPos = new Vector3(0f, 0.5f, 0f);
    
    void Start()
    {
        // プレイヤーを非アクティブ状態にし、敵の生成を停止
        player.gameObject.SetActive(false);
        spawnManager.gameObject.SetActive(false);

        // ゲーム中UIを非表示
        gameUI.gameObject.SetActive(false);

        // タイトル画面を表示
        gameStartUI.gameObject.SetActive(true);
    }

    private void Update()
    {
        // スコアが更新されたら
        if (score != oldScore) {
            scoreText.text = "SCORE " + score;
            oldScore = score;
        }
    }

    /// <summary>
    /// ゲームスタート処理
    /// </summary>
    public void GameStart()
    {
        // スコアの初期化
        score = 0;

        // タイトル画面とゲームオーバー画面を非アクティブ状態に
        gameStartUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);

        // ゲーム中UIをアクティブ状態に
        gameUI.gameObject.SetActive(true);

        // プレイヤーを開始地点に戻しアクティブ状態に
        player.transform.position = playerIniPos;
        player.transform.rotation = Quaternion.identity;
        player.gameObject.SetActive(true);

        // 敵の生成を開始
        spawnManager.gameObject.SetActive(true);
    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    public void GameOver()
    {
        // ゲームオーバー画面をアクティブ状態に
        gameOverUI.gameObject.SetActive(true);

        // 敵の生成を停止
        spawnManager.gameObject.SetActive(false);
        // 残っている敵を消去する
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy")){
            Destroy(enemy.gameObject);
        }
    }

    /// <summary>
    /// スコアを加算する
    /// </summary>
    /// <param name="addPoint">加算するスコア</param>
    public void AddScore(int addPoint)
    {
        score += addPoint;
    }
}
