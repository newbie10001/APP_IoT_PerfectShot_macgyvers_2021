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
    // �÷��̾ ��� �ִ� ��
    private Gun gun;
    // �� �ϸ��� ������ ź�� ��
    const int AMMO = 5;
    // ũ��ũ ���� �޴�â (3D)
    public GameObject ClickSettingMenu;
    // �ȳ� UI
    public Text Indicator;
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
        gun = FindObjectOfType<Gun>();
        zeroPaper = FindObjectOfType<ZeroTarget>();
        StartCoroutine(StartShooting());
    }

    IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(1.0f);
        // 5�߾� 3�� ���
        for(int i = 0; i < 3; i++)
        {
            zeroPaper.MoveToSet();
            gun.Reload(AMMO);
            Indicator.text = "��� ����";
            while (gun.Ammo > 0)
            {
                yield return new WaitForSeconds(1.0f);
            }
            // ammo���� ���� ���� �ʾ��� ��츦 ���.
            if(zeroPaper.HitPoints.Count < AMMO)
            {
                Indicator.text = $"{JudgePoints(zeroPaper.HitPoints)}";
            }
            else
            {
                int len = zeroPaper.HitPoints.Count;
                JudgeResult result = JudgePointsBetter(zeroPaper.HitPoints.GetRange(len - AMMO, AMMO));
                if (result == JudgeResult.����ȹ��) gotZero = true;
                Indicator.text = $"{result}";
            }
            yield return StartClickSetting();
        }
        StartCoroutine(Utility.MoveTo(RetryMenu.transform, RetryMenu.transform.position + new Vector3(0, 0, 20), 1.0f));
        StartCoroutine(Utility.MoveTo(MainMenu.transform, MainMenu.transform.position + new Vector3(0, 0, 20), 1.0f));
        // gotZero == true ���߸� �ǰŸ������ ������
        if (gotZero)
        {
            RealShotMenu.SetActive(true);
            StartCoroutine(Utility.MoveTo(RealShotMenu.transform, new Vector3(0, 4, 25), 0.5f));
        }
    }

    // ������ Ŭ��ũ �������� �������� ����
    private bool isSettingDone;
    IEnumerator StartClickSetting()
    {
        if(!ClickSettingMenu.activeSelf) ClickSettingMenu.GetComponent<ToggleObject>().Toggle();
        zeroPaper.MoveToPlayer();
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
        float _cutOffPoint = 0.035f;
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
        // ĿƮ���� (�߾����κ��� 5ũ��ũ �̳��� �־�� ������ ȹ���ߴٰ� ����)
        float _cutOffPoint = 0.075f;
        // �߾� ����(�÷��̾ ���� �������� ���� ���� ��, ź���� �ݿ�)
        Vector3 _curPos = GameObject.FindWithTag("Player").transform.position;
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
        const float STD_DEV_CUT = 0.018f;
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
        Vector3 _curPos = GameObject.FindWithTag("Player").transform.position;
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
