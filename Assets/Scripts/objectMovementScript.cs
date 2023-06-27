using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMovementScript : MonoBehaviour
{
    private int softscore = 0; // Soft score variable

    private GameObject blocks; // Reference to the parent object of blocks

    private gameplayManagerScript myGameplayManagerScript; // Reference to the gameplay manager script

    private canvasScript myCanvasScript; // Reference to the canvas script

    public int moveSpeed = 10; // Movement speed of the object
    public bool canSpawn = true; // Flag to determine if a new block can be spawned
    public bool isMoving_ = true; // Flag to indicate if the object is currently moving

    void Start()
    {
        blocks = GameObject.Find("BLOCKS"); // Find the parent object of blocks

        GameObject gameplayManagerObject = GameObject.Find("gameplayManager");
        myGameplayManagerScript = gameplayManagerObject.GetComponent<gameplayManagerScript>(); // Get the gameplay manager script

        GameObject canvasObject = GameObject.Find("Canvas");
        myCanvasScript = canvasObject.GetComponent<canvasScript>(); // Get the canvas script component
    }

    void Update()
    {
        if (myCanvasScript.speed != moveSpeed)
        {
            moveSpeed = myCanvasScript.speed; // Update the movement speed from the canvas script
        }

        if (myCanvasScript.playPauseBTNString == "PAUSE")
        {
            if (isMoving_)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    softscore += 1;
                    if (moveSpeed < 60)
                    {
                        transform.Translate(0, (-60) * Time.deltaTime, 0);
                    }
                    else
                    {
                        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
                    }
                }
                else
                {
                    transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
                }
            }
            else
            {
                if (canSpawn)
                {
                    if ((softscore / 20) > 40)
                    {
                        softscore = 40;
                    }
                    else
                    {
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

    // Move the object forward
    void forward()
    {
        if (isMoving_ && myCanvasScript.playPauseBTNString == "PAUSE")
        {
            bool canMove = true;
            foreach (Transform child in transform)
            {
                foreach (Transform grandchild in child)
                {
                    Vector3 worldPosition = grandchild.TransformPoint(Vector3.zero);
                    if (worldPosition.x >= 10)
                    {
                        canMove = false; // Object cannot move forward if it reaches the boundary
                        break;
                    }
                    foreach (Transform block in blocks.transform)
                    {
                        
                        if (Mathf.Abs(block.position.z - worldPosition.z) < 0.001f && Mathf.Abs(block.position.x - (worldPosition.x + 10)) < 0.001f)
                        {
                                
                            if (Mathf.Abs(block.position.y - worldPosition.y) < 10.1f)
                            {
                                canMove = false; // Object cannot move forward if there's a block in the way
                                break;
                            }
                                                       
                        }
                    }
                }
            }
            if (canMove)
            {
                transform.Translate(10f, 0f, 0f); // Move the object forward
            }
        }
    }

    // Move the object backward
    void backward()
    {
        if (isMoving_ && myCanvasScript.playPauseBTNString == "PAUSE")
        {
            bool canMove = true;
            foreach (Transform child in transform)
            {
                foreach (Transform grandchild in child)
                {
                    Vector3 worldPosition = grandchild.TransformPoint(Vector3.zero);
                    if (worldPosition.x <= -10)
                    {
                        canMove = false; // Object cannot move backward if it reaches the boundary
                        break;
                    }
                    foreach (Transform block in blocks.transform)
                    {
                        if (Mathf.Abs(block.position.z - worldPosition.z) < 0.001f && Mathf.Abs(block.position.x - (worldPosition.x - 10)) < 0.001f)
                        {
                            if (Mathf.Abs(block.position.y - worldPosition.y) < 10.1f)
                            {
                                canMove = false; // Object cannot move backward if there's a block in the way
                                break;
                            }
                        }
                    }
                }
            }
            if (canMove)
            {
                transform.Translate(-10f, 0f, 0f); // Move the object backward
            }
        }
    }

    // Move the object to the left
    void left()
    {
        if (isMoving_ && myCanvasScript.playPauseBTNString == "PAUSE")
        {
            bool canMove = true;
            foreach (Transform child in transform)
            {
                foreach (Transform grandchild in child)
                {
                    Vector3 worldPosition = grandchild.TransformPoint(Vector3.zero);
                    if (worldPosition.z >= 10)
                    {
                        canMove = false; // Object cannot move to the left if it reaches the boundary
                        break;
                    }
                    foreach (Transform block in blocks.transform)
                    {
                        if (block.position.x == worldPosition.x && block.position.z == worldPosition.z + 10)
                        {
                            if (Mathf.Abs(block.position.y - worldPosition.y) < 10.1f)
                            {
                                canMove = false; // Object cannot move to the left if there's a block in the way
                                break;
                            }
                        }
                    }
                }
            }
            if (canMove)
            {
                transform.Translate(0f, 0f, 10f); // Move the object to the left
            }
        }
    }

    // Move the object to the right
    void right()
    {
        if (isMoving_ && myCanvasScript.playPauseBTNString == "PAUSE")
        {
            bool canMove = true;
            foreach (Transform child in transform)
            {
                foreach (Transform grandchild in child)
                {
                    Vector3 worldPosition = grandchild.TransformPoint(Vector3.zero);
                    if (worldPosition.z <= -10)
                    {
                        canMove = false; // Object cannot move to the right if it reaches the boundary
                        break;
                    }
                    foreach (Transform block in blocks.transform)
                    {
                        if (block.position.x == worldPosition.x && block.position.z == worldPosition.z - 10)
                        {
                            if (Mathf.Abs(Mathf.Abs(block.position.y) - Mathf.Abs(worldPosition.y)) < 10.1f)
                            {
                                canMove = false; // Object cannot move to the right if there's a block in the way
                                break;
                            }
                        }
                    }
                }
            }
            if (canMove)
            {
                transform.Translate(0f, 0f, -10f); // Move the object to the right
            }
        }
    }
}

           