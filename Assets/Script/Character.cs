using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    inCombat,idle,block,attack,runAttack
}

public enum MovementState
{
    Stand,Run,Jump,Back
}

public class Character : MonoBehaviour {
    static Animator anim;
    static RuntimeAnimatorController aC;
    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;
    public Rigidbody rigid;
    public Transform point;
    public float ClampDistance;
    public float GroundDistance;
    public float jumpHeight;

    public bool onGround;
    public bool notToSteep;
    private PlayerState myState;
    private MovementState myMoveState;

    private bool paused = false;

    private int comboCount = 0;
    private int maxCombo = 3;

    public GameObject SwordSheathed;
    public GameObject SwordDraw;

    public float drawSwitchTime;
    public float sheathSwitchTime;

    public bool inRunAttack;

    public Transform directionTarget;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        myState = PlayerState.idle;
        SwordDraw.SetActive(false);
        SwordSheathed.SetActive(true);
	}

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            if (GroundCheck())
            {
                anim.SetBool("Grounded", true);
                onGround = true;
            }
            else
            {
                anim.SetBool("Grounded", false);
                onGround = false;
            }

            if(myState == PlayerState.inCombat)
            {
                myState = InCombat();
                return;
            }

            if (myState == PlayerState.idle)
            {
                myState = Idle();
                return;
            }

            if( myState == PlayerState.attack)
            {
                myState = inAttack();
            }

        }
           
    }

    void OnCollisionExit(Collision col)
    {

    }

    void OnCollisionEnter(Collision col)
    {

    }

    void MoveFoward()
    {
        float translation;
        
        RaycastHit hit;
        if (Physics.Raycast(point.position, transform.forward, out hit, ClampDistance))
        {
            if (Vector3.Dot(Vector3.up, hit.normal) > 0.7f || hit.collider.isTrigger)
            {
                
                translation = Input.GetAxis("Vertical") * speed;
                translation *= Time.deltaTime;
                if (translation > 0)
                {
                    anim.SetFloat("direction", 1f);
                    anim.SetBool("Running", true);
                    translation *= 0.8f;
                    transform.Translate(0, 0, translation);
                    myMoveState = MovementState.Run;

                }

                else
                {
                    anim.SetBool("Running", false);
                    Debug.Log("In Second");
                    myMoveState = MovementState.Stand;

                }
            }
        }

        else
        {

            translation = Input.GetAxis("Vertical") * speed;
            translation *= Time.deltaTime;

            if (translation > 0)
            {
                anim.SetFloat("direction", 1f);
                anim.SetBool("Running", true);
                translation *= 0.8f;
                transform.Translate(0, 0, translation);
                myMoveState = MovementState.Run;

            }
        
        }
        
    }

    

    private void Jump()
    {
        if (GroundCheck())
        {
            RaycastHit hit;
            if (Physics.Raycast(point.position, transform.forward, out hit, GroundDistance))
            {
                if (Vector3.Dot(Vector3.up, hit.normal) < 0.25f && onGround)
                {
                    rigid.AddForce(new Vector3(0, jumpHeight, 0));
                    anim.SetTrigger("Jump");
                    myMoveState = MovementState.Jump;
                }
                else
                {
                    
                }
            }

            else{

                if(onGround)
                {
                    rigid.AddForce(new Vector3(0, jumpHeight, 0));
                    anim.SetTrigger("Jump");
                    myMoveState = MovementState.Jump;
                }
            }
            
        }
    }

    private void MoveBackwards()
    {
        float translation;

        RaycastHit hit;
        if (Physics.Raycast(point.position, -transform.forward, out hit, ClampDistance))
        {
            if (Vector3.Dot(Vector3.up, hit.normal) > 0.7f || hit.collider.isTrigger)
            {
                translation = Input.GetAxis("Vertical") * speed;
                translation *= Time.deltaTime;
                if (translation < 0)
                {
                    anim.SetFloat("direction", -1f);
                    anim.SetBool("Running", true);
                    myMoveState = MovementState.Run;
                    translation *= 0.5f;
                    transform.Translate(0, 0, translation);

                }

                else
                {
                    anim.SetBool("Running", false);
                    myMoveState = MovementState.Stand;

                }
            }
        }

        else
        {

            translation = Input.GetAxis("Vertical") * speed;
            translation *= Time.deltaTime;

            if (translation < 0)
            {
                anim.SetFloat("direction", -1f);
                anim.SetBool("Running", true);
                myMoveState = MovementState.Back;
                translation *= 0.5f;
                transform.Translate(0, 0, translation);

            }

        }
    }

    bool GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(point.position, -transform.up, out hit, GroundDistance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private PlayerState Idle()
    {
        
        if (Input.GetAxis("Vertical") > 0.1)
        {
            MoveFoward();
        }
        else if (Input.GetAxis("Vertical") < -0.1)
        {
            MoveBackwards();
        }
        else
        {
            anim.SetBool("Running", false);
            myMoveState = MovementState.Stand;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        rotation *= Time.deltaTime;

        transform.Rotate(0, rotation, 0);

        if (Input.GetKeyDown(KeyCode.LeftShift) && myState != PlayerState.inCombat && GroundCheck())
        {
            paused = true;
            anim.SetTrigger("Draw");
            return PlayerState.inCombat;
        }

        return myState;
    }

    private PlayerState InCombat()
    {
       
        comboCount = 0;
        if (Input.GetAxis("Vertical") > 0.1)
        {
            MoveFoward();
        }
        else if (Input.GetAxis("Vertical") < -0.1)
        {
            MoveBackwards();
        }
        else
        {
            anim.SetBool("Running", false);
            myMoveState = MovementState.Stand;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        rotation *= Time.deltaTime;

        transform.Rotate(0, rotation, 0);

        if (Input.GetKeyDown(KeyCode.LeftShift) && myState == PlayerState.inCombat && GroundCheck())
        {
            paused = true;
            anim.SetTrigger("Sheath");
            return PlayerState.idle;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && myState == PlayerState.inCombat && GroundCheck())
        {
            AttackSystem();
            return PlayerState.attack;
        }
        return myState;
    }

    private PlayerState inAttack()
    {
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        rotation *= Time.deltaTime;

        transform.Rotate(0, rotation, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0) && comboCount < maxCombo && GroundCheck()) // Combo
        {
            return PlayerState.attack;
        }

        return PlayerState.inCombat;
    }

    void OnPauseGame()
    {
        paused = true;
    }

    void OnResumeGame()
    {
        paused = false;
    }

    void AttackSystem()
    {
        if(myMoveState == MovementState.Stand)
        {
            StandCombo();
        }
        if (myMoveState == MovementState.Run)
        {
            Run_Combo();
        }
    }

    void StandCombo()
    {
        ++comboCount;
        paused = true;
        anim.SetTrigger("Attack");
    }

    void Run_Combo()
    {
        
        if(comboCount == 0 || true)
        {
            inRunAttack = true;
            rigid.AddForce(transform.forward * 600);
            paused = true;
            anim.SetTrigger("Attack");
            
            ++comboCount;
            inRunAttack = false;
        }
        else if(comboCount == 1)
        {
            paused = true;
            anim.SetTrigger("Attack");
            ++comboCount;
        }
        
    }

    public void stopRunning()
    {
        anim.SetBool("Running", false);
    }

    public void WaitForAnimation()
    {
        paused = false;
    }   

    public void SheathForMe()
    {
        SwordDraw.SetActive(false);
        SwordSheathed.SetActive(true);

    }

    public void DrawForMe()
    {
        SwordDraw.SetActive(true);
        SwordSheathed.SetActive(false);

    }
}

/*
 * if(true)//Input.GetAxis("Vertical") < -0.2f && Input.GetAxis("Vertical")> 0.2f
        {
            
        }
        
            RaycastHit hit;
            if (Physics.Raycast(point.position, transform.forward, out hit, ClampDistance))
            {
                if (Vector3.Dot(Vector3.up, hit.normal) > 0.7f)
                {
                    float translation = Input.GetAxis("Vertical") * speed;
                    translation *= Time.deltaTime;

                    transform.Translate(0, 0, translation);

                    if (translation > 0)
                    {
                        anim.SetFloat("direction", 1f);
                        anim.SetBool("Running", true);
                        translation *= 0.8f;
                    }
                    else if (translation < 0)
                    {
                        anim.SetFloat("direction", -1f);
                        anim.SetBool("Running", true);
                        translation *= 0.5f;
                    }
                    else
                    {
                        anim.SetBool("Running", false);
                    }
                    if (Input.GetButtonDown("Jump") && onGround)
                    {
                        rigid.AddForce(new Vector3(0, 300, 0));
                        anim.SetTrigger("Jump");
                    }
                }
                else
                {
                anim.SetBool("Running", false);
                }
            }
        else
        {
            float translation = Input.GetAxis("Vertical") * speed;
            translation *= Time.deltaTime;

            transform.Translate(0, 0, translation);

            if (translation > 0)
            {
                anim.SetFloat("direction", 1f);
                anim.SetBool("Running", true);
                translation *= 0.8f;
            }
            else if (translation < 0)
            {
                anim.SetFloat("direction", -1f);
                anim.SetBool("Running", true);
                translation *= 0.5f;
            }
            else
            {
                anim.SetBool("Running", false);
            }

            if (Input.GetButtonDown("Jump") && onGround)
            {
                        rigid.AddForce(new Vector3(0, 300, 0));
                        anim.SetTrigger("Jump");
             }
            
        }
*/