using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    public float MIN_WaterTranslation;
    public float MAX_WaterTranslation;
    public float MIN_WaterHeight;
    public float MAX_WaterHeight;
    float current_Y;
    public float Speed;

    private bool currentLerp;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (currentLerp)
        {
            if(transform.position.y < current_Y)
            {
                transform.position += new Vector3(0, Random.Range(MIN_WaterTranslation, MAX_WaterTranslation), 0);
                transform.position = new Vector3(transform.position.z, Mathf.Clamp(transform.position.y, MIN_WaterHeight, current_Y), transform.position.z);
            }
            else if(transform.position.y > current_Y)
            {
                transform.position -= new Vector3(0, Random.Range(MIN_WaterTranslation, MAX_WaterTranslation), 0);
                transform.position = new Vector3(transform.position.z, Mathf.Clamp(transform.position.y, current_Y, MAX_WaterHeight), transform.position.z);
            }
            else
            {
                currentLerp = false;
            }
            
        }
        else
        {
            currentLerp = getRandomHeight();
        }
    }
    private bool getRandomHeight()
    {
        current_Y = Random.Range(MIN_WaterHeight, MAX_WaterHeight);
        return true;
    }
}
