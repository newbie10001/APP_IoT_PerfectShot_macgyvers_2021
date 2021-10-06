using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 총알의 스크립트
// 설명 : 지나온 궤적을 라인 렌더러로 그린다. 만약 라인 렌더러가 충돌하면 충돌 처리
// 이 스크립트는 총알 모델이 있는 곳에 적용된다.
public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"Bullet 출발 : pos = ({transform.position.x}, {transform.position.y}, {transform.position.z})\nrot = ({this.transform.parent.transform.rotation.eulerAngles.x}, {this.transform.parent.transform.rotation.eulerAngles.y}, {this.transform.parent.transform.rotation.eulerAngles.z})");
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        if (pos.z > 20 && pos.z < 30) Debug.Log($"{pos.z}m일 때 y값 : {pos.y}");
        else if (pos.z > 90 && pos.z < 110) Debug.Log($"{pos.z}m일 때 y값 : {pos.y}");
        else if (pos.z > 190 && pos.z < 210) Debug.Log($"{pos.z}m일 때 y값 : {pos.y}");
        else if (pos.z > 249 && pos.z < 251) Debug.Log($"{pos.z}m일 때 y값 : {pos.y}");
        if (pos.z > 600)
        {
            Debug.Log("유효사거리는 600");
            Destroy(this.transform.parent.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ITarget target = collision.gameObject.GetComponent<ITarget>();
        Debug.Log($"{this.gameObject.name} 충돌 : {collision.gameObject.name}");
        if (target == null) return;
        else
        {
            if (target.GetState()) target.OnHit(new RaycastHit { point = transform.position, normal = new Vector3(0, 0, -1) });
        }
        Destroy(this.transform.parent.gameObject);
    }
}