using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroRotate : MonoBehaviour
{
    void Start()
    {
        Input.gyro.enabled = true;
        StartCoroutine(SetZAxisAngle());
    }

    void Update()
    {
        transform.Rotate(-Input.gyro.rotationRate.x, -Input.gyro.rotationRate.y, Input.gyro.rotationRate.z);
    }

    IEnumerator SetZAxisAngle()
    {
        float z_angle;
        while (true)
        {
            z_angle = GetZAxisAngle();
            if (Mathf.Abs(transform.eulerAngles.z - z_angle) > 5f)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z_angle);
            }
            yield return new WaitForSeconds(1);
        }
    }

    // 중력이 향하는 기준점인 e_y 벡터
    // 가속도 센서의 벡터와 e_y가 이루는 각도가 z방향 회전값이다.
    readonly Vector3 e_y = new Vector3(0, -1, 0);
    float GetZAxisAngle()
    {
        Vector3 angleAcceler = Input.acceleration;
        return -Vector2.SignedAngle(e_y, new Vector2(angleAcceler.x, angleAcceler.y));
    }
}
