using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 설정 패널
public class SettingPanel : MonoBehaviour
{
    public Text ClickHorizontal;
    public Text ClickVertical;
    public Toggle FrontSightEnabled;
    public Toggle StaringMode;
    public Toggle Bluetooth;
    public Button Reset;
    public Button InitCam;
    public Button Save;
    public Button Confirm;

    void Start()
    {
        SetSettings();
        Reset.onClick.AddListener(ResetSettings);
        InitCam.onClick.AddListener(() => SceneManager.LoadScene("SetCamera"));
        Save.onClick.AddListener(() => SaveSettings());
        Confirm.onClick.AddListener(() =>{
            SaveSettings();
            this.gameObject.SetActive(false);
        });
    }

    private void OnEnable()
    {
        Start();
    }

    void Update()
    {
        
    }

    // 세팅값들을 불러와서 UI에 갱신
    void SetSettings()
    {
        ClickHorizontal.text = GameManager.instance.Click.x.ToString();
        ClickVertical.text = GameManager.instance.Click.y.ToString();
        FrontSightEnabled.isOn = GameManager.instance.FrontSightEnabled;
        StaringMode.isOn = GameManager.instance.StaringMode;
        Bluetooth.isOn = GameManager.instance.Bluetooth;
    }

    // 리셋 버튼을 누르면 호출되는 메서드. 설정들을 모두 초기화하는 함수...!
    void ResetSettings()
    {
        PlayerPrefs.DeleteAll();
        SetSettings();
        SaveClick();
    }

    // 패널을 보여주고 가리는 토글
    public void TogglePanel()
    {
        if (this.gameObject.activeSelf)
        {
            SaveSettings();
            this.gameObject.SetActive(false);
        }
        else this.gameObject.SetActive(true);
    }

    // 현재 세팅창에 있는 값들을 게임매니저에 저장. 저장 버튼을 누르면 호출됨.
    public void SaveSettings()
    {
        SaveClick();
        GameManager.instance.FrontSightEnabled = FrontSightEnabled.isOn;
        GameManager.instance.StaringMode = StaringMode.isOn;
        GameManager.instance.Bluetooth = Bluetooth.isOn;
    }

    // 크리크 값을 수정하고 반영.
    public void SaveClick()
    {
        GameManager.instance.Click = new Vector2(int.Parse(ClickHorizontal.text), int.Parse(ClickVertical.text));
        SettingCamera settingCamera = FindObjectOfType<SettingCamera>();
        if(settingCamera != null) settingCamera.InitCamAndApplyClick();
    }
}
