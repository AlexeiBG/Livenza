using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebFormCtrl : MonoBehaviour {

	public InputField inputName, inputLastName, inputEmail, inputPhone, 
					inputComments;

	public string messageSuccess = "";
	public string messageInfoError = "";
	public string messageWebServiceError = "";
	public string messageWWWError = "";

	public PopUpManager myPopUpManager;
	public int popupIndex = 0;

	public Text textMessage;

	bool bError = false;
	bool bErrorWWW = false;

    [Header("URL of form")]
	public string RegisterURL = "http://inmersys.com/develop/ambar/contact.php";

	[System.Serializable]
	public class RegisterResponse {
		public bool status = true;
		public string message;
	}

	public void SubmitInfo ()
	{
		StartCoroutine(algo());
	}


	IEnumerator algo ()
	{
		if (ValidateTextInfo ()){
			Debug.Log("Contenido valido. Corutina iniciada");
			yield return StartCoroutine (corout_WWWForm ());
			myPopUpManager.showPopUp(popupIndex);
			if (!bError && !bErrorWWW)
			{			
				textMessage.text = messageSuccess;
				ClearInputFields ();
			}
			else if (bError)
			{
				textMessage.text = messageWebServiceError;
			}
			else if (bErrorWWW)
			{
				textMessage.text = messageWWWError;
			}
		}
		else {
			// Show some kind of error
			Debug.Log("Contenido invalido");
			myPopUpManager.showPopUp(popupIndex);
			textMessage.text = messageInfoError;
		}



		bError = false;
		bErrorWWW = false;
	}

	IEnumerator corout_WWWForm (){
		WWWForm form = new WWWForm ();

		form.AddField ("name", inputName.text);
		form.AddField ("lastName", inputLastName.text);
		form.AddField ("email", inputEmail.text);
		form.AddField ("phone", inputPhone.text);
		form.AddField ("comments", inputComments.text);

		WWW www = new WWW (RegisterURL, form);

		yield return www;

		if (string.IsNullOrEmpty(www.error)){
			// Request succesful
			RegisterResponse response = JsonUtility.FromJson<RegisterResponse> (www.text);
			bErrorWWW = false;
			if (response.status == true){
				// Register succesful
				Debug.Log("Register succesful");
				bError = false;
			}
			else {
				// Webservice error
				Debug.Log("Webservice error");
				bError = true;
			}
		}
		else {
			// Some error
			Debug.Log(www.error);
			bErrorWWW = true;
		}

		print("Register response: " + www.text);

		// Update ui and stuff


	}

	void ClearInputFields ()
	{
		inputName.text = "";
		inputLastName.text = "";
		inputEmail.text = "";
		inputPhone.text = "";
		inputComments.text = "";
	}

	bool ValidateTextInfo (){
		if (inputName.text == "" || inputLastName.text == "" 
			|| inputEmail.text == "" || inputPhone.text == "" 
			|| inputComments.text == "")
		{
			return false;
		}
		return true;
	}
}
