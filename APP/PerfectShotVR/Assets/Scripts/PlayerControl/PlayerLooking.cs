using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾ ����ĳ��Ʈ�� ���� IReactAtLooking Ŭ����Ʋ ���� ������Ʈ�� ���� OnLook�� �����Ű�� ������Ʈ
// �����Ÿ��� 100����
public class PlayerLooking : MonoBehaviour
{
    IReactAtLooking lookObject;
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100))
        {
            lookObject = hit.collider.GetComponent<IReactAtLooking>();
            if (lookObject != null) lookObject.OnLook(transform);
        }
    }
}
