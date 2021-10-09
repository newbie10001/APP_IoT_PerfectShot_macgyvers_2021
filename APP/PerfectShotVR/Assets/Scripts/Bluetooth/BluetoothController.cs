using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 설정창에서 블루투스 관련 UI 제어 및 블루투스 매니저 연결
public class BluetoothController : MonoBehaviour
{
    // 블루투스 설정을 끄고 켜는 토글
    public Toggle BluetoothToggle;
    // 현재 상태를 알려주는 인디케이터
    public Text StateIndicator;
    // 디바이스 이름을 담은 텍스트 인풋 필드
    // 연결 성공 시 해당 디바이스 이름을 PlayerPrefs에 DeviceName으로 저장.
    public InputField DeviceNameInputField;
    // 연결 / 해제 버튼들.
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
            StateIndicator.text = "연결을 시도합니다...";
            BluetoothManager.instance.deviceName = DeviceNameInputField.text;
            BluetoothManager.instance.OnConnectButtonClick();
            StateIndicator.text = UpdateState();
            SetButtonsEnabled();
        });
        DisconnectButton.onClick.AddListener(() => {
            StateIndicator.text = "연결을 해제합니다...";
            BluetoothManager.instance.OnDisconnectButtonClick();
            if (BluetoothManager.instance.IsConnected) StateIndicator.text = "해제 완료";
            else StateIndicator.text = "해제 실패";
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

    // 버튼의 활성화를 갱신함.
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

    // 상태 지시기 업데이트
    string UpdateState()
    {
        if (BluetoothManager.instance == null) return "블루투스 연결 불가능";
        string state = "";
        state += "페어링" + (BluetoothManager.instance.IsPaired ? "O" : "X");
        state += " 연결" + (BluetoothManager.instance.IsConnected ? "O" : "X");
        return state;
    }

    IEnumerator ConnectionMonitor()
    {
        Debug.Log("커넥션 모니터링 중...");
        // 연결이 되어도 onConnnected 이벤트가 실행되지 않는 문제에 대한 조치.
        if (BluetoothManager.instance.IsConnected)
        {
            BluetoothManager.instance.OnConnected();
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ConnectionMonitor());
    }
}