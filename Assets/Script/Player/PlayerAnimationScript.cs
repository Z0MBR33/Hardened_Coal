using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour {

    PlayerControl playerControl;

    public float rollSpeed = 15;

    float horizontal;
    float vertical;
    bool rollInput;

    public bool rolling;
    bool hasRolled;

    Vector3 directionPos;
    Vector3 storeDir;
    Transform camHolder;
    Rigidbody myrigid;
    Animator anim;

    public bool hasDirection;

    Vector3 dirForward;
    Vector3 dirSides;
    Vector3 dir;

	// Use this for initialization
	void Start () {
        playerControl = GetComponent<PlayerControl>();
        camHolder = playerControl.camHolder;
        myrigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.rollInput = playerControl.rollInput;
        this.horizontal = playerControl.horizontal;
        this.vertical = playerControl.vertical;
        storeDir = camHolder.right;

        if(rollInput)
        {
            if (!rolling)
            {
                playerControl.canMove = false;
                rolling = true;
            }
        }

        if (rolling)
        {
            if(!hasDirection)
            {
                if(dirForward == new Vector3(0,0,0) && dirSides == new Vector3(0,0,0))
                {
                    dirForward = new Vector3(1,0,0);
                }
                dirForward = storeDir * horizontal;
                dirSides = camHolder.forward * vertical;

                directionPos = transform.position + (storeDir * horizontal) + (camHolder.forward * vertical);
                dir = directionPos - transform.position;
                dir.y = 0;

                anim.SetTrigger("Roll");
                hasDirection = true;
            }

            myrigid.AddForce((dirForward + dirSides).normalized * rollSpeed / Time.deltaTime);
            float angle = Vector3.Angle(transform.forward, dir);
            /**
            float animValue = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

            animValue = Mathf.Clamp01(animValue);
            **/
            if (angle != 0)
            {
                myrigid.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 15 * Time.deltaTime);
            }
        }
	}
}
