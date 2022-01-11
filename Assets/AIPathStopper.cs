using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIPathStopper : MonoBehaviour
{
    private AIPath _aIPath;
    void Start()
    {
        _aIPath = GetComponent<AIPath>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (_aIPath.enabled )
            {
            _aIPath.enabled = false;
            }
            else
            {
                _aIPath.enabled = true;
            }

        }
    }
}
