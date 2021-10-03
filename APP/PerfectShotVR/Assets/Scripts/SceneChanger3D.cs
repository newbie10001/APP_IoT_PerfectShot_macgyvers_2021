using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 씬을 전환해주는 3D 오브젝트. 맞으면 정해진 씬으로 이동
public class SceneChanger3D : MonoBehaviour, ITarget
{
    // 이동할 씬의 이름
    public string sceneName;

    // 날아가기 위한 몸체
    private Rigidbody _rigidbody;
    // 충돌 시 가해지는 힘
    public Vector3 force = new Vector3(0, 500, 300);

    void Awake()
    {
        // 리지드바디 얻어오기
        _rigidbody = GetComponent<Rigidbody>();
    }

    public bool GetState() => true;

    // 맞았을 때 날아감
    // 날아가고 씬 변경
    public void OnHit(RaycastHit hit)
    {
        _rigidbody.useGravity = true;
        _rigidbody.AddForceAtPosition(force, hit.normal);
        StartCoroutine(LoadScene());
    }

    // sceneName에 해당하는 씬으로 1초후 이동
    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
