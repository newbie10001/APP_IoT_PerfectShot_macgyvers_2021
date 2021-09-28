using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ����������� ����Ǵ� ��ũ��Ʈ
public class ZeroTarget : MonoBehaviour, ITarget
{
    // ��� ���� �˷��ִ� �ؽ�Ʈ UI
    public Text Indicator;
    public bool State { get; private set; }
    public bool GetState() => State;
    public GameObject bulletHolePrefab;
    // �ʱ� ��ġ
    private Vector3 _initPos;
    // �÷��̾� ���� ��ġ
    private Vector3 DestPos { get => _initPos - new Vector3(0, -0.5f, 23.5f); }
    // ���� �������� ���
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

    // �÷��̾� �� �տ� ���̰� ���ƿ�.
    public void MoveToPlayer()
    {
        State = false;
        StartCoroutine(Utility.MoveTo(transform, DestPos, 1f));
    }

    // ����� �ڸ��� ���ư�.
    public void MoveToSet()
    {
        // Ȯ�ε� �Ѿ˵��� �������ϰ� ó��
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
