using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ǰŸ� ��� �Ŵ���
public class RealShootingManager : MonoBehaviour
{
    // �� ���� �ִ� �÷��̾�
    GameObject player;
    // �� ���� �ִ� ��
    Gun gun;
    // 20�� ����
    private int _ammo = 20;
    // Ÿ�ٵ��� �Ŵ������� ����Ʈ�� �ξ ������.
    public List<HumanoidTargetManager> targets;
    // ���带 ����ϴ� ��ũ��Ʈ
    private RealShotNarrationSound narrator;
    // ��� ����, �λ�� ������ ���ִ� �ε�������
    public Text Indicator;
    // ��� �ǽ� ������ ����� ���� off�̸� �ٷ� ����
    public bool useShootingSequence;
    // ��� �ǽ� ����
    readonly string[] _shootingSeq = { "ź���� �ΰ�\n(���ź 20�� �̻�)", "ź���� ����", "ź�� �Ϲ� ����", "������ �ܹ�" };
    // ��� ���� ����
    readonly string[] _shootEndingSeq = { "��� ����", "������ ����", "ź���� ����", "���� ���� �����ɾƴ��", "" };
    // Ÿ�ٵ��� ����� ����. ������(100m)���� 0��.
    readonly string[] _targetName = { "100m", "200m", "250m"};
    readonly int[] _targetSeq = { 0, 1, 0, 1, 0, 1, 0, 1, 0, 2, 1, 0, 1, 0, 1, 0, 1, 0, 1, 2 };
    // Ÿ�ٸ��� �������� �ð� 100m : 5sec, 200m : 7sec, 250m : 10sec
    readonly int[] _times = { 5, 7, 10 };
    // ����� ���� �� ������ ������
    readonly Vector3[] resultPos = { new Vector3(-2.5f, 1f, 20f), new Vector3(0, 1f, 20f), new Vector3(2.5f, 1f, 20f) };
    // �׽�Ʈ��
    // ���(0��(Ư��), 1��, 2��, 3��)
    readonly int[] _grade = { 18, 16, 14, 12 };
    // ����(���� Ƚ��)
    public int Score { get; private set; }
    // �ְ� ����
    int HighScore
    {
        get => PlayerPrefs.GetInt("RealShootingHighScore");
        set
        {
            // Ȥ�ó� �� ������ ���
            if(value > HighScore) PlayerPrefs.SetInt("RealShootingHighScore", value);
        }
    }
    // ����� �����ֱ� ���� 3D �ؽ�Ʈ
    public GameObject ResultText;
    // �޴��� (�ٽ��ϱ�, ��������)
    public GameObject RetryMenuItem;
    public GameObject MainMenuItem;

    
    void Start()
    {
        // ���� ���ӿ�����Ʈ ���� �ִ� �����̼� ������Ʈ ��������
        narrator = GetComponent<RealShotNarrationSound>();
        // �÷��̾� �Ҵ�
        player = GameObject.FindGameObjectWithTag("Player");
        // ���� ���� �ִ� ���� �Ҵ�.
        gun = FindObjectOfType<Gun>();
        // ź���� ������ ���� Ÿ���� ������ŭ
        _ammo = _targetSeq.Length;
        // ���� �ʱ�ȭ.
        Score = 0;
        StartCoroutine(StartShooting());
    }

    // ��ŵ ���θ� Ȯ���ϱ� ���� endTime�� ���� �÷��̾��� �Է°��� ������.
    bool playerSkip;
    IEnumerator SkipInputCheckForSeconds(float endTime)
    {
        float t = 0;
        playerSkip = false;
        var playerController = player.GetComponent<PlayerController>();
        while (true)
        {
            t += Time.deltaTime;
            if (playerController.PlayerInput)
            {
                Debug.Log("��� ��ŵ");
                playerSkip = true;
                break;
            }
            if (t > endTime) break;
            yield return null;
        }
    }

