using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 실거리 사격에서 플레이어의 움직임. 다음과 같은 세 가지 움직임으로 구현이 된다.
/// 1. 사로 입장
/// 2. 엎드려 쏴(사로 입장이 끝난 상태에서)
/// 3. 무릎앉아대기(엎드려쏴 상태에서)
/// </summary>
public class RealShotPlayerMove : MonoBehaviour
{
    // {위치, 오일러 회전값}으로 플레이어의 상태를 결정함.
    // 사로 입장
    readonly Vector3[] Enter0 = { new Vector3(-15, 1.5f, -15), new Vector3(0, 0, 0) };
    readonly Vector3[] Enter1 = { new Vector3(-15, 1.5f, -2), new Vector3(0, 0, 0) };
    readonly Vector3[] Enter2 = { new Vector3(-15, 1.5f, -2), new Vector3(0, 90, 0) };
    readonly Vector3[] Enter3 = { new Vector3(0, 1.5f, -2), new Vector3(0, 90, 0) };
    readonly Vector3[] Enter4 = { new Vector3(0, 1.5f, -2), new Vector3(0, 0, 0) };
    // 엎드려 쏴
    readonly Vector3[] Prone0 = { new Vector3(0, 1.5f, -2), new Vector3(0, 0, 0) };
    readonly Vector3[] Prone1 = { new Vector3(0, 0.5f, 0), new Vector3(0, 0, 0) };
    // 무릎 앉아 대기
    readonly Vector3[] Sitting0 = { new Vector3(0, 0.5f, 0), new Vector3(0, 0, 0) };
    readonly Vector3[] Sitting1 = { new Vector3(0, 1.0f, -2), new Vector3(0, 0, 0) };

    /// <summary>
    /// {위치, 회전} 값을 두 개 받아 from에서 to로 상태 이동
    /// </summary>
    /// <param name="from">시작 지점</param>
    /// <param name="to">목표 지점</param>
    /// <param name="time">걸리는 시간</param>
    private void MoveToState(Vector3[] from, Vector3[] to, float time)
    {
        transform.position = from[0];
        transform.eulerAngles = from[1];
        StartCoroutine(Utility.MoveTo(this.transform, to[0], 1.0f / time));
        StartCoroutine(Utility.RotateTo(this.transform, to[1], 1.0f / time));
    }

    public void SkipCheckMove(ref bool skip, IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
        while (true)
        {
            if (skip)
            {
                StopCoroutine(coroutine);
                break;
            }
        }
    }

    // 사로 입장 약 7초 가량 걸림
    public IEnumerator EnteringShootingLane()
    {
        // 시작 지점으로 가기
        transform.position = Enter0[0];
        transform.eulerAngles = Enter0[1];
        yield return new WaitForSeconds(2.0f);
        yield return Utility.MoveTo(transform, Enter1[0], 1.5f / 3.0f);
        // 회전
        yield return Utility.RotateTo(transform, Enter2[1], 1.5f);
        yield return Utility.MoveTo(transform, Enter3[0], 1.0f / 3.0f);
        // 회전
        yield return (Utility.RotateTo(transform, Enter4[1], 1.5f));
    }

    // 엎드려 쏴
    public void AssumingPronePosition()
    {
        transform.position = Prone0[0];
        transform.eulerAngles = Prone0[1];
        StartCoroutine(Utility.MoveTo(transform, Prone1[0], 1.25f));
    }

    // 무릎 앉아 대기
    public void GetSittingPosition()
    {
        transform.position = Sitting0[0];
        StartCoroutine(Utility.MoveSlerpTo(transform, Sitting1[0], 2.0f));
    }

    // 어떤 위치에서든 즉시 사로입장
    public void GoToShootingLane()
    {
        MoveToState(new Vector3[] { transform.position, transform.eulerAngles}, Enter4, 1.0f );
    }
}
