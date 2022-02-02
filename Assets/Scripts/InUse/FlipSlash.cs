using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSlash : MonoBehaviour
{
    private SpriteRenderer spriteRend;
    private float rotationNumber;
    public float minAngle = -90;
    public float maxAngle = 90;

    void Start()
    {
        spriteRend = GetComponentInChildren<SpriteRenderer>();

        // get relative range +/-
        float relRange = (maxAngle - minAngle) / 2f;

        // calculate offset
        float offset = maxAngle - relRange;

        // convert to a relative value
        Vector3 angles = this.transform.eulerAngles;
        float z = ((angles.z + 540) % 360) - 180 - offset;

        // if outside range
        if (Mathf.Abs(z) > relRange)
        {
            spriteRend.flipY = true;
        }
    }
}
