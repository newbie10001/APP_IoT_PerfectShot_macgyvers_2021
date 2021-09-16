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
    // ��ȯ������ ź�������� ��ȯ�ϴµ�.. �ʿ��ұ�?
    public void Fire()
    {
        // ���� �߻�Ǵ� �Ҹ����� ���
        audioSource.Play();
        // �Ѿ��� �ϴ� ����. ���� ���� �� �߰��� ������.
        bulletLineRenderer.SetPosition(0, transform.position);
        // Vector3? hitPosition = Shot();
        // �߻��ϴ� ����Ʈ ���
        // null ���� ������ �� �ִ�.
        // 1��Ī���� �� ���̴� �������� ����. ���� �� ���� ����.
        // StartCoroutine(DrawShotEffect(hitPosition));
    }

    // �߻� ó��
    // �¾��� ��쿡 ���� ������ vector3�� ��ȯ�Ѵ�. ���� �ʾ��� ��� null ��ȯ.
    protected Vector3? Shot()
    {
        RaycastHit hit;
        // �浹�� ��� ���ǹ� ����
        // Debug.Log($"����ĳ��Ʈ : ({transform.position.x}, {transform.position.y}, {transform.position.z})���� ({transform.forward.x}, {transform.forward.y}, {transform.forward.z})����");
        if (Physics.Raycast(transform.position, transform.forward, out hit, 300))
        {
            ITarget target = hit.collider.GetComponent<ITarget>();
            // ���� �� ǥ���̰� Ȱ��ȭ ������ ��
            if (target != null && target.GetState())
            {
                target.OnHit(hit);
                return hit.transform.position;
            }
            Debug.Log($"Ÿ�� ��� {hit.collider.name} ����");
        }
        return null;
    }

    // �߻�Ǵ� ������ �׸���. ����� ���� ����!
    private IEnumerator DrawShotEffect(Vector3? hitPosition)
    {
        bulletLineRenderer.SetPosition(0, transform.position);
        bulletLineRenderer.SetPosition(1, hitPosition ?? transform.position + transform.forward * 250);
        // ���� �������� Ȱ��ȭ�Ͽ� �Ѿ� ������ �׸���
        bulletLineRenderer.enabled = true;

        // 0.03�� ���� ��� ó���� ���
        yield return new WaitForSeconds(0.03f);

        // ���� �������� ��Ȱ��ȭ�Ͽ� �Ѿ� ������ �����
        bulletLineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
