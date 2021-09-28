using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button3D: MonoBehaviour, ITarget
{
    // ������ �۵��� ��ư�� ������Ʈ�� �Ҵ�
    private Button _button;
    // ���� ȿ���� �߻��ϰ� ���ư� ��ġ.
    private Vector3 originPos;

    private void Start()
    {
        _button = GetComponent<Button>();
        originPos = transform.localPosition;
    }

    public bool GetState() => true;

    public void OnHit(RaycastHit hit)
    {
        if (_button != null) _button.onClick.Invoke();
        StartCoroutine(Shake(0.03f, 0.5f));
    }

    /// <summary>
    /// ������Ʈ�� ���� �Լ�
    /// </summary>
    /// <param name="_amount">���� ����</param>
    /// <param name="_duration">���� �ð�</param>
    /// <returns></returns>
    public IEnumerator Shake(float _amount, float _duration)
    {
        float timer = 0;
        while(timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitSphere * _amount + originPos;
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;
    }
}