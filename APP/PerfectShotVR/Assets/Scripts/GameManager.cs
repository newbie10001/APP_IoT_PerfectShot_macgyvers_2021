using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѱ����� ���� �Ŵ����μ�, ������ �⺻ �����̳� ����� ��ȣ�ۿ� �޴��� �����Ѵ�.
// ���� ������ ���� ����Ŵ����� ȣ���� �̿��� �� �ֵ��� �Ѵ�.
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
    /// ���������� ������Ƽ�� ����
    /// </summary>
    #region setting values
    // ī�޶� ����Ʈ �ʱⰪ
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
    // ���� ũ��ũ �� ����. ���� �¿�, ����
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
    /// bool ���� int�� �޾ƿ´�.
    /// 0 : �������� ����. �⺻�� �Ҵ�
    /// 1 : true
    /// -1 : false
    /// </summary>
    // ���Ƽ� UI Ȱ��ȭ �ɼ�. 0�̸� false ��ȯ
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
    // ���� Ȱ��ȭ ����
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
    // ������� Ȱ��ȭ ����
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
    // ���ø�� Ȱ��ȭ ����
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

    // ���������� Vector2�� �����ϱ� ���� ���� �޼���.
    public static void SetVector2(string key, Vector2 vector2)
    {
        PlayerPrefs.SetFloat(key + "X", vector2.x);
        PlayerPrefs.SetFloat(key + "Y", vector2.y);
        Debug.Log($"{key} ���� : ({vector2.x}, {vector2.y})");
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

        // ȭ�� ���� ����
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
