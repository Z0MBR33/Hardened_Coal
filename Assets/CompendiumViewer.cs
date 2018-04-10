using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompendiumViewer : MonoBehaviour {
    public GameObject currentSelected;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        currentSelected.transform.Rotate(new Vector3(0, 0.25f, 0));
    }

    public void rotate(int rotate)
    {
        //currentSelected.transform.Rotate(new Vector3(0,rotate,0));
    }
}
