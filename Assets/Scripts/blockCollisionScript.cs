using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockCollisionScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("block")){
            transform.parent.parent.gameObject.tag = "block";
        }
        
    }
}
