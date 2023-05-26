using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockCollisionScript : MonoBehaviour
{

    private objectMovementScript myObjectMovementScript;

    private rotatorScript myRotatorScript;

    private gameplayManagerScript myGameplayManagerScript;

    private Rigidbody rb;

    void Start(){

        GameObject mainObject = transform.parent.parent.gameObject;
        myObjectMovementScript = mainObject.GetComponent<objectMovementScript>();

        GameObject rotatorObject = transform.parent.gameObject;
        myRotatorScript = rotatorObject.GetComponent<rotatorScript>();

        

        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("block")){
            myRotatorScript.hasCollided = true;
            
        }
        
    }

    void Update(){
        if (myRotatorScript.hasCollided){
            
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Vector3 position = transform.position;
            position.y = (Mathf.Round(position.y/5) * 5);
            transform.position = position;
            myRotatorScript.MoveChildrenToTarget();
            
            myObjectMovementScript.isMoving_ = false;

        }
    }

}
