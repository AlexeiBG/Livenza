using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

	public Image splashImage;
	public string loadLevel;

	IEnumerator Start()
	{
		splashImage.canvasRenderer.SetAlpha(0.0f);

		FadeIn();
		yield return new WaitForSeconds(2.0f);

		FadeOut();

		yield return new WaitForSeconds(0.7f);
		SceneManager.LoadSceneAsync(loadLevel);


	}

	void FadeIn ()
	{
		splashImage.CrossFadeAlpha(1.0f, 1.0f, false);
	}

	void FadeOut ()
	{
		splashImage.CrossFadeAlpha(0.0f, 0.8f, false);
	}
}
