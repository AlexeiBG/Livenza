#pragma strict

//Simple script to change Cameras, Lights and Environments in realtime. I am not a coder.. so if you want change this code as you wish.. and make it better !

// Skyboxes
var SKY_Day : Material;
var SKY_Evening : Material;
var SKY_Night : Material;
var SKY_LowSun : Material;
var SKY_Cloudy : Material;
var SKY_Stonewall : Material;
var SKY_Sunset : Material;

// Lights
var Light_BedWall_S_Up : GameObject;
var Light_BedWall_S_Down : GameObject;
var Light_BedWall_R_Up : GameObject;
var Light_BedWall_R_Down : GameObject;
var Light_Wall : GameObject;
var Light_TableLamp_L_Spot : GameObject;
var Light_TableLamp_L_Point : GameObject;
var Light_TableLamp_R_Spot : GameObject;
var Light_TableLamp_R_Point : GameObject;
var Light_Leukon_Up : GameObject;
var Light_Leukon_Down : GameObject;
var Light_Ceiling : GameObject;
var Light_Wardrobe_S : GameObject;
var Light_Wardrobe_D : GameObject;
var Light_Mezzanine_UP : GameObject;
var Light_Mezzanine_DOWN : GameObject;
var Windows_Area_Mat : Material;
var Windows_Area1 : Renderer;
var Windows_Area2 : Renderer;
var Windows_Area3 : Renderer;
var Sun1 : GameObject;
var Sun2 : GameObject;
var Sun3 : GameObject;
// Cameras
var CameraMain : Camera;
// Reflection Probe
var Probe_Main : ReflectionProbe;
var Probe_Ceiling : ReflectionProbe;
var Probe_Bed : ReflectionProbe;
var Probe_Mezzanine : ReflectionProbe;
// Colors
var Windows_Area_Mat_Day : Color = Color.white;
// Cameras
var Camera1 : GameObject;
var Camera2 : GameObject;
var Camera3 : GameObject;
var Camera4 : GameObject;
var Camera5 : GameObject;
var Camera6 : GameObject;
var Camera7 : GameObject;
var Camera8 : GameObject;
var Camera9 : GameObject;
var Camera10 : GameObject;
var CameraDetail1 : GameObject;
var CameraDetail2 : GameObject;
var CameraDetail3 : GameObject;
var CameraDetail4 : GameObject;
var CameraDetail5 : GameObject;
var CameraDetail6 : GameObject;
var CameraDetail7 : GameObject;
var CameraDetail8 : GameObject;
var CameraDetail9 : GameObject;
var CameraDetail10 : GameObject;
var CameraDetail11 : GameObject;

function Awake () {
		// Make the game run as fast as possible
		Application.targetFrameRate = 300;
	}

function Start (){
    ProbeUpdate();
    ENV_Day();
}

function Update () {
    if (Input.GetKey ("escape")) {
        Application.Quit();
    }
}

// Hide all cameras in the scene //
function HideCameras (){
var gos : GameObject[];
		gos = GameObject.FindGameObjectsWithTag("MainCamera"); 
		for (var go : GameObject in gos)  { 
			 go.SetActive (false);
			}
}
// Swow the selected Camera  //
function Camera1Switch (){
		HideCameras ();
		Camera1.SetActive (true);
		} 
		
function Camera2Switch (){
		HideCameras ();
		Camera2.SetActive (true);
		}
		
function Camera3Switch (){
		HideCameras (); 
		Camera3.SetActive (true);	
		}
		
function Camera4Switch (){
		HideCameras (); 
		Camera4.SetActive (true);	
		}
		
function Camera5Switch (){
		HideCameras (); 
		Camera5.SetActive (true);
		}
		
function Camera6Switch (){
		HideCameras (); 
		Camera6.SetActive (true);	
		}
		
function Camera7Switch (){
		HideCameras (); 
		Camera7.SetActive (true);	
		}
		
function Camera8Switch (){
		HideCameras (); 
		Camera8.SetActive (true);
		}
		
