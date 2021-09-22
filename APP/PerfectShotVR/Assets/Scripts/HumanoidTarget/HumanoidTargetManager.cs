using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 내려갔다 올라가는 기능이 있는 인간형 타겟의 매니저
public class HumanoidTargetManager : MonoBehaviour
{
    // 애니메이션 인수 받아오기.
    private Animator animator;
    // 탄착 지점에 놓을 프리팹.
    public GameObject hitMarkPrefab;
    // 3D Text들.
    public GameObject Label;
    public GameObject Result;

    // 이 표적이 활성화되어있는가?
    public bool IsSet { get; private set; }
    // 맞은 지점들을 저장.
    public List<Vector3> Hits { get; private set; }

    void Awake()
    {
        // 인수들 초기화
        animator = GetComponent<Animator>();
        IsSet = false;
        Hits = new List<Vector3>();
        // 3D UI들 비활성화
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

    // 일어나기만 하고 맞춰도 OnHit이 되지 않음.
    public void OnlyGetUp()
    {
        animator.ResetTrigger("GetDown");
        animator.SetTrigger("GetUp");
        IsSet = false;
    }

    // 맞았을 때 호출.
    public void OnHit(RaycastHit hit)
    {
        if(IsSet)
        {
            Hits.Add(hit.point);
            Debug.Log($"{this.gameObject.name} 명중 횟수 {Hits.Count}");
            GameObject hithole = Instantiate(hitMarkPrefab, hit.point - new Vector3(0, 0, 1f), Quaternion.FromToRotation(Vector3.up, hit.normal));
            hithole.transform.parent = this.transform;
            GetDown();
        }
    }

    // 결과를 보여줌.
    public void ShowResult(string result)
    {
        Label.SetActive(true);
        Result.GetComponent<TextMesh>().text = result;
        Result.SetActive(true);
    }
}
