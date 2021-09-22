using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѿ��� ��ũ��Ʈ
// ���� : ������ ������ ���� �������� �׸���. ���� ���� �������� �浹�ϸ� �浹 ó��
// �� ��ũ��Ʈ�� �Ѿ� ���� �ִ� ���� ����ȴ�.
public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"Bullet ��� : pos = ({transform.position.x}, {transform.position.y}, {transform.position.z})\nrot = ({this.transform.parent.transform.rotation.eulerAngles.x}, {this.transform.parent.transform.rotation.eulerAngles.y}, {this.transform.parent.transform.rotation.eulerAngles.z})");
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        if (pos.z > 20 && pos.z < 30) Debug.Log($"{pos.z}m�� �� y�� : {pos.y}");
        else if (pos.z > 90 && pos.z < 110) Debug.Log($"{pos.z}m�� �� y�� : {pos.y}");
        else if (pos.z > 190 && pos.z < 210) Debug.Log($"{pos.z}m�� �� y�� : {pos.y}");
        else if (pos.z > 249 && pos.z < 251) Debug.Log($"{pos.z}m�� �� y�� : {pos.y}");
        if (pos.z > 600)
        {
            Debug.Log("��ȿ��Ÿ��� 600");
            Destroy(this.transform.parent.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ITarget target = collision.gameObject.GetComponent<ITarget>();
        Debug.Log($"{this.gameObject.name} �浹 : {collision.gameObject.name}");
        if (target == null) return;
        else
        {
            if(target.GetState()) target.OnHit(new RaycastHit { point = transform.position, normal = new Vector3(0,0,-1)});
        }
        Destroy(this.transform.parent.gameObject);
    }
}
