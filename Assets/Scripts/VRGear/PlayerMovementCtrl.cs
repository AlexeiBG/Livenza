using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementCtrl : MonoBehaviour {

	public Camera cam;
	public float movementSpeed = 8f;
	public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 0.5f), new Keyframe(0.0f, 0.5f), new Keyframe(90.0f, 0.0f));

	private Rigidbody m_RigidBody;
	private CapsuleCollider m_Capsule;
	private Vector3 m_GroundContactNormal = Vector3.up;

	// Use this for initialization
	void Start () {
		m_RigidBody = GetComponent<Rigidbody>();
		m_Capsule = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 input = GetInput();

		if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon))
		{
			// always move along the camera forward as it is the direction that it being aimed at
			Vector3 desiredMove = cam.transform.forward*input.y + cam.transform.right*input.x;
			desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

			desiredMove.x = desiredMove.x*movementSpeed;
			desiredMove.z = desiredMove.z*movementSpeed;
			desiredMove.y = desiredMove.y*movementSpeed;
			if (m_RigidBody.velocity.sqrMagnitude <
				(movementSpeed*movementSpeed))
			{
				m_RigidBody.AddForce(desiredMove*SlopeMultiplier(), ForceMode.Impulse);
			}

		}

		m_RigidBody.drag = 5f;

		if (Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && m_RigidBody.velocity.magnitude < 1f)
		{
			m_RigidBody.Sleep();
		}
	}

	private Vector2 GetInput()
	{

		Vector2 input = new Vector2
			{
				x = CrossPlatformInputManager.GetAxis("Horizontal"),
				y = CrossPlatformInputManager.GetAxis("Vertical")
			};
		return input;
	}

	private float SlopeMultiplier()
	{
		float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
		return SlopeCurveModifier.Evaluate(angle);
	}
}
