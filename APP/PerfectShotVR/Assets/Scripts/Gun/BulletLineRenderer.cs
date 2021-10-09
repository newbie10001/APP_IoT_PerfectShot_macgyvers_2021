using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ź���� ������ �����ֱ� ���� ���η������� ������ �׸�
public class BulletLineRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Material LineMaterial;
    int idx = 0;
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = LineMaterial;
        lineRenderer.startColor = Color.yellow;
        lineRenderer.startWidth = 10f;
        lineRenderer.endWidth = 10f;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.positionCount = idx + 1;
        lineRenderer.SetPosition(idx++, transform.position);
        StartCoroutine(DrawLine());
    }

    // 0.01�ʸ��� ������ �׸�
    IEnumerator DrawLine()
    {
        // �뷫 7�ʵ���.
        while (idx < 700)
        {
            lineRenderer.positionCount = idx + 1;
            lineRenderer.SetPosition(idx++, transform.position);
            yield return new WaitForSeconds(0.01f);
        }
    }
}