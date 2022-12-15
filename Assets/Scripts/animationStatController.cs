using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStatController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPresed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        //if player presses w
        if (!isWalking && forwardPresed)
        {
            //set isWalking to true
            animator.SetBool(isWalkingHash, true);
        }
        //if player is not pressing w key
        if (isWalking && !forwardPresed)
        {
            //set isWalking to be false
            animator.SetBool(isWalkingHash, false);
        }
        //if player is not running, player is walking and presses left shift
        if (!isRunning && (forwardPresed && runPressed))
        {
            //set isRunning to true
            animator.SetBool(isRunningHash, true);
        }
        //if player is running and stops running or stops walking
        if(isRunning && (!forwardPresed || !runPressed))
        {
            //set isRunning to be false
            animator.SetBool(isRunningHash, false);
        }
    }
}
