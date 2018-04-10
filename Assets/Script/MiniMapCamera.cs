using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour {
    public Transform Player;
    private float CameraHeight = 50;
    private const float MIN_CAMERA_HEIGHT = 30;
    private const float MAX_CAMERA_HEIGHT = 60;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Player.position.x, CameraHeight, Player.position.z);
	}

    public void miniMapZoom(float value)
    {
        CameraHeight = value;
        Mathf.Clamp(CameraHeight, MIN_CAMERA_HEIGHT, MAX_CAMERA_HEIGHT);
    }
}
