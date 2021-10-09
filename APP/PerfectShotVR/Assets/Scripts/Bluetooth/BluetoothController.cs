using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ����â���� ������� ���� UI ���� �� ������� �Ŵ��� ����
public class BluetoothController : MonoBehaviour
{
    // ������� ������ ���� �Ѵ� ���
    public Toggle BluetoothToggle;
    // ���� ���¸� �˷��ִ� �ε�������
    public Text StateIndicator;
    // ����̽� �̸��� ���� �ؽ�Ʈ ��ǲ �ʵ�
    // ���� ���� �� �ش� ����̽� �̸��� PlayerPrefs�� DeviceName���� ����.
    public InputField DeviceNameInputField;
    // ���� / ���� ��ư��.
    public Button ConnectButton;
    public Button DisconnectButton;

    void Start()
    {
        BluetoothToggle.onValueChanged.AddListener((value) => {
            this.gameObject.SetActive(value);
        });
        DeviceNameInputField.text = PlayerPrefs.GetString("DeviceName");
        DeviceNameInputField.onValueChanged.AddListener(delegate { BluetoothManager.instance.SetDeviceName(DeviceNameInputField.text); });
        ConnectButton.onClick.AddListener(() => {
            StateIndicator.text = "������ �õ��մϴ�...";
            BluetoothManager.instance.deviceName = DeviceNameInputField.text;
            BluetoothManager.instance.OnConnectButtonClick();
            StateIndicator.text = UpdateState();
            SetButtonsEnabled();
        });
        DisconnectButton.onClick.AddListener(() => {
            StateIndicator.text = "������ �����մϴ�...";
            BluetoothManager.instance.OnDisconnectButtonClick();
            if (BluetoothManager.instance.IsConnected) StateIndicator.text = "���� �Ϸ�";
            else StateIndicator.text = "���� ����";
            SetButtonsEnabled();
        });
        this.gameObject.SetActive(GameManager.instance.Bluetooth);
        // StartCoroutine(ConnectionMonitor());
        StateIndicator.text = UpdateState();
    }

    private void OnEnable()
    {
        SetButtonsEnabled();
    }

    // Update is called once per frame
    void Update()
    {
        if (BluetoothManager.instance != null) StateIndicator.text = UpdateState() + $"\n{BluetoothManager.instance.State}";
        BluetoothManager.instance.deviceName = DeviceNameInputField.text;
    }

    // ��ư�� Ȱ��ȭ�� ������.
    void SetButtonsEnabled()
    {
        if (BluetoothManager.instance == null) return;

        if (BluetoothManager.instance.IsConnected)
        {
            ConnectButton.interactable = false;
            DisconnectButton.interactable = true;
        }
        else
        {
            ConnectButton.interactable = true;
            DisconnectButton.interactable = false;
        }
    }

    // ���� ���ñ� ������Ʈ
    string UpdateState()
    {
        if (BluetoothManager.instance == null) return "������� ���� �Ұ���";
        string state = "";
        state += "��" + (BluetoothManager.instance.IsPaired ? "O" : "X");
        state += " ����" + (BluetoothManager.instance.IsConnected ? "O" : "X");
        return state;
    }

    IEnumerator ConnectionMonitor()
    {
        Debug.Log("Ŀ�ؼ� ����͸� ��...");
        // ������ �Ǿ onConnnected �̺�Ʈ�� ������� �ʴ� ������ ���� ��ġ.
        if (BluetoothManager.instance.IsConnected)
        {
            BluetoothManager.instance.OnConnected();
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ConnectionMonitor());
    }
}