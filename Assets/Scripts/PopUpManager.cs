using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopUpManager : MonoBehaviour
{
	[HideInInspector]
	public int MainView = 0;
	[HideInInspector]
	public int activeView = 0;
	[HideInInspector]
	public int lastView = 0;

	public GameObject GlobalPopUp;
	public GameObject[] PopUpChilds;

	private static float speed = 2f;
	private bool popIsShown = false;

	private int sWidth;



	//[HideInInspector]
	public bool NotificacionActiva;
	public List<int> NotificacionesPasadas;

	void Awake()
	{
		Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () 
	{
		sWidth = Screen.width;
		NotificacionesPasadas = new List<int>();
		initPopUps ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (NotificacionActiva)
			{
				if (NotificacionesPasadas.Count > 1)
				{
					NotificacionesPasadas.RemoveAt(NotificacionesPasadas.Count - 1);
					showPopUp(NotificacionesPasadas[NotificacionesPasadas.Count - 1]);
				}
				else
				{
					hidePopUp();
				}
			}
		}
	}
		
	public void initPopUps() 
	{
		
			GlobalPopUp.transform.localPosition = new Vector3 (-2560, 0, 0);

	}

	public void showPopUp(int index) 
	{
		if (NotificacionActiva)
			return;

		NotificacionesPasadas.Remove(index);
		NotificacionesPasadas.Add(index);


		NotificacionActiva = true;
		StartCoroutine (showPop (index));
	}

	public void show2PopUps(int index)
	{
		
	}

	public void hidePopUp() 
	{
		if (GlobalPopUp.transform.localPosition.x != 0)
			return;

		NotificacionesPasadas.Clear();
		StartCoroutine (hidePop ());
	}

	public void showPopUpNoAnim(int index){
		for (int i = 0; i < PopUpChilds.Length; i++)
			PopUpChilds[i].SetActive(false);
		PopUpChilds [index].SetActive(true);
		GlobalPopUp.transform.localPosition = new Vector3 (0, 0, 0);

	}

	public void hidePopUpNoAnim(int index){
		GlobalPopUp.transform.localPosition = new Vector3 (sWidth *-2, 0, 0);
	}


	private IEnumerator showPop (int index){
		Debug.Log("Pinche rutina");
		for (int i = 0; i < PopUpChilds.Length; i++)
			PopUpChilds[i].SetActive(false);	
		PopUpChilds[index].SetActive(true);
		float elapsedTime = 0f;
		float alpha = 0f;
		float speed2 = speed / 2;
		Color c = GlobalPopUp.GetComponent<Image> ().color;
		GlobalPopUp.GetComponent<Image> ().color = new Color(c.r, c.g, c.b, 0f);
		while (elapsedTime < speed) {
			if (GlobalPopUp.transform.localPosition.x > -0.1f) {
				GlobalPopUp.transform.localPosition = new Vector3 (0, 0, 0);
				elapsedTime = speed;
			}
			else {
				GlobalPopUp.transform.localPosition = Vector3.Lerp (GlobalPopUp.transform.localPosition, new Vector3 (0, 0, 0), elapsedTime / speed2);
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		while (alpha < 0.7f){
			alpha += Time.deltaTime*2;
			GlobalPopUp.GetComponent<Image> ().color = new Color(c.r, c.g, c.b, alpha);
			yield return null;
		}
		popIsShown = true;



	}

	private IEnumerator hidePop ()
	{
		Debug.Log("Pinche rutina");
		float elapsedTime = 0f;
		float speed2 = speed / 2;
		Color c = GlobalPopUp.GetComponent<Image> ().color;
		GlobalPopUp.GetComponent<Image> ().color = new Color(c.r, c.g, c.b, 0);
		while (elapsedTime < speed) {
			if ( GlobalPopUp.transform.localPosition.x < -600 ) 
			{
				GlobalPopUp.transform.localPosition = new Vector3 (-2560, 0, 0);
				elapsedTime = speed;
			}
			else 
			{
				GlobalPopUp.transform.localPosition = Vector3.Lerp (GlobalPopUp.transform.localPosition, new Vector3 (-2560, 0, 0), elapsedTime / speed2);
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		popIsShown = false;
		NotificacionActiva = false;
	}
}
