using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatorScript : MonoBehaviour
{
    void Update()
    {
        if (transform.parent.CompareTag("block")){

            foreach (Transform child in transform)
            {
                child.gameObject.tag = "block";
            }

        }
    }
}
