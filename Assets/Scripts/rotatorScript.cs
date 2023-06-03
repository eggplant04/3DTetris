using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatorScript : MonoBehaviour
{
    public GameObject blocks;

    public bool hasCollided = false;

    public GameObject GHOSTS;

    void Start(){
        blocks = GameObject.Find("BLOCKS");
        GHOSTS = transform.Find("GHOSTS").gameObject;

    }


    void Update()
    {
        

        

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
        bool canMove = true;

        GHOSTS.transform.Rotate(90f, 0f, 0f, Space.World);
        foreach(Transform ghost in GHOSTS.transform){
            if(Mathf.Abs(ghost.position.x) > 10.1 || Mathf.Abs(ghost.position.z) > 10.1 || ghost.position.y < 5.1){
                canMove = false;
                break;
            }

            foreach (Transform block in blocks.transform){
                if (Mathf.Abs(block.position.x - ghost.position.x) < 10f && Mathf.Abs(block.position.y - ghost.position.y) < 10f && Mathf.Abs(block.position.z - ghost.position.z) < 10f){
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
        }


    }

    void t(){
        bool canMove = true;

        GHOSTS.transform.Rotate(0f, 90f, 0f, Space.World);
        foreach(Transform ghost in GHOSTS.transform){
            if(Mathf.Abs(ghost.position.x) > 10.1 || Mathf.Abs(ghost.position.z) > 10.1 || ghost.position.y < 5.1){
                canMove = false;
                break;
            }

            foreach (Transform block in blocks.transform){
                if (Mathf.Abs(block.position.x - ghost.position.x) < 10f && Mathf.Abs(block.position.y - ghost.position.y) < 10f && Mathf.Abs(block.position.z - ghost.position.z) < 10f){
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
        }
    }
    void y(){
        bool canMove = true;

        GHOSTS.transform.Rotate(0f, 0f, 90f, Space.World);
        foreach(Transform ghost in GHOSTS.transform){
            if(Mathf.Abs(ghost.position.x) > 10.1 || Mathf.Abs(ghost.position.z) > 10.1 || ghost.position.y < 5.1){
                canMove = false;
                break;
            }

            foreach (Transform block in blocks.transform){
                if (Mathf.Abs(block.position.x - ghost.position.x) < 10f && Mathf.Abs(block.position.y - ghost.position.y) < 10f && Mathf.Abs(block.position.z - ghost.position.z) < 10.1f){
                    canMove = false;
                    break;
                }
            }
            if (!canMove)
            {
                break;
            }

        }
        GHOSTS.transform.Rotate(0f, 0f, -90f, Space.World);
        if (canMove){
            transform.Rotate(0f, 0f, 90f, Space.World);
        }
    }

    public void MoveChildrenToTarget()
    {
        foreach (Transform child in transform)
        {
            if (child != null)
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
