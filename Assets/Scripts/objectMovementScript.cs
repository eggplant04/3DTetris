using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMovementScript : MonoBehaviour
{

    private Rigidbody rb;
    public float moveSpeed = 10f;
    
    public float moveDelay = 3f;

    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("StartMoving", moveDelay);
    }

    void Update()
    {
        

        if (isMoving)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(0, (-moveSpeed* 3) * Time.deltaTime, 0);
            }
            else{
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
            }
        }
        
        if (transform.CompareTag("block")){
            isMoving = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Vector3 position = transform.position;
            position.y = (Mathf.Round(position.y/5) * 5);
            transform.position = position;
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
