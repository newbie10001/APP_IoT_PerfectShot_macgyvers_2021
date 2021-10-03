using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라를 설정하는 스크립트
public class SettingCamera : MonoBehaviour
{
    void Start()
    {
        InitCamAndApplyClick();
    }

    private void Update()
    {

    }

    // 크리크 하나 당 움직여야 할 값 (카메라의 센서 사이즈가 x : 4, y : 8, fov : 21.78679 일 때)
    private const float VALUE_PER_CLICK = -0.0015f;
    public Vector2 ConvertClickToLensShift()
    {
        Vector2 _click = GameManager.instance.Click;
        return _click * VALUE_PER_CLICK;
    }

    // 렌즈시프트값을 초기화하고 크리크 값을 적용하는 메서드
    public void InitCamAndApplyClick()
    {
        Camera.main.lensShift = GameManager.instance.InitialCameraShift + ConvertClickToLensShift();
    }
}
