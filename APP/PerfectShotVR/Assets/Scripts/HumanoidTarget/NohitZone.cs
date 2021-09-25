using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �κ��� ����ϴ� ó��.
public class NohitZone : MonoBehaviour, ITarget
{
    public bool GetState() => true;
    // ź�� ������ ���� ������.
    public GameObject hitMarkPrefab;

    public void OnHit(RaycastHit hit)
    {
        GameObject hithole = Instantiate(hitMarkPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
        hithole.transform.parent = this.transform;
    }
}
