using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRb;
    public float bulletSpeed;

    void Start()
    {
        bulletRb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 前方に進み続ける
        bulletRb.velocity = transform.forward * bulletSpeed * Time.deltaTime;
    }

    /// <summary>
    /// オブジェクトと衝突した時に実行される
    /// </summary>
    /// <param name="other">衝突したオブジェクト</param>
    void OnCollisionEnter(Collision other)
    {
        // 衝突したのがプレイヤーか弾でないなら消滅する
        if (other.gameObject.tag != "Player" || other.gameObject.tag != "Bullet"){
            Destroy(this.gameObject);
        }
    }
}
