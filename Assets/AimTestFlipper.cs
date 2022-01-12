using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTestFlipper : MonoBehaviour
{

    private Transform aimTransform;
    private float _angle;
    private Animator _animator;

    private void Awake()
    {
        //_animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        _angle = aimTransform.localEulerAngles.z;

        if (_angle > 270 || _angle < 90)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(180, 0, 0);
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //_animator.SetTrigger("Shoot");
        //}
    }
}
