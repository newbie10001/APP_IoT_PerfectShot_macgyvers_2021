using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� �ö󰡴� ����� �ִ� �ΰ��� Ÿ���� �Ŵ���
public class HumanoidTargetManager : MonoBehaviour
{
    // �ִϸ��̼� �μ� �޾ƿ���.
    private Animator animator;
    // ź�� ������ ���� ������.
    public GameObject hitMarkPrefab;
    // 3D Text��.
    public GameObject Label;
    public GameObject Result;

    // �� ǥ���� Ȱ��ȭ�Ǿ��ִ°�?
    public bool IsSet { get; private set; }
    // ���� �������� ����.
    public List<Vector3> Hits { get; private set; }

    void Awake()
    {
        // �μ��� �ʱ�ȭ
        animator = GetComponent<Animator>();
        IsSet = false;
        Hits = new List<Vector3>();
        // 3D UI�� ��Ȱ��ȭ
        Label.SetActive(false);
        Result.SetActive(false);
    }

    public void GetUp()
    {
        animator.ResetTrigger("GetDown");
        animator.SetTrigger("GetUp");
        IsSet = true;
    }

    public void GetDown()
    {
        animator.ResetTrigger("GetUp");
        animator.SetTrigger("GetDown");
        IsSet = false;
    }

    // �Ͼ�⸸ �ϰ� ���絵 OnHit�� ���� ����.
    public void OnlyGetUp()
    {
        animator.ResetTrigger("GetDown");
        animator.SetTrigger("GetUp");
        IsSet = false;
    }

    // �¾��� �� ȣ��.
    public void OnHit(RaycastHit hit)
    {
        if(IsSet)
        {
            Hits.Add(hit.point);
            Debug.Log($"{this.gameObject.name} ���� Ƚ�� {Hits.Count}");
            GameObject hithole = Instantiate(hitMarkPrefab, hit.point - new Vector3(0, 0, 1f), Quaternion.FromToRotation(Vector3.up, hit.normal));
            hithole.transform.parent = this.transform;
            GetDown();
        }
    }

    // ����� ������.
    public void ShowResult(string result)
    {
        Label.SetActive(true);
        Result.GetComponent<TextMesh>().text = result;
        Result.SetActive(true);
    }
}
