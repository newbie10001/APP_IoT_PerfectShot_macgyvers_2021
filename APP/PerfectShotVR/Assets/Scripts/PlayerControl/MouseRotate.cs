using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 디버깅용 마우스 입력 스크립트
public class MouseRotate : MonoBehaviour
{
    private float rot_speed = 30;
    private float my;
    private float mx;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.eulerAngles;
        mx = rot.y;
        my = rot.x;
    }

    // Update is called once per frame
    void Update()
    {
        float dy = Input.GetAxis("Mouse Y");
        float dx = Input.GetAxis("Mouse X");

        //my += dy * rot_speed * Time.deltaTime;
        //mx += dx * rot_speed * Time.deltaTime;

        //my = Mathf.Clamp(my, -90f, 90f);

        //transform.eulerAngles = new Vector3(-my, mx, 0);
        transform.eulerAngles += new Vector3(-dy * rot_speed * Time.deltaTime, dx * rot_speed * Time.deltaTime, 0);
    }
}
