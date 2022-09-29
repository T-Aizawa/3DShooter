using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 playerPos;

    void Update()
    {
        // 距離を保ってPlayerに追従
        this.transform.position += player.transform.position - playerPos;
        playerPos = player.transform.position;

        float angleY = player.transform.eulerAngles.y - this.transform.eulerAngles.y;
        // Playerの位置のY軸を基準に公転する
        this.transform.RotateAround(playerPos, Vector3.up, angleY);
    }
}
