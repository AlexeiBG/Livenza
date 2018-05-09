using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour {

	public Player player;
	private Collider playerCol;
	public Vector3 PlayerPosition;
	public bool reticleOnImage = false;
	public bool waitingComplete = false;

	//image to fill
	private Image ImageToFill;
	public float timeToFill=2f;


	// Use this for initialization
	void Start () {
		ImageToFill = GetComponent <Image> ();
		player = GameObject.FindObjectOfType <Player>();	
		playerCol = GetComponent <Collider> ();

	}
	void Update(){
		transform.LookAt (player.transform);
	}

	// makes this circle dissapear
	void OnTriggerEnter(Collider col){

		if (col.GetComponent <Player> ()) {
			Debug.Log ("player collides");
			this.GetComponent <Image>().enabled=false;
			this.GetComponent <Teleport>().enabled=false;
			this.transform.GetChild (0).GetComponentInChildren <Image> ().enabled = false;
			//this.GetComponent <Collider>().enabled=false;
		}
	}

	void OnTriggerExit(Collider col){
		Debug.Log (col.name);

		if (col.GetComponent <Player> ()) {
			Debug.Log ("player leaves");
			this.GetComponent <Image>().enabled=true;
			this.GetComponent <Teleport>().enabled=true;
			this.transform.GetChild (0).GetComponentInChildren <Image> ().enabled = true;
			//this.GetComponent <Collider>().enabled=true;

		}
	}

	public void OnPointerEnters(){
		reticleOnImage = true;
		if (reticleOnImage == true) {	
			StartCoroutine (FillImage ());
		} else {
			EmptyImage ();
		}
	}

	//reticle exit the cube, stop all coroutins to avoid accidental teleport
	public void OnPointerExits(){
		reticleOnImage = false;
		StopAllCoroutines ();
		EmptyImage ();
	}

	IEnumerator FillImage(){
		//yield return StartCoroutine (waiting ());
		yield return StartCoroutine (Corout_FillImage ());
	}

	IEnumerator Corout_FillImage(){
		Debug.Log ("filling");
	    float fillSpeed = 1 / timeToFill;
		Debug.Log (fillSpeed);
		while (!Mathf.Approximately(ImageToFill.fillAmount, 1f))
		{
			ImageToFill.fillAmount = Mathf.MoveTowards(ImageToFill.fillAmount, 1, fillSpeed * Time.deltaTime);
		//	Debug.Log (ImageToFill.fillAmount);
			yield return null;
		}
		yield return null;
		StartCoroutine (teleportNextTarget ());
	}


	IEnumerator waiting(){
		Debug.Log ("waiting...");
		yield return new WaitForSeconds (2f);
		if (reticleOnImage == true) {
			
		} else {
			reticleOnImage = false;
		}
	}

	public void TargetAquired(){
		StartCoroutine ( teleportNextTarget () );
	}

	private IEnumerator teleportNextTarget(){
	    Debug.Log ("teleporting");
	 //   yield return new WaitForSeconds (2f);
		Vector3 newPos = this.transform.position;
	//  Debug.Log ("moving to pos:" + newPos);
		player.transform.localPosition = newPos + PlayerPosition;
		waitingComplete = false;
		yield return null;
	}
		
	public void EmptyImage(){
		Debug.Log ("out of image, unfill");
		ImageToFill.fillAmount = 0;
	}
}
