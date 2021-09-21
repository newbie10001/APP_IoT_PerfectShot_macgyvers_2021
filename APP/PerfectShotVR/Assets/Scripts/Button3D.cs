using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button3D: MonoBehaviour, ITarget
{
    // ������ �۵��� ��ư�� ������Ʈ�� �Ҵ�
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
    }

    public bool GetState() => true;

    public void OnHit(RaycastHit hit)
    {
        if (_button != null) _button.onClick.Invoke();
    }
}