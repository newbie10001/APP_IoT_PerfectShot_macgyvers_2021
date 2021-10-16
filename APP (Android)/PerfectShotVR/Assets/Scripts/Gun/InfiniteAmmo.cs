using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 총을 무한 대로 쓸 수 있게 하는 스크립트
public class InfiniteAmmo : MonoBehaviour
{
    void Start()
    {
        GetComponent<Gun>().Reload(-1);
    }
}
