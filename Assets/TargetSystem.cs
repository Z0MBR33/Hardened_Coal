using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour {
    public PlayerControl plControl;
    private GameObject[] tmpEnemis;
	// Use this for initialization
	void Start () {
        plControl = GetComponentInParent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (!collision.isTrigger)
        {

            if (collision.gameObject.tag == "Enemy")
            {

                if (collision.gameObject.GetComponents<Stats>() != null)
                {

                    if (!plControl.Enemies.Contains(collision.gameObject.GetComponent<Stats>()))
                    {
                        
                        plControl.Enemies.Add(collision.gameObject.GetComponent<Stats>());
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (!collision.isTrigger)
        {
            if (collision.gameObject.tag == "Enemy")
            {

                if (plControl.Enemies.Contains(collision.gameObject.GetComponent<Stats>()))
                {
                    if(plControl.currentTarget >= plControl.Enemies.FindIndex(a => collision.gameObject.GetComponent<Stats>()))
                    {
                        if(plControl.currentTarget != 0)
                        {
                            plControl.currentTarget += -1;
                        }
                        else
                        {
                            plControl.getNearestTarget();
                        }
                        collision.gameObject.GetComponent<Stats>().showUrStats(false);

                    }
                    
                    plControl.Enemies.Remove(collision.gameObject.GetComponent<Stats>());
                    
                }
            }
        }
        
    }
}
