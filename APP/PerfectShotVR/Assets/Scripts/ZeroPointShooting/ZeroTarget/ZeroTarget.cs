using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 영점사격지에 적용되는 스크립트
public class ZeroTarget : MonoBehaviour, ITarget
{
    // 결과 등을 알려주는 텍스트 UI
    public Text Indicator;
    public bool State { get; private set; }
    public bool GetState() => State;
    public GameObject bulletHolePrefab;
    // 초기 위치
    private Vector3 _initPos;
    // 플레이어 눈앞 위치
    private Vector3 DestPos { get => _initPos - new Vector3(0, -0.5f, 23.5f); }
    // 맞은 지점들을 기록
    public List<Vector3> HitPoints { get; private set; }


    void Start()
    {
        HitPoints = new List<Vector3>();
        _initPos = this.transform.position;
    }

    public void OnHit(RaycastHit hit)
    {
        if (!State) return;
        Vector3 pos = hit.point;
        Debug.Log($"hit at {this.gameObject.name} : ({pos.x}, {pos.y}, {pos.z})");
        HitPoints.Add(pos);
        if(bulletHolePrefab != null)
        {
            GameObject hithole = Instantiate(bulletHolePrefab, hit.point, Quaternion.Euler(0, 0, 0));
            hithole.transform.parent = this.transform;
        }
    }

    // 플레이어 눈 앞에 종이가 날아옴.
    public void MoveToPlayer()
    {
        State = false;
        StartCoroutine(Utility.MoveTo(transform, DestPos, 1f));
    }

    // 사격지 자리로 날아감.
    public void MoveToSet()
    {
        // 확인된 총알들을 반투명하게 처리
        for(int i = 0; i < this.transform.childCount; i++)
        {
            Renderer renderer = this.transform.GetChild(i).GetComponentInChildren<Renderer>();
            Color c = renderer.material.color;
            c.a *= 0.5f;
            renderer.material.color = c;
        }
        StartCoroutine(Utility.MoveTo(transform, _initPos, 5f));
        State = true;
    }
}
