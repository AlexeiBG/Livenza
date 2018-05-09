using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerMove : MonoBehaviour
{
	
    CharacterController charControl;
    public float walkSpeed;
	public Camera mainC;

    void Awake()
    {
        charControl = GetComponent<CharacterController>();
		mainC = GameObject.FindObjectOfType<Camera> ();
		mainC.ResetAspect ();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
		float horiz = CrossPlatformInputManager.GetAxis ("Horizontal");

		float vert = CrossPlatformInputManager.GetAxis ("Vertical");

        Vector3 moveDirSide = transform.right * horiz * walkSpeed;
        Vector3 moveDirForward = transform.forward * vert * walkSpeed;

        charControl.SimpleMove(moveDirSide);
        charControl.SimpleMove(moveDirForward);

    }
}
