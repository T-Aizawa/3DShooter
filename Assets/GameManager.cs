using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] Canvas gameStartUI;
    [SerializeField] Canvas gameOverUI;

    [SerializeField] Vector3 playerIniPos = new Vector3(0f, 0.5f, 0f);
    
    void Start()
    {
        // プレイヤーを非アクティブ状態にし、敵の生成を停止
        player.gameObject.SetActive(false);
        spawnManager.isActive = false;

        // タイトル画面を表示
        gameStartUI.gameObject.SetActive(true);
    }

    /// <summary>
    /// ゲームスタート処理
    /// </summary>
    public void GameStart()
    {
        // タイトル画面とゲームオーバー画面を非アクティブ状態に
        gameStartUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);

        // プレイヤーを開始地点に戻しアクティブ状態に
        player.transform.position = playerIniPos;
        player.transform.rotation = Quaternion.identity;
        player.gameObject.SetActive(true);

        // 敵の生成を開始
        spawnManager.isActive = true;
    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    public void GameOver()
    {
        // ゲームオーバー画面をアクティブ状態に
        gameOverUI.gameObject.SetActive(true);

        // 敵の生成を停止
        spawnManager.isActive = false;
    }
}
