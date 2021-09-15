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
        // ���� z�� ȸ���� ���ؼ��� ������ ������ �� �ֵ��� �����ϴ� ����� ���
        transform.Rotate(-Input.gyro.rotationRate.x, -Input.gyro.rotationRate.y, 0);
    }
}
