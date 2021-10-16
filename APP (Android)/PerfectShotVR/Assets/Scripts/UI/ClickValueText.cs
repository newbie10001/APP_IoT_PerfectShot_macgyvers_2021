using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 크리크 수정 UI에서 버튼을 눌렀을 떄 호출할 메서드들.
// ValueText에 이 스크립트를 컴포넌트로 할당한다.
public class ClickValueText : MonoBehaviour
{
    // 자기자신을 할당.
    public Text ValueText;
    // 좌우크리크의 값을 1 증가
    public void AddHorizontal()
    {
        int value = int.Parse(ValueText.text) + 1;
        value = Mathf.Clamp(value, -17, 17);
        ValueText.text = value.ToString();
    }
    // 좌우크리크의 값을 1 감소
    public void SubHorizontal()
    {
        int value = int.Parse(ValueText.text) - 1;
        value = Mathf.Clamp(value, -17, 17);
        ValueText.text = value.ToString();
    }

    // 상하크리크의 값을 1 증가
    public void AddVertical()
    {
        int value = int.Parse(ValueText.text) + 1;
        value = Mathf.Clamp(value, -20, 20);
        ValueText.text = value.ToString();
    }

    // 상하크리크의 값을 1 감소
    public void SubVertical()
    {
        int value = int.Parse(ValueText.text) - 1;
        value = Mathf.Clamp(value, -20, 20);
        ValueText.text = value.ToString();
    }
}
