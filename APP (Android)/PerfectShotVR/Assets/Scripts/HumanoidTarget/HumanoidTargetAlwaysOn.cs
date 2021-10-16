using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidTargetAlwaysOn : MonoBehaviour
{
    private HumanoidTargetManager _targetManager;

    void Start()
    {
        _targetManager = GetComponent<HumanoidTargetManager>();
    }

    void Update()
    {
        _targetManager.GetUp();
    }
}
