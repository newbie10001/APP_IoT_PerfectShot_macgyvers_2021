using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroRotate : MonoBehaviour
{
    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        // 추후 z축 회전에 대해서는 오차를 수정할 수 있도록 조정하는 방법을 고안
        transform.Rotate(-Input.gyro.rotationRate.x, -Input.gyro.rotationRate.y, 0);
    }
}
