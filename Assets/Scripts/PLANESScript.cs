using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLANESScript : MonoBehaviour
{





    public void move(){
        Vector3 newPosition = transform.position;
        newPosition.y -= 100f;
        transform.position = newPosition;
        Invoke("moveUp", 0f);
    }
    void moveUp(){
        Vector3 newPosition = transform.position;
        newPosition.y += 100f;
        transform.position = newPosition;
    }
}
