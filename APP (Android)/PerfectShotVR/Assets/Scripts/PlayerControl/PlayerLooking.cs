using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 레이캐스트를 쏴서 IReactAtLooking 클래스틀 가진 오브젝트에 대해 OnLook을 실행시키는 오브젝트
// 사정거리는 100유닛
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
