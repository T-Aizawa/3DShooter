using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    public float enemySpeed;
    public AudioClip soundExplosion;

    private GameObject player;
    private Transform playerTrans;

    void Start()
    {
        // プレイヤーのオブジェクトを取得 → Inspector上でできるならそっちの方が軽い
        player = GameObject.Find("Player");

        enemyRb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // プレイヤー位置情報の取得
        playerTrans = player.GetComponent<Transform>();
        // プレイヤーの方を向く
        this.transform.LookAt(playerTrans);
        // 前方に進み続ける
        enemyRb.velocity = transform.forward * enemySpeed * Time.deltaTime;
    }

    /// <summary>
    /// オブジェクトと衝突した時に実行される
    /// </summary>
    /// <param name="other">衝突したオブジェクト</param>
    void OnCollisionEnter(Collision other)
    {
        // 衝突したのが弾のとき
        if (other.gameObject.tag == "Bullet"){

            // その場に爆発音を鳴らすオブジェクトを一時的に生成
            AudioSource.PlayClipAtPoint(soundExplosion, this.transform.position);

            Destroy(this.gameObject);
        }
    }
}
