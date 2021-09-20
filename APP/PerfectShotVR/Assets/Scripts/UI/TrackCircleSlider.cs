using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� �����ڰ� ��� �ִ��� �����ؼ� UI�� ������. (��Ŭ �����̴�)
public class TrackCircleSlider : MonoBehaviour
{
    // Gun ������Ʈ �ؿ� �ִ� Front Sight
    // ���� �������� ������Ʈ
    public GameObject frontSight;

    private void Start()
    {

    }

    private void OnEnable()
    {
        StartCoroutine(updateUI());
    }

    private void Update()
    {
        
    }

    // ������ ���� 0.5�� �������� ����
    private IEnumerator updateUI()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(frontSight.transform.position);
        this.GetComponent<RectTransform>().position = screenPos;
        yield return new WaitForSeconds(0.5f);
        // false�� ��� ������ ����.
        if(GameManager.instance.StaringMode) StartCoroutine(updateUI());
    }
}
