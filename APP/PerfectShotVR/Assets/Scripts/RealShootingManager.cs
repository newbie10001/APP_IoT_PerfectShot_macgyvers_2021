using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ǰŸ� ��� �Ŵ���
public class RealShootingManager : MonoBehaviour
{
    // �� ���� �ִ� ��
    Gun gun;
    // 20�� ����
    private int _ammo = 20;
    // Ÿ�ٵ��� �Ŵ������� ����Ʈ�� �ξ ������.
    public List<HumanoidTargetManager> targets;
    // �λ�� ������ ���ִ� �ε�������
    public Text Indicator;
    // ��� �ǽ� ������ ����� ���� off�̸� �ٷ� ����
    public bool useShootingSequence;
    // ��� �ǽ� ����
    readonly string[] _shootingSeq = { "ź���� �ΰ�", "���ź �̻�", "ź���� ����", "ź�� �Ϲ� ����", "������ �ܹ�" };
    // ��� ���� ����
    readonly string[] _shootEndingSeq = { "��� ��", "������ ����", "ź���� ����", "���� ���� �����ɾƴ��", "" };
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
        // ���� ���� �ִ� ���� �Ҵ�.
        gun = FindObjectOfType<Gun>();
        // ź���� ������ ���� Ÿ���� ������ŭ
        _ammo = _targetSeq.Length;
        // ���� �ʱ�ȭ.
        Score = 0;
        StartCoroutine(StartShooting());
    }

    IEnumerator StartShooting()
    {
        #region ��� �غ�
        Indicator.text = "��� �غ�...";
        if (useShootingSequence)
        {
            foreach (string ment in _shootingSeq)
            {
                Indicator.text = ment;
                yield return new WaitForSeconds(3.0f);
            }
        }
        // ź�� ����
        gun.Reload(_ammo);
        Indicator.text = "��� ����";
        yield return new WaitForSeconds(3.0f);
        #endregion
        StartCoroutine(ShotCheck());
        // Ÿ���� ������ ���� ����.
        foreach(int t in _targetSeq)
        {
            targets[t].GetUp();
            Indicator.text = _targetName[t];
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

    // �߻� ���� �˷��ִ� �λ�� ����
    IEnumerator ShotCheck()
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
        // ��� ���� ����
        foreach(string ment in _shootEndingSeq)
        {
            Indicator.text = ment;
            yield return new WaitForSeconds(3.0f);
        }

        yield return null;
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
            StartCoroutine(MoveTo(targets[i].transform, resultPos[i], 1f));
        }
        targets[0].ShowResult($"{targets[0].Hits.Count} / 9");
        targets[1].ShowResult($"{targets[1].Hits.Count} / 9");
        targets[2].ShowResult($"{targets[2].Hits.Count} / 2");

        ResultText.GetComponent<TextMesh>().text = $"��� : {Score} / 20 ({GetGrade(Score)})\n���̽��ھ� : {HighScore} ({GetGrade(HighScore)})";
        // 3D �޴� �������� ���� ���� ����
        // StartCoroutine(MoveTo(RetryMenuItem.transform, new Vector3(-2, 7, 40), 1f));
        // StartCoroutine(MoveTo(MainMenuItem.transform, new Vector3(2, 7, 40), 1f));
        // StartCoroutine(ThrowMenuItems());
    }

    // ��� ����� ���ڿ��� ��ȯ.
    string GetGrade(int Score)
    {
        if (Score > _grade[0])
        {
            return "Ư��!";
        }
        else if (Score > _grade[1]) return "1��";
        else if (Score > _grade[2]) return "2��";
        else if (Score > _grade[3]) return "3��";
        return "Ż��";
    }

    // ������Ʈ�� �ű�� �޼���
    IEnumerator MoveTo(Transform transform, Vector3 dest, float speed)
    {
        float count = 0;
        Vector3 startPos = transform.position;
        while(true)
        {
            count += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPos, dest, count);

            if(count >= 1)
            {
                transform.position = dest;
                break;
            }
            yield return null;
        }
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
            StartCoroutine(MoveTo(retry.transform, new Vector3(-2, 7, 40), 1f));
            StartCoroutine(MoveTo(main.transform, new Vector3(2, 7, 40), 1f));
            yield return new WaitForSeconds(10.0f);
        }
    }
}