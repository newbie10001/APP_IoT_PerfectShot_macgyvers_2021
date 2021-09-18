using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Save ��ư�� ������ ������ �� �� ���� ȭ������ ����.
public class SaveAndGoMain : MonoBehaviour
{
    public void SaveAndExit()
    {
        Vector2 _clickShift = FindObjectOfType<SettingCamera>().ConvertClickToLensShift();
        // ũ��ũ ������ ī�޶� ������ ���ԵǹǷ� �׸�ŭ ���ܵǾ�� ��.
        GameManager.instance.InitialCameraShift = Camera.main.lensShift - _clickShift;
        SceneManager.LoadScene("MainScene");
    }
}