function Camera9Switch (){
		HideCameras (); 
		Camera9.SetActive (true);	
		}
		
function Camera10Switch (){
		HideCameras (); 
		Camera10.SetActive (true);	
		}

function CameraDetail1Switch (){
		HideCameras (); 
		CameraDetail1.SetActive (true);	
		}

function CameraDetail2Switch (){
		HideCameras (); 
		CameraDetail2.SetActive (true);	
		}
		
function CameraDetail3Switch (){
		HideCameras (); 
		CameraDetail3.SetActive (true);	
		}

function CameraDetail4Switch (){
		HideCameras (); 
		CameraDetail4.SetActive (true);	
		}
		
function CameraDetail5Switch (){
		HideCameras (); 
		CameraDetail5.SetActive (true);	
		}
		
function CameraDetail6Switch (){
		HideCameras (); 
		CameraDetail6.SetActive (true);	
		}
		
function CameraDetail7Switch (){
		HideCameras (); 
		CameraDetail7.SetActive (true);	
		}

function CameraDetail8Switch (){
		HideCameras (); 
		CameraDetail8.SetActive (true);	
		}

function CameraDetail9Switch (){
		HideCameras (); 
		CameraDetail9.SetActive (true);	
		}
		
function CameraDetail10Switch (){
		HideCameras (); 
		CameraDetail10.SetActive (true);	
		}
		
function CameraDetail11Switch (){
		HideCameras (); 
		CameraDetail11.SetActive (true);	
		}
		
function ENV_Day(){
// Turn ON/OFF Lights
DisableAllLight ();
Sun1.SetActive (true);

    CameraMain.clearFlags = CameraClearFlags.Skybox;
    RenderSettings.skybox = SKY_Day;
    RenderSettings.ambientIntensity = 1; 

    ProbeUpdate();
    ENV_Day_Setup();
}

function ENV_Evening(){
// Turn ON/OFF Lights
DisableAllLight ();
Light_TableLamp_L_Spot.SetActive (true);
Light_TableLamp_L_Point.SetActive (true);
Sun2.SetActive (true);

    CameraMain.clearFlags = CameraClearFlags.Skybox;
    RenderSettings.skybox = SKY_Evening;
    RenderSettings.ambientIntensity = 1; 

    ProbeUpdate();
    ENV_Evening_Setup();
}

function ENV_Night(){
// Turn ON/OFF Lights
DisableAllLight ();
Light_BedWall_S_Up.SetActive (true);
Light_BedWall_S_Down.SetActive (true);
Light_BedWall_R_Up.SetActive (true);
Light_BedWall_R_Down.SetActive (true);
Light_Wall.SetActive (true);
INT_Wall_Bed_L.EmissiveON ();
INT_Wall_Bed_R.EmissiveON ();
INT_Wall.EmissiveON ();

    CameraMain.clearFlags = CameraClearFlags.Skybox;
    RenderSettings.skybox = SKY_Night;
    RenderSettings.ambientIntensity = 1; 

    ProbeUpdate();
    ENV_Night_Setup();
}

function ENV_LowSun(){
// Turn ON/OFF Lights

DisableAllLight ();
Light_Leukon_Up.SetActive (true);
Light_Leukon_Down.SetActive (true);
INT_Leukon.EmissiveON ();


    CameraMain.clearFlags = CameraClearFlags.Skybox;
    RenderSettings.skybox = SKY_LowSun;
    RenderSettings.ambientIntensity = 1; 

    ProbeUpdate();
}

function ENV_Cloudy(){
// Turn ON/OFF Lights
DisableAllLight ();
Light_BedWall_R_Up.SetActive (true);
Light_BedWall_R_Down.SetActive (true);

    CameraMain.clearFlags = CameraClearFlags.Skybox;
    RenderSettings.skybox = SKY_Cloudy;
    RenderSettings.ambientIntensity = 1; 

    ProbeUpdate();
}

