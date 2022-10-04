using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    RectTransform myRectTfm;

    void Awake()
    {
        myRectTfm = GetComponent<RectTransform>();
    }
    void Update()
    {
        myRectTfm.LookAt(Camera.main.transform);
    }
}
