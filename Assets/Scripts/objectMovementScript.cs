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


    public int moveSpeed = 10;
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
        
        if(myCanvasScript.speed != moveSpeed){
            moveSpeed = myCanvasScript.speed;
        }

        if (myCanvasScript.playPauseBTNString == "PAUSE"){

        
            if (isMoving_ )
            {


                if (Input.GetKey(KeyCode.Space))
                {
                    softscore += 1;
                    if (moveSpeed < 60){
                        transform.Translate(0, (-60) * Time.deltaTime, 0);
                    }
                    else{
                        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
                    }
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


            
            string forwardKey = PlayerPrefs.GetString("Forward", "W");
            KeyCode forwardKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), forwardKey);

            if (Input.GetKeyDown(forwardKeyCode))
            {
                forward();
            }

            string backwardKey = PlayerPrefs.GetString("Backward", "S");
            KeyCode backwardKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), backwardKey);

            if (Input.GetKeyDown(backwardKeyCode))
            {
                backward();
            }

            string leftKey = PlayerPrefs.GetString("Left", "A");
            KeyCode leftKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), leftKey);

            if (Input.GetKeyDown(leftKeyCode))
            {
                left();
            }

            string rightKey = PlayerPrefs.GetString("Right", "D");
            KeyCode rightKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), rightKey);

            if (Input.GetKeyDown(rightKeyCode))
            {
                right();
            }
        
        }

    }

    
    

    void forward(){
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
    void backward(){
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
    void left(){
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
    void right(){
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