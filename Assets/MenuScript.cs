using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {
    public GameObject Stats, Skills, Compendium, Map;
    public GameObject currentFolder;
    public GameObject Inventory;
	// Use this for initialization
	void Start () {
        Cursor.visible = true;
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.time != 0.0f)
        {
            Time.timeScale = 0.0f;
        }

        // Folder Controll with R1 and L1
        //if (Input.GetButton(""))
        //{

        //}
        
    }

    //Change Folder of Menue
    public void changeCurrentFolder(int folderName)
    {
        switch (folderName)
        {
            case 0:
                currentFolder.SetActive(false);
                currentFolder = Stats;
                break;
            case 1:
                currentFolder.SetActive(false);
                currentFolder = Skills;
                break;
            case 2:
                currentFolder.SetActive(false);
                currentFolder = Compendium;
                break;
            case 3:
                currentFolder.SetActive(false);
                currentFolder = Map;
                break;
            default:
                break;
        }

        currentFolder.SetActive(true);

    }

    public void closeMenue()
    {
        Cursor.visible = false;
        Inventory.SetActive(false);
        Time.timeScale = 1.0f;
    }

    //private void changeFolderWithController(bool left)
    //{
    //    if (left)
    //    {
    //        changeCurrentFolder(currentFolder - 1);
    //    }
    //    else
    //    {
    //        changeCurrentFolder(currentFolder - 1);
    //    }
    //}
}
