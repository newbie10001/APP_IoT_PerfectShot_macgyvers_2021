using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 카메라를 끌어서 가늠자를 맞추는 메커니즘을 구현한 스크립트
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
                // UI가 존재하는 부분인 위쪽에 터치가 입력되었는가?
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
            // 세부 조정은 영점조절로 한다.
            centerText.text = $"카메라 렌즈 시프트 : ({Camera.main.lensShift.x}, {Camera.main.lensShift.y})";
            yield return null;
        }
    }
}
