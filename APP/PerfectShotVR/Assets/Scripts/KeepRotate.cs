using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ���ٰ� �ü��� ����ġ�� �÷��̾ �ٶ󺸴� ������Ʈ
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