function ENV_Stonewall(){
// Turn ON/OFF Lights
DisableAllLight ();

    CameraMain.clearFlags = CameraClearFlags.Skybox;
    RenderSettings.skybox = SKY_Stonewall;
    RenderSettings.ambientIntensity = 1; 

    ProbeUpdate();
    ENV_Day_Setup();
}

function ENV_Sunset(){
// Turn ON/OFF Lights
DisableAllLight ();
Sun3.SetActive (true);

    CameraMain.clearFlags = CameraClearFlags.Skybox;
    RenderSettings.skybox = SKY_Sunset;
    RenderSettings.ambientIntensity = 1; 

    ProbeUpdate();
    ENV_Day_Setup();
}

function DisableAllLight (){
Light_BedWall_S_Up.SetActive (false);
Light_BedWall_S_Down.SetActive (false);
Light_BedWall_R_Up.SetActive (false);
Light_BedWall_R_Down.SetActive (false);
Light_Wall.SetActive (false);
Light_TableLamp_L_Spot.SetActive (false);
Light_TableLamp_L_Point.SetActive (false);
Light_TableLamp_R_Spot.SetActive (false);
Light_TableLamp_R_Point.SetActive (false);
Light_Leukon_Up.SetActive (false);
Light_Leukon_Down.SetActive (false);
Light_Ceiling.SetActive (false);
Light_Wardrobe_S.SetActive (false);
Light_Wardrobe_D.SetActive (false);
Light_Mezzanine_UP.SetActive (false);
Light_Mezzanine_DOWN.SetActive (false);
Sun1.SetActive (false);
Sun2.SetActive (false);
Sun3.SetActive (false);

INT_Leukon.EmissiveOFF ();
INT_CeilingLamp.EmissiveOFF ();
INT_Wall.EmissiveOFF ();
INT_Wall_Bed_L.EmissiveOFF ();
INT_Wall_Bed_R.EmissiveOFF ();
INT_TableLight.EmissiveOFF ();
INT_Wardrobe.EmissiveOFF ();
INT_TableLight_R.EmissiveOFF ();
INT_Mezzanine.EmissiveOFF ();
}


function ENV_Day_Setup (){
    Windows_Area_Mat.EnableKeyword ("_EMISSION");
    Windows_Area_Mat.SetColor("_EmissionColor", Windows_Area_Mat_Day*2);
    // Inutile per Invisibili
    DynamicGI.SetEmissive(Windows_Area1,Windows_Area_Mat_Day);


    DynamicGI.UpdateEnvironment ();

}
function ENV_Evening_Setup (){
    Windows_Area_Mat.EnableKeyword ("_EMISSION");
    Windows_Area_Mat.SetColor("_EmissionColor", Windows_Area_Mat_Day*0);
    // Inutile per Invisibili
    DynamicGI.SetEmissive(Windows_Area1,Windows_Area_Mat_Day*0);


}

function ENV_Night_Setup (){
    Windows_Area_Mat.EnableKeyword ("_EMISSION");
    Windows_Area_Mat.SetColor("_EmissionColor", Windows_Area_Mat_Day*0);
    // Inutile per Invisibili
    DynamicGI.SetEmissive(Windows_Area1,Windows_Area_Mat_Day*0);

}

function ProbeUpdate(){
Probe_Main.RenderProbe();
Probe_Ceiling.RenderProbe();
Probe_Bed.RenderProbe();
Probe_Mezzanine.RenderProbe();
}

var INT_Leukon : LightOnOff;
var INT_CeilingLamp : LightOnOff;
var INT_Wall : LightOnOff;
var INT_Wall_Bed_L : LightOnOff;
var INT_Wall_Bed_R : LightOnOff;
var INT_TableLight : LightOnOff;
var INT_Wardrobe : LightOnOff;
var INT_TableLight_R : LightOnOff;
var INT_Mezzanine : LightOnOff;

// CLOSE Startup Info
var CanvasInfo : GameObject;

function CloseCanvasInfo (){
CanvasInfo.GetComponent(Canvas).enabled = false;
}


