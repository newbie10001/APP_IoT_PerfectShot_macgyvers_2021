using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Gun gun;
    // ������ ���� ��� true, ���� ���� false�� ��.
    private bool pressed;

    void Update()
    {
        GetInputAndFire();
    }

    // �Է��� �����Ͽ� �ݹ� ����
    void GetInputAndFire()
    {
        if (GetInput())
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

    // ���� ��ġ, ���콺, ������� �Է��� �޴´�.
    bool GetInput()
    {
        return (
            (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
            || (Input.GetMouseButtonDown(1) && EventSystem.current.IsPointerOverGameObject() == false) );
    }

    // �÷��̾ ������ ������ �缳��
    public void SetRotationFront()
    {
        //this.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        transform.eulerAngles = Vector3.zero;
    }
}
