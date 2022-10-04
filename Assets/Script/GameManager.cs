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

    int level = 1;
    int oldLevel = 1;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] int maxLevel;
    [SerializeField] int[] thresholdScores = new int[5];

    AudioSource audioSource;
    [SerializeField] AudioClip soundLevelUp;

    [SerializeField] Vector3 playerIniPos = new Vector3(0f, 0.5f, 0f);
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        // スコアが更新されたとき
        if (score != oldScore) {
            scoreText.text = "SCORE " + score;
            oldScore = score;

            // レベルアップするか確認
            if (level < maxLevel && CheckLevelUp()) {
                // レベル表示の更新
                if (level == maxLevel) {
                    levelText.text = "LEVEL MAX";
                } else {
                    levelText.text = "LEVEL " + level;
                }
                // レベルアップ音をならす
                if(level != 1) audioSource.PlayOneShot(soundLevelUp, 0.5f);
                // プレイヤー速度の計算
                player.calcPlayerSpeed(level);
                // 敵生成制御にレベルを設定
                spawnManager.SetLevel(level);

                oldLevel = level;
            }
        }
    }

    /// <summary>
    /// レベルアップを判定する
    /// </summary>
    private bool CheckLevelUp()
    {
        // スコアに対してのレベルを取得
        for (var i = 0; i < thresholdScores.Length; i++) {
            if (score >= thresholdScores[i]) {
                level = i + 1;
            } else {
                break;
            }
        }
        // レベルアップしているかを返す
        return level != oldLevel;
    }

    /// <summary>
    /// ゲームスタート処理
    /// </summary>
    public void GameStart()
    {
        // スコアなどの初期化
        score = 0;
        level = 1;

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