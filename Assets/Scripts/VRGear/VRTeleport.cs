using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTeleport : MonoBehaviour {

	public PlayerTransform[] teleportGoals;
	public GameObject[] teleportCanvas;
	public GameObject playerVR;
	public string initialRoom = "CocinaComedor";

	public delegate void PlayerTeleportation ();
	public static event PlayerTeleportation OnPlayerTeleport;

	private Dictionary<string, PlayerTransform> dictTeleport;
	private Dictionary<string, GameObject> dictCanvas;
	private string lastKey;

	// Use this for initialization
	void Start () {

		dictTeleport = new Dictionary<string, PlayerTransform> ();
		dictCanvas = new Dictionary<string, GameObject> ();

		foreach (PlayerTransform goal in teleportGoals)
		{
			string newKey = goal.name;
			dictTeleport.Add(newKey, goal);
		}

		foreach (GameObject canvas in teleportCanvas)
		{
			string newKey = canvas.name;
			dictCanvas.Add(newKey, canvas);
		}

		dictCanvas[initialRoom].gameObject.SetActive(false);
		lastKey = initialRoom;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Teleport (string key, bool bCanvasPasillo)
	{
		dictCanvas[lastKey].gameObject.SetActive(true);

		Vector3 newPosition = dictTeleport[key].playerPosition;
		Vector3 newRotation = dictTeleport[key].playerRotation;

		if (!bCanvasPasillo)
		{
			dictCanvas[key].gameObject.SetActive(false);
			lastKey = key;
		}

		playerVR.transform.localPosition = newPosition;
		playerVR.transform.localEulerAngles = newRotation;

		if (OnPlayerTeleport != null)
		{
			Debug.Log("Evento OnPLayerTeleport llamado");
			OnPlayerTeleport();
		}
	}
}
