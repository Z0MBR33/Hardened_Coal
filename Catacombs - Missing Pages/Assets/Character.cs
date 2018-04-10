using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public Rigidbody myRigid;
    public Animator myAnimator;
    public float speed;
    public float jumpVelocity;
    private bool grounded;

    public int jumpCount;
    public int maxJumps;

	// Use this for initialization
	void Start () {
        if(GetComponent<Rigidbody>() != null)
        {
            myRigid = GetComponent<Rigidbody>();
        }
        /*
        if (GetComponent<Animator>() != null)
        {
            myAnimator = GetComponentInChildren<Animator>();
        }
        */

        jumpCount = maxJumps;

    }
	
	// Update is called once per frame
	void Update () {

            grounded = IsGrounded();
            myAnimator.SetBool("isGrounded", grounded);
        if (grounded)
        {
            jumpCount = maxJumps;
        }

        handleMovement();
        throwKnives();


    }
    void handleMovement()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") != 0 && grounded)
        {
            this.transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0);
            myAnimator.SetBool("isRunning", true);


        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            this.transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0);
            myAnimator.SetBool("isRunning", true);
        }

        else
        {
            myAnimator.SetBool("isRunning", false);
        }

        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            myAnimator.SetTrigger("Jump");
            jump();
            jumpCount--;

        }

        if (Input.GetAxis("Vertical") < 0)
        {
            transform.Rotate(0, 90, 0);
        }
    }

    void jump()
    {
        myRigid.AddForce(0,jumpVelocity, 0);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0, 0.025f, 0), -Vector3.up, 0.1f);
    }
    public void throwKnives()
    {

    }

}