    // ��� ���� �� �������
    IEnumerator EnteringShootingLane()
    {
        RealShotPlayerMove playerMove = player.GetComponent<RealShotPlayerMove>();
        Coroutine coroutine;
        Indicator.text = "��� ����";
        narrator.PlayEntrance();
        yield return new WaitForSeconds(1.5f);
        coroutine = StartCoroutine(playerMove.EnteringShootingLane());
        yield return SkipInputCheckForSeconds(9.0f);
        // ��ŵ��ư�� ������ �����Ͽ��� ��
        if (playerSkip)
        {
            StopCoroutine(coroutine);
            playerMove.GoToShootingLane();
        }
        narrator.PlaySetProne();
        Indicator.text = "��� ����� ��";
        yield return new WaitForSeconds(2.0f);
        playerMove.AssumingPronePosition();
    }

    // ��� �غ� �ܰ�
    IEnumerator GetReadyToShot()
    {
        Indicator.text = "��� �غ�...";
        do
        {
            if (!playerSkip)
            {
                // �λ�� ź���� �ΰ�
                Indicator.text = _shootingSeq[0];
                narrator.PlayTakeOverMagazine();
                yield return SkipInputCheckForSeconds(3.0f);
                if (playerSkip) break;
                // ��� ź���� ����
                Indicator.text = _shootingSeq[1];
                narrator.PlayCombineMagazine();
                yield return SkipInputCheckForSeconds(4.0f);
                if (playerSkip) break;
                // ź���Ϲ�����
                Indicator.text = _shootingSeq[2];
                narrator.PlayLoadShot();
                yield return SkipInputCheckForSeconds(2.5f);
                if (playerSkip) break;
                // ������ �ܹ�
                Indicator.text = _shootingSeq[3];
                narrator.PlaySetSingle();
                yield return SkipInputCheckForSeconds(2.0f);
                if (playerSkip) break;
            }
        } while (false);
        // ź�� ����
        gun.Reload(_ammo);
        Indicator.text = "��� ����";
        narrator.PlayInitiateShot();
        yield return new WaitForSeconds(3.0f);
    }


    // ��� ����
    IEnumerator StartShooting()
    {
        // �÷��̾ �����ϱ� ���� ��ũ��Ʈ
        var playerController = player.GetComponent<PlayerController>();
        // ���� ������ ���̷� off
        playerController.SetGyroEnabled(false);
        // ��� ����
        yield return EnteringShootingLane();
        yield return SkipInputCheckForSeconds(2.0f);
        // ��� �غ�
        yield return GetReadyToShot();
        // ��� ���� ���� ���̷� on
        playerController.SetGyroEnabled(true);
        // �߻� ���� ���� (�� ��, �� ��...)
        StartCoroutine(ShotCounting());
        // Ÿ���� ������ ���� ����.
        foreach(int t in _targetSeq)
        {
            targets[t].GetUp();
            Indicator.text = _targetName[t];
            switch (t)
            {
                case 0:
                    narrator.Play100m();
                    break;
                case 1:
                    narrator.Play200m();
                    break;
                case 2:
                    narrator.Play250m();
                    break;
            }
            yield return new WaitForSeconds(_times[t]);
            targets[t].GetDown();
            yield return new WaitForSeconds(3.0f);
        }
        UpdateScore();
        yield return new WaitForSeconds(2.0f);
        while(gun.Ammo > 0)
        {
            Indicator.text = "��ź ���";
            yield return new WaitForSeconds(1.0f);
        }
        Indicator.text = "��� ����";
        // ��� ��. ���� ���â�� ������� ��.
        ShowResult();
    }

    // ��� ���� ����
    IEnumerator ShotEnd()
    {
        Indicator.text = _shootEndingSeq[0];
        narrator.PlayShotEnd();
        yield return new WaitForSeconds(2.0f);
        // ������ ����
        Indicator.text = _shootEndingSeq[1];
        narrator.PlaySetSafe();
        yield return new WaitForSeconds(2.0f);
        // ź���� ����(����)
        Indicator.text = _shootEndingSeq[2];
        narrator.PlayDetachMagazine();
        yield return new WaitForSeconds(2.0f);
        // ��� ���ѳ��� �����ɾƴ��
        Indicator.text = _shootEndingSeq[3];
        narrator.PlayLayGunAndSit();
        yield return new WaitForSeconds(2.0f);
        player.GetComponent<RealShotPlayerMove>().GetSittingPosition();
    }

