using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 총 자동발사 컴포넌트
public class AutoShot : MonoBehaviour
{
    Muzzle muzzle;
    void Start()
    {
        muzzle = GetComponentInChildren<Muzzle>();
        StartCoroutine(AutoFire());
    }

    // 0.5초마다 총 발사
    IEnumerator AutoFire()
    {
        while (true)
        {
            muzzle.Fire();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
