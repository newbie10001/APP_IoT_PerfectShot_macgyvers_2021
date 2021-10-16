using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 탄도학 구현을 보여주기 위해 라인렌더러로 궤적을 그림
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

    // 0.01초마다 라인을 그림
    IEnumerator DrawLine()
    {
        // 대략 7초동안.
        while (idx < 700)
        {
            lineRenderer.positionCount = idx + 1;
            lineRenderer.SetPosition(idx++, transform.position);
            yield return new WaitForSeconds(0.01f);
        }
    }
}