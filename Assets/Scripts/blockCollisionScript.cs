using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockCollisionScript : MonoBehaviour
{
    private canvasScript myCanvasScript; // Reference to the canvas script

    private objectMovementScript myObjectMovementScript; // Reference to the objectMovementScript

    private rotatorScript myRotatorScript; // Reference to the rotatorScript

    private gameplayManagerScript myGameplayManagerScript; // Reference to the gameplayManagerScript

    private Rigidbody rb; // Reference to the Rigidbody component

    void Start()
    {
        // Get the objectMovementScript from the parent's parent object
        GameObject mainObject = transform.parent.parent.gameObject;
        myObjectMovementScript = mainObject.GetComponent<objectMovementScript>();

        // Get the rotatorScript from the parent object
        GameObject rotatorObject = transform.parent.gameObject;
        myRotatorScript = rotatorObject.GetComponent<rotatorScript>();

        // Find the Canvas object and get the canvas script component
        GameObject canvasObject = GameObject.Find("Canvas");
        myCanvasScript = canvasObject.GetComponent<canvasScript>();

        // Get the Rigidbody component attached to the current object
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the "block" tag
        if (other.CompareTag("block"))
        {
            myRotatorScript.hasCollided = true; // Set the collision flag in the rotatorScript
        }
    }

    void Update()
    {
        // Check if collision occurred and the game is not paused
        if (myRotatorScript.hasCollided && myCanvasScript.playPauseBTNString == "PAUSE")
        {
            // Stop the object's velocity and angular velocity
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Round the object's position to the nearest multiple of 5 on the y-axis
            Vector3 position = transform.position;
            position.y = (Mathf.Round(position.y / 5) * 5);
            transform.position = position;

            // Move the children of the rotatorScript to their target positions
            myRotatorScript.MoveChildrenToTarget();

            // Set the objectMovementScript's moving flag to false
            myObjectMovementScript.isMoving_ = false;
        }
    }
}