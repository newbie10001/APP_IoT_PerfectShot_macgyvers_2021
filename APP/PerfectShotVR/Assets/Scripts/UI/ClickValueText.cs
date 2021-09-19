using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ũ��ũ ���� UI���� ��ư�� ������ �� ȣ���� �޼����.
// ValueText�� �� ��ũ��Ʈ�� ������Ʈ�� �Ҵ��Ѵ�.
public class ClickValueText : MonoBehaviour
{
    // �ڱ��ڽ��� �Ҵ�.
    public Text ValueText;
    // �¿�ũ��ũ�� ���� 1 ����
    public void AddHorizontal()
    {
        int value = int.Parse(ValueText.text) + 1;
        value = Mathf.Clamp(value, -17, 17);
        ValueText.text = value.ToString();
    }
    // �¿�ũ��ũ�� ���� 1 ����
    public void SubHorizontal()
    {
        int value = int.Parse(ValueText.text) - 1;
        value = Mathf.Clamp(value, -17, 17);
        ValueText.text = value.ToString();
    }

    // ����ũ��ũ�� ���� 1 ����
    public void AddVertical()
    {
        int value = int.Parse(ValueText.text) + 1;
        value = Mathf.Clamp(value, -20, 20);
        ValueText.text = value.ToString();
    }

    // ����ũ��ũ�� ���� 1 ����
    public void SubVertical()
    {
        int value = int.Parse(ValueText.text) - 1;
        value = Mathf.Clamp(value, -20, 20);
        ValueText.text = value.ToString();
    }
}
