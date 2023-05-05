using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockCollisionScript : MonoBehaviour
{
    private Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("block")){
            transform.parent.parent.gameObject.tag = "block";
            
        }
        
    }

    void Update(){
        if (transform.CompareTag("block")){
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Vector3 position = transform.position;
            position.y = (Mathf.Round(position.y/5) * 5);
            transform.position = position;
        }
    }

}
