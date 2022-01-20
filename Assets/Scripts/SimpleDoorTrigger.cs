using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoorTrigger : MonoBehaviour
{
    public NextStage openDoor;
    //public GameObject doorObject;
    void Start()
    {
        //doorObject = FindObjectOfType<NextStage>().gameObject;

        openDoor = GameObject.FindObjectOfType(typeof(NextStage)) as NextStage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("trying to open door");
        //GameObject.Find("NextStage").GetComponent<NextStage>().objectiveComplete = true;  // this would have worked if the nextstage was controlled mainly by the bool but it is controlled by "OpenDoor()" method.
        openDoor.OpenDoor();
    }
}
