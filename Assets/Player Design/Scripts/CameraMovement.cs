using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	private const float Y_ANGLE_MIN = -100.0f;
	private const float Y_ANGLE_MAX = 70.0f;
	
	public Transform lookAt;
	public Transform camTransform;

	public Transform turret;
	
	public float camHeight = 0.5f;
	private float distance = 4.0f;
	public float distanceZoom = 4.0f;

	private float currentX = 0.0f;
	public float currentY = 0.0f;
	private float sensitiveX = 4.0f;
//	private float sensitiveY = 1.0f;

	private void Start()
	{
		camTransform = transform;
	}
	
	private void Update() 
	{
		distance -= Input.GetAxis("Mouse ScrollWheel");
		currentX += Input.GetAxis("Mouse X") * sensitiveX;
		currentY -= Input.GetAxis("Mouse Y");
		currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
	}
	
	private void LateUpdate()
	{
		Vector3 dir = new Vector3(0, camHeight, -distance);
		Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
		turret.rotation = Quaternion.Euler(Mathf.Clamp(currentY, -10, 25), currentX, 0) * Quaternion.Euler(0, 180, 0);
		//print(transform.eulerAngles.y);
		camTransform.position = lookAt.position + rotation * dir;
		camTransform.LookAt(lookAt.position);
	}
	
}