    // �߻� ���� �˷��ִ� �λ�� ����
    IEnumerator ShotCounting()
    {
        int lastAmmo = _ammo;
        while(gun.Ammo > 0)
        {
            if (gun.Ammo < lastAmmo) 
            {
                Indicator.text = $"{_ammo - gun.Ammo}��";
                lastAmmo = gun.Ammo;
            }
            yield return new WaitForSeconds(0.5f);
        }
        // ��� ����
        yield return ShotEnd();
    }

    void UpdateScore()
    {
        int _score = 0;
        foreach (var _target in targets)
        {
            _score += _target.Hits.Count;
        }
        Score = _score;
    }

    /// <summary>
    /// ����� �����ִ� �޼���.
    /// �÷��̾� �տ� ǥ�������� ���ƿͼ� ����� ������.
    /// 3D �ؽ�Ʈ�� �̿�.
    /// ������ '���θ޴���', '�ٽ��ϱ�' �޴��� ��Ÿ��.
    /// </summary>
    void ShowResult()
    {
        Debug.Log("��� ��. ShowResult ȣ��");
        // ���ھ� �ٽ� ����
        UpdateScore();
        // ���� źâ ����
        gun.Reload(-1);
        // ���̽��ھ� ����
        if(Score > HighScore) HighScore = Score;
        // Ÿ�ٸ��� ����� �����ִ� ����
        // ���� �������� �� �������ϰ� ������
        for(int i = 0; i < targets.Count; i++)
        {
            targets[i].OnlyGetUp();
            StartCoroutine(Utility.MoveTo(targets[i].transform, resultPos[i], 1f / 5.0f));
        }
        targets[0].ShowResult($"{targets[0].Hits.Count} / 9");
        targets[1].ShowResult($"{targets[1].Hits.Count} / 9");
        targets[2].ShowResult($"{targets[2].Hits.Count} / 2");

        ResultText.GetComponent<TextMesh>().text = $"��� : {Score} / 20 ({GetGrade(Score)})\n���̽��ھ� : {HighScore} ({GetGrade(HighScore)})";
        // 3D �޴� �������� ���� ���� ����
        StartCoroutine(Utility.MoveTo(RetryMenuItem.transform, new Vector3(-2, 7, 40), 1f));
        StartCoroutine(Utility.MoveTo(MainMenuItem.transform, new Vector3(2, 7, 40), 1f));
        StartCoroutine(ThrowMenuItems());
    }

    // ��� ����� ���ڿ��� ��ȯ.
    string GetGrade(int Score)
    {
        if (Score >= _grade[0])
        {
            return "Ư��!";
        }
        else if (Score >= _grade[1]) return "1��";
        else if (Score >= _grade[2]) return "2��";
        else if (Score >= _grade[3]) return "3��";
        return "Ż��";
    }

    // �޴� �����۵��� ������ �޼���
    IEnumerator ThrowMenuItems()
    {
        yield return new WaitForSeconds(300.0f);
        int count = 0;
        while(count++ < 100)
        {
            GameObject retry = Instantiate(RetryMenuItem, new Vector3(-2, 7, 10), Quaternion.Euler(Vector3.zero));
            GameObject main = Instantiate(MainMenuItem, new Vector3(2, 7, 10), Quaternion.Euler(Vector3.zero));
            StartCoroutine(Utility.MoveTo(retry.transform, new Vector3(-2, 7, 40), 1f));
            StartCoroutine(Utility.MoveTo(main.transform, new Vector3(2, 7, 40), 1f));
            yield return new WaitForSeconds(10.0f);
        }
    }
}