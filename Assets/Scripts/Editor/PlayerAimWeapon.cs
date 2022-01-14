using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if (UNITY_EDITOR)
public class PlayerAimWeapon : MonoBehaviour
{
    // event handler for the actual shooting;
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition; // not sure about this
        public Vector3 shellPosition; // don't need this.
    }

    private Transform aimTransform; // not sure about this
    private Transform aimGunEndPointTransform;
    private Transform aimShellPositionTransform;  // dont need this
    private Animator aimAnimator;  // or this

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimAnimator = aimTransform.GetComponentInChildren<Animator>();  // or this
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition"); // mby need this, not sure
        aimShellPositionTransform = aimTransform.Find("ShellPosition"); // not this
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting(); // or this

    }

    // can I use this for referencing sprite animator like with how the ant and such in the blend tree?
    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = MathF.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle); // (x, y, z) zero on z and y because we are working in 2d. This is important.

        // dont need anything under this. It is for flipping gun sprite.
        Vector3 aimLocasScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            aimLocasScale.y = -1f;
        }
        else
        {
            aimTransform.localScale = aimLocasScale;
        }
        
    }

    private void HandleShooting() // or this
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = GetMouseWorldPosition();

            aimAnimator.SetTrigger("Shoot");
            OnShoot?.Invoke(this, new OnShootEventArgs { 
                gunEndPointPosition = aimGunEndPointTransform.position,     // where the projectile spawns
                shootPosition = mousePosition,         // and flies into the mouseposition. if i want only the direktion and it not to stop here mby change this ? will w8 and see what he does next.
                shellPosition = aimShellPositionTransform.position,
            });
        }
    }


    private static Vector3 GetMouseWorldPosition() // probably need this.
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
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
