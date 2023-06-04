using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class objectMovementScript : MonoBehaviour
{

    private int softscore = 0;

    private GameObject blocks;

    private Rigidbody rb;

    private gameplayManagerScript myGameplayManagerScript;

    private canvasScript myCanvasScript;


    public float moveSpeed = 10f;
    public float moveDelay = 3f;
    public bool canSpawn = true;
    public bool isMoving_ = true;

    

    void Start()
    {
        blocks = GameObject.Find("BLOCKS");
        
        rb = GetComponent<Rigidbody>();

        GameObject gameplayManagerObject = GameObject.Find("gameplayManager");
        myGameplayManagerScript = gameplayManagerObject.GetComponent<gameplayManagerScript>();
        
        GameObject canvasObject = GameObject.Find("Canvas");
        myCanvasScript = canvasObject.GetComponent<canvasScript>();

        
        
    }

    void Update()
    {
        


        if (myCanvasScript.playPauseBTNString == "PAUSE"){

        
            if (isMoving_ )
            {


                if (Input.GetKey(KeyCode.Space))
                {
                    softscore += 1;
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
                    if ((softscore / 20) > 40){
                        softscore = 40;
                    }
                    else{
                        softscore = softscore / 20;
                    }
                    myCanvasScript.scoreLBLTextInt += (softscore + 10);
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

    }

    
    

    void w(){
        if(isMoving_ && myCanvasScript.playPauseBTNString == "PAUSE"){
            bool canMove = true;
            foreach (Transform child in transform)
            {
                
                foreach (Transform grandchild in child)
                {
                    Vector3 worldPosition = grandchild.TransformPoint(Vector3.zero);
                    if (worldPosition.x >= 10){
                        canMove = false;
                        break;
                    }
                    foreach (Transform block in blocks.transform){
                        if (block.position.z == worldPosition.z && block.position.x == worldPosition.x + 10){
                            if (Mathf.Abs(Mathf.Abs(block.position.y) - Mathf.Abs(worldPosition.y)) < 10.1f){
                                canMove = false;
                                break;
                            }
                        }
                    }
                }

            }
            if (canMove){
                transform.Translate(10f, 0f, 0f);
            }
            
        } 
    }
    void s(){
        if(isMoving_ && myCanvasScript.playPauseBTNString == "PAUSE"){
            bool canMove = true;
            foreach (Transform child in transform)
            {
                foreach (Transform grandchild in child)
                {
                    Vector3 worldPosition = grandchild.TransformPoint(Vector3.zero);
                    if (worldPosition.x <= -10){
                        canMove = false;
                        break;
                    }
                    foreach (Transform block in blocks.transform){
                        if (block.position.z == worldPosition.z && block.position.x == worldPosition.x - 10){
                            if (Mathf.Abs(Mathf.Abs(block.position.y) - Mathf.Abs(worldPosition.y)) < 10.1f){
                                canMove = false;
                                break;
                            }
                        }
                    }
                }

            }
            if (canMove){
                transform.Translate(-10f, 0f, 0f);
            }
            
        }
    }
    void a(){
        if(isMoving_ && myCanvasScript.playPauseBTNString == "PAUSE"){
            bool canMove = true;
            foreach (Transform child in transform)
            {
                foreach (Transform grandchild in child)
                {
                    Vector3 worldPosition = grandchild.TransformPoint(Vector3.zero);
                    if (worldPosition.z >= 10){
                        canMove = false;
                    }
                    foreach (Transform block in blocks.transform){
                        if (block.position.x == worldPosition.x && block.position.z == worldPosition.z + 10){
                            if (Mathf.Abs(Mathf.Abs(block.position.y) - Mathf.Abs(worldPosition.y)) < 10.1f){
                                canMove = false;
                                break;
                            }
                        }
                    }
                }

            }
            if (canMove){
                transform.Translate(0f, 0f, 10f);
            }
            
            
        }
    }
    void d(){
        if(isMoving_ && myCanvasScript.playPauseBTNString == "PAUSE"){
            bool canMove = true;
            foreach (Transform child in transform)
            {
                foreach (Transform grandchild in child)
                {
                    Vector3 worldPosition = grandchild.TransformPoint(Vector3.zero);
                    if (worldPosition.z <= -10){
                        canMove = false;
                    }
                    foreach (Transform block in blocks.transform){
                        if (block.position.x == worldPosition.x && block.position.z == worldPosition.z -10){
                            if (Mathf.Abs(Mathf.Abs(block.position.y) - Mathf.Abs(worldPosition.y)) < 10.1f){
                                canMove = false;
                                break;
                            }
                        }
                    }
                }

            }
            if (canMove){
                transform.Translate(0f, 0f, -10f);
            }
            
            
        }
    }
}