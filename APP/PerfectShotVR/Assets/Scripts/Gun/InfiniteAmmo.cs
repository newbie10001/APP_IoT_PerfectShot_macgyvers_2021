using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ��� �� �� �ְ� �ϴ� ��ũ��Ʈ
public class InfiniteAmmo : MonoBehaviour
{
    void Start()
    {
        GetComponent<Gun>().Reload(-1);
    }
}
