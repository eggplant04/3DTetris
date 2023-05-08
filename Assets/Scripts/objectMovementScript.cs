using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMovementScript : MonoBehaviour
{

    private Rigidbody rb;
    public float moveSpeed = 10f;
    
    public float moveDelay = 3f;

    private bool isMoving = false;


    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    public GameObject DETECTPLANESPrefab;

    private GameObject objectToDestroy;

    void Start()
    {
        objectToDestroy = GameObject.Find("DETECTPLANES(Clone)");
        rb = GetComponent<Rigidbody>();
        Invoke("StartMoving", moveDelay);
    }

    void Update()
    {
        

        if (isMoving)
        {

            if (transform.CompareTag("block")){
            
                GameObject DETECTPLANES = Instantiate(DETECTPLANESPrefab);
            
                isMoving = false;

                int randomNumber = Random.Range(1, 4);
        
                switch (randomNumber)
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


            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(0, (-moveSpeed* 6) * Time.deltaTime, 0);
            }
            else{
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
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

    void StartMoving()
    {
        isMoving = true;
        if (objectToDestroy != null) {
                    GameObject.Destroy(objectToDestroy);
        }
    }
    

    void w(){
        if(!transform.CompareTag("block")){
            transform.Translate(10f, 0f, 0f);
        } 
    }
    void s(){
        if(!transform.CompareTag("block")){
            transform.Translate(-10f, 0f, 0f);
        }
    }
    void a(){
        if(!transform.CompareTag("block")){
            transform.Translate(0f, 0f, 10f);
        }
    }
    void d(){
        if(!transform.CompareTag("block")){
            transform.Translate(0f, 0f, -10f);
        }
    }
    
}