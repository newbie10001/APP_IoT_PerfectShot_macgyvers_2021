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

    public void OnLook(Transform _transform)
    {
        transform.LookAt(_transform);
    }
}
