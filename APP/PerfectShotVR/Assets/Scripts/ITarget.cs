using UnityEngine;

// Target �������̽�
public interface ITarget
{
    // ���¸� ��ȯ�ϴ� �޼���.
    bool GetState();
    // �¾��� �� ȣ��Ǵ� �޼���.
    void OnHit(RaycastHit hit);
}