using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform lookAt;
    public Transform camTransform;
    public Vector3 startPosition;
    public Quaternion startRotation;

    private Camera cam;

    private float distance = 5.0f;
    private float cameraHeight = 2f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 8.0f;
    private float sensivityY = 2.0f;

    private const float Y_ANGLE_MIN = -10.0f;
    private const float Y_ANGLE_MAX = 50.0f;
    private const float CAM_DISTANCE_MIN = 3f;
    private const float CAM_DISTANCE_MAX = 10f;

    private Vector3 dir;
    private Quaternion rotation;

    // Use this for initialization
    void Start () {
        camTransform = transform;
        startPosition = transform.position;
	}

    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            currentX += Input.GetAxis("Mouse X");
            currentY += Input.GetAxis("Mouse Y");
            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }
        distance -= Input.GetAxis("Mouse ScrollWheel");

        distance = Mathf.Clamp(distance, CAM_DISTANCE_MIN, CAM_DISTANCE_MAX);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        dir = new Vector3(0, cameraHeight, -distance);
        if (Input.GetMouseButton(2))
        {
        rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position + new Vector3(0,cameraHeight,0));
        startRotation = new Quaternion(startRotation.x,rotation.y,0,0);
        }
        else
        {
            
            //camTransform.position = lookAt.position + Quaternion.Euler(0, 0, 0) * dir;
            //camTransform.LookAt(lookAt.position + new Vector3(0, cameraHeight, 0));
        }
	}
}
