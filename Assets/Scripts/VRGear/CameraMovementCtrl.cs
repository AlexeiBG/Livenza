using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraMovementCtrl : MonoBehaviour {

	public Camera myCamera;
	public float rotateSpeed = 8f;
	public float atenuacion = 0.5f;
	private float pitch, yaw;

	// Use this for initialization
	void Start () {
		pitch = 0;
		yaw = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector2 input = GetInput();

		pitch -= input.y * rotateSpeed * atenuacion;
		yaw += input.x * rotateSpeed;
		//limit so we dont do backflips
		pitch = Mathf.Clamp (pitch, -80, 80);
		//do the rotations of our camera
		myCamera.transform.eulerAngles = (Vector3.up * yaw) + (Vector3.right * pitch);

	}

	private Vector2 GetInput()
	{

		Vector2 input = new Vector2
			{
				x = CrossPlatformInputManager.GetAxis("Pan"),
				y = CrossPlatformInputManager.GetAxis("Tilt")
			};
		return input;
	}
}
