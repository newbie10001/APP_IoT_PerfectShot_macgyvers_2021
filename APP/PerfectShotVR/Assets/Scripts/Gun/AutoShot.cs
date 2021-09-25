using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� �ڵ��߻� ������Ʈ
public class AutoShot : MonoBehaviour
{
    Muzzle muzzle;
    void Start()
    {
        muzzle = GetComponentInChildren<Muzzle>();
        StartCoroutine(AutoFire());
    }

    // 0.5�ʸ��� �� �߻�
    IEnumerator AutoFire()
    {
        while (true)
        {
            muzzle.Fire();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
