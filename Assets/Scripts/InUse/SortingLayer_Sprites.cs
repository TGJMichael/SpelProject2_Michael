
using UnityEngine;
using UnityEngine.Rendering;



[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class SortingLayer_Sprites : MonoBehaviour
{
    private const int IsometricRangePerYUnit = 100;

    [Tooltip("Will use this object to compute z-order")]
    public Transform Target;

    [Tooltip("Use this to offset the object slightly in front or behind the Target object")]
    public int TargetOffset = 0;

    void Update()
    {
        if (Target == null)
            Target = transform;
        Renderer renderer = GetComponent<SpriteRenderer>();
        renderer.sortingOrder = -(int)(Target.position.y * IsometricRangePerYUnit) + TargetOffset;
    }
}
