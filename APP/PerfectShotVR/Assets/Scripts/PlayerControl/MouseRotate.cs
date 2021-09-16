using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 디버깅용 마우스 입력 스크립트
public class MouseRotate : MonoBehaviour
{
    readonly float rot_speed = 30;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float dy = Input.GetAxis("Mouse Y");
        float dx = Input.GetAxis("Mouse X");
        transform.eulerAngles += new Vector3(-dy * rot_speed * Time.deltaTime, dx * rot_speed * Time.deltaTime, 0);
    }
}
