using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ArchitectureHelperTools : MonoBehaviour {

	public static Dictionary<Renderer, Shader> selection;

	[System.Serializable]
	public class RendererPlusShader {
		public string rend;
		public string shader;

		public RendererPlusShader (string rend, string shader){
			this.rend = rend;
			this.shader = shader;
		}
	}

	[System.Serializable]
	public class RendererPlusShaderArray {
		public List<RendererPlusShader> array;

		public bool rendererExists (string rendName){
			if (array == null || array.Count == 0){
				return false;
			}
			foreach (RendererPlusShader rs in array){
				if (rs.rend == rendName){
					return true;
				}
			}

			return false;
		}
	}

	public static string jsonPath = Application.persistentDataPath + "/ArchitectureWireframed.json";
	public static RendererPlusShaderArray wireframedArray;

	public static void getCurrentWireframed (){
		if (!File.Exists (jsonPath)){
			File.Create (jsonPath);
			updateWireframed ();
		}
		wireframedArray = new RendererPlusShaderArray ();
		wireframedArray.array = new List<RendererPlusShader> ();
		wireframedArray = JsonUtility.FromJson<RendererPlusShaderArray> (File.ReadAllText (jsonPath));
	}

	public static void clearCurrentWireframed (){
		wireframedArray.array.Clear ();
		selection.Clear ();
		File.WriteAllText (jsonPath, "");
	}

	public static void updateWireframed (){
		getCurrentWireframed ();
		foreach (KeyValuePair<Renderer, Shader> w in selection){
			if (wireframedArray != null){
				if (!wireframedArray.rendererExists (w.Key.name)){
					wireframedArray.array.Add (new RendererPlusShader (w.Key.name, w.Value.name));
				}
			}
			else {
				wireframedArray = new RendererPlusShaderArray ();
				wireframedArray.array = new List<RendererPlusShader> ();
				wireframedArray.array.Add (new RendererPlusShader (w.Key.name, w.Value.name));
			}
		}

		string newJson = JsonUtility.ToJson (wireframedArray, true);
		File.WriteAllText (jsonPath, newJson);
	}

	[MenuItem ("Inmersys/Architecture/Wireframe in Selection")]
	public static void RenderWireframe (){
		selection = new Dictionary<Renderer, Shader> ();
		foreach (GameObject selectedGO in Selection.gameObjects){
			Renderer rend = selectedGO.GetComponent<Renderer> ();
			if (rend != null){
				selection.Add (rend, rend.sharedMaterial.shader);
				rend.sharedMaterial.shader = Shader.Find ("Inmersys/Wireframe");
			}
		}

		updateWireframed ();
	}

	[MenuItem ("Inmersys/Architecture/Restore materials")]
	public static void RestoreMaterials (){
//		foreach (KeyValuePair<Renderer, Shader> sel in selection){
//			sel.Key.sharedMaterial.shader = sel.Value;
//		}
		getCurrentWireframed ();
		foreach (RendererPlusShader rs in wireframedArray.array){
			
			GameObject find = GameObject.Find (rs.rend);
			if (find != null){
				find.GetComponent<Renderer> ().sharedMaterial.shader = Shader.Find (rs.shader);	
			}
		}
		clearCurrentWireframed ();
	}

	[MenuItem ("Inmersys/Architecture/Floor selected _END")]
	public static void FloorSelected (){

		foreach (Transform t in Selection.transforms){
			RaycastHit rayhit;
			if (Physics.Raycast(t.position, Vector3.down, out rayhit)){
				Vector3 offset = Vector3.zero;
				MeshRenderer renderer = t.GetComponentInChildren<MeshRenderer>();
				if (renderer != null){
					if (renderer.bounds.center.y - renderer.bounds.extents.y < t.position.y){
						offset = new Vector3(0, renderer.bounds.extents.y, 0);
					}
				}
				t.position = rayhit.point + offset;
			}
		}
	}

}
