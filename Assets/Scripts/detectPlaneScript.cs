using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPlaneScript : MonoBehaviour
{
    private int numOfCollides = 0;
    public GameObject blocks;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("block"))
        {
            
            add();
            if (numOfCollides >= 5)
            {
                Debug.Log("bang");
                foreach (Transform child in blocks.transform)
                {
                    Debug.Log(child.name);
                }
            }
        }
    }

    void add(){
        numOfCollides += 1;
    }
}