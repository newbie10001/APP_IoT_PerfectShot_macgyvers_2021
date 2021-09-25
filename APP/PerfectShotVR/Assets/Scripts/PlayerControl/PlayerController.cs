using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // �÷��̾��� �ڽ��� ��
    private Gun gun;
    // �÷��̾� ���̷� ���� ��ũ��Ʈ
    private GyroRotate gyro;
    // �÷��̾��� �Է� ���θ� ����Ͽ� ����
    public bool PlayerInput { get; private set; }
    // ������ ���� ��� true, ���� ���� false�� ��.
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

    // �Է��� �����Ͽ� �ݹ� ����
    void GetInputAndFire()
    {
        PlayerInput = GetInput();
        if (PlayerInput)
        {
            // ���� ����
            if (!pressed)
            {
                Debug.Log("�߻� �õ�");
                gun.Fire();
                pressed = true;
            }
        }
        else
        {
            pressed = false;
        }
    }

    // �ึ�� ���� ��ġ, ���콺, ������� �Է��� �޴´�.
    public bool GetInput()
    {
        return (
            (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
            || (Input.GetMouseButtonDown(1) && EventSystem.current.IsPointerOverGameObject() == false)
            || (BluetoothManager.instance != null && BluetoothManager.instance.input)
            );
    }

    // �÷��̾ ������ ������ �缳��
    public void SetRotationFront()
    {
        //this.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        transform.eulerAngles = Vector3.zero;
    }
    
    public void SetGyroEnabled(bool value) => gyro.enabled = value;
}