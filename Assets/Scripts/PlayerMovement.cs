using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float jumpHeight;
    [SerializeField] float groundCheckDistance;
    [SerializeField] float gravity;
    
    [SerializeField] LayerMask groundMask;

    private Vector3 moveDirection;
    private Vector3 velocity;

    public float moveZ;

    public bool isPlaying;

    CharacterController controller;
    CharacterController cameraController;

    [SerializeField] AudioSource BallSound;

    public string GameMode;

    Vector3 fp;   
    Vector3 lp;   
    float dragDistance; 
    

    private void Start()
    {
        controller = GetComponent < CharacterController > ();
   
    }

    private void Update()
    { 
        Move();

     if(isPlaying){
        moveSpeed += acceleration;
        }

    if(GameMode!="Classic" && GameMode!=String.Empty){

       

         if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list
 
                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            if(transform.position.x<3){
                            transform.position = new Vector3(transform.position.x+3,transform.position.y,transform.position.z);
                           
                            }
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            if(transform.position.x>-3){
                            transform.position = new Vector3(transform.position.x-3,transform.position.y,transform.position.z);
                           
                            }
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                            if(transform.position.y<6){
                            transform.position = new Vector3(transform.position.x,transform.position.y+3,transform.position.z);
                           
                            }
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                            if(transform.position.y>0){
                            transform.position = new Vector3(transform.position.x,transform.position.y-3,transform.position.z);
                           
                            }
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }
    }

    private void Move()
    {

        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        if(GameMode == "Classic"){
           if (Input.GetMouseButtonDown(0) &&  isPlaying)
            {
                Jump();
            
            }
      
            if(controller.enabled){
                controller.Move(moveDirection * Time.deltaTime);
                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }
           
        }
       

            if(controller.enabled){
                controller.Move(moveDirection * Time.deltaTime);
              
            }
         
    }

    private void Jump()
    {
      velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
      BallSound.Play();

    }

}