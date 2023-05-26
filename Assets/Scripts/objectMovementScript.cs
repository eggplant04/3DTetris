using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMovementScript : MonoBehaviour
{

    private Rigidbody rb;
    public float moveSpeed = 10f;
    
    public float moveDelay = 3f;
    
    public bool canSpawn = true;

    public bool isMoving_ = true;

    private gameplayManagerScript myGameplayManagerScript;

    void Start()
    {
        
        GameObject gameplayManagerObject = GameObject.Find("gameplayManager");
        myGameplayManagerScript = gameplayManagerObject.GetComponent<gameplayManagerScript>();

        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        



        if (isMoving_)
        {

            
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(0, (-moveSpeed* 6) * Time.deltaTime, 0);
            }
            else{
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
            }
        }
        else
        {
            if (canSpawn)
            {
                myGameplayManagerScript.spawnBlock();
                canSpawn = false;
            }
        }
        
        


        if (Input.GetKeyDown(KeyCode.W))
        {
            w();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            s();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            a();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            d();
        }
        


    }

    
    

    void w(){
        if(isMoving_){
            bool canMove = true;
            foreach (Transform child in transform)
            {
                foreach (Transform grandchild in child)
                {
                    Vector3 worldPosition = grandchild.TransformPoint(Vector3.zero);
                    if (worldPosition.x >= 10){
                        canMove = false;
                    }
                }

            }
            if (canMove){
                transform.Translate(10f, 0f, 0f);
            }
            
           
        } 
    }
    void s(){
        if(isMoving_){
            bool canMove = true;
            foreach (Transform child in transform)
            {
                foreach (Transform grandchild in child)
                {
                    Vector3 worldPosition = grandchild.TransformPoint(Vector3.zero);
                    if (worldPosition.x <= -10){
                        canMove = false;
                    }
                }

            }
            if (canMove){
                transform.Translate(-10f, 0f, 0f);
            }
            
        }
    }
    void a(){
        if(isMoving_){
            bool canMove = true;
            foreach (Transform child in transform)
            {
                foreach (Transform grandchild in child)
                {
                    Vector3 worldPosition = grandchild.TransformPoint(Vector3.zero);
                    if (worldPosition.z >= 10){
                        canMove = false;
                    }
                }

            }
            if (canMove){
                transform.Translate(0f, 0f, 10f);
            }
            
            
        }
    }
    void d(){
        if(isMoving_){
            bool canMove = true;
            foreach (Transform child in transform)
            {
                foreach (Transform grandchild in child)
                {
                    Vector3 worldPosition = grandchild.TransformPoint(Vector3.zero);
                    if (worldPosition.z <= -10){
                        canMove = false;
                    }
                }

            }
            if (canMove){
                transform.Translate(0f, 0f, -10f);
            }
            
            
        }
    }

    
    
}