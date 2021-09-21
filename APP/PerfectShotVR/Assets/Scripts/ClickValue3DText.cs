using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ClickValue3DText : MonoBehaviour
{
    private TextMesh textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMesh>();
        if(textMesh.text.Contains("�¿�"))
        {
            textMesh.text = $"�¿� : {GameManager.instance.Click.x}";
        }
        else
        {
            textMesh.text = $"���� : {GameManager.instance.Click.y}";
        }
    }

    private void OnEnable()
    {
        Start();
    }

    public void AddOneHorizontal()
    {
        Vector2 _click = GameManager.instance.Click;
        int num = (int)_click.x;
        num = Mathf.Clamp(num + 1, -17, 17);
        GameManager.instance.Click = new Vector2(num, _click.y);
        textMesh.text = $"�¿� : {num}";
        SaveClick();
    }

    public void SubOneHorizontal()
    {
        Vector2 _click = GameManager.instance.Click;
        int num = (int)_click.x;
        num = Mathf.Clamp(num - 1, -17, 17);
        GameManager.instance.Click = new Vector2(num, _click.y);
        textMesh.text = $"�¿� : {num}";
        SaveClick();
    }

    public void AddOneVertical()
    {
        Vector2 _click = GameManager.instance.Click;
        int num = (int)_click.y;
        num = Mathf.Clamp(num + 1, -20, 20);
        GameManager.instance.Click = new Vector2(_click.x, num);
        textMesh.text = $"���� : {num}";
        SaveClick();
    }

    public void SubOneVertical()
    {
        Vector2 _click = GameManager.instance.Click;
        int num = (int)_click.y;
        num = Mathf.Clamp(num - 1, -20, 20);
        GameManager.instance.Click = new Vector2(_click.x, num);
        textMesh.text = $"���� : {num}";
        SaveClick();
    }

    private void SaveClick()
    {
        FindObjectOfType<SettingCamera>().InitCamAndApplyClick();
    }
}