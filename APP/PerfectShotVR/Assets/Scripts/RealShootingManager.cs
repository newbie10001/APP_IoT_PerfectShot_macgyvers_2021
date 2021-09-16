using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 실거리 사격 매니저
public class RealShootingManager : MonoBehaviour
{
    // 씬 내에 있는 총
    Gun gun;
    // 20발 장전
    private int _ammo = 20;
    // 타겟들의 매니저들을 리스트에 두어서 관리함.
    public List<HumanoidTargetManager> targets;
    // 부사수 역할을 해주는 인디케이터
    public Text Indicator;
    // 사격 실시 절차를 밟는지 여부 off이면 바로 시작
    public bool useShootingSequence;
    // 사격 실시 절차
    readonly string[] _shootingSeq = { "탄알집 인계", "우상탄 이상무", "탄알집 결합", "탄알 일발 장전", "조정간 단발" };
    // 사격 종료 절차
    readonly string[] _shootEndingSeq = { "사격 끝", "조정간 안전", "탄알집 제거", "소총 놓고 무릎앉아대기", "" };
    // 타겟들을 세우는 순서. 가까운거(100m)부터 0번.
    readonly string[] _targetName = { "100m", "200m", "250m"};
    readonly int[] _targetSeq = { 0, 1, 0, 1, 0, 1, 0, 1, 0, 2, 1, 0, 1, 0, 1, 0, 1, 0, 1, 2 };
    // 타겟마다 세워지는 시간 100m : 5sec, 200m : 7sec, 250m : 10sec
    readonly int[] _times = { 5, 7, 10 };
    // 결과가 나올 때 세워질 포지션
    readonly Vector3[] resultPos = { new Vector3(-2.5f, 1f, 20f), new Vector3(0, 1f, 20f), new Vector3(2.5f, 1f, 20f) };
    // 테스트용
    // 등급(0급(특급), 1급, 2급, 3급)
    readonly int[] _grade = { 18, 16, 14, 12 };
    // 점수(맞춘 횟수)
    public int Score { get; private set; }
    // 최고 점수
    int HighScore
    {
        get => PlayerPrefs.GetInt("RealShootingHighScore");
        set
        {
            // 혹시나 할 오류를 대비
            if(value > HighScore) PlayerPrefs.SetInt("RealShootingHighScore", value);
        }
    }
    // 결과를 보여주기 위한 3D 텍스트
    public GameObject ResultText;
    // 메뉴들 (다시하기, 메인으로)
    public GameObject RetryMenuItem;
    public GameObject MainMenuItem;

    
    void Start()
    {
        // 현재 씬에 있는 총을 할당.
        gun = FindObjectOfType<Gun>();
        // 탄알의 개수는 맞출 타겟의 갯수만큼
        _ammo = _targetSeq.Length;
        // 점수 초기화.
        Score = 0;
        StartCoroutine(StartShooting());
    }

    IEnumerator StartShooting()
    {
        #region 사격 준비
        Indicator.text = "사격 준비...";
        if (useShootingSequence)
        {
            foreach (string ment in _shootingSeq)
            {
                Indicator.text = ment;
                yield return new WaitForSeconds(3.0f);
            }
        }
        // 탄알 장전
        gun.Reload(_ammo);
        Indicator.text = "사격 개시";
        yield return new WaitForSeconds(3.0f);
        #endregion
        StartCoroutine(ShotCheck());
        // 타겟을 순서에 따라 세움.
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
            Indicator.text = "잔탄 사격";
            yield return new WaitForSeconds(1.0f);
        }
        Indicator.text = "사격 종료";
        // 사격 끝. 이제 결과창을 보여줘야 함.
        ShowResult();
    }

    // 발사 개수 알려주는 부사수 역할
    IEnumerator ShotCheck()
    {
        int lastAmmo = _ammo;
        while(gun.Ammo > 0)
        {
            if (gun.Ammo < lastAmmo) 
            {
                Indicator.text = $"{_ammo - gun.Ammo}발";
                lastAmmo = gun.Ammo;
            }
            yield return new WaitForSeconds(0.5f);
        }
        // 사격 종료 절차
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
    /// 결과를 보여주는 메서드.
    /// 플레이어 앞에 표적지들이 날아와서 결과를 보여줌.
    /// 3D 텍스트를 이용.
    /// 위에는 '메인메뉴로', '다시하기' 메뉴가 나타남.
    /// </summary>
    void ShowResult()
    {
        Debug.Log("사격 끝. ShowResult 호출");
        // 스코어 다시 갱신
        UpdateScore();
        // 무한 탄창 적용
        gun.Reload(-1);
        // 하이스코어 갱신
        if(Score > HighScore) HighScore = Score;
        // 타겟마다 결과를 보여주는 과정
        // 위로 날려보낸 후 스무스하게 내려옴
        for(int i = 0; i < targets.Count; i++)
        {
            targets[i].OnlyGetUp();
            StartCoroutine(MoveTo(targets[i].transform, resultPos[i], 1f));
        }
        targets[0].ShowResult($"{targets[0].Hits.Count} / 9");
        targets[1].ShowResult($"{targets[1].Hits.Count} / 9");
        targets[2].ShowResult($"{targets[2].Hits.Count} / 2");

        ResultText.GetComponent<TextMesh>().text = $"결과 : {Score} / 20 ({GetGrade(Score)})\n하이스코어 : {HighScore} ({GetGrade(HighScore)})";
        // 3D 메뉴 아이템은 추후 구현 예정
        // StartCoroutine(MoveTo(RetryMenuItem.transform, new Vector3(-2, 7, 40), 1f));
        // StartCoroutine(MoveTo(MainMenuItem.transform, new Vector3(2, 7, 40), 1f));
        // StartCoroutine(ThrowMenuItems());
    }

    // 사격 등급을 문자열로 반환.
    string GetGrade(int Score)
    {
        if (Score > _grade[0])
        {
            return "특급!";
        }
        else if (Score > _grade[1]) return "1급";
        else if (Score > _grade[2]) return "2급";
        else if (Score > _grade[3]) return "3급";
        return "탈락";
    }

    // 오브젝트를 옮기는 메서드
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

    // 메뉴 아이템들을 던지는 메서드
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