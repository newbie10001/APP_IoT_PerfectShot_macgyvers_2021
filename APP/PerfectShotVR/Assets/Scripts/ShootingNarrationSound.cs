using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� �����̼� ����
// �Ŵ����� ������Ʈ�� ��
public class ShootingNarrationSound : MonoBehaviour
{
    private AudioSource audioSource;
    // ��� ����
    public AudioClip Entrance;
    // ��� �������
    public AudioClip SetProne;
    // ź���� �ΰ�
    public AudioClip TakeOverMagazine;
    // ź���� ����
    public AudioClip CombineMagazine;
    // ź���Ϲ�����
    public AudioClip LoadShot;
    // ������ �ܹ�
    public AudioClip SetSingle;
    // ��� ����
    public AudioClip InitiateShot;

    // 100m ����
    public AudioClip Sound100;
    // 200m ����
    public AudioClip Sound200;
    // 250m ����
    public AudioClip Sound250;
    // ǥ���� Ȯ��
    public AudioClip CheckPaper;

    // ��� ����
    public AudioClip ShotEnd;
    // ������ ����
    public AudioClip SetSafe;
    // ź���� ����
    public AudioClip DetachMagazine;
    // ���� ���� �����ɾƴ��
    public AudioClip LayGunAndSit;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    public void PlayAudioClip(AudioClip clip) => audioSource.PlayOneShot(clip);
    #region ��� �ǽ� ����
    // ��� ����
    public void PlayEntrance() => audioSource.PlayOneShot(Entrance);
    // �������
    public void PlaySetProne() => audioSource.PlayOneShot(SetProne);
    // �λ�� ź���� �ΰ�
    public void PlayTakeOverMagazine() => audioSource.PlayOneShot(TakeOverMagazine);
    // ��� ź���� ����
    public void PlayCombineMagazine() => audioSource.PlayOneShot(CombineMagazine);
    // ź���Ϲ�����
    public void PlayLoadShot() => audioSource.PlayOneShot(LoadShot);
    // ������ �ܹ�
    public void PlaySetSingle() => audioSource.PlayOneShot(SetSingle);
    // ��ݰ���
    public void PlayInitiateShot() => audioSource.PlayOneShot(InitiateShot);
    #endregion
    #region ��� �� ����
    public void Play100m() => audioSource.PlayOneShot(Sound100);
    public void Play200m() => audioSource.PlayOneShot(Sound200);
    public void Play250m() => audioSource.PlayOneShot(Sound250);
    public void PlayCheckPaper() => audioSource.PlayOneShot(CheckPaper);
    #endregion
    #region ��� ���� ����
    // ��� ����
    public void PlayShotEnd() => audioSource.PlayOneShot(ShotEnd);
    // ������ ����
    public void PlaySetSafe() => audioSource.PlayOneShot(SetSafe);
    // ź���� ����
    public void PlayDetachMagazine() => audioSource.PlayOneShot(DetachMagazine);
    // ���� ���� �����ɾƴ��
    public void PlayLayGunAndSit() => audioSource.PlayOneShot(LayGunAndSit);
    #endregion
}