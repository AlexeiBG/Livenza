using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScreensManager : MonoBehaviour {

	//public bool flagToShow;
	public int MainScreen = 0;
	public int ActiveScreen;
	public int instruccionesScreenIndex = 0;
	public GameObject[] Screens;

	//[ HideInInspector ]
	public List<int> BackSCreenList;
	[ Range (1,3) ]
	public float speed = 2f;

	public int ScreenWidth;
	public int ScreenHeight;
	private bool camaraChecked = false;

	public bool InTransition;

	private PopUpManager popUpManager;


	public delegate void OnStartTransition (int screenIndex);
    public static event OnStartTransition InicioTransicion;

	public delegate void OnStartBack (int screenIndex);
    public static event OnStartBack InicioRegreso;


	void Awake(){
		Screen.orientation = ScreenOrientation.Portrait;
	}


	public void Start()
	{
		//Screen.orientation = ScreenOrientation.Portrait;
		if (ScreenWidth == 0 || ScreenWidth != Screen.width)
			ScreenWidth = Screen.width * 2;

		if (ScreenHeight == 0 || ScreenHeight != Screen.height)
			ScreenHeight = Screen.height;

		// Disable all screens but the MainScreen
		for ( int i = 0; i < Screens.Length; i++ )
		{
			if (i != MainScreen )
			{
				Screens[i].transform.localPosition = new Vector3 (ScreenWidth, 0, 0);
				Screens[i].SetActive(false);
			}
		}
		// Activate Main Screen
		Screens[MainScreen].SetActive(true);

		// Starts Back Screen List and search for PopUpManager
		BackSCreenList = new List<int>();
		popUpManager = GameObject.FindObjectOfType<PopUpManager>();

		// Sets transition false
		InTransition = false;
		// Active Screen is the same as main screen
		ActiveScreen = MainScreen;
	}

	public void Update()
	{
		// If transition, exit
		if (InTransition)
			return;

		// When Escape key is pressed...
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			// Go Back if PopUpManager is not showing PopUp
			if (popUpManager == null)
			{
				goBack();
			}
			#if UNITY_ANDROID
			else if (ActiveScreen == MainScreen)
			{
				Debug.Log("Cerrar app");
				Application.Quit();
			}
			#endif
			else if (!popUpManager.NotificacionActiva)
			{
				Debug.Log("Se llamo desde !popUpManager.NotificacionActiva");
				goBack();
			}
		}
	}

	/// <summary>
	/// Go to Screen Index
	/// </summary>
	/// <param name="index">Index Screen in Screens Array.</param>
	public void goToIndex( int index )
	{
		// If the anim is still moving screens then exit
		if ( InTransition )
			return;

		// If the screen is in back list then exit
		if ( BackSCreenList.Count > 0 )
		{
			if ( BackSCreenList[BackSCreenList.Count - 1].Equals(index) )
				return;
		}

		// Active indexed screen and start movin it to center
		Screens [index].SetActive (true);
		StartCoroutine ( moveThisCenter (index));

		// Add indexed screen to backlist
		BackSCreenList.Add( ActiveScreen );

		ActiveScreen = index;

		if (InicioTransicion != null)
		{
			InicioTransicion(index);
		}
		else
		{
			Debug.Log ("Sin suscriptores a nuestro evento InicioTrancision.");
		}
		// Send Message transition starts
//		if (index == instruccionesScreenIndex)
//		{
//			if (InicioTransicion != null)
//			{
//				InicioTransicion();
//			}
//			else
//			{
//				Debug.Log ("Sin suscriptores a nuestro evento InicioTrancision.");
//			}
//		}
//		else
//		{
//			if (InicioRegreso != null)
//			{
//				InicioRegreso();
//			}
//			else
//			{
//				Debug.Log ("Sin suscriptores a nuestro evento InicioTrancision.");
//			}
//		}
		//InicioTransicion();

		// Change state InTransition = true
		InTransition = true;

	}

	public void goToIndexLeft( int index )
	{
		// If the anim is still moving screens then exit
		if ( InTransition )
			return;

		// If the screen is in back list then exit
		if ( BackSCreenList.Count > 0 )
		{
			if ( BackSCreenList[BackSCreenList.Count - 1].Equals(index) )
				return;
		}

		// Active indexed screen and start movin it to center
		Screens [index].SetActive (true);
		Screens[index].transform.localPosition = new Vector3 (-ScreenWidth, 0, 0);
		StartCoroutine ( moveThisCenter (index));

		// Add indexed screen to backlist
		BackSCreenList.Add( ActiveScreen );

		ActiveScreen = index;

		// Send Message transition starts

		// Change state InTransition = true
		InTransition = true;

	}

	public void goToIndexUp( int index )
	{
		// If the anim is still moving screens then exit
		if ( InTransition )
			return;

		// If the screen is in back list then exit
		if ( BackSCreenList.Count > 0 )
		{
			if ( BackSCreenList[BackSCreenList.Count - 1].Equals(index) )
				return;
		}

		// Active indexed screen and start movin it to center
		Screens [index].SetActive (true);
		Screens[index].transform.localPosition = new Vector3 (0, -ScreenHeight, 0);
		StartCoroutine ( moveThisCenter (index, 1));

		// Add indexed screen to backlist
		BackSCreenList.Add( ActiveScreen );

		ActiveScreen = index;

		// Send Message transition starts
		//InicioTransicion();

		// Change state InTransition = true
		InTransition = true;

	}

	public void goToIndexDown( int index )
	{
		// If the anim is still moving screens then exit
		if ( InTransition )
			return;

		// If the screen is in back list then exit
		if ( BackSCreenList.Count > 0 )
		{
			if ( BackSCreenList[BackSCreenList.Count - 1].Equals(index) )
				return;
		}

		// Active indexed screen and start movin it to center
		Screens [index].SetActive (true);
		Screens[index].transform.localPosition = new Vector3 (0, ScreenHeight, 0);
		StartCoroutine ( moveThisCenter (index, 1));

		// Add indexed screen to backlist
		BackSCreenList.Add( ActiveScreen );

		ActiveScreen = index;

		// Send Message transition starts
		//InicioTransicion();

		// Change state InTransition = true
		InTransition = true;

	}

	/// <summary>
	/// Go to Index screen without move, just appear
	/// </summary>
	/// <param name="index">Screen Index.</param>
	public void goToIndexNoAnim(int index)
	{
		// If the anim is still moving screens then exit
		if ( InTransition )
			return;

		// If the screen is in back list then exit
		if ( BackSCreenList.Count > 0 )
		{
			if ( BackSCreenList[BackSCreenList.Count - 1].Equals(index) )
				return;
		}

		// Active indexed screen and start movin it to center
		Screens [index].SetActive (true);
		Screens [index].transform.localPosition = Vector3.zero;

		// Add indexed screen to backlist
		BackSCreenList.Add( ActiveScreen );

		ActiveScreen = index;

		// Send Message transition starts
		//InicioTransicion();

	}

	/// <summary>
	/// Moves 2 screens instead of just move new screen to center
	/// </summary>
	/// <param name="index">Index.</param>
	public void goToIndex2Screens(int index)
	{
		// If the anim is still moving screens then exit
		if (InTransition)
			return;

		Screens [index].SetActive (true);
		StartCoroutine (moveThisCenter (index));
		StartCoroutine (moveThisLeft (ActiveScreen));
		BackSCreenList.Add(ActiveScreen);
		ActiveScreen = index;

		InTransition = true;

	}

	/// <summary>
	/// Go Back to last screen
	/// </summary>
	public void goBack()
	{
		// If script is in transion, exit
		if (InTransition)
			return;

		int indice = ActiveScreen;

		// If Back Screen List has at least one element
		if (BackSCreenList.Count >= 1)
		{
			// Activate older screen
			Screens [ BackSCreenList[BackSCreenList.Count - 1]].SetActive (true);
			// Remove from list active sreen and moves
			int Activa = BackSCreenList[BackSCreenList.Count - 1];
			BackSCreenList.RemoveAt(BackSCreenList.Count - 1);
			StartCoroutine (moveThisRight (ActiveScreen));

			// Change Active screen 
			ActiveScreen = Activa;

			// If the screen is not in (0,0,0), moves to center
			if (Screens[ActiveScreen].transform.localPosition.x != 0)
			{
				StartCoroutine (moveThisCenter (ActiveScreen));
			}
		}

		//InicioRegreso();
		//if (ActiveScreen == 1)
		//{
			if (InicioRegreso != null)
			{
				InicioRegreso(ActiveScreen);
			}
			else
			{
				Debug.Log ("Sin suscriptores a nuestro evento InicioRegreso.");
			}
		//}

		// Changes in transition to true
		InTransition = true;
	}

	public void goBackLeft()
	{
		// If script is in transion, exit
		if (InTransition)
			return;

		// If Back Screen List has at least one element
		if (BackSCreenList.Count >= 1)
		{
			// Activate older screen
			Screens [ BackSCreenList[BackSCreenList.Count - 1]].SetActive (true);
			// Remove from list active sreen and moves
			int Activa = BackSCreenList[BackSCreenList.Count - 1];
			BackSCreenList.RemoveAt(BackSCreenList.Count - 1);
			StartCoroutine (moveThisLeft (ActiveScreen));

			// Change Active screen 
			ActiveScreen = Activa;

			// If the screen is not in (0,0,0), moves to center
			if (Screens[ActiveScreen].transform.localPosition.x != 0)
			{
				StartCoroutine (moveThisCenter (ActiveScreen));
			}
		}

		//InicioRegreso();

		// Changes in transition to true
		InTransition = true;
	}

	public void goBackUp()
	{
		// If script is in transion, exit
		if (InTransition)
			return;

		// If Back Screen List has at least one element
		if (BackSCreenList.Count >= 1)
		{
			// Activate older screen
			Screens [ BackSCreenList[BackSCreenList.Count - 1]].SetActive (true);
			// Remove from list active sreen and moves
			int Activa = BackSCreenList[BackSCreenList.Count - 1];
			BackSCreenList.RemoveAt(BackSCreenList.Count - 1);
			StartCoroutine (moveThisUp (ActiveScreen));

			// Change Active screen 
			ActiveScreen = Activa;

			// If the screen is not in (0,0,0), moves to center
			if (Screens[ActiveScreen].transform.localPosition.x != 0)
			{
				StartCoroutine (moveThisCenter (ActiveScreen));
			}
		}

		//InicioRegreso();

		// Changes in transition to true
		InTransition = true;
	}

	public void goBackDown()
	{
		// If script is in transion, exit
		if (InTransition)
			return;

		// If Back Screen List has at least one element
		if (BackSCreenList.Count >= 1)
		{
			// Activate older screen
			Screens [ BackSCreenList[BackSCreenList.Count - 1]].SetActive (true);
			// Remove from list active sreen and moves
			int Activa = BackSCreenList[BackSCreenList.Count - 1];
			BackSCreenList.RemoveAt(BackSCreenList.Count - 1);
			StartCoroutine (moveThisDown (ActiveScreen));

			// Change Active screen 
			ActiveScreen = Activa;

			// If the screen is not in (0,0,0), moves to center
			if (Screens[ActiveScreen].transform.localPosition.x != 0)
			{
				StartCoroutine (moveThisCenter (ActiveScreen));
			}
		}

		//InicioRegreso();

		// Changes in transition to true
		InTransition = true;
	}

	/// <summary>
	/// Go Back to indexed screen
	/// </summary>
	/// <param name="index">Screen index.</param>
	public void goBack(int index)
	{
		// If script is in transion, exit
		if (InTransition)
			return;

		int indice = ActiveScreen;

		// If there's no screen in back screen list, exit
		if (BackSCreenList.Count == 0)
			return;

		// Get index of screen in back list 
		int Indice = BackSCreenList.IndexOf(index);

		// Activate Screen and hide active screen
		Screens [ Indice ].SetActive(true);
		int Activa = BackSCreenList[ Indice ];
		BackSCreenList.RemoveRange(Indice, BackSCreenList.Count - Indice);
		StartCoroutine (moveThisRight (ActiveScreen));
		ActiveScreen = Activa;

		//if (indice == 1)
		{
			if (InicioRegreso != null)
			{
				InicioRegreso(ActiveScreen);
			}
			else
			{
				Debug.Log ("Sin suscriptores a nuestro evento InicioRegreso.");
			}
		}

		// Changes in transition to true
		InTransition = true;
	}

	/// <summary>
	/// Deactivate other screens but the Active
	/// </summary>
	public void deactivateOthers()
	{
		for (int i=0; i<Screens.Length; i++)
		{
			if (i != ActiveScreen)
			{
				Screens [i].SetActive (false);
			} 
		}
	}
		
	private IEnumerator moveThisCenter (int index)
	{
		if ( Screens [index].transform.localPosition.magnitude == 0)
			Screens [index].transform.localPosition = new Vector3(ScreenWidth,0,0);

		float elapsedTime = 0f;
		while (elapsedTime < speed) 
		{
			//			print (Screens [index].transform.localPosition.x);
			if (Mathf.Abs( Screens [index].transform.localPosition.x) < 0.1f) 
			{
				Screens [index].transform.localPosition = new Vector3 (0, 0, 0);
				elapsedTime = speed;
			}
			else 
			{
				Screens [index].transform.localPosition = Vector3.Lerp (Screens [index].transform.localPosition, new Vector3 (0, 0, 0), elapsedTime/speed);
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		deactivateOthers ();
		InTransition = false;
	}

	private IEnumerator moveThisCenter (int index, int direction)
	{
		Vector3 InitialPos;
		float CurrentAxisPos;
		switch(direction)
		{
			case 0:
				InitialPos = new Vector3 (ScreenHeight, 0, 0);
				CurrentAxisPos = Screens [index].transform.localPosition.x;
				break;
			case 1:
				InitialPos = new Vector3 (0, ScreenHeight, 0);
				CurrentAxisPos = Screens [index].transform.localPosition.y;
				break;
			default:
				InitialPos = new Vector3 (0, ScreenHeight, 0);
				CurrentAxisPos = Screens [index].transform.localPosition.x;
				break;
		}

		if ( Screens [index].transform.localPosition.magnitude == 0)
			Screens [index].transform.localPosition = InitialPos;

		float elapsedTime = 0f;
		while (elapsedTime < speed) 
		{
			//			print (Screens [index].transform.localPosition.x);
			if (Mathf.Abs(CurrentAxisPos) < 0.1f) 
			{
				Screens [index].transform.localPosition = new Vector3 (0, 0, 0);
				elapsedTime = speed;
			}
			else 
			{
				Screens [index].transform.localPosition = Vector3.Lerp (Screens [index].transform.localPosition, new Vector3 (0, 0, 0), elapsedTime/speed);
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		deactivateOthers ();
		InTransition = false;
	}

	private IEnumerator moveThisRight (int index)
	{
		float elapsedTime = 0f;
		while (elapsedTime < speed)
		{
			if (Screens [index].transform.localPosition.x > ScreenWidth)
			{
				Screens [index].transform.localPosition = new Vector3 (ScreenWidth, 0, 0);
				elapsedTime = speed;
			}
			else
			{
				Screens [index].transform.localPosition = Vector3.Lerp (Screens [index].transform.localPosition, new Vector3 (ScreenWidth, 0, 0), elapsedTime/speed);
			}
			elapsedTime += Time.deltaTime;
			//			print (Screens [index].transform.localPosition.x);
			yield return null;
		}
		deactivateOthers (  );
		InTransition = false;
	}

	private IEnumerator moveThisLeft (int index)
	{
		float elapsedTime = 0f;
		while (elapsedTime < speed)
		{
			if (Screens [index].transform.localPosition.x < -ScreenWidth)
			{
				Screens [index].transform.localPosition = new Vector3 (-ScreenWidth, 0, 0);
				elapsedTime = speed;
			}
			else
			{
				Screens [index].transform.localPosition = Vector3.Lerp (Screens [index].transform.localPosition, new Vector3 (-ScreenWidth, 0, 0), elapsedTime/speed);
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		InTransition = false;
	}

	private IEnumerator moveThisUp (int index)
	{
		float elapsedTime = 0f;
		while (elapsedTime < speed)
		{
			if (Screens [index].transform.localPosition.y > ScreenHeight)
			{
				Screens [index].transform.localPosition = new Vector3 (0, ScreenHeight, 0);
				elapsedTime = speed;
			}
			else
			{
				Screens [index].transform.localPosition = Vector3.Lerp (Screens [index].transform.localPosition, new Vector3 (0, ScreenHeight, 0), elapsedTime/speed);
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		InTransition = false;
	}
	
	private IEnumerator moveThisDown (int index)
	{
		float elapsedTime = 0f;
		while (elapsedTime < speed)
		{
			if (Screens [index].transform.localPosition.y < -ScreenHeight)
			{
				Screens [index].transform.localPosition = new Vector3 (0, -ScreenHeight, 0);
				elapsedTime = speed;
			}
			else
			{
				Screens [index].transform.localPosition = Vector3.Lerp (Screens [index].transform.localPosition, new Vector3 (0, -ScreenHeight, 0), elapsedTime/speed);
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		InTransition = false;
	}

}
