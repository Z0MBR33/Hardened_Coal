using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {

    public float Minit;
    public float Hours;
    public bool Am;
    public float Day;
    public float TimeSpeed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        if(Minit < 60)
        {
            Minit = 0;
            Hours = 1;
            if( Hours == 12)
            {
                Am = !Am;
                if(Am == false)
                {

                }
            }
        }
    }
}
