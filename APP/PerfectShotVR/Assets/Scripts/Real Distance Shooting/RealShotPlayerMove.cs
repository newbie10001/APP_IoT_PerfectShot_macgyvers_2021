using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ǰŸ� ��ݿ��� �÷��̾��� ������. ������ ���� �� ���� ���������� ������ �ȴ�.
/// 1. ��� ����
/// 2. ����� ��(��� ������ ���� ���¿���)
/// 3. �����ɾƴ��(������� ���¿���)
/// </summary>
public class RealShotPlayerMove : MonoBehaviour
{
    // {��ġ, ���Ϸ� ȸ����}���� �÷��̾��� ���¸� ������.
    // ��� ����
    readonly Vector3[] Enter0 = { new Vector3(-15, 1.5f, -15), new Vector3(0, 0, 0) };
    readonly Vector3[] Enter1 = { new Vector3(-15, 1.5f, -2), new Vector3(0, 0, 0) };
    readonly Vector3[] Enter2 = { new Vector3(-15, 1.5f, -2), new Vector3(0, 90, 0) };
    readonly Vector3[] Enter3 = { new Vector3(0, 1.5f, -2), new Vector3(0, 90, 0) };
    readonly Vector3[] Enter4 = { new Vector3(0, 1.5f, -2), new Vector3(0, 0, 0) };
    // ����� ��
    readonly Vector3[] Prone0 = { new Vector3(0, 1.5f, -2), new Vector3(0, 0, 0) };
    readonly Vector3[] Prone1 = { new Vector3(0, 0.5f, 0), new Vector3(0, 0, 0) };
    // ���� �ɾ� ���
    readonly Vector3[] Sitting0 = { new Vector3(0, 0.5f, 0), new Vector3(0, 0, 0) };
    readonly Vector3[] Sitting1 = { new Vector3(0, 1.0f, -2), new Vector3(0, 0, 0) };

    /// <summary>
    /// {��ġ, ȸ��} ���� �� �� �޾� from���� to�� ���� �̵�
    /// </summary>
    /// <param name="from">���� ����</param>
    /// <param name="to">��ǥ ����</param>
    /// <param name="time">�ɸ��� �ð�</param>
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

    // ��� ���� �� 7�� ���� �ɸ�
    public IEnumerator EnteringShootingLane()
    {
        // ���� �������� ����
        transform.position = Enter0[0];
        transform.eulerAngles = Enter0[1];
        yield return new WaitForSeconds(2.0f);
        yield return Utility.MoveTo(transform, Enter1[0], 1.5f / 3.0f);
        // ȸ��
        yield return Utility.RotateTo(transform, Enter2[1], 1.5f);
        yield return Utility.MoveTo(transform, Enter3[0], 1.0f / 3.0f);
        // ȸ��
        yield return (Utility.RotateTo(transform, Enter4[1], 1.5f));
    }

    // ����� ��
    public void AssumingPronePosition()
    {
        transform.position = Prone0[0];
        transform.eulerAngles = Prone0[1];
        StartCoroutine(Utility.MoveTo(transform, Prone1[0], 1.25f));
    }

    // ���� �ɾ� ���
    public void GetSittingPosition()
    {
        transform.position = Sitting0[0];
        StartCoroutine(Utility.MoveSlerpTo(transform, Sitting1[0], 2.0f));
    }

    // � ��ġ������ ��� �������
    public void GoToShootingLane()
    {
        MoveToState(new Vector3[] { transform.position, transform.eulerAngles}, Enter4, 1.0f );
    }
}
