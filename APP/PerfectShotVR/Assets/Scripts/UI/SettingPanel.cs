using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ���� �г�
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

    // ���ð����� �ҷ��ͼ� UI�� ����
    void SetSettings()
    {
        ClickHorizontal.text = GameManager.instance.Click.x.ToString();
        ClickVertical.text = GameManager.instance.Click.y.ToString();
        FrontSightEnabled.isOn = GameManager.instance.FrontSightEnabled;
        StaringMode.isOn = GameManager.instance.StaringMode;
        Bluetooth.isOn = GameManager.instance.Bluetooth;
    }

    // ���� ��ư�� ������ ȣ��Ǵ� �޼���. �������� ��� �ʱ�ȭ�ϴ� �Լ�...!
    void ResetSettings()
    {
        PlayerPrefs.DeleteAll();
        SetSettings();
        SaveClick();
    }

    // �г��� �����ְ� ������ ���
    public void TogglePanel()
    {
        if (this.gameObject.activeSelf)
        {
            SaveSettings();
            this.gameObject.SetActive(false);
        }
        else this.gameObject.SetActive(true);
    }

    // ���� ����â�� �ִ� ������ ���ӸŴ����� ����. ���� ��ư�� ������ ȣ���.
    public void SaveSettings()
    {
        SaveClick();
        GameManager.instance.FrontSightEnabled = FrontSightEnabled.isOn;
        GameManager.instance.StaringMode = StaringMode.isOn;
        GameManager.instance.Bluetooth = Bluetooth.isOn;
    }

    // ũ��ũ ���� �����ϰ� �ݿ�.
    public void SaveClick()
    {
        GameManager.instance.Click = new Vector2(int.Parse(ClickHorizontal.text), int.Parse(ClickVertical.text));
        SettingCamera settingCamera = FindObjectOfType<SettingCamera>();
        if(settingCamera != null) settingCamera.InitCamAndApplyClick();
    }
}
