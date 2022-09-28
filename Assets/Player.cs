using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // プレイヤー制御に関する
    public float playerSpeed;
    private Rigidbody playerRb;
    private Vector3 moving;
    private Plane plane;
    private float mouseDis = 0f;
    private Quaternion lookRot;
    public float playerRadSpeed;
    // 発射制御に関する
    [SerializeField] private GameObject bullet;
    public float timeBetweenShoot;
    private float timerShoot;

    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // タイマーの時間を追加
        timerShoot += Time.deltaTime;

        // 方向制御
        RotateControll();
        Rotate();

        // 移動制御
        MoveControll();
        Move();  

        // マウスの左クリック押下で弾を発射する
        if (Input.GetMouseButton(0)) {
            Shoot();    
        }
    }

    /// <summary>
    /// プレイヤーの向きを算出する
    /// </summary>
    void RotateControll()
    {
        // カメラからマウスの位置へRayを生成
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // プレイヤーの高さにPlaneを更新
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);

        // PlaneとRayが交差する場合
        if (plane.Raycast(ray, out mouseDis)){
            // 距離から交点を算出
            Vector3 lookPoint = ray.GetPoint(mouseDis);
            // 交点までの回転を取得
            lookRot = Quaternion.LookRotation(lookPoint - transform.localPosition);
        }
    }

    /// <summary>
    /// プレイヤーの向きを変更する
    /// </summary>
    void Rotate()
    {
        // プレイヤーの向きをマウスの向きへ指定の角速度で更新
        transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRot, Time.deltaTime * playerRadSpeed);
    }

    /// <summary>
    /// プレイヤーが移動する向きを算出する
    /// </summary>
    void MoveControll()
    {
        // プレイヤーの向きの単位ベクトルを取得
        Vector3 playerForward =  Vector3.Scale(transform.forward, new Vector3(1.0f, 0f, 1.0f));
        // 入力値をベクトルとして取得
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        // 入力値とプレイヤーの向きから移動方向の単位ベクトルを算出
        moving = playerForward * inputVector.z + transform.right * inputVector.x;
        moving.Normalize();

        // 移動スピードを掛けて移動ベクトルを算出
        moving = moving * playerSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 移動する
    /// </summary>
    void Move()
    {
        playerRb.velocity = moving;
    }

    /// <summary>
    /// 弾を発射する
    /// </summary>
    void Shoot()
    {
        // クールタイムが経過していたら発射する
        if (timerShoot > timeBetweenShoot) {
            Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            // タイマー初期化
            timerShoot = 0.0f;
        }
    }

}
