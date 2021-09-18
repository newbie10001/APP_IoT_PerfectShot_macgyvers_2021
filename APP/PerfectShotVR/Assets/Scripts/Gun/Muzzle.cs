using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

// ���� ������ �߻�Ǵ� �κ��� �ѱ��̴�.
public class Muzzle : MonoBehaviour
{
    private LineRenderer bulletLineRenderer;
    private AudioSource audioSource;

    public AudioClip fireSound;
    public GameObject hitholePrefab;

    private void Awake()
    {
        // ���η����� �ʱ�ȭ �κ�.
        bulletLineRenderer = GetComponent<LineRenderer>();
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
        // ����� �ҽ� �ʱ�ȭ �κ�.
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = fireSound;
    }

    // Shot�� ���� �������鼭 �����ϴ� ����Ʈ�� ó����.
    // �Ҹ� ȿ�� ���� �̰��� ������.
    public void Fire()
    {
        // ���� �߻�Ǵ� �Ҹ����� ���
        audioSource.Play();
        // �Ѿ��� �ϴ� �ѱ��κ��� ����
        bulletLineRenderer.SetPosition(0, transform.position);
        Shot();
    }

    // �߻� ó��
    // �¾��� ��쿡 ���� ������ vector3�� ��ȯ�Ѵ�. ���� �ʾ��� ��� null ��ȯ.
    protected void Shot()
    {
        // �浹�� ��� ���ǹ� ����
        // Debug.Log($"����ĳ��Ʈ : ({transform.position.x}, {transform.position.y}, {transform.position.z})���� ({transform.forward.x}, {transform.forward.y}, {transform.forward.z})����");
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 300))
        {
            ITarget target = hit.collider.GetComponent<ITarget>();
            // ���� �� ǥ���̰� Ȱ��ȭ ������ ��
            if (target != null && target.GetState()) target.OnHit(hit);
            // Ÿ���� ���� ���� ���
            else Debug.Log($"Ÿ�� ��� {hit.collider.name} ����");
        }
    }
}
