using System;
using System.Collections.Generic;
using UnityEngine;
#if (UNITY_EDITOR)

public class NewAimScript : MonoBehaviour
{
    public Vector3 gunEndPointPosition; // not needed
    public Vector3 shootPosition;

    private Transform aimTransform;
    private Transform aimGunEndPointTransform;  //not needed

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition"); // not needed
    }

    private void Update()
    {
        HandleAiming();

    }

    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDiretion = (mousePosition - transform.position).normalized;
        float angle = MathF.Atan2(aimDiretion.y, aimDiretion.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
#endif