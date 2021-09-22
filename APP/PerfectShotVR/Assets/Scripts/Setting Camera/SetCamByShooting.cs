using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ���� ���� ī�޶� �ʱ�ȭ�ϴ� ��ũ��Ʈ
public class SetCamByShooting : MonoBehaviour, IMuzzle
{
    // �÷��̾ �ܳ��� Ÿ��
    public GameObject Target;

    private void Update()
    {

    }

    // �߻� => ī�޶� �����ϰ� ����ȭ������ ��.
    public void Fire()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(Target.transform.position);
        // ȭ���� ���� ũ��
        float xScreenHalfSize = Screen.width * 0.5f;
        float yScreenHalfSize = Screen.height * 0.5f;

        // �Űܾ� �� x, y ��(pixels).
        float shiftX = screenPos.x - xScreenHalfSize;
        float shiftY = screenPos.y - yScreenHalfSize;
        // ī�޶� �������Ʈ ���밪 (ratio)
        float lensShiftX = (shiftX / xScreenHalfSize) * (-0.5f);
        float lensShiftY = (shiftY / yScreenHalfSize) * (-0.5f);
        // ī�޶� �������Ʈ ����
        Debug.Log($"�������Ʈ �̵� : {lensShiftX}, {lensShiftY}");
        GameManager.instance.InitialCameraShift = new Vector2(lensShiftX, lensShiftY);
        SceneManager.LoadScene("MainScene");
    }
}
