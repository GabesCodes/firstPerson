using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerScript : MonoBehaviour
{
    Health health;
   

    [SerializeField]
    public float currentSpeed;
    public float walkSpeed = 7.5f;
    public float sprintModifier = 2.5f;

    [SerializeField]
    private float jumpSpeed = 3.5f;

    //[SerializeField]
    public static int playerHealth;
    public static bool isGameOver;
    public static bool isDead;

    [SerializeField]
    private float gravity = 9.81f;
    public float xPos;
    public float zPos;
    public Vector3 velocity;

    float playerHp;
   
    private CharacterController ch;

    // Start is called before the first frame update
    void Start()
    {
        ch = gameObject.GetComponent<CharacterController>();
        playerHp = gameObject.GetComponent<Health>().health;        
        isDead = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerHp);
        //collect input
        xPos = Input.GetAxis("Horizontal"); //x is horizontal, i.e A and D key 
        zPos = Input.GetAxis("Vertical"); //z is vertical, i.e W and S key, 

        //keeps our velocity from forever incrementing
        if (groundCheck.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (groundCheck.isGrounded )
        {
            if (Input.GetButtonDown("Jump"))
            {
                //directionY is equal to jumpSpeed value
                velocity.y = jumpSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            selfHeal();
        }
        
        //sprint code - look into turning this into a method
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Sprint();
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        void selfHeal()
        {
            this.GetComponent<Health>().health++;
        }

        void Sprint()
        {
            currentSpeed = walkSpeed + sprintModifier;
            //Debug.Log("running!");
        }
   

        //create vector 3 move, transform (move player) to the right * xPos and do the same for zPos
        //this is better than previous implementation bc this takes in the objects current situation instead of global 
        Vector3 move = transform.right * xPos + transform.forward * zPos;

        //direction Y is then subtracted by the gravity value every frame?
        velocity.y -= gravity * Time.deltaTime;

        //then we update our move vector3 y value with our calculated directionY value
        move.y = velocity.y;

        //ultimately moves our character according to our calculations from the previous move vector3 * speed * deltatime(idk this one)
        ch.Move(move * currentSpeed * Time.deltaTime);
    }

}