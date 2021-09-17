using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ī�޶� �����ϴ� ��ũ��Ʈ
public class SettingCamera : MonoBehaviour
{
    void Start()
    {
        ApplyClick();
    }

    private void Update()
    {

    }

    // ũ��ũ �ϳ� �� �������� �� �� (ī�޶��� ���� ����� x : 4, y : 8, fov : 21.78679 �� ��)
    private const float VALUE_PER_CLICK = -0.0015f;
    public Vector2 ConvertClickToLensShift()
    {
        Vector2 _click = GameManager.instance.Click;
        return _click * VALUE_PER_CLICK;
    }

    // ũ��ũ ���� �����ϴ� �޼���
    public void ApplyClick()
    {
        Camera.main.lensShift = GameManager.instance.InitialCameraShift + ConvertClickToLensShift();
    }
}
