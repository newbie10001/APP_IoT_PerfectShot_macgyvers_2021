using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArduinoBluetoothAPI;

// ������� ����, �Է��� ������.
public class BluetoothManager : MonoBehaviour
{
    // �̱���. �� ��ȯ �ÿ��� �����ǵ��� ��.
    public static BluetoothManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<BluetoothManager>();
                if(m_instance != null) DontDestroyOnLoad(m_instance.gameObject);
            }
            return m_instance;
        }
    }
    private static BluetoothManager m_instance;

    // �Ƶ��̳� ������� API Ȱ��
    BluetoothHelper bluetoothHelper;
    // ������� ����� �̸�
    public string deviceName;
    // �� ���¸� ��Ÿ��
    public bool IsPaired
    {
        get
        {
            if (bluetoothHelper != null) return bluetoothHelper.isDevicePaired();
            return false;
        }
    }
    // ���� ���� ���¸� ��Ÿ��.
    public bool IsConnected
    {
        get
        {
            if (bluetoothHelper != null) return bluetoothHelper.isConnected();
            return false;
        }
    }
    // ���� ������� ���� ���¸� ��Ÿ��
    public string State { get; private set; }
    // ��������� �Է� ����(on / off)�� ��Ÿ��.
    public bool Input { get; private set; } = false;
    // ��������� �Է��� ������ �ð�. 0.3�ʰ� ������ input�� false�� �ǵ���.
    float lastTime;

    // ����׿�.
    public bool debugging = false;
    public Text debugText;

    // �ȵ���̵� ������� Ȱ��ȭ �Լ�
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
        if (Time.time > lastTime + 0.3f) Input = false;
        if(debugging)
        {
            debugText.text = $"�� : {IsPaired} Ŀ���� : {IsConnected}\n����̽� �̸� : {bluetoothHelper.getDeviceName()}" +
                $"\nbluetoothHelper.Available : {bluetoothHelper.Available}\nreadData : {bluetoothHelper.Read()}";
        }
        // �̺�Ʈ�� �� �Ծ... ��¿ �� ���� ����.
        if (bluetoothHelper.Read() != "")
        {
            OnMessageReceived();
        }
    }

    void Awake()
    {
        Debug.Log("bluetooth manager awake");
        //setAndroidBluetoothEnabled();
        // �̱���!
        if (instance != this.GetComponent<BluetoothManager>())
        {
            Destroy(gameObject);
            return;
        }
        deviceName = "trigger";
        InstantiateBluetoothHelper();
        // ������� ������ �Ǿ��־��ٸ� Ŀ��Ʈ
        if (GameManager.instance.Bluetooth) OnConnectButtonClick();
    }

    public void InstantiateBluetoothHelper()
    {
        try
        {
            bluetoothHelper = BluetoothHelper.GetInstance(deviceName);

            bluetoothHelper.OnConnected += OnConnected;
            bluetoothHelper.OnConnectionFailed += OnConnectionFailed;
            // ���� �Ѿ�鼭 �̺�Ʈ�� �߻����� �ʴ� ���� �߻�
            // ���� ������Ʈ �������� �̸� �ذ���.
            bluetoothHelper.OnDataReceived += OnMessageReceived;

            bluetoothHelper.setTerminatorBasedStream("\n");
            State = "TryConnectToDevice ����";
        }
        catch (Exception ex)
        {
            Debug.Log($"������� ���� ���� : {ex}");
        }
    }

    public void OnConnectButtonClick()
    {
        //TryConnectToDevice();
        Debug.Log("���� ��ư ����");
        if (IsPaired && !IsConnected)
        {
            if (bluetoothHelper != null)
            {
                State = "��� �̸� �ݿ�";
                bluetoothHelper.setDeviceName(deviceName);
                State = "������ �õ��մϴ�...";
                bluetoothHelper.Connect();
            }
        }
    }

    public void OnDisconnectButtonClick()
    {
        State = "������ �����մϴ�.";
        if(bluetoothHelper != null) bluetoothHelper.Disconnect();
    }

    public void OnConnected()
    {
        // ������ ����̽������� ������.
        PlayerPrefs.SetString("DeviceName", deviceName);
        try
        {
            Debug.Log("������� ������ ��ŸƮ");
            bluetoothHelper.StartListening();
            State = "���� ����";
        }
        catch
        {
            Debug.Log("������� ������ ����");
        }
    }

    bool _triedToConnect = false;
    void OnConnectionFailed()
    {
        Debug.Log("���� ����");
        State = "������ ���������ϴ�.";
        // ���� ��õ�(�ѹ���)
        if(!_triedToConnect) OnConnectButtonClick();
        _triedToConnect = true;
    }

    void OnMessageReceived()
    {
        Debug.Log("������� ��ȣ ����");
        Input = true;
        lastTime = Time.time;
    }

    // ���� �� ����Ǵ� �޼���
    private void OnDisable()
    {
        Debug.Log("������� disabled");
        if (bluetoothHelper != null)
            bluetoothHelper.Disconnect();
    }

    void OnDestroy()
    {
        Debug.Log("������� destroyed");
        if (bluetoothHelper != null)
            bluetoothHelper.Disconnect();
    }

    void OnApplicationQuit()
    {
        Debug.Log("�� quit");
        if (bluetoothHelper != null)
            bluetoothHelper.Disconnect();
    }
}