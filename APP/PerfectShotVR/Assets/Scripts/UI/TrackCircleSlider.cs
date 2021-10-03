using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 현재 가늠자가 어디 있는지 추적해서 UI에 갱신함. (서클 슬라이더)
public class TrackCircleSlider : MonoBehaviour
{
    // Gun 오브젝트 밑에 있는 Front Sight
    // 실제 가늠자의 오브젝트
    public GameObject frontSight;

    private void Start()
    {

    }

    private void OnEnable()
    {
        StartCoroutine(updateUI());
    }

    private void Update()
    {
        
    }

    // 성능을 위해 0.5초 간격으로 실행
    private IEnumerator updateUI()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(frontSight.transform.position);
        this.GetComponent<RectTransform>().position = screenPos;
        yield return new WaitForSeconds(0.5f);
        // false일 경우 루프가 멈춤.
        if(GameManager.instance.StaringMode) StartCoroutine(updateUI());
    }

    // 설정창에서 토글하면 반응하는 메서드
    public void ToggleStaringSlider(Toggle _toggle)
    {
        this.gameObject.SetActive(_toggle.isOn);
    }
}
