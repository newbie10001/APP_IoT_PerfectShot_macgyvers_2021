using UnityEngine;

// Target 인터페이스
public interface ITarget
{
    // 활성화 상태인가? 활성된 상태이면 true 반환 아니면 false.
    bool GetState();
    // 맞았을 때 실행되는 메서드, 맞은 지점을 건네줘야 함.
    void OnHit(RaycastHit hit);
}