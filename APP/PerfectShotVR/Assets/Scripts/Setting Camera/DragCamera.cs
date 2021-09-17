using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// ī�޶� ��� �����ڸ� ���ߴ� ��Ŀ������ ������ ��ũ��Ʈ
public class DragCamera : MonoBehaviour
{
    float drag_speed = -0.05f;
    public Text centerText;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(mouseDrag());
        }
    }

    private IEnumerator mouseDrag()
    {
        while(Input.GetMouseButton(0))
        {
            float dx;
            float dy;
            if(Input.touchCount > 0)
            {
                // UI�� �����ϴ� �κ��� ���ʿ� ��ġ�� �ԷµǾ��°�?
                if (Input.touches[0].position.y > Screen.height - 230)
                {
                    dx = 0;
                    dy = 0;
                }
                else
                {
                    dx = Input.touches[0].deltaPosition.x * Mathf.Abs(drag_speed) * 0.1f;
                    dy = Input.touches[0].deltaPosition.y * Mathf.Abs(drag_speed) * 0.1f;
                }
            }
            else
            {
                dx = Input.GetAxis("Mouse X");
                dy = Input.GetAxis("Mouse Y");
            }
            Camera.main.lensShift += new Vector2(dx * drag_speed, dy * drag_speed);
            // ���� ������ ���������� �Ѵ�.
            centerText.text = $"ī�޶� ���� ����Ʈ : ({Camera.main.lensShift.x}, {Camera.main.lensShift.y})";
            yield return null;
        }
    }
}
