using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ÿ���� �´� ������ ����ϴ� ��ũ��Ʈ.
// �̷��� �ϴ� ������... �ݶ��̴��� ���� ��� �ֱ� ����.
// �׸��� �ִϸ��̼��� Ÿ���� �ٱ� �κ��� ���� �ֱ� �����̴�.
public class HumanoidTargetHit : MonoBehaviour, ITarget
{
    // �ΰ��� Ÿ�� Ŭ������ ������. �浹�� ���⼭ �ð� ������ ó���� �װ���
    public HumanoidTargetManager targetManager;
    
    public bool GetState() => targetManager.IsSet;

    public void OnHit(RaycastHit hit)
    {
        Vector3 position = hit.transform.position;
        Debug.Log($"hit at : ({position.x}, {position.y}, {position.z})");
        // ��� ����
        Handheld.Vibrate();
        targetManager.OnHit(hit);
    }
}
