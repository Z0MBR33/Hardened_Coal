using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kickAttack : MonoBehaviour {
    private float attackTypeDamage;
    private float currentDamage;
    private Stats plStats;
    private Collider myTrigger;

    // Use this for initialization
    void Start()
    {
        plStats = GetComponentInParent<Stats>();
        myTrigger = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void letMeKick()
    {
            currentDamage = (plStats.dexterity + plStats.strenght)/4;
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
