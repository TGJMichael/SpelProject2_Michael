using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    public float minX = -3f;
    public float maxX = 3f;
    public float minY = -7.3f;
    public float maxY = 6.4f;

    void Update()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        //playerPosition.x = Mathf.Clamp(player.transform.position.x, minX, maxX);
        //playerPosition.y = Mathf.Clamp(player.transform.position.y, minY, maxY);
        transform.position = playerPosition;
    }
}
