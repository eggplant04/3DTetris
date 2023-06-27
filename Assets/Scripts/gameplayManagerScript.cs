using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplayManagerScript : MonoBehaviour
{
    // Reference to the tetris block prefabs
    public GameObject object1; 
    public GameObject object2; 
    public GameObject object3; 

    private float yLevelToDetect = 5f; // The initial y-level to detect collisions

    private GameObject blocks; // Reference to the parent object holding all fallen blocks

    private canvasScript myCanvasScript; // Reference to the canvas script

    public int next; // Represents the next block to spawn

    void Start()
    {
        // Find the Canvas object and get the canvas script component
        GameObject canvasObject = GameObject.Find("Canvas");
        myCanvasScript = canvasObject.GetComponent<canvasScript>();

        // Find the parent object of blocks
        blocks = GameObject.Find("BLOCKS");

        // Generate a random number between 1 and 3 to determine the next block to spawn
        next = Random.Range(1, 4);
    }

    void Update()
    {
        // Call the detectLayer function to check for complete layers
        detectLayer();
        
        // Check if a new block needs to be spawned
        if (myCanvasScript.needsToSpawn == true)
        {
            myCanvasScript.needsToSpawn = false;
            
            // Call the spawnBlock function to spawn a new block
            spawnBlock();
        }
    }

    // Function to spawn a new block
    public void spawnBlock()
    {
        // Instantiate the next block based on the value of the 'next' variable
        switch (next)
        {
            case 1:
                Instantiate(object1);
                break;
            case 2:
                Instantiate(object2);
                break;
            case 3:
                Instantiate(object3);
                break;
            default:
                Debug.LogError("Invalid random number generated!");
                break;
        }

        // Generate a new random number for the next block
        next = Random.Range(1, 4);
    }

    // Function to check for complete layers and clear them
    public void detectLayer()
    {
        yLevelToDetect = 5; // Set the initial y-level to detect collisions
        int i = 0;
        
        // Repeat the detection process for four different y-levels
        while (i < 4)
        {
            // Loop through each y-level to check for complete layers
            while (yLevelToDetect <= 65)
            {
                int numOfCollides = 0; // Counter to keep track of the number of collisions

                // Check each block's position at the current y-level
                foreach (Transform child in blocks.transform)
                {
                    if (Mathf.Abs(child.position.y - yLevelToDetect) < 0.1f)
                    {
                        numOfCollides++; // Increment the collision counter if a block is found at the y-level
                    }
                }

                if (numOfCollides >= 9)
                {
                    // If there are 9 or more blocks at the y-level, clear the layer
                    myCanvasScript.numOfClears += 1; // Increment the number of cleared layers counter
                    myCanvasScript.scoreLBLTextInt += 400; // Increase the score

                    // Destroy the blocks at the cleared y-level
                    foreach (Transform child in blocks.transform)
                    {
                        if (Mathf.Abs(child.position.y - yLevelToDetect) < 0.1f)
                        {
                            Destroy(child.gameObject);
                        }
                    }

                    // Move down the remaining blocks above the cleared layer
                    foreach (Transform child in blocks.transform)
                    {
                        if (child.position.y > yLevelToDetect)
                        {
                            Vector3 newPos = child.position;
                            newPos.y -= 10;
                            child.position = newPos;
                        }
                    }
                }
                
                yLevelToDetect += 10f; // Move to the next y-level
                numOfCollides = 0; // Reset the collision counter
            }
            
            i++; // Move to the next iteration
        }
    }
}