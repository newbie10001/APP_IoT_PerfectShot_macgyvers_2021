using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 계속 돌다가 시선이 마주치면 플레이어를 바라보는 오브젝트
public class KeepRotate : MonoBehaviour, IReactAtLooking
{
    void Start()
    {
        
    }

    float _speed = 60f;
    void Update()
    {
        transform.Rotate(0, _speed * Time.deltaTime, 0);
    }

    readonly float damp = 6.0f;
    Quaternion rotate;
    public void OnLook(Transform _transform)
    {
        rotate = Quaternion.LookRotation(_transform.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * damp);
    }
}
