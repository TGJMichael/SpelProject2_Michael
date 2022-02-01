using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
//{ 
//    public static CameraShake instance;

//private Vector3 _originalPos;
//private float _timeAtCurrentFrame;
//private float _timeAtLastFrame;
//private float _fakeDelta;

//void Awake()
//{
//    instance = this;
//}

//void Update()
//{
//    // Calculate a fake delta time, so we can Shake while game is paused.
//    _timeAtCurrentFrame = Time.realtimeSinceStartup;
//    _fakeDelta = _timeAtCurrentFrame - _timeAtLastFrame;
//    _timeAtLastFrame = _timeAtCurrentFrame;
//}

//public static void Shake(float duration, float amount)
//{
//    instance._originalPos = instance.gameObject.transform.localPosition;
//    instance.StopAllCoroutines();
//    instance.StartCoroutine(instance.cShake(duration, amount));
//}

//public IEnumerator cShake(float duration, float amount)
//{
//    float endTime = Time.time + duration;

//    while (duration > 0)
//    {
//        transform.localPosition = _originalPos + Random.insideUnitSphere * amount;

//        duration -= _fakeDelta;

//        yield return null;
//    }

//    transform.localPosition = _originalPos;
//}
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;


    //CameraFollow
    [SerializeField] GameObject player;
    public float minX = -3f;
    public float maxX = 3f;
    public float minY = -7.3f;
    public float maxY = 6.4f;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
            //Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10);
            //transform.position = playerPosition;
        }
    }

    public void Shake(int shakeTime)
    {
        shakeDuration += shakeTime;
    }
}
