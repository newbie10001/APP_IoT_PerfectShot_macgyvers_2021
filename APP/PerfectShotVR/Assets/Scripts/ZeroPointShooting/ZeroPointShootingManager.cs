using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 영점 사격 매니저(총괄)
/// <summary>
/// 역할 : n발씩 3번 쏘게한 후 표적을 보여주고, 크리크 수정을 할 수 있도록 함. 크리크 수정 UI를 띄워야 함.
/// 영점획득 여부를 알려줌. 사격 문제점을 피드백함(나중에 구현).
/// 영점사격이 끝나면 '메인으로', '다시하기' 라는 메뉴아이템이 활성화됨.
/// 그때 영점이 획득되면 실거리 사격 메뉴아이템을 활성화함.
/// </summary>
public class ZeroPointShootingManager : MonoBehaviour
{
    // 씬 내에 있는 플레이어
    private GameObject player;
    // 플레이어가 들고 있는 총
    private Gun gun;
    // 한 턴마다 장전할 탄알 수
    const int AMMO = 5;
    // 크리크 세팅 메뉴창 (3D)
    public GameObject ClickSettingMenu;
    // 가장 최근에 판정한 결과.
    private JudgeResult _result;
    // 안내 UI
    public Text Indicator;
    // 사운드를 재생하는 스크립트
    private ShootingNarrationSound narrator;
    // 영점사격지
    private ZeroTarget zeroPaper;
    // 영점을 획득했는지 여부
    bool gotZero = false;
    // 메뉴들
    public GameObject RetryMenu;
    public GameObject MainMenu;
    public GameObject RealShotMenu;

    void Start()
    {
        // 플레이어 할당
        player = GameObject.FindGameObjectWithTag("Player");
        // 총 할당
        gun = FindObjectOfType<Gun>();
        // 타겟 할당
        zeroPaper = FindObjectOfType<ZeroTarget>();
        // 현재 게임오브젝트 내에 있는 내레이션 컴포넌트 가져오기
        narrator = GetComponent<ShootingNarrationSound>();
        StartCoroutine(StartShooting());
    }

