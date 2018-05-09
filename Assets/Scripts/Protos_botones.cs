using UnityEngine;
using UnityEngine.UI;
public class Protos_botones : MonoBehaviour {

	public Button botonSelected;

	void Start(){
		botonSelected.GetComponent <Button>();

		botonSelected.Select ();
	}
}
