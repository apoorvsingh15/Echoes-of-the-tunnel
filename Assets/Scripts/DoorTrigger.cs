using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public Door door; // Reference to the Door script attached to the door object

    private bool isOpen = false; // Flag to track whether the door is open
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.GetComponent<Door>());
        if (other.CompareTag("Player"))
        {
            // Display interaction prompt or message
            door = FindObjectOfType<Door>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide interaction prompt or message
            door = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            // Check if the door reference is set
            if (door != null && !isOpen)
            {
                door.OpenDoor();
                isOpen = true;
              
            }
            else
            {
                door.CloseDoor();
                isOpen = false;

            }
        }
    }
}
