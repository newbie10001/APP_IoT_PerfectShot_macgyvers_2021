using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // 총구 인스턴스를 할당
    public Muzzle muzzle;

    // 발사 속도 조절
    private float lastShot;
    private float timeBetShot = 0.5f;
    // 총알의 개수
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

    // 장전하는 메서드. num만큼 총알을 다시 채움.
    // num < 0인 경우 무한탄창
    public void Reload(int num)
    {
        Ammo = num;
        Debug.Log($"장전됨 : {Ammo}발");
    }

    // 발사를 시도해보고 가능하면 발사.
    public void Fire()
    {
        // 탄알이 없으면 탄알이 없다고 경고(구현 예정), 총을 쏘지 않음.
        if(Ammo == 0)
        {
            Debug.Log($"Ammo {Ammo}발");
            return;
        }
        // 연사 방지
        if(Time.time >= lastShot + timeBetShot)
        {
            Debug.Log("총알 발사!");
            // hitPoint가 null이면 적중하지 못했다는 뜻.
            muzzle.Fire();
            lastShot = Time.time;
            Ammo--;
            Debug.Log($"총알 {Ammo}발 남음.");
        }
    }
}
