using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

// ���� ������ �߻�Ǵ� �κ��� �ѱ��̴�.
public class Muzzle : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip fireSound;
    // ������ �߻��� �Ѿ� ������Ʈ
    public GameObject Bullet;

    private void Awake()
    {
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
        RealShot();
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

    // ���� �Ѿ� ������Ʈ�� �߻��ϴ�, �� ������� ��� ����
    public float bullet_speed = 960f;
    protected void RealShot()
    {
        GameObject bullet = Instantiate(Bullet, this.transform.position, this.transform.rotation);
        Rigidbody rigidbody = bullet.GetComponentInChildren<Rigidbody>();
        rigidbody.velocity = bullet.transform.forward * bullet_speed;
    }
}
