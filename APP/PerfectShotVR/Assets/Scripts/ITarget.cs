using UnityEngine;

// Target 인터페이스
public interface ITarget
{
    // 상태를 반환하는 메서드.
    bool GetState();
    // 맞았을 때 호출되는 메서드.
    void OnHit(RaycastHit hit);
}