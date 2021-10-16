using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    // speed에 반비례하여 
    public static IEnumerator MoveTo(Transform transform, Vector3 dest, float speed)
    {
        float count = 0;
        Vector3 startPos = transform.position;
        while (true)
        {
            count += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPos, dest, count);

            if (count >= 1)
            {
                transform.position = dest;
                break;
            }
            yield return null;
        }
    }

    // 구형 표면을 따라서 이동
    public static IEnumerator MoveSlerpTo(Transform transform, Vector3 dest, float speed)
    {
        float count = 0;
        Vector3 startPos = transform.position;
        while (true)
        {
            count += Time.deltaTime * speed;
            transform.position = Vector3.Slerp(startPos, dest, count);

            if (count >= 1)
            {
                transform.position = dest;
                break;
            }
            yield return null;
        }
    }

    public static IEnumerator RotateTo(Transform transform, Vector3 dest, float speed)
    {
        float count = 0;
        Vector3 startPos = transform.eulerAngles;
        while (true)
        {
            count += Time.deltaTime * speed;
            transform.eulerAngles = Vector3.Lerp(startPos, dest, count);

            if (count >= 1)
            {
                transform.eulerAngles = dest;
                break;
            }
            yield return null;
        }
    }
}