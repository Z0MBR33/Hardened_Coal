using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_On_Hit : MonoBehaviour {
    public GameObject obj;
    public GameObject objDestroyed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            return;
        }
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag.Equals("Sword") || true)
        {
            obj.SetActive(false);
            objDestroyed.SetActive(true);
        }
        
    }
}
