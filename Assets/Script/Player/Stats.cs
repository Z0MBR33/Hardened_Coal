using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
    [HideInInspector]
    public bool statBar = false;

    public float health = 100;
    public float maxHealth = 100;
    public float healthReg = 1;
    public float stamina = 100;
    public float maxStamina = 100;
    public float staminaReg = 5;

    public int strenght;
    public int dexterity;
    public int vitalität;
    public int ember;
    public int ghostShards;

    public float rollCosts;

    public bool dead;
    public bool blocking;
    public bool invincebile;
    public bool getHitTrigger;

    public bool inCombat = false;
	// Use this for initialization
	void Start () {
        health = maxHealth;
        stamina = maxStamina;
        rollCosts = 20 - (dexterity * 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
        HandleRegeneration();
	}

    private void HandleRegeneration()
    {
        if(health <= 0)
        {
            dead = true;
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
        if (!dead) { 
            if(health < maxHealth && inCombat == false)
            {
                health += healthReg * Time.deltaTime;
            }
            if(stamina < maxStamina)
            {
                stamina += staminaReg * Time.deltaTime;
            }

            health = Mathf.Clamp(health, 0, maxHealth);
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
        }
    }

    public void showUrStats(bool v)
    {
        statBar = v;
    }

    public bool checkStamina(float amountOfStamina)
    {
        if( amountOfStamina < stamina)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }
}
