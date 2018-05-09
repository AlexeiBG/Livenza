using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.XR;
using UnityEngine.UI;


public class VRModeController : MonoBehaviour {

#if UNITY_EDITOR

    [MenuItem("Inmersys/VR/Cardboard + Joysticks")]
    private static void AddCardboardController()
    {
        print("add cardboard controller");
        GameObject prefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>("Assets/Prefabs/PlayerControllers_LastVersion.prefab"));
        prefab.name = "PlayerControllers_LastVersion";

        if(Selection.activeTransform != null) {
            prefab.transform.SetParent(Selection.activeTransform, false);
        }
        prefab.transform.localPosition = Vector3.zero;
        prefab.transform.localEulerAngles = Vector3.zero;
        prefab.transform.localScale = Vector3.one;

        Selection.activeGameObject = prefab;

    }

    [MenuItem("Inmersys/VR/Joysticks Only")]
    private static void AddJoysticksOnly(){
        GameObject prefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>("Assets/Prefabs/PlayerControllers_JoystickOnly.prefab"));
        prefab.name = "PlayerControllers_JoystickOnly";

        if (Selection.activeTransform != null)
        {
            prefab.transform.SetParent(Selection.activeTransform, false);
        }
        prefab.transform.localPosition = Vector3.zero;
        prefab.transform.localEulerAngles = Vector3.zero;
        prefab.transform.localScale = Vector3.one;

        Selection.activeGameObject = prefab;
    }

    [MenuItem("Inmersys/VR/Cardboard Only")]
    private static void AddCardboardOnly()
    {
        GameObject prefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>("Assets/Prefabs/PlayerControllers_VR.prefab"));
        prefab.name = "PlayerControllers_VR";

        if (Selection.activeTransform != null)
        {
            prefab.transform.SetParent(Selection.activeTransform, false);
        }
        prefab.transform.localPosition = Vector3.zero;
        prefab.transform.localEulerAngles = Vector3.zero;
        prefab.transform.localScale = Vector3.one;

        Selection.activeGameObject = prefab;
    }


#endif


    public GameObject vrComponents;
	//public GameObject vrPlayer;
	public GameObject joysticksComponents;
	public GameObject CanvasEventSystem;
//	public GvrViewer myGvrViewer;
	public float timeToFill= 2f;
	public Image ImageToFill;
	private bool bGvrEnabled = false;
	private MobileControlRig myMobileControlRig;
	private Joystick[] myJoysticks;
	public Canvas joysticksCanvas;
	public bool reticleOnTarget = false;

	void Awake(){
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}

	void Start ()
	{
		//Screen.orientation = ScreenOrientation.LandscapeLeft;
    	//myCanvas = joysticksComponents.GetComponentInChildren<Canvas> ();

	}





	public void OnPointerEnter(){
		reticleOnTarget = true;
		EmptyImage ();
		if (reticleOnTarget == false) {
			EmptyImage ();
			StopAllCoroutines ();
		} else {
			StartCoroutine (FillImage ());
		}
	}

	public void OnPointerExits(){
		EmptyImage ();
		reticleOnTarget = false;
		StopAllCoroutines ();
		EmptyImage ();
	}

	public void ToogleVR ()
	{
		StartCoroutine(corout_ToogleVR(bGvrEnabled));
		bGvrEnabled = !bGvrEnabled;
	}

	IEnumerator corout_ToogleVR (bool GvrState)
	{
		if (GvrState)
		{
			yield return null;
			StartCoroutine (XROFF ());
		//	myGvrViewer.VRModeEnabled = false;

			yield return null;

			vrComponents.SetActive(!vrComponents.activeSelf);
			//vrPlayer.SetActive(!vrPlayer.activeSelf);

			yield return null;
			CanvasEventSystem.SetActive (true);

			yield return null;

			joysticksComponents.SetActive(!joysticksComponents.activeSelf);
			

			joysticksCanvas.enabled = true;
			yield return null;

		}else{



			//	myGvrViewer.VRModeEnabled = true;
			StartCoroutine (ONXR ());
			yield return null;

			joysticksCanvas.enabled = false;
			joysticksComponents.SetActive(!joysticksComponents.activeSelf);
			CanvasEventSystem.SetActive (false);
			yield return null;

			vrComponents.SetActive(!vrComponents.activeSelf);
			
			//vrPlayer.SetActive(!vrPlayer.activeSelf);

			yield return null;
		}
	}

	public void newToggleVR(){
		if (reticleOnTarget == true) {
			StartCoroutine (FillImage ());
			//StartCoroutine (wait ());

		} else {
			EmptyImage ();
			StopAllCoroutines ();
		}

	}

	IEnumerator ONXR(){
		XRSettings.LoadDeviceByName ("Cardboard");
		yield return null;
		XRSettings.enabled = true;
	}

	IEnumerator XROFF(){
		XRSettings.LoadDeviceByName ("");
		yield return null;
		XRSettings.enabled = false;
	}

	IEnumerator wait(){
		yield return new WaitForSeconds (.5f);
	}

	IEnumerator FillImage(){
		//yield return StartCoroutine (waiting ());
		yield return StartCoroutine (Corout_FillImage ());
	}

    IEnumerator Corout_FillImage(){
		Debug.Log ("filling");
		float fillSpeed = 1 / timeToFill;
		//Debug.Log (fillSpeed);
		while (!Mathf.Approximately(ImageToFill.fillAmount, 1f))
		{
			ImageToFill.fillAmount = Mathf.MoveTowards(ImageToFill.fillAmount, 1, fillSpeed * Time.deltaTime);
			//	Debug.Log (ImageToFill.fillAmount);
			yield return null;
		}
		ToogleVR ();
		yield return null;

	}

	public void EmptyImage(){
		//	Debug.Log ("out of image, unfilling");
			ImageToFill.fillAmount = 0;
	}
	
}
