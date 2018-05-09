using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour {

	public string vrSceneName = "";
	public float fadeDuration = 1f;
	public CanvasGroup faderCanvasGroup;
	public CanvasGroup splashScreen;
	//public CanvasGroup americaLink;
	public Image imageToRotate;
	public Text textCargando;

	private RectTransform rectTransImage;




	// Use this for initialization
	void Start () {
		
		if (imageToRotate != null)
		{
			rectTransImage = imageToRotate.GetComponent<RectTransform> ();
		}
		InitialFade ();
	}

	public void ChangeScene (string sceneName)
	{
		StartCoroutine (FadeAndChangeScene(sceneName));
	}

	public void InitialFade ()
	{
		Debug.Log("Initial Fade llamado");
		StartCoroutine(corout_InitialFade());
	}

	private IEnumerator FadeAndChangeScene (string sceneName)
	{
		yield return Fade (1f, faderCanvasGroup);
//
//		if (sceneName == vrSceneName)
//		{
//			yield return RotateImage (-90f);
//		}
//		else
//		{
//			yield return RotateImage (90f);
//		}
		Screen.orientation = (sceneName == vrSceneName) ? ScreenOrientation.LandscapeLeft : ScreenOrientation.Portrait;
		if (textCargando != null)
		{
			textCargando.gameObject.SetActive(true);
		}
		yield return SceneManager.LoadSceneAsync(sceneName);

	}

	private IEnumerator corout_InitialFade()
	{
		yield return new WaitForSeconds (1f);

		//yield return Fade (0f, americaLink);



		yield return new WaitForSeconds (1f);
		yield return Fade (0f, splashScreen);

	}

	private IEnumerator Fade (float finalAlpha, CanvasGroup canvasGroup)
	{
		canvasGroup.blocksRaycasts = true;

		float fadeSpeed = Mathf.Abs(canvasGroup.alpha - finalAlpha) / fadeDuration;

		while (!Mathf.Approximately(canvasGroup.alpha, finalAlpha))
		{
			canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);

			yield return null;
		}

		canvasGroup.blocksRaycasts = false;
	}

	private IEnumerator RotateImage (float finalRotation)
	{
		float elapsedTime = 0f;
		float initAngle = rectTransImage.eulerAngles.z;

		while (elapsedTime <= fadeDuration){

			float t = elapsedTime / fadeDuration;
			float newAngle = Mathf.LerpAngle (initAngle, finalRotation, t);
			rectTransImage.eulerAngles = new Vector3 (0f, 0f, newAngle);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		rectTransImage.eulerAngles = new Vector3 (0f, 0f, finalRotation);
	}
}
