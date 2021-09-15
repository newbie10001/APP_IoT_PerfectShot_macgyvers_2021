using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 타겟이 맞는 부위를 담당하는 스크립트.
// 이렇게 하는 이유는... 콜라이더를 모델이 들고 있기 때문.
// 그리고 애니메이션은 타겟의 바깥 부분이 갖고 있기 때문이다.
public class HumanoidTargetHit : MonoBehaviour, ITarget
{
    // 인간형 타겟 클래스를 가져옴. 충돌은 여기서 맡고 나머지 처리는 그곳에
    public HumanoidTargetManager targetManager;
    
    public bool GetState() => targetManager.IsSet;

    public void OnHit(RaycastHit hit)
    {
        Vector3 position = hit.transform.position;
        Debug.Log($"hit at : ({position.x}, {position.y}, {position.z})");
        // 기기 진동
        Handheld.Vibrate();
        targetManager.OnHit(hit);
    }
}
