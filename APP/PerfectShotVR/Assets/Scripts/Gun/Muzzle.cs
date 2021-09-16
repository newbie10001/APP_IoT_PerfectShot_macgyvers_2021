using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

// 총이 실제로 발사되는 부분은 총구이다.
public class Muzzle : MonoBehaviour
{
    private LineRenderer bulletLineRenderer;
    private AudioSource audioSource;

    public AudioClip fireSound;
    public GameObject hitholePrefab;

    private void Awake()
    {
        // 라인렌더러 초기화 부분.
        bulletLineRenderer = GetComponent<LineRenderer>();
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
        // 오디오 소스 초기화 부분.
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = fireSound;
    }

    // Shot의 값을 가져오면서 발포하는 이펙트를 처리함.
    // 소리 효과 등을 이곳에 가져옴.
    // 반환값으로 탄착지점을 반환하는데.. 필요할까?
    public void Fire()
    {
        // 총이 발사되는 소리부터 재생
        audioSource.Play();
        // 총알은 일단 직선. 추후 점을 더 추가할 예정임.
        bulletLineRenderer.SetPosition(0, transform.position);
        // Vector3? hitPosition = Shot();
        // 발사하는 이펙트 재생
        // null 값을 전해줄 수 있다.
        // 1인칭에서 안 보이는 문제점이 있음. 검토 후 삭제 예정.
        // StartCoroutine(DrawShotEffect(hitPosition));
    }

    // 발사 처리
    // 맞았을 경우에 맞은 지점의 vector3를 반환한다. 맞지 않았을 경우 null 반환.
    protected Vector3? Shot()
    {
        RaycastHit hit;
        // 충돌한 경우 조건문 진입
        // Debug.Log($"레이캐스트 : ({transform.position.x}, {transform.position.y}, {transform.position.z})에서 ({transform.forward.x}, {transform.forward.y}, {transform.forward.z})으로");
        if (Physics.Raycast(transform.position, transform.forward, out hit, 300))
        {
            ITarget target = hit.collider.GetComponent<ITarget>();
            // 맞은 게 표적이고 활성화 상태일 때
            if (target != null && target.GetState())
            {
                target.OnHit(hit);
                return hit.transform.position;
            }
            Debug.Log($"타겟 대신 {hit.collider.name} 맞음");
        }
        return null;
    }

    // 발사되는 궤적을 그린다. 현재는 쓰지 않음!
    private IEnumerator DrawShotEffect(Vector3? hitPosition)
    {
        bulletLineRenderer.SetPosition(0, transform.position);
        bulletLineRenderer.SetPosition(1, hitPosition ?? transform.position + transform.forward * 250);
        // 라인 렌더러를 활성화하여 총알 궤적을 그린다
        bulletLineRenderer.enabled = true;

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 총알 궤적을 지운다
        bulletLineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
