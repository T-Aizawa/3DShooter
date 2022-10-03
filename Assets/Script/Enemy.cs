using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody enemyRb;
    [SerializeField] EnemyData data;
    [SerializeField] int enemyHp;
    [SerializeField] float enemySpeed;

    Quaternion lookRot;
    bool isShooting = false;
    [SerializeField] AudioClip soundExplosion;

    GameObject player;
    Transform playerTrans;

    [SerializeField] GameObject bullet;

    GameObject gManager;

    void Start()
    {
        // オブジェクトを取得
        player = GameObject.Find("Player");
        gManager = GameObject.Find("GameManager");

        enemyRb = this.GetComponent<Rigidbody>();
        enemyHp = data.Hp;
        enemySpeed = data.Speed;
    }

    void Update()
    {
        if (data.EnableTurning) {
            RotateControl();
            Rotate();
        }

        if (data.EnableShooting && !isShooting) {
            StartCoroutine(Shoot());
        }

        if (data.EnableMoving) {
            Move();
        }
    }

    /// <summary>
    /// オブジェクトと衝突した時に実行される
    /// </summary>
    /// <param name="other">衝突したオブジェクト</param>
    void OnCollisionEnter(Collision other)
    {
        // 衝突したのが弾のとき
        if (other.gameObject.tag == "Bullet"){
            // hpを減らす
            enemyHp--;
            if (enemyHp <= 0) {
                // その場に爆発音を鳴らすオブジェクトを一時的に生成
                AudioSource.PlayClipAtPoint(soundExplosion, this.transform.position);
                gManager.GetComponent<GameManager>().AddScore(data.Score);
                Destroy(this.gameObject);
            } else {
                // HPが残ってる時は弾を消す
                Destroy(other.gameObject);
            }
        }
    }

    /// <summary>
    /// 向く方向を算出する
    /// </summary>
    void RotateControl()
    {
        // プレイヤー位置情報の取得
        playerTrans = player.GetComponent<Transform>();
        
        if (data.FaceToPlayer) {
            // プレイヤーの方向
            lookRot = Quaternion.LookRotation(playerTrans.position - this.transform.position);
        } else {
            // プレイヤーから遠ざかる方向
            lookRot = Quaternion.LookRotation(this.transform.position - playerTrans.position);
        }        
    }

    void Rotate()
    {
        // 指定地点の方向へ角速度で更新
        transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRot, data.RadSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 弾を撃つ
    /// </summary>
    IEnumerator Shoot()
    {
        // 射撃クールタイム中
        isShooting = true;

        // 弾を指定していない場合エラーログをはいてクールタイムのまま処理を抜ける
        if (bullet == null) {
            Debug.Log("撃つ弾を指定して下さい");
            yield break;
        }
        // 弾を生成
        Instantiate(bullet, transform.position + transform.forward, transform.rotation);
        yield return new WaitForSeconds(data.ShootInterval);
        // クールタイム解除
        isShooting = false;
    }

    /// <summary>
    /// 移動する
    /// </summary>
    void Move()
    {
        // 前方に進み続ける
        enemyRb.velocity = transform.forward * enemySpeed * Time.deltaTime;
    }
}
