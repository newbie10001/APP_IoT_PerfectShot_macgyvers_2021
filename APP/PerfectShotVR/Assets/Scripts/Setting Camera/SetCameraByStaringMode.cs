using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 8초를 기다린 후 자동설정을 한다.
public class SetCameraByStaringMode : MonoBehaviour
{
    public Text Timer;
    public SetCamByShooting setCamByShooting;
    const float TIME = 8f;
    float elapsed = default;
    private void Start()
    {
        this.enabled = GameManager.instance.StaringMode;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        Timer.text = $"{(int)(TIME - elapsed)}초 후에 자동으로\n가늠쇠가 설정됩니다.";
        if (elapsed > TIME) setCamByShooting.Fire();
    }
}
