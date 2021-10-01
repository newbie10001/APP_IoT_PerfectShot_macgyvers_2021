using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� ��� �Ŵ���(�Ѱ�)
/// <summary>
/// ���� : n�߾� 3�� ����� �� ǥ���� �����ְ�, ũ��ũ ������ �� �� �ֵ��� ��. ũ��ũ ���� UI�� ����� ��.
/// ����ȹ�� ���θ� �˷���. ��� �������� �ǵ����(���߿� ����).
/// ��������� ������ '��������', '�ٽ��ϱ�' ��� �޴��������� Ȱ��ȭ��.
/// �׶� ������ ȹ��Ǹ� �ǰŸ� ��� �޴��������� Ȱ��ȭ��.
/// </summary>
public class ZeroPointShootingManager : MonoBehaviour
{
    // �� ���� �ִ� �÷��̾�
    private GameObject player;
    // �÷��̾ ��� �ִ� ��
    private Gun gun;
    // �� �ϸ��� ������ ź�� ��
    const int AMMO = 5;
    // ũ��ũ ���� �޴�â (3D)
    public GameObject ClickSettingMenu;
    // ���� �ֱٿ� ������ ���.
    private JudgeResult _result;
    // �ȳ� UI
    public Text Indicator;
    // ���带 ����ϴ� ��ũ��Ʈ
    private ShootingNarrationSound narrator;
    // ���������
    private ZeroTarget zeroPaper;
    // ������ ȹ���ߴ��� ����
    bool gotZero = false;
    // �޴���
    public GameObject RetryMenu;
    public GameObject MainMenu;
    public GameObject RealShotMenu;

    void Start()
    {
        // �÷��̾� �Ҵ�
        player = GameObject.FindGameObjectWithTag("Player");
        // �� �Ҵ�
        gun = FindObjectOfType<Gun>();
        // Ÿ�� �Ҵ�
        zeroPaper = FindObjectOfType<ZeroTarget>();
        // ���� ���ӿ�����Ʈ ���� �ִ� �����̼� ������Ʈ ��������
        narrator = GetComponent<ShootingNarrationSound>();
        StartCoroutine(StartShooting());
    }

