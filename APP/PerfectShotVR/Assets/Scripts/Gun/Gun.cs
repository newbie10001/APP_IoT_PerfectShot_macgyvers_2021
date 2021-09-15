using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // �ѱ� �ν��Ͻ��� �Ҵ�
    public Muzzle muzzle;

    // �߻� �ӵ� ����
    private float lastShot;
    private float timeBetShot = 0.5f;
    // �Ѿ��� ����
    public int Ammo { get; private set; }

    void Awake()
    {
        Ammo = 0;
        lastShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �����ϴ� �޼���. num��ŭ �Ѿ��� �ٽ� ä��.
    // num < 0�� ��� ����źâ
    public void Reload(int num)
    {
        Ammo = num;
        Debug.Log($"������ : {Ammo}��");
    }

    // �߻縦 �õ��غ��� �����ϸ� �߻�.
    public void Fire()
    {
        // ź���� ������ ź���� ���ٰ� ���(���� ����), ���� ���� ����.
        if(Ammo == 0)
        {
            Debug.Log($"Ammo {Ammo}��");
            return;
        }
        // ���� ����
        if(Time.time >= lastShot + timeBetShot)
        {
            Debug.Log("�Ѿ� �߻�!");
            // hitPoint�� null�̸� �������� ���ߴٴ� ��.
            muzzle.Fire();
            lastShot = Time.time;
            Ammo--;
            Debug.Log($"�Ѿ� {Ammo}�� ����.");
        }
    }
}
