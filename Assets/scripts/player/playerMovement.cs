using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
    public float maxSpeed = 10.0f;
    public float timeBetweenFastAndSlow = 0.1f;
    public float playerStopTime = 0.05f;
    public float shipBoundry = 20.0f;
    private float moveTimer = 0;
    private float stopTime = 0;


    private SpriteRenderer spriteRenderer;
    private Animator animationController;

	// Use this for initialization
	void Start () {
            animationController = GetComponent<Animator>();
	}

    // Update is called once per frame.  This is where we should get inputs and make changes to the game
    // based on the inputs.
    void Update () {
        Vector3 pos = transform.position;
        float inputAxis = Input.GetAxis("Horizontal");

        if(inputAxis > 0.75 || inputAxis < -0.75)
        {
            moveTimer += Time.deltaTime;
            stopTime = playerStopTime;
            pos.x += inputAxis * maxSpeed * Time.deltaTime;

            if(moveTimer < timeBetweenFastAndSlow){
                // Check if we are going left or right.
                if(inputAxis > 0){
                    animationController.SetInteger("Speed", 1);
                }
                else{
                    animationController.SetInteger("Speed", -1);
                }
            }

            else{
                // Check if we are going left or right.
                if(inputAxis > 0){
                    animationController.SetInteger("Speed", 2);
                }
                else{
                    animationController.SetInteger("Speed", -2);
                }
            }
        }
        else if(inputAxis > 0.05 || inputAxis < -0.05)
        {
            moveTimer = 0;
            stopTime = playerStopTime;
            pos.x += inputAxis * maxSpeed * Time.deltaTime;

            // Check if we are going left or right.
            if(inputAxis > 0){
                animationController.SetInteger("Speed", 1);
            }
            else{
                animationController.SetInteger("Speed", -1);
            }         
        }
        else{
            moveTimer = 0;
            stopTime -= Time.deltaTime;

            if(stopTime < 0){
                animationController.SetInteger("Speed", 0);
            }
            else{
                int currentSpeed = animationController.GetInteger("Speed");
                if(currentSpeed > 0){
                    animationController.SetInteger("Speed", 1);
                }
                else{
                    animationController.SetInteger("Speed", -1);
                }
            }
        }

        // Keep the player in the boundaries
        if(pos.x > shipBoundry)
        {
            pos.x = -shipBoundry;
        }
        if (pos.x < -shipBoundry)
        {
            pos.x = shipBoundry;
        }

        // Set the position to the calculated value.
        transform.position = pos;
    }
}
