using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���� ��ȯ���ִ� 3D ������Ʈ. ������ ������ ������ �̵�
public class SceneChanger3D : MonoBehaviour, ITarget
{
    // �̵��� ���� �̸�
    public string sceneName;

    // ���ư��� ���� ��ü
    private Rigidbody _rigidbody;
    // �浹 �� �������� ��
    public Vector3 force = new Vector3(0, 500, 300);

    void Awake()
    {
        // ������ٵ� ������
        _rigidbody = GetComponent<Rigidbody>();
    }

    public bool GetState() => true;

    // �¾��� �� ���ư�
    // ���ư��� �� ����
    public void OnHit(RaycastHit hit)
    {
        _rigidbody.useGravity = true;
        _rigidbody.AddForceAtPosition(force, hit.normal);
        StartCoroutine(LoadScene());
    }

    // sceneName�� �ش��ϴ� ������ 1���� �̵�
    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
