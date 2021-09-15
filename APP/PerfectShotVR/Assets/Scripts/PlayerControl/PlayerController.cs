using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Gun gun;
    // 누르고 있을 경우 true, 떼는 순간 false가 됨.
    private bool pressed;

    void Update()
    {
        GetInputAndFire();
    }

    // 입력을 감지하여 격발 실행
    void GetInputAndFire()
    {
        if (GetInput())
        {
            // 연사 방지
            if (!pressed)
            {
                Debug.Log("발사 시도");
                gun.Fire();
                pressed = true;
            }
        }
        else
        {
            pressed = false;
        }
    }

    // 각각 터치, 마우스, 블루투스 입력을 받는다.
    bool GetInput()
    {
        return (
            (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
            || (Input.GetMouseButtonDown(1) && EventSystem.current.IsPointerOverGameObject() == false) );
    }

    // 플레이어가 정면을 보도록 재설정
    public void SetRotationFront()
    {
        //this.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        transform.eulerAngles = Vector3.zero;
    }
}
