using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatorScript : MonoBehaviour
{
    public GameObject blocks;

    public bool hasCollided = false;

    public GameObject GHOSTS;

    private canvasScript myCanvasScript;

    void Start(){
        blocks = GameObject.Find("BLOCKS");
        GHOSTS = transform.Find("GHOSTS").gameObject;

        GameObject canvasObject = GameObject.Find("Canvas");
        myCanvasScript = canvasObject.GetComponent<canvasScript>();
    }


    void Update()
    {
        

        
        string rotateXKey = PlayerPrefs.GetString("RotateX", "R");
        KeyCode rotateXKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), rotateXKey);
        if (Input.GetKeyDown(rotateXKeyCode))
        {
            rotateX();
        }

        string rotateZKey = PlayerPrefs.GetString("RotateZ", "T");
        KeyCode rotateZKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), rotateZKey);
        if (Input.GetKeyDown(rotateZKeyCode))
        {
            rotateZ();
        }
        


    }

    

    void rotateX(){
        float ghostsMoved = 0f;
        if (GHOSTS != null && myCanvasScript.playPauseBTNString == "PAUSE"){

        
            bool canMove = true;

            GHOSTS.transform.Rotate(90f, 0f, 0f, Space.World);
            foreach(Transform ghost in GHOSTS.transform){
                if(Mathf.Abs(ghost.position.z) > 10.1){
                    
                    if(ghost.position.z > 0){
                        ghostsMoved -= 10f;
                    }
                    else{
                        ghostsMoved += 10f;
                    }
                    
                    break;
                }
                
                else if (ghost.position.y < 5.1) {
                    canMove = false;
                    break;
                }

                

            }
            foreach(Transform ghost in GHOSTS.transform){
                
                
                foreach (Transform block in blocks.transform){
                    if (Mathf.Abs(block.position.x - ghost.position.x) < 10f && Mathf.Abs(block.position.y - ghost.position.y) < 10f && Mathf.Abs(block.position.z - (ghost.position.z + ghostsMoved)) < 10f){
                        canMove = false;
                        break;
                    }
                }
                if (!canMove)
                {
                    break;
                }
            }

            GHOSTS.transform.Rotate(-90f, 0f, 0f, Space.World);

            if (canMove){
                
                transform.Rotate(90f, 0f, 0f, Space.World);
                transform.parent.Translate(new Vector3(0f, 0f, ghostsMoved));
            }


        }
    }


    void rotateZ(){
        float ghostsMovedX = 0f;
        float ghostsMovedZ = 0f;
        if (GHOSTS != null && myCanvasScript.playPauseBTNString == "PAUSE"){

       
            bool canMove = true;

            GHOSTS.transform.Rotate(0f, 90f, 0f, Space.World);
            foreach(Transform ghost in GHOSTS.transform){
                if(Mathf.Abs(ghost.position.x) > 10.1){
                    
                    
                    if(ghost.position.x > 0){
                        ghostsMovedX -= 10f;
                    }
                    else{
                        ghostsMovedX += 10f;
                    }
                    
                    break;
                }
                else if (Mathf.Abs(ghost.position.z) > 10.1){
                    
                    
                    if(ghost.position.z > 0){
                        ghostsMovedZ -= 10f;
                    }
                    else{
                        ghostsMovedZ += 10f;
                    }
                }
                
                

                
            
            }
            foreach(Transform ghost in GHOSTS.transform){
                
                
                foreach (Transform block in blocks.transform){
                    if (Mathf.Abs(block.position.x - (ghost.position.x + ghostsMovedX)) < 10f && Mathf.Abs(block.position.y - ghost.position.y) < 10f && Mathf.Abs(block.position.z - (ghost.position.z + ghostsMovedZ)) < 10f){
                        canMove = false;
                        break;
                    }
                }
                if (!canMove)
                {
                    break;
                }
            }

            GHOSTS.transform.Rotate(0f, -90f, 0f, Space.World);

            if (canMove){
                
                transform.Rotate(0f, 90f, 0f, Space.World);
                transform.parent.Translate(new Vector3(ghostsMovedX, 0f, ghostsMovedZ));
            }
            

        }
    }
    
        
    

    public void MoveChildrenToTarget()
    {
        foreach (Transform child in transform)
        {
            if (child != null && myCanvasScript.playPauseBTNString == "PAUSE")
            {
                if (child.gameObject.name == "GHOSTS")
                {
                    Destroy(child.gameObject);
                }
                else
                {
                    child.gameObject.tag = "block";
                    child.SetParent(blocks.transform);
                }
            }
        }
    }
}
