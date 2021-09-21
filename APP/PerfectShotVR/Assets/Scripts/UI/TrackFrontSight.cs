using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� �����ڰ� ��� �ִ��� �����ؼ� UI�� ������.
public class TrackFrontSight : MonoBehaviour
{
    // Gun ������Ʈ �ؿ� �ִ� Front Sight
    // ���� �������� ������Ʈ
    public GameObject frontSight;

    private void Start()
    {
        // Debug.Log($"������ Ȱ��ȭ �ɼ� : {GameManager.instance.FrontSightEnabled}");
        // �ɼ� ����
        this.gameObject.SetActive(GameManager.instance.FrontSightEnabled);
    }

    private void OnEnable()
    {
        StartCoroutine(UpdateUI());
    }

    // ������ ���� 0.5�� �������� ����
    private IEnumerator UpdateUI()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(frontSight.transform.position);
        this.GetComponent<RectTransform>().position = screenPos;
        yield return new WaitForSeconds(0.5f);
        // false�� ��� ������ ����.
        if(GameManager.instance.FrontSightEnabled) StartCoroutine(UpdateUI());
    }

    // ����â���� ����ϸ� �����ϴ� �޼���
    public void ToggleFrontSight(Toggle _toggle)
    {
        this.gameObject.SetActive(_toggle.isOn);
    }
}
