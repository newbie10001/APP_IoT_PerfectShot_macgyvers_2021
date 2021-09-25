using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 실거리 사격 내레이션 사운드
// 매니저의 컴포넌트로 들어감
public class RealShotNarrationSound : MonoBehaviour
{
    private AudioSource audioSource;
    // 사로 입장
    public AudioClip Entrance;
    // 사수 엎드려쏴
    public AudioClip SetProne;
    // 탄알집 인계
    public AudioClip TakeOverMagazine;
    // 탄알집 결합
    public AudioClip CombineMagazine;
    // 탄알일발장전
    public AudioClip LoadShot;
    // 조정간 단발
    public AudioClip SetSingle;
    // 사격 개시
    public AudioClip InitiateShot;

    // 사격 종료
    public AudioClip ShotEnd;
    // 조정간 안전
    public AudioClip SetSafe;
    // 탄알집 제거
    public AudioClip DetachMagazine;
    // 소총 놓고 무릎앉아대기
    public AudioClip LayGunAndSit;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    public void PlayAudioClip(AudioClip clip) => audioSource.PlayOneShot(clip);
    #region 사격 실시 절차
    // 사로 입장
    public void PlayEntrance() => audioSource.PlayOneShot(Entrance);
    // 엎드려쏴
    public void PlaySetProne() => audioSource.PlayOneShot(SetProne);
    // 부사수 탄알집 인계
    public void PlayTakeOverMagazine() => audioSource.PlayOneShot(TakeOverMagazine);
    // 사수 탄알집 결합
    public void PlayCombineMagazine() => audioSource.PlayOneShot(CombineMagazine);
    // 탄알일발장전
    public void PlayLoadShot() => audioSource.PlayOneShot(LoadShot);
    // 조정간 단발
    public void PlaySetSingle() => audioSource.PlayOneShot(SetSingle);
    // 사격개시
    public void PlayInitiateShot() => audioSource.PlayOneShot(InitiateShot);
    #endregion

    #region 사격 종료 절차
    // 사격 종료
    public void PlayShotEnd() => audioSource.PlayOneShot(ShotEnd);
    // 조정간 안전
    public void PlaySetSafe() => audioSource.PlayOneShot(SetSafe);
    // 탄알집 제거
    public void PlayDetachMagazine() => audioSource.PlayOneShot(DetachMagazine);
    // 소총 놓고 무릎앉아대기
    public void PlayLayGunAndSit() => audioSource.PlayOneShot(LayGunAndSit);
    #endregion
}