using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 총괄적인 게임 매니저로서, 게임의 기본 설정이나 사용자 상호작용 메뉴를 관리한다.
// 게임 종류에 따라 서브매니저를 호출해 이용할 수 있도록 한다.
public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }

            return m_instance;
        }
    }
    private static GameManager m_instance;
    #endregion
    /// <summary>
    /// 설정값들은 프로퍼티로 구현
    /// </summary>
    #region setting values
    // 카메라 시프트 초기값
    public Vector2 InitialCameraShift
    {
        get
        {
            return GetVector2("InitialCameraShift");
        }
        set
        {
            SetVector2("InitialCameraShift", value);
        }
    }
    // 총의 크리크 값 조정. 각각 좌우, 상하
    public Vector2 Click
    {
        get
        {
            return GetVector2("Click");
        }
        set
        {
            SetVector2("Click", value);
        }
    }
    /// <summary>
    /// bool 값을 int로 받아온다.
    /// 0 : 설정되지 않음. 기본값 할당
    /// 1 : true
    /// -1 : false
    /// </summary>
    // 가늠쇠 UI 활성화 옵션. 0이면 false 반환
    public bool FrontSightEnabled
    {
        get
        {
            int tmp = PlayerPrefs.GetInt("FrontSightEnabled");
            int val = tmp == 0 ? 1 : tmp;
            return val == 1;
        }
        set
        {
            if (value) PlayerPrefs.SetInt("FrontSightEnabled", 1);
            else PlayerPrefs.SetInt("FrontSightEnabled", -1);
        }
    }
    // 총의 활성화 여부
    public bool GunState
    {
        get
        {
            int tmp = PlayerPrefs.GetInt("GunState");
            int val = tmp == 0 ? 1 : tmp;
            return val == 1;
        }
        set
        {
            if (value) PlayerPrefs.SetInt("GunState", 1);
            else PlayerPrefs.SetInt("GunState", -1);
        }
    }
    // 블루투스 활성화 여부
    public bool Bluetooth
    {
        get
        {
            int tmp = PlayerPrefs.GetInt("Bluetooth");
            int val = tmp == 0 ? -1 : tmp;
            return val == 1;
        }
        set
        {
            if (value) PlayerPrefs.SetInt("Bluetooth", 1);
            else PlayerPrefs.SetInt("Bluetooth", -1);
        }
    }
    // 응시모드 활성화 여부
    public bool StaringMode
    {
        get
        {
            int tmp = PlayerPrefs.GetInt("StaringMode");
            int val = tmp == 0 ? 1 : tmp;
            return val == 1;
        }
        set
        {
            if (value) PlayerPrefs.SetInt("StaringMode", 1);
            else PlayerPrefs.SetInt("StaringMode", -1);
        }
    }
    #endregion

    // 설정값으로 Vector2를 저장하기 위한 정적 메서드.
    public static void SetVector2(string key, Vector2 vector2)
    {
        PlayerPrefs.SetFloat(key + "X", vector2.x);
        PlayerPrefs.SetFloat(key + "Y", vector2.y);
        Debug.Log($"{key} 설정 : ({vector2.x}, {vector2.y})");
    }
    public static Vector2 GetVector2(string key)
    {
        Vector2 ret;
        ret.x = PlayerPrefs.GetFloat(key + "X");
        ret.y = PlayerPrefs.GetFloat(key + "Y");
        return ret;
    }

    void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }

        // 화면 꺼짐 방지
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
