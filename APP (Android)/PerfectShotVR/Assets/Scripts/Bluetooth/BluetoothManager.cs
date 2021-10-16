using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArduinoBluetoothAPI;

// 블루투스 연결, 입력을 관리함.
public class BluetoothManager : MonoBehaviour
{
    // 싱글톤
    public static BluetoothManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<BluetoothManager>();
                if (m_instance != null) DontDestroyOnLoad(m_instance.gameObject);
            }
            return m_instance;
        }
    }
    private static BluetoothManager m_instance;

    // 아두이노 블루투스 API 활용
    BluetoothHelper bluetoothHelper;
    // 블루투스 기기의 이름
    public string deviceName;
    // 페어링 상태를 나타냄
    public bool IsPaired
    {
        get
        {
            if (bluetoothHelper != null) return bluetoothHelper.isDevicePaired();
            return false;
        }
    }
    // 현재 연결 상태를 나타냄.
    public bool IsConnected
    {
        get
        {
            if (bluetoothHelper != null) return bluetoothHelper.isConnected();
            return false;
        }
    }
    // 현재 블루투스 연결 상태를 나타냄
    public string ConnectionState { get; private set; }
    // 블루투스의 입력 상태(on / off)를 나타냄.
    public bool InputState { get; private set; }
    // 블루투스로 입력한 마지막 시간. 0.3초가 지나면 input을 false로 되돌림.
    float lastTime;

    // 디버그용.
    public bool debugging = false;
    public Text debugText;

    // 안드로이드 블루투스 활성화 함수
    private void setAndroidBluetoothEnabled()
    {
        try
        {
            using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
            {
                try
                {
                    using (var BluetoothManager = activity.Call<AndroidJavaObject>("getSystemService", "bluetooth"))
                    {
                        using (var BluetoothAdaptor = BluetoothManager.Call<AndroidJavaObject>("getAdaptor"))
                        {
                            Debug.Log("set Bluetooth Enabled");
                            BluetoothAdaptor.Call<bool>("enable");
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return;
        }
    }

    private void Update()
    {
        if (Time.time > lastTime + 0.3f) InputState = false;
        if (debugging)
        {
            debugText.text = $"페어링 : {IsPaired} 커넥팅 : {IsConnected}\n디바이스 이름 : {bluetoothHelper.getDeviceName()}" +
                $"\nbluetoothHelper.Available : {bluetoothHelper.Available}\nreadData : {bluetoothHelper.Read()}";
        }
        // 이벤트가 안 먹어서... 어쩔 수 없는 선택.
        if (bluetoothHelper.Read() != "")
        {
            OnMessageReceived();
        }
    }

    void Awake()
    {
        Debug.Log("bluetooth manager awake");
        //setAndroidBluetoothEnabled();
        // 싱글톤!
        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        deviceName = "trigger";
        InstantiateBluetoothHelper();
        // 블루투스 설정이 되어있었다면 커넥트
        if (GameManager.instance.Bluetooth) OnConnectButtonClick();
    }

    private void InstantiateBluetoothHelper()
    {
        try
        {
            bluetoothHelper = BluetoothHelper.GetInstance(deviceName);

            bluetoothHelper.OnConnected += OnConnected;
            bluetoothHelper.OnConnectionFailed += OnConnectionFailed;
            // 씬이 넘어가면서 이벤트가 발생하지 않는 문제 발생
            // 따라서 업데이트 루프에서 이를 해결함.
            bluetoothHelper.OnDataReceived += OnMessageReceived;

            bluetoothHelper.setTerminatorBasedStream("\n");
            ConnectionState = "TryConnectToDevice 성공";
        }
        catch (Exception ex)
        {
            Debug.Log($"블루투스 연결 예외 : {ex}");
        }
    }

    public void SendMessageToDevice(string msg)
    {
        if (bluetoothHelper.Available) bluetoothHelper.SendData(msg);
    }

    public void SetDeviceName(string name)
    {
        if (bluetoothHelper != null)
        {
            deviceName = name;
            bluetoothHelper.setDeviceName(name);
        }
    }

    public void OnConnectButtonClick()
    {
        //TryConnectToDevice();
        Debug.Log("연결 버튼 눌림");
        if (IsPaired && !IsConnected)
        {
            if (bluetoothHelper != null)
            {
                ConnectionState = "기기 이름 반영";
                bluetoothHelper.setDeviceName(deviceName);
                Debug.Log($"현재 기기 이름 : {bluetoothHelper.getDeviceName()}");
                ConnectionState = "연결을 시도합니다...";
                bluetoothHelper.Connect();
            }
        }
    }

    public void OnDisconnectButtonClick()
    {
        ConnectionState = "연결을 해제합니다.";
        if (bluetoothHelper != null) bluetoothHelper.Disconnect();
    }

    public void OnConnected()
    {
        // 성공한 디바이스네임을 저장함.
        PlayerPrefs.SetString("DeviceName", deviceName);
        try
        {
            Debug.Log("블루투스 리스닝 스타트");
            bluetoothHelper.StartListening();
            ConnectionState = "연결 성공";
        }
        catch
        {
            Debug.Log("블루투스 리스닝 실패");
        }
    }

    bool _triedToConnect = false;
    void OnConnectionFailed()
    {
        Debug.Log("연결 실패");
        ConnectionState = "연결이 끊어졌습니다.";
        // 연결 재시도(한번만)
        if (!_triedToConnect) OnConnectButtonClick();
        _triedToConnect = true;
    }

    void OnMessageReceived()
    {
        Debug.Log("블루투스 신호 들어옴");
        InputState = true;
        lastTime = Time.time;
    }

    // 종료 시 실행되는 메서드
    private void OnDisable()
    {
        Debug.Log("블루투스 disabled");
        if (bluetoothHelper != null)
            bluetoothHelper.Disconnect();
    }

    void OnDestroy()
    {
        Debug.Log("블루투스 destroyed");
        if (bluetoothHelper != null)
            bluetoothHelper.Disconnect();
    }

    void OnApplicationQuit()
    {
        Debug.Log("앱 quit");
        if (bluetoothHelper != null)
            bluetoothHelper.Disconnect();
    }
}