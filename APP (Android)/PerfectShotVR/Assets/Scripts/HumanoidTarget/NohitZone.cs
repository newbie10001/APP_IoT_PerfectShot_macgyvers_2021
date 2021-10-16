using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 빗맞은 부분을 담당하는 처리.
public class NohitZone : MonoBehaviour, ITarget
{
    public bool GetState() => true;
    // 탄착 지점에 놓을 프리팹.
    public GameObject hitMarkPrefab;

    public void OnHit(RaycastHit hit)
    {
        GameObject hithole = Instantiate(hitMarkPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
        hithole.transform.parent = this.transform;
    }
}
