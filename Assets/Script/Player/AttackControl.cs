using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum attackType
{
    standingSlashRight,standingSlashLeft, standingHeavy, MoveLightSlash, MoveHeavySlash,kick,
    
}

public class AttackControl : MonoBehaviour {

    Animator anim;
    Rigidbody myRigid;
    PlayerControl plControl;
    Stats plStats;
    public DoDamage doDamage;
    public kickAttack kickDamage;

    bool attackInput;
    bool blockInput;
    public bool currentlyAttacking;
    bool blocking;
    bool decreaseStamina;
    attackType currentType;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        plControl = GetComponent<PlayerControl>();
        plStats = GetComponent<Stats>();
        doDamage = GetComponentInChildren<DoDamage>();
        kickDamage = GetComponentInChildren<kickAttack>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        UpdateInput();
        HandleAttacks();
        HandleBlock();
	}

    private void HandleBlock()
    {
        if (blockInput)
        {
            plControl.canMove = false;
            blocking = true;
            anim.SetBool("Block", true);
        }
        else
        {
            blocking = false;
            anim.SetBool("Block", false);
        }
    }

    private void HandleAttacks()
    {
        if(currentlyAttacking)
        {
            anim.applyRootMotion = true;
        }
        else
        {
            anim.applyRootMotion = false;
        }

        if(attackInput && !blocking && !currentlyAttacking)
        {
            plControl.canMove = false;
            currentlyAttacking = true;

            if (!decreaseStamina)
            {
                plStats.stamina -= 30;

                decreaseStamina = true;
            }

            StartCoroutine("InitiateAttack", plControl.currentAttackType);
        }
        else
        {
            decreaseStamina = false;
        }
    }

    private void UpdateInput()
    {
        this.blockInput = plControl.blockInput;
        this.attackInput = plControl.attackInput;
        
    }

    IEnumerator InitiateAttack(attackType type)
    {
        yield return new WaitForEndOfFrame();
        anim.SetBool("Attack", true);
        anim.SetInteger("AttackType", (int)type);
        if(type != attackType.kick)
        {
            
            plControl.stopMoveAnim();
            doDamage.letMeAttack(type);
        }
        else
        {
            plControl.stopMoveAnim();
            kickDamage.letMeKick();
        }
        
    }

    public void enableAttackColider()
    {
        doDamage.activateWeaponColider(true);
    }

    public void disableAttackColider()
    {
        doDamage.activateWeaponColider(false);
    }

    public void enableKickColider()
    {
        kickDamage.activateWeaponColider(true);
    }
    public void disableKickColider()
    {
        kickDamage.activateWeaponColider(false);
    }
}
