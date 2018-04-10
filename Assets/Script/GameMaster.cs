using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    private bool GamePaused;
    public GameObject[] PauseObject;
    public GameObject spawnSystem;
	// Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void letMePause()
    {
        foreach (GameObject go in PauseObject)
        {
            go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }
        GamePaused = true;
    }

    public void letmeStopPause()
    {
        foreach (GameObject go in PauseObject)
        {
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        }
        GamePaused = false;
    }
}
