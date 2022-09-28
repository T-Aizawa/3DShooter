using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRb;
    public float bulletSpeed;
    public float lifetime;

    void Start()
    {
        bulletRb = this.GetComponent<Rigidbody>();
        // 時間経過で消滅する
        Destroy(this.gameObject, lifetime);
    }

    void Update()
    {
        // 前方に進み続ける
        bulletRb.velocity = transform.forward * bulletSpeed * Time.deltaTime;
    }

    /// <summary>
    /// オブジェクトと衝突した時に実行
    /// </summary>
    /// <param name="other">衝突したオブジェクト</param>
    void OnCollisionEnter(Collision other)
    {
        // 壁に衝突したら消滅する
        if (other.gameObject.tag == "Wall"){
            Destroy(this.gameObject);
        }
    }
}
