using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.VR;

public class VRButtonCtrl : MonoBehaviour {

	public float timeToFill = 1f;
	public GameObject vrPlayer;
	public GameObject canvasPasillo;
	public bool bCanvasPasillo;
	public VRModeController myVRCtrl;

	private VRTeleport myVRTelerporter;
	private Button vrButton;
	[SerializeField]
	private Image imageToFill;
	[SerializeField]
	private bool bReticleOffImage = false;
	private GameObject parentCanvas;

	void OnEnable ()
	{
		VRTeleport.OnPlayerTeleport += LookAtPlayer;
		if (canvasPasillo != null)
		{
			canvasPasillo.SetActive(false);
		}
	}

	void OnDisable ()
	{
		VRTeleport.OnPlayerTeleport -= LookAtPlayer;
		if (canvasPasillo != null)
		{
			canvasPasillo.SetActive(true);
		}
	}

	// Use this for initialization
	void Start () {
		parentCanvas = GetComponentInParent <Canvas> ().gameObject;
		myVRTelerporter = FindObjectOfType<VRTeleport> ();
		vrButton = GetComponentInChildren<Button> ();
		Image[] myImages = GetComponentsInChildren<Image> ();
		foreach (Image myImage in myImages)
		{
			if (myImage.type == Image.Type.Filled)
			{
				imageToFill = myImage;
			}
		}
		LookAtPlayer();
	}

	public void ReticleOnImage (string key)
	{
		bReticleOffImage = false;
		StartCoroutine(FIllImage(key));
	}

	public void ReticleOffImage ()
	{
		bReticleOffImage = true;
	}

	IEnumerator FIllImage (string key)
	{
		yield return StartCoroutine(Corout_FillImage());

		if (!bReticleOffImage) 
		{
			if (key != "GoBack")
			{
				myVRTelerporter.Teleport(key, bCanvasPasillo);
			}
			else
			{
				myVRCtrl.ToogleVR();
			}
		}
		else
		{
			EmptyImage ();
		}

		imageToFill.fillAmount = 0;
	}

	void EmptyImage ()
	{
		imageToFill.fillAmount = 0;
	}

	IEnumerator Corout_FillImage ()
	{
		float fillSpeed = 1 / timeToFill;

		while (!Mathf.Approximately(imageToFill.fillAmount, 1f) && !bReticleOffImage)
		{
			imageToFill.fillAmount = Mathf.MoveTowards(imageToFill.fillAmount, 1, fillSpeed * Time.deltaTime);

			yield return null;
		}
	}

	IEnumerator Corout_ChangeScene ()
	{
		yield return null;

		//VRSettings.LoadDeviceByName ("");

		yield return null;
		/*
		if (!VRSettings.loadedDeviceName.Equals("")) {
			Debug.LogWarning("Waiting extra frame…");
			yield return null;
		}
*/
//		VRSettings.enabled = false;
		/*
		if(VRSettings.enabled) {
			Debug.Log("Waiting an extra frame to enable VRSettings");
			yield return null;
		}
*/
//		Debug.Log ("VR device " + VRSettings.loadedDeviceName + "VR status " + VRSettings.enabled);
	}

	void LookAtPlayer ()
	{
		if (parentCanvas != null && vrPlayer != null)
		{
			parentCanvas.transform.LookAt(vrPlayer.transform, Vector3.up);
		}
	}
}
