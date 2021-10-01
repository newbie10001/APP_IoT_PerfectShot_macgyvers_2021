using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// restrains object not to get out of the screen
public class RestrainObjectNotToOverflow : MonoBehaviour
{
    Vector3 pos;
    void Update()
    {
        pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