    // 스킵 여부를 확인하기 위해 time초 동안 플레이어의 입력값을 관찰함.
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
                Debug.Log("장면 스킵");
                playerSkip = true;
                break;
            }
            if (t > endTime) break;
            yield return null;
        }
    }

    // 사로 입장 및 엎드려쏴
    IEnumerator EnteringShootingLane()
    {
        PlayerMove playerMove = player.GetComponent<PlayerMove>();
        Coroutine coroutine;
        Indicator.text = "사로 입장";
        narrator.PlayEntrance();
        yield return new WaitForSeconds(1.5f);
        Indicator.text = "자신의 사로를\n외치면서 입장합니다.";
        coroutine = StartCoroutine(playerMove.EnteringShootingLane());
        yield return SkipInputCheckForSeconds(9.0f);
        // 스킵버튼이 눌려서 도착하였을 때
        if (playerSkip)
        {
            StopCoroutine(coroutine);
            playerMove.GoToShootingLane();
        }
    }

    // 사격 준비 단계
    IEnumerator GetReadyToShot()
    {
        Indicator.text = "사격 준비...";
        narrator.PlaySetProne();
        Indicator.text = "사수 엎드려 쏴";
        yield return SkipInputCheckForSeconds(2.0f);
        player.GetComponent<PlayerMove>().AssumingPronePosition();
        do
        {
            if (!playerSkip)
            {
                // 부사수 탄알집 인계
                Indicator.text = "부사수 탄알집 인계\n(좌상탄 5발 이상무)";
                narrator.PlayTakeOverMagazine();
                yield return SkipInputCheckForSeconds(3.0f);
                if (playerSkip) break;
                // 사수 탄알집 결합
                Indicator.text = "사수 탄알집 결합";
                narrator.PlayCombineMagazine();
                yield return SkipInputCheckForSeconds(4.0f);
                if (playerSkip) break;
                // 탄알일발장전
                Indicator.text = "탄알 일발 장전";
                narrator.PlayLoadShot();
                yield return SkipInputCheckForSeconds(2.5f);
                if (playerSkip) break;
                // 조정간 단발
                Indicator.text = "조정간 단발";
                narrator.PlaySetSingle();
                yield return SkipInputCheckForSeconds(2.0f);
                if (playerSkip) break;
            }
        } while (false);
        // 탄알 장전
        gun.Reload(AMMO);
        Indicator.text = "사격 개시";
        narrator.PlayInitiateShot();
        yield return new WaitForSeconds(3.0f);
    }

    // 발사 개수 알려주는 부사수 역할
    IEnumerator ShotCounting()
    {
        int lastAmmo = AMMO;
        while (gun.Ammo > 0)
        {
            if (gun.Ammo < lastAmmo)
            {
                Indicator.text = $"{AMMO - gun.Ammo}발";
                lastAmmo = gun.Ammo;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    // 사격 종료 절차
    IEnumerator ShotEnd()
    {
        Indicator.text = "사격 종료";
        narrator.PlayShotEnd();
        yield return new WaitForSeconds(2.0f);
        // 조정간 안전
        Indicator.text = "조정간 안전";
        narrator.PlaySetSafe();
        yield return new WaitForSeconds(2.0f);
        // 탄알집 제거(분해)
        Indicator.text = "탄알집 제거";
        narrator.PlayDetachMagazine();
        yield return new WaitForSeconds(2.0f);
        // 사수 소총놓고 무릎앉아대기
        Indicator.text = "사수 소총놓고 무릎앉아대기";
        narrator.PlayLayGunAndSit();
        yield return new WaitForSeconds(2.0f);
        player.GetComponent<PlayerMove>().GetSittingPosition();
    }

    IEnumerator StartShooting()
    {
        // 플레이어를 조작하기 위한 스크립트
        var playerController = player.GetComponent<PlayerController>();
        // 시작 전에는 자이로 off
        playerController.SetGyroEnabled(false);
        playerController.SetStaringModeEnabled(false);
        // 사로 입장
        yield return EnteringShootingLane();
        yield return SkipInputCheckForSeconds(2.0f);
        playerController.SetGyroEnabled(GameManager.instance.StaringMode);
        // 5발씩 3번 사격
        for (int i = 0; i < 3; i++)
        {
            // 사격 준비
            zeroPaper.MoveToSet();
            yield return GetReadyToShot();
            StartCoroutine(ShotCounting());
            Indicator.text = "사격 개시";
            playerController.SetStaringModeEnabled(GameManager.instance.StaringMode);
            while (gun.Ammo > 0)
            {
                yield return new WaitForSeconds(1.0f);
            }
            yield return ShotEnd();
            // ammo발이 전부 맞지 않았을 경우를 대비.
            if (zeroPaper.HitPoints.Count < AMMO)
            {
                Indicator.text = $"{JudgePoints(zeroPaper.HitPoints)}";
            }
            else
            {
                int len = zeroPaper.HitPoints.Count;
                _result = JudgePointsBetter(zeroPaper.HitPoints.GetRange(len - AMMO, AMMO));
                if (_result == JudgeResult.영점획득) gotZero = true;
            }
            yield return StartClickSetting();
        }
        gun.Reload(-1);
        StartCoroutine(Utility.MoveTo(RetryMenu.transform, RetryMenu.transform.position + new Vector3(0, 0, 20), 1.0f));
        StartCoroutine(Utility.MoveTo(MainMenu.transform, MainMenu.transform.position + new Vector3(0, 0, 20), 1.0f));
        // gotZero == true 여야만 실거리사격을 제안함
        if (gotZero)
        {
            RealShotMenu.SetActive(true);
            StartCoroutine(Utility.MoveTo(RealShotMenu.transform, new Vector3(0, 6, 25), 0.2f));
        }
    }

    // 각각의 클리크 수정마다 끝났는지 여부
    private bool isSettingDone;
    IEnumerator StartClickSetting()
    {
        yield return new WaitForSeconds(1.0f);
        Indicator.text = "표적지 확인";
        narrator.PlayCheckPaper();
        yield return new WaitForSeconds(2.0f);
        if (!ClickSettingMenu.activeSelf) ClickSettingMenu.GetComponent<ToggleObject>().Toggle();
        zeroPaper.MoveToPlayer();
        Indicator.text = $"{_result}";
        isSettingDone = false;
        // 무한 탄창 (총을 쏴서 크리크 수정하기 떄문에)
        gun.Reload(-1);
        // 이제 크리크 수정 구현
        while (!isSettingDone)
        {
            // 클리크 세팅창이 꺼지면 그걸로 끝
            isSettingDone = !ClickSettingMenu.activeSelf;
            yield return new WaitForSeconds(1.0f);
        }
        gun.Reload(0);
        yield return null;
    }

    // JudgePoints의 반환값.
    enum JudgeResult
    {
        // 호흡 불량(상하 흩어짐)
        호흡불량,
        // 격발 불량(좌우 흩어짐)
        격발불량,
        // 산탄(상하좌우로 흩어짐)
        산탄,
        // 탄착군 형성(그러나 가운데에 맞지는 않은 상태)
        탄착군형성,
        // 영점 획득
        영점획득,
    }
    // 맞은 지점들을 보고 탄착군이 생겼는지, 영점을 획득했는지 등을 판단함.
    JudgeResult JudgePoints(List<Vector3> points)
    {
        // 가장 작은 x, y
        Vector2 _min = new Vector2(float.MaxValue, float.MaxValue);
        // 가장 큰 x, y
        Vector2 _max = new Vector2(float.MinValue, float.MinValue);
        // 평균 지점
        Vector2 _avg = new Vector2(0, 0);
        // 커트라인 (5크리크)
        float _cutOffPoint = 0.075f;
        // 중앙 지점(플레이어)
        Vector2 _center = gun.transform.position;
        foreach (Vector3 _point in points)
        {
            _min.x = Mathf.Min(_min.x, _point.x);
            _min.y = Mathf.Min(_min.y, _point.y);
            _max.x = Mathf.Max(_max.x, _point.x);
            _max.y = Mathf.Max(_max.y, _point.y);
            _avg += new Vector2(_point.x, _point.y);
        }
        _avg *= Mathf.Pow(points.Count, -1);
        // 탄착군이 좌우로 넓게 형성되었는가?
        bool isWide = (_max.x - _min.x) > _cutOffPoint;
        // 탄착군이 상하로 넓게 형성되었는가?
        bool isHigh = (_max.y - _min.y) > _cutOffPoint;
        // 결과 반환
        if (isWide && isHigh) return JudgeResult.산탄;
        else if (isWide) return JudgeResult.격발불량;
        else if (isHigh) return JudgeResult.호흡불량;
        // _avg와 _center의 ()거리가 커트라인 이하면 영점획득. 아니라면 그냥 탄착군 형성.
        Debug.Log($"_avg : ({_avg.x}, {_avg.y}), _center : ({_center.x}, {_center.y})");
        Debug.Log($"_avg to _center : {Vector2.Distance(_avg, _center)}");
        if (Vector2.Distance(_avg, _center) <= _cutOffPoint) return JudgeResult.영점획득;
        else return JudgeResult.탄착군형성;
    }

    // 표준편차(StdDev)를 이용하여 보다 더 정확하게 판단하는 메서드
    private Vector2 _avg;
    JudgeResult JudgePointsBetter(List<Vector3> points)
    {
        // 평균 지점
        _avg = new Vector2(0, 0);
        // 커트라인 (중앙으로부터 4크리크 이내에 있어야 영점을 획득했다고 판정)
        float _cutOffPoint = 0.060f;
        // 중앙 지점(플레이어가 직선 정면으로 총을 쐈을 때, 탄도학 반영)
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
        Debug.Log($"X축 표준편차 : {stdDevX}, Y축 표준편차 : {stdDevY}");
        // 결과 판정
        // 표준편차의 커트라인
        const float STD_DEV_CUT = 0.03f;
        // 탄착군이 좌우로 넓게 형성되었는가?
        bool isWide = stdDevX > STD_DEV_CUT;
        // 탄착군이 상하로 넓게 형성되었는가?
        bool isHigh = stdDevY > STD_DEV_CUT;
        // 결과 반환
        if (isWide && isHigh) return JudgeResult.산탄;
        else if (isWide) return JudgeResult.격발불량;
        else if (isHigh) return JudgeResult.호흡불량;
        // _avg와 _center의 ()거리가 커트라인 이하면 영점획득. 아니라면 그냥 탄착군 형성.
        Debug.Log($"_avg : ({_avg.x}, {_avg.y}), _center : ({_center.x}, {_center.y})");
        Debug.Log($"_avg to _center : {Vector2.Distance(_avg, _center)}");
        if (Vector2.Distance(_avg, _center) <= _cutOffPoint) return JudgeResult.영점획득;
        else return JudgeResult.탄착군형성;
    }

    // _avg를 기반으로 자동으로 크리크 수정
    public void AutoSetClick()
    {
        // 중앙 지점(플레이어가 직선 전방으로 총을 쏠 때)
        Vector2 _curPos = new Vector2(0, 0.5f);
        Vector2 _center = new Vector2(_curPos.x, _curPos.y) - new Vector2(0, 0.015f);
        // 한 칸마다의 거리는 0.015유닛(현실은 7mm지만 크기를 고려하여...)
        const float DISTANCE_PER_CLICK = 0.015f;
        // 각각 수정해야 할 크리크 값
        int dx = Mathf.FloorToInt((_center.x - _avg.x) / DISTANCE_PER_CLICK);
        int dy = Mathf.FloorToInt((_center.y - _avg.y) / DISTANCE_PER_CLICK);
        Debug.Log($"_center.y = {_center.y}, _avg.y = {_avg.y}, dy = {dy}");
        // 크리크 값 수정
        GameManager.instance.Click += new Vector2(dx, dy);
        // 크리크 수정 방향
        string _hori = dx < 0 ? "좌" : "우";
        string _vert = dy < 0 ? "하" : "상";
        Indicator.text = $"크리크 값 자동 수정\n{_hori} {Mathf.Abs(dx)}크리크, {_vert}  {Mathf.Abs(dy)}크리크";
    }
}
