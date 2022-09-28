using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// オブジェクトと衝突した時に実行される
    /// </summary>
    /// <param name="other">衝突したオブジェクト</param>
    void OnCollisionEnter(Collision other)
    {
        // 衝突したのが弾のとき
        if (other.gameObject.tag == "Bullet"){
            Destroy(this.gameObject);
        }
    }
}
