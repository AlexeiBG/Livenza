using System.Collections;
using UnityEngine;

public class WebBrowser : MonoBehaviour {

	public void OpenBrowser(string URL)
	{
		Debug.Log("Web Browser:" + URL);

		Application.OpenURL(URL);
	}

		public void Phonecall (string URL)
	{
		#if UNITY_IPHONE || UNITY_ANDROID
		Application.OpenURL(URL);
		#endif
	}
}
