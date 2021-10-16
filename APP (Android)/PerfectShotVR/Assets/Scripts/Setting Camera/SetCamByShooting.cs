using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 총을 쏴서 카메라를 초기화하는 스크립트
public class SetCamByShooting : MonoBehaviour, IMuzzle
{
    // 플레이어가 겨냥할 타겟
    public GameObject Target;
    public AudioClip FireSound;

    // 발사 => 카메라를 저장하고 메인화면으로 감.
    public void Fire()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(Target.transform.position);
        // 화면의 절반 크기
        float xScreenHalfSize = Screen.width * 0.5f;
        float yScreenHalfSize = Screen.height * 0.5f;

        // 옮겨야 할 x, y 값(pixels).
        float shiftX = screenPos.x - xScreenHalfSize;
        float shiftY = screenPos.y - yScreenHalfSize;
        // 카메라 렌즈시프트 적용값 (ratio)
        float lensShiftX = (shiftX / xScreenHalfSize) * (-0.5f);
        float lensShiftY = (shiftY / yScreenHalfSize) * (-0.5f);
        // 카메라 렌즈시프트 적용
        Debug.Log($"렌즈시프트 이동 : {lensShiftX}, {lensShiftY}");
        GameManager.instance.InitialCameraShift = new Vector2(lensShiftX, lensShiftY);
        // 총 발사하는 소리 재생
        gameObject.GetComponent<AudioSource>().PlayOneShot(FireSound);
        SceneManager.LoadScene("MainScene");
    }
}
