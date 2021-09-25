using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // 플레이어의 자식인 총
    private Gun gun;
    // 플레이어 자이로 무브 스크립트
    private GyroRotate gyro;
    // 플레이어의 입력 여부를 계속하여 갱신
    public bool PlayerInput { get; private set; }
    // 누르고 있을 경우 true, 떼는 순간 false가 됨.
    private bool pressed;

    private void Awake()
    {
        gun = GetComponentInChildren<Gun>();
        gyro = GetComponent<GyroRotate>();
    }

    void Update()
    {
        GetInputAndFire();
    }

    // 입력을 감지하여 격발 실행
    void GetInputAndFire()
    {
        PlayerInput = GetInput();
        if (PlayerInput)
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

    // 행마다 각각 터치, 마우스, 블루투스 입력을 받는다.
    public bool GetInput()
    {
        return (
            (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
            || (Input.GetMouseButtonDown(1) && EventSystem.current.IsPointerOverGameObject() == false)
            || (BluetoothManager.instance != null && BluetoothManager.instance.input)
            );
    }

    // 플레이어가 정면을 보도록 재설정
    public void SetRotationFront()
    {
        //this.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        transform.eulerAngles = Vector3.zero;
    }
    
    public void SetGyroEnabled(bool value) => gyro.enabled = value;
}