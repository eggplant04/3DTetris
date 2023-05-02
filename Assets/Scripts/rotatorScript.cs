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


        if (Input.GetKeyDown(KeyCode.R))
        {
            r();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            t();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            y();
        }


    }

    void r(){
        if(!transform.parent.gameObject.CompareTag("block")){
            transform.Rotate(90f, 0f, 0f, Space.World);
        } 
    }
    void t(){
        if(!transform.parent.gameObject.CompareTag("block")){
            transform.Rotate(0f, 90f, 0f, Space.World);
        } 
    }
    void y(){
        if(!transform.parent.gameObject.CompareTag("block")){
            transform.Rotate(0f, 0f, 90f, Space.World);
        } 
    }
}
