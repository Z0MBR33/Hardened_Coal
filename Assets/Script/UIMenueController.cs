using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenueController : MonoBehaviour {
    private GameObject GameMaster;
    public GameObject MenueCanvas;
    public GameObject Menue;

	// Use this for initialization
	void Start () {
        GameMaster = this.gameObject;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Tab))
        {
            Cursor.visible = !Cursor.visible;
            //MenueCanvas.SetActive(true);
            Menue.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
            Menue.SetActive(!Menue.activeSelf);
            //MenueCanvas.SetActive(false);
            //Menue.SetActive(true);
        }

        else{ 
            if(true)
            {
                Time.timeScale = 1.0f;
                //MenueCanvas.SetActive(false);
                
            }
        }
	}
}
