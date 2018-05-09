#pragma strict
var Light1 : GameObject;
var Light2 : GameObject;

var Emissive = true;
var Emissive_Mat : Material;
var Emissive_Obj : Renderer;
var Emissive_Color : Color;
var Emissive_Int : float;

var Emissive2 = true;
var Emissive2_Mat : Material;
var Emissive2_Obj : Renderer;
var Emissive2_Color : Color;
var Emissive2_Int : float;

var On : boolean;


function OnMouseDown () {
if ( Light1.activeInHierarchy == false ){
        //if ( On == true ){
		Light1.SetActive (true);
		Light2.SetActive (true);
		EmissiveON ();
		On = !On;
		} else {
		Light1.SetActive (false);
		Light2.SetActive (false);
		EmissiveOFF ();
		On = !On;
		}
	}


function EmissiveON (){
if (Emissive){
Emissive_Mat.EnableKeyword ("_EMISSION");
Emissive_Mat.SetColor("_EmissionColor", Emissive_Color*Emissive_Int);
DynamicGI.SetEmissive(Emissive_Obj,Emissive_Color*Emissive_Int);
}
if(Emissive2){
Emissive2_Mat.EnableKeyword ("_EMISSION");
Emissive2_Mat.SetColor("_EmissionColor", Emissive2_Color*Emissive2_Int);
DynamicGI.SetEmissive(Emissive_Obj,Emissive2_Color*Emissive2_Int);
}
}

function EmissiveOFF (){
if (Emissive){
Emissive_Mat.EnableKeyword ("_EMISSION");
Emissive_Mat.SetColor("_EmissionColor", Emissive_Color*0);
DynamicGI.SetEmissive(Emissive_Obj,Emissive_Color*0);
}
if(Emissive2){
Emissive2_Mat.EnableKeyword ("_EMISSION");
Emissive2_Mat.SetColor("_EmissionColor", Emissive2_Color*0);
DynamicGI.SetEmissive(Emissive_Obj,Emissive2_Color*0);
}
}