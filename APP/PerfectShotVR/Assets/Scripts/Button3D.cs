using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button3D: MonoBehaviour, ITarget
{
    // 실제로 작동될 버튼을 컴포넌트로 할당
    private Button _button;
    // 진동 효과가 발생하고 돌아갈 위치.
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
    /// 오브젝트를 흔드는 함수
    /// </summary>
    /// <param name="_amount">진동 세기</param>
    /// <param name="_duration">진동 시간</param>
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