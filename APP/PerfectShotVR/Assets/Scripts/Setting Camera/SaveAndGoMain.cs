using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Save 버튼을 누르면 설정을 한 후 메인 화면으로 나감.
public class SaveAndGoMain : MonoBehaviour
{
    public void SaveAndExit()
    {
        Vector2 _clickShift = FindObjectOfType<SettingCamera>().ConvertClickToLensShift();
        // 크리크 수정이 카메라 수정에 포함되므로 그만큼 제외되어야 함.
        GameManager.instance.InitialCameraShift = Camera.main.lensShift - _clickShift;
        SceneManager.LoadScene("MainScene");
    }
}
