using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour {

    public float weaponDamage = 5;
    private float attackTypeDamage;
    private float currentDamage;
    private Stats plStats;
    private Collider myTrigger;

	// Use this for initialization
	void Start () {
        plStats = GetComponentInParent<Stats>();
        myTrigger = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void letMeAttack(attackType aT)
    {
       
        if(aT != attackType.kick)
        {
            currentDamage = (weaponDamage + plStats.strenght / 2)*getAttackBonus(aT);
        }
        else
        {
            currentDamage = (plStats.dexterity + plStats.strenght) * getAttackBonus(aT);
        }
        
    }

    float getAttackBonus(attackType aT)
    {
        if(aT == attackType.standingSlashRight)
        {
            return 0.90f;
        }
        if (aT == attackType.standingSlashLeft)
        {
            return 0.90f;
        }
        if (aT == attackType.MoveHeavySlash)
        {
            return 1.2f;
        }
        if (aT == attackType.standingHeavy)
        {
            return 1.15f;
        }
        if (aT == attackType.kick)
        {
            return 0.9f;
        }
        return 0;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Stats>() && !collision.isTrigger)
        {
            
            collision.gameObject.GetComponent<Stats>().health -= currentDamage;
            collision.gameObject.GetComponent<Stats>().getHitTrigger = true;
        }
    }

    public void activateWeaponColider(bool v)
    {
        myTrigger.enabled = v;
    }
}
