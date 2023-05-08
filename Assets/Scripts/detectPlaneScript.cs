using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPlaneScript : MonoBehaviour
{
    private int numOfCollides = 0;
    private GameObject blocks;


    void Start(){
        blocks = GameObject.Find("BLOCKS");
        gameObject.name = "holder";
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("block"))
        {
            
            add();
            if (numOfCollides >= 25)
            {
                reset();
                DestroyAndMoveAllChildren();
                
            }
        }
    }

    void add(){
        numOfCollides += 1;
    }
    void reset(){
        numOfCollides = 0;
    }

    void DestroyAndMoveAllChildren()
    {
        foreach (Transform child in blocks.transform)
        {
            if (child.position.y == transform.position.y){
                Destroy(child.gameObject);
            }

            
        }
        foreach (Transform child in blocks.transform)
        {
            if (child.position.y > transform.position.y){
                Vector3 newPos = child.position;
                newPos.y -= 10;
                child.position = newPos;
            }

            
        }
    }

}