    // ��ŵ ���θ� Ȯ���ϱ� ���� time�� ���� �÷��̾��� �Է°��� ������.
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
        PlayerMove playerMove = player.GetComponent<PlayerMove>();
        Coroutine coroutine;
        Indicator.text = "��� ����";
        narrator.PlayEntrance();
        yield return new WaitForSeconds(1.5f);
        Indicator.text = "�ڽ��� ��θ�\n��ġ�鼭 �����մϴ�.";
        coroutine = StartCoroutine(playerMove.EnteringShootingLane());
        yield return SkipInputCheckForSeconds(9.0f);
        // ��ŵ��ư�� ������ �����Ͽ��� ��
        if (playerSkip)
        {
            StopCoroutine(coroutine);
            playerMove.GoToShootingLane();
        }
    }

    // ��� �غ� �ܰ�
    IEnumerator GetReadyToShot()
    {
        Indicator.text = "��� �غ�...";
        narrator.PlaySetProne();
        Indicator.text = "��� ����� ��";
        yield return SkipInputCheckForSeconds(2.0f);
        player.GetComponent<PlayerMove>().AssumingPronePosition();
        do
        {
            if (!playerSkip)
            {
                // �λ�� ź���� �ΰ�
                Indicator.text = "�λ�� ź���� �ΰ�\n(�»�ź 5�� �̻�)";
                narrator.PlayTakeOverMagazine();
                yield return SkipInputCheckForSeconds(3.0f);
                if (playerSkip) break;
                // ��� ź���� ����
                Indicator.text = "��� ź���� ����";
                narrator.PlayCombineMagazine();
                yield return SkipInputCheckForSeconds(4.0f);
                if (playerSkip) break;
                // ź���Ϲ�����
                Indicator.text = "ź�� �Ϲ� ����";
                narrator.PlayLoadShot();
                yield return SkipInputCheckForSeconds(2.5f);
                if (playerSkip) break;
                // ������ �ܹ�
                Indicator.text = "������ �ܹ�";
                narrator.PlaySetSingle();
                yield return SkipInputCheckForSeconds(2.0f);
                if (playerSkip) break;
            }
        } while (false);
        // ź�� ����
        gun.Reload(AMMO);
        Indicator.text = "��� ����";
        narrator.PlayInitiateShot();
        yield return new WaitForSeconds(3.0f);
    }

    // �߻� ���� �˷��ִ� �λ�� ����
    IEnumerator ShotCounting()
    {
        int lastAmmo = AMMO;
        while (gun.Ammo > 0)
        {
            if (gun.Ammo < lastAmmo)
            {
                Indicator.text = $"{AMMO - gun.Ammo}��";
                lastAmmo = gun.Ammo;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    // ��� ���� ����
    IEnumerator ShotEnd()
    {
        Indicator.text = "��� ����";
        narrator.PlayShotEnd();
        yield return new WaitForSeconds(2.0f);
        // ������ ����
        Indicator.text = "������ ����";
        narrator.PlaySetSafe();
        yield return new WaitForSeconds(2.0f);
        // ź���� ����(����)
        Indicator.text = "ź���� ����";
        narrator.PlayDetachMagazine();
        yield return new WaitForSeconds(2.0f);
        // ��� ���ѳ��� �����ɾƴ��
        Indicator.text = "��� ���ѳ��� �����ɾƴ��";
        narrator.PlayLayGunAndSit();
        yield return new WaitForSeconds(2.0f);
        player.GetComponent<PlayerMove>().GetSittingPosition();
    }

    IEnumerator StartShooting()
    {
        // �÷��̾ �����ϱ� ���� ��ũ��Ʈ
        var playerController = player.GetComponent<PlayerController>();
        // ���� ������ ���̷� off
        playerController.SetGyroEnabled(false);
        playerController.SetStaringModeEnabled(false);
        // ��� ����
        yield return EnteringShootingLane();
        yield return SkipInputCheckForSeconds(2.0f);
        playerController.SetGyroEnabled(true);
        // 5�߾� 3�� ���
        for (int i = 0; i < 3; i++)
        {
            // ��� �غ�
            zeroPaper.MoveToSet();
            yield return GetReadyToShot();
            StartCoroutine(ShotCounting());
            Indicator.text = "��� ����";
            playerController.SetStaringModeEnabled(true);
            while (gun.Ammo > 0)
            {
                yield return new WaitForSeconds(1.0f);
            }
            yield return ShotEnd();
            // ammo���� ���� ���� �ʾ��� ��츦 ���.
            if (zeroPaper.HitPoints.Count < AMMO)
            {
                Indicator.text = $"{JudgePoints(zeroPaper.HitPoints)}";
            }
            else
            {
                int len = zeroPaper.HitPoints.Count;
                _result = JudgePointsBetter(zeroPaper.HitPoints.GetRange(len - AMMO, AMMO));
                if (_result == JudgeResult.����ȹ��) gotZero = true;
            }
            yield return StartClickSetting();
        }
        StartCoroutine(Utility.MoveTo(RetryMenu.transform, RetryMenu.transform.position + new Vector3(0, 0, 20), 1.0f));
        StartCoroutine(Utility.MoveTo(MainMenu.transform, MainMenu.transform.position + new Vector3(0, 0, 20), 1.0f));
        // gotZero == true ���߸� �ǰŸ������ ������
        if (gotZero)
        {
            RealShotMenu.SetActive(true);
            StartCoroutine(Utility.MoveTo(RealShotMenu.transform, new Vector3(0, 6, 25), 0.2f));
        }
    }

    // ������ Ŭ��ũ �������� �������� ����
    private bool isSettingDone;
    IEnumerator StartClickSetting()
    {
        yield return new WaitForSeconds(1.0f);
        Indicator.text = "ǥ���� Ȯ��";
        narrator.PlayCheckPaper();
        yield return new WaitForSeconds(2.0f);
        if(!ClickSettingMenu.activeSelf) ClickSettingMenu.GetComponent<ToggleObject>().Toggle();
        zeroPaper.MoveToPlayer();
        Indicator.text = $"{_result}";
        isSettingDone = false;
        // ���� źâ (���� ���� ũ��ũ �����ϱ� ������)
        gun.Reload(-1);
        // ���� ũ��ũ ���� ����
        while (!isSettingDone)
        {
            // Ŭ��ũ ����â�� ������ �װɷ� ��
            isSettingDone = !ClickSettingMenu.activeSelf;
            yield return new WaitForSeconds(1.0f);
        }
        gun.Reload(0);
        yield return null;
    }

    // JudgePoints�� ��ȯ��.
    enum JudgeResult
    {
        // ȣ�� �ҷ�(���� �����)
        ȣ��ҷ�,
        // �ݹ� �ҷ�(�¿� �����)
        �ݹߺҷ�,
        // ��ź(�����¿�� �����)
        ��ź,
        // ź���� ����(�׷��� ����� ������ ���� ����)
        ź��������,
        // ���� ȹ��
        ����ȹ��,
    }
    // ���� �������� ���� ź������ �������, ������ ȹ���ߴ��� ���� �Ǵ���.
    JudgeResult JudgePoints(List<Vector3> points)
    {
        // ���� ���� x, y
        Vector2 _min = new Vector2(float.MaxValue, float.MaxValue);
        // ���� ū x, y
        Vector2 _max = new Vector2(float.MinValue, float.MinValue);
        // ��� ����
        Vector2 _avg = new Vector2(0, 0);
        // ĿƮ���� (5ũ��ũ)
        float _cutOffPoint = 0.075f;
        // �߾� ����(�÷��̾�)
        Vector2 _center = gun.transform.position;
        foreach(Vector3 _point in points)
        {
            _min.x = Mathf.Min(_min.x, _point.x);
            _min.y = Mathf.Min(_min.y, _point.y);
            _max.x = Mathf.Max(_max.x, _point.x);
            _max.y = Mathf.Max(_max.y, _point.y);
            _avg += new Vector2(_point.x, _point.y);
        }
        _avg *= Mathf.Pow(points.Count, -1);
        // ź������ �¿�� �а� �����Ǿ��°�?
        bool isWide = (_max.x - _min.x) > _cutOffPoint;
        // ź������ ���Ϸ� �а� �����Ǿ��°�?
        bool isHigh = (_max.y - _min.y) > _cutOffPoint;
        // ��� ��ȯ
        if (isWide && isHigh) return JudgeResult.��ź;
        else if (isWide) return JudgeResult.�ݹߺҷ�;
        else if (isHigh) return JudgeResult.ȣ��ҷ�;
        // _avg�� _center�� ()�Ÿ��� ĿƮ���� ���ϸ� ����ȹ��. �ƴ϶�� �׳� ź���� ����.
        Debug.Log($"_avg : ({_avg.x}, {_avg.y}), _center : ({_center.x}, {_center.y})");
        Debug.Log($"_avg to _center : {Vector2.Distance(_avg, _center)}");
        if (Vector2.Distance(_avg, _center) <= _cutOffPoint) return JudgeResult.����ȹ��;
        else return JudgeResult.ź��������;
    }

    // ǥ������(StdDev)�� �̿��Ͽ� ���� �� ��Ȯ�ϰ� �Ǵ��ϴ� �޼���
    private Vector2 _avg; 
    JudgeResult JudgePointsBetter(List<Vector3> points)
    {
        // ��� ����
        _avg = new Vector2(0, 0);
        // ĿƮ���� (�߾����κ��� 4ũ��ũ �̳��� �־�� ������ ȹ���ߴٰ� ����)
        float _cutOffPoint = 0.060f;
        // �߾� ����(�÷��̾ ���� �������� ���� ���� ��, ź���� �ݿ�)
        Vector2 _curPos = new Vector2(0, 0.5f);
        Vector2 _center = new Vector2(_curPos.x, _curPos.y) - new Vector2(0, 0.015f);
        foreach (Vector3 _point in points)
        {
            _avg += new Vector2(_point.x, _point.y);
        }
        _avg *= Mathf.Pow(points.Count, -1);
        float stdDevX = 0;
        float stdDevY = 0;
        foreach (Vector3 _point in points)
        {
            stdDevX += Mathf.Pow(_point.x - _avg.x, 2);
            stdDevY += Mathf.Pow(_point.y - _avg.y, 2);
        }
        stdDevX /= (points.Count - 1);
        stdDevY /= (points.Count - 1);
        stdDevX = Mathf.Sqrt(stdDevX);
        stdDevY = Mathf.Sqrt(stdDevY);
        Debug.Log($"X�� ǥ������ : {stdDevX}, Y�� ǥ������ : {stdDevY}");
        // ��� ����
        // ǥ�������� ĿƮ����
        const float STD_DEV_CUT = 0.03f;
        // ź������ �¿�� �а� �����Ǿ��°�?
        bool isWide = stdDevX > STD_DEV_CUT;
        // ź������ ���Ϸ� �а� �����Ǿ��°�?
        bool isHigh = stdDevY > STD_DEV_CUT;
        // ��� ��ȯ
        if (isWide && isHigh) return JudgeResult.��ź;
        else if (isWide) return JudgeResult.�ݹߺҷ�;
        else if (isHigh) return JudgeResult.ȣ��ҷ�;
        // _avg�� _center�� ()�Ÿ��� ĿƮ���� ���ϸ� ����ȹ��. �ƴ϶�� �׳� ź���� ����.
        Debug.Log($"_avg : ({_avg.x}, {_avg.y}), _center : ({_center.x}, {_center.y})");
        Debug.Log($"_avg to _center : {Vector2.Distance(_avg, _center)}");
        if (Vector2.Distance(_avg, _center) <= _cutOffPoint) return JudgeResult.����ȹ��;
        else return JudgeResult.ź��������;
    }

    // _avg�� ������� �ڵ����� ũ��ũ ����
    public void AutoSetClick()
    {
        // �߾� ����(�÷��̾ ���� �������� ���� �� ��)
        Vector2 _curPos = new Vector2(0, 0.5f);
        Vector2 _center = new Vector2(_curPos.x, _curPos.y) - new Vector2(0, 0.015f);
        // �� ĭ������ �Ÿ��� 0.015����(������ 7mm���� ũ�⸦ ����Ͽ�...)
        const float DISTANCE_PER_CLICK = 0.015f;
        // ���� �����ؾ� �� ũ��ũ ��
        int dx = Mathf.FloorToInt((_center.x - _avg.x) / DISTANCE_PER_CLICK);
        int dy = Mathf.FloorToInt((_center.y - _avg.y) / DISTANCE_PER_CLICK);
        Debug.Log($"_center.y = {_center.y}, _avg.y = {_avg.y}, dy = {dy}");
        // ũ��ũ �� ����
        GameManager.instance.Click += new Vector2(dx, dy);
        // ũ��ũ ���� ����
        string _hori = dx < 0 ? "��" : "��";
        string _vert = dy < 0 ? "��" : "��";
        Indicator.text = $"ũ��ũ �� �ڵ� ����\n{_hori} {Mathf.Abs(dx)}ũ��ũ, {_vert}  {Mathf.Abs(dy)}ũ��ũ";
    }
}
