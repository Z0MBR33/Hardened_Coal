using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkelettonAI : MonoBehaviour
{
    public bool cantMove;
    private NavMeshAgent myAgent;
    private GameObject myTarget;
    private Vector3 myStartPosition;
    private Vector3 myStartLookat;
    private bool followPlayer;

    public Stats myStats;
    public GameObject head;

    private Animator myAnim;

    public float myRadius;

    // Use this for initialization
    void Start()
    {
        myStats = GetComponent<Stats>();
        myStats.enabled = true;
        myStartPosition = transform.position;
        myStartLookat = transform.forward + myStartPosition;
        myAgent = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myStats.getHitTrigger)
        {
            myAnim.SetTrigger("React");
            cantMove = true;
            myAgent.isStopped = true;
            myStats.getHitTrigger = false;
        }
        if (!cantMove)
        {


            if (Vector3.Distance(transform.position, myStartPosition) > myRadius)
            {
                myAgent.SetDestination(myStartPosition);
                followPlayer = false;
                myStats.inCombat = false;

            }
            else if (followPlayer)
            {
                myAgent.SetDestination(myTarget.transform.position);

                if (myAgent.velocity.x <= 0.3f && myAgent.velocity.y <= 0.3f && myAgent.velocity.x >= -0.3f && myAgent.velocity.y >= -0.3f)
                {
                    transform.LookAt(new Vector3(myTarget.transform.position.x, transform.position.y, myTarget.transform.position.z));
                }
            }
            else
            {
                if (myAgent.velocity.x <= 0.1f && myAgent.velocity.y <= 0.1f && myAgent.velocity.x >= -0.1f && myAgent.velocity.y >= -0.1f)
                {
                    transform.LookAt(new Vector3(myStartLookat.x, transform.position.y, myStartLookat.z));

                }
            }


            if (myAgent.velocity != new Vector3(0, 0, 0))
            {
                myAnim.SetBool("Running", true);
            }
            else
            {
                myAnim.SetBool("Running", false);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {

        /*
        var CoolDownTime = 1f;
        var CoolDownEndTime;

        if (CoolDownEndTime < Time.time)
        {
            //DO it
            CoolDownEndTime = Time.time + CoolDownTime;
        }
        */



        if (col.gameObject.tag == "Player")
        {
            myTarget = col.gameObject;
            followPlayer = true;
            myStats.inCombat = true;
        }
    }
    public void reStartAgent()
    {
        myAgent.isStopped = false;
    }
}
