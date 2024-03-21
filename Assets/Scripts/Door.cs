using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnimation; // Reference to the Animation component attached to the door
    public string openAnimationName = "DoorOpen"; // Name of the opening animation
    public string closeAnimationName = "DoorClose"; // Name of the closing animation

    public void OpenDoor()
    {
        if (doorAnimation != null)
        {
            doorAnimation.Play(openAnimationName);
            // Optionally, you can disable collider or do other actions when opening the door
        }
    }
    public void CloseDoor()
    {
        if (doorAnimation != null)
        {
            Debug.Log(doorAnimation);
            doorAnimation.Play(closeAnimationName);
            // Optionally, you can disable collider or do other actions when opening the door
        }
    }
}
