using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    Rigidbody myRigid;
    Animator anim;
    CapsuleCollider myCol;
    Transform camTransform;

    [HideInInspector] public Transform camHolder;

    [SerializeField] float lockMoveSpeed = 0.05f;
    [SerializeField] float normalMoveSpeed = 0.08f;
    float speed;

    [SerializeField] float trunSpeed = 5;

    public Vector3 directionPos;
    public Vector3 storeDir;

    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;
    [HideInInspector] public bool rollInput;
    [HideInInspector] public bool attackInput;
    [HideInInspector] public bool blockInput;

    public bool lockTarget;
    public int currentTarget;
    public bool changeTarget;

    float targetTurnAmount;
    float currentTurnAmount;
    public bool canMove;
    public List<Stats> Enemies = new List<Stats>();

    public Transform camTarget;
    public float camTargetSpeed = 5;
    Vector3 targetPos;
    public attackType currentAttackType;
    public float maxTargetDistance;
    public GameObject targetTrigger;
    public GameObject CurrentTargetCorsure;

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
        camTransform = Camera.main.transform;
        camHolder = camTransform.parent.parent;
        myCol = GetComponent<CapsuleCollider>();
        setUpAnimator();
        targetTrigger.SetActive(true);
        GetComponent<PlayerAnimationScript>().enabled = true;
	}



    // Update is called once per frame
    void Update () {
		
	}

    private void FixedUpdate()
    {
        checkInput();
        checkAttackInput();
        handleCameraTarget();

        if(canMove)
        {
            if (!lockTarget)
            {
                speed = normalMoveSpeed;
                HandleMovementNormal();
                if (Enemies.Count > 0 && Enemies.Count <= currentTarget)
                {
                    Enemies[currentTarget].showUrStats(false);
                }
            }
            else
            {
                speed = lockMoveSpeed;

                if (Enemies.Count > 0 && Enemies.Count -1 <= currentTarget)
                {
                    HandleMovementLockOn();
                    HandleRotationOnLock();
                    
                }
                else
                {
                    lockTarget = false;
                    Debug.Log("1");
                }
            }
        }
        
    }

    internal void getNearestTarget()
    {
        if (Enemies.Count > 0 && Enemies.Count - 1 <= currentTarget)
        {
            float minDistance = maxTargetDistance;
            float tmpDistance;

            for (int i = 0; i < Enemies.Count; i++)
            {
                tmpDistance = Mathf.Abs(Vector3.Distance(Enemies[i].transform.position, transform.position));
                if (tmpDistance < minDistance)
                {
                    currentTarget = i;
                    Debug.Log("2");
                    lockTarget = true;
                }
            }
        }
    }

    private void checkAttackInput()
    {
        if (!blockInput)
        {
            if (Input.GetButtonDown("Light") && Input.GetAxis("Vertical") < 0)
            {
                attackInput = true;
                currentAttackType = attackType.kick;
            }
            else if (Input.GetButtonDown("Light") && Input.GetAxis("Vertical") > 0)
            {
                attackInput = true;
                currentAttackType = attackType.MoveLightSlash;
            }
            else if (Input.GetButton("Light"))
            {
                attackInput = true;
                currentAttackType = attackType.standingSlashRight;
            }
            else if (Input.GetButtonDown("Heavy"))
            {
                attackInput = true;
                currentAttackType = attackType.standingHeavy;
            }
            else if (Input.GetButtonDown("Heavy") && Input.GetAxis("Vertical") > 0)
            {
                attackInput = true;
                currentAttackType = attackType.MoveHeavySlash;
            }
            else
            {
                attackInput = false;
            }
        }
    }

    private void HandleMovementNormal()
    {
        canMove = anim.GetBool("CanMove");

        Vector3 dirForward = storeDir * horizontal;
        Vector3 dirSIdes = camHolder.forward * vertical;
        Debug.Log(dirForward + " input =" + horizontal);
        Debug.Log(dirSIdes + " input =" + vertical);
        if (canMove)
        {
            //myRigid.AddForce((dirForward + dirSIdes).normalized * speed / Time.deltaTime);
            transform.position += (dirForward + dirSIdes) * speed * Time.deltaTime; ;
        }

        directionPos = transform.position + (storeDir * horizontal) + (camTransform.forward * vertical);

        Vector3 dir = directionPos - transform.position;
        dir.y = 0;

        float angle = Vector3.Angle(transform.forward, dir);

        float animValue = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

        animValue = Mathf.Clamp01(animValue);

        anim.SetFloat("Forward", animValue);
        anim.SetBool("LockOn", false);

        if(horizontal != 0 || vertical != 0)
        {
            if(angle != 0 && canMove)
            {
                myRigid.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), trunSpeed * Time.deltaTime);
            }
        }
    }

    private void HandleRotationOnLock()
    {
        Vector3 lookPos = Enemies[currentTarget].transform.position;

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;

        Quaternion rot = Quaternion.LookRotation(lookDir);
        myRigid.rotation = Quaternion.Slerp(myRigid.rotation, rot, Time.deltaTime * trunSpeed);
    }

    private void HandleMovementLockOn()
    {
        Transform camHolder = camTransform.parent.parent;
        Vector3 camForward = Vector3.Scale(camHolder.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(camHolder.right, new Vector3(1, 0, 1)).normalized;
        Vector3 move = vertical * camForward + horizontal * camTransform.right;

        Vector3 moveForward = camForward * vertical;
        Vector3 moveSideways = camRight * horizontal;
        transform.position += (moveForward + moveSideways) * speed * Time.deltaTime;
        //myRigid.AddForce((moveForward + moveSideways).normalized * speed / Time.deltaTime);

        convertMoveInputAndPassItAnimator(move);
    }

    private void convertMoveInputAndPassItAnimator(Vector3 move)
    {
        Vector3 localMove = transform.InverseTransformDirection(move);
        float turnAmount = localMove.x;
        float forwardAmount = localMove.z;

        if(turnAmount != 0)
        {
            turnAmount *= 2;

            if (turnAmount < 0.9 && turnAmount >-0.9)
            {
                turnAmount = 0;
            }
            /*
            if (forwardAmount < 0.9 && forwardAmount > -0.9)
            {
                forwardAmount = 0;
            }*/
            anim.SetBool("LockOn", true);
            anim.SetFloat("Forward", forwardAmount * 2, 0.1f, Time.deltaTime);
            anim.SetFloat("Sideways", turnAmount, 0.1f, Time.deltaTime);
        }
    }

    private void handleCameraTarget()
    {
        if (!lockTarget)
        {
            
            targetPos = transform.position;
            
        }
        else
        {
            if (Enemies.Count > 0 && Enemies[currentTarget] != null)
            {
                Vector3 direction = Enemies[currentTarget].transform.position - transform.position;
                direction.y = 0;

                float distance = Vector3.Distance(transform.position, Enemies[currentTarget].transform.position);

                targetPos = direction.normalized * distance / 4;

                targetPos += transform.position;

                if (distance > maxTargetDistance)
                {
                    Debug.Log("3");
                    lockTarget = false;
                }
                else
                {
                    Enemies[currentTarget].showUrStats(true);
                }
            }
            else
            {
                Debug.Log("4");
                lockTarget = false;
                
                if(Enemies.Count > 0 && Enemies.Count -1 <= currentTarget)
                {
                    Enemies.Remove(Enemies[currentTarget]);
                }
                Debug.Log("dead or away");
                getNearestTarget();
            }
        }
        camTarget.position = targetPos;
        
        
    }

    private void checkInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        rollInput = Input.GetButtonDown("Jump");
        blockInput = Input.GetButton("Block");
        storeDir = camHolder.right;
        changeTargetsLogic();
    }

    private void changeTargetsLogic()
    {
        if (Input.GetButtonUp("Fire3") && Enemies.Count != 0)
        {
            Debug.Log("!Self");
            lockTarget = !lockTarget;
        }
        if (Input.GetKeyUp(KeyCode.Q)) // Change To Input Setting
        {
            if(currentTarget < Enemies.Count - 1)
            {
                Enemies[currentTarget].statBar = false;
                currentTarget++;
            }
            else
            {
                Enemies[currentTarget].statBar = false;
                currentTarget = 0;
            }
        }
    }

    private void setUpAnimator()
    {
        anim = GetComponent<Animator>();

        foreach (var childAnimator in GetComponentsInChildren<Animator>())
        {
            if(childAnimator != anim)
            {
                anim.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
            }
        }
    }

    public void stopMoveAnim()
    {
        anim.SetFloat("Forward", 0,0f, Time.deltaTime);
        anim.SetFloat("Sideways", 0, 0f, Time.deltaTime);
    }
}
