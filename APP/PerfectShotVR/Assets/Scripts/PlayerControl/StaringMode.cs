using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���ø�带 �����ϴ� ��ũ��Ʈ. ���Ƽ� ������Ʈ�� ������Ʈ�� �Ҵ�ȴ�.
public class StaringMode : MonoBehaviour
{
    // �� ���ø���� �ð��� ������ �� �ִ� ����� ����
    public float TriggerTime { get { return PlayerPrefs.GetFloat("StaringTime") == default(float) ? 1.5f : PlayerPrefs.GetFloat("StaringTime"); } }
    float elapsed;
    Gun gun;
    // ���ø�� ���� UI
    public Image CircleSlider;

    void Start()
    {
        gun = FindObjectOfType<Gun>();
        elapsed = 0;
        CircleSlider.fillAmount = 0;
        this.enabled = GameManager.instance.StaringMode;
    }

    private void OnEnable()
    {
        elapsed = 0;
        CircleSlider.fillAmount = 0;
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 600))
        {
            if(hit.collider.TryGetComponent<ITarget>(out ITarget _target) && _target.GetState())
            {
                CircleSlider.fillAmount = elapsed / TriggerTime;
                elapsed += Time.deltaTime;
                if (elapsed > TriggerTime)
                {
                    gun.Fire();
                    elapsed = 0;
                }
                return;
            }
        }
        elapsed = 0;
        CircleSlider.fillAmount = 0;
    }

    public void ToggleStaringMode(Toggle _toggle)
    {
        if (!_toggle.isOn) CircleSlider.fillAmount = 0;
        this.enabled = _toggle.isOn;
        GameManager.instance.StaringMode = _toggle.isOn;
    }
}
