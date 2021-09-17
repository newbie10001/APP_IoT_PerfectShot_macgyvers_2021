using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
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
}
