using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplayManagerScript : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;    

    private float yLevelToDetect = 5f;

    private GameObject blocks;

    private canvasScript myCanvasScript;

    void Start()
    {
        GameObject canvasObject = GameObject.Find("Canvas");
        myCanvasScript = canvasObject.GetComponent<canvasScript>();

        blocks = GameObject.Find("BLOCKS");
        
    }

    void Update(){
        detectLayer();
        if (myCanvasScript.needsToSpawn == true){
            myCanvasScript.needsToSpawn = false;
            spawnBlock();
        }
    }


    public void spawnBlock(){

        
        switch (Random.Range(1, 4))
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


    }

    public void detectLayer(){

    

        yLevelToDetect = 5;
        int i = 0;
        while(i < 4){

        
            while (yLevelToDetect <= 65){
                int numOfCollides = 0;

                foreach (Transform child in blocks.transform){

                    if (Mathf.Abs(child.position.y - yLevelToDetect) < 0.1f){
                        numOfCollides++;
                    }
                }
                if(numOfCollides >= 9){
                    myCanvasScript.numOfClears += 1;
                    myCanvasScript.scoreLBLTextInt += 400;
                    foreach (Transform child in blocks.transform){

                        if (Mathf.Abs(child.position.y - yLevelToDetect) < 0.1f){
                            Destroy(child.gameObject);
                        }
                    }

                    foreach (Transform child in blocks.transform){

                        if (child.position.y > yLevelToDetect){
                            Vector3 newPos = child.position;
                            newPos.y -= 10;
                            child.position = newPos;
                        }
                    }
                    yLevelToDetect += 10f;
                }
                else{
                    yLevelToDetect += 10f;
                }
                numOfCollides = 0;


            }
            i++;
            

        }


    }

}
