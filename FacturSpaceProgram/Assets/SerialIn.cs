using UnityEngine;
using System.Collections;

public class SerialIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUILayout.BeginVertical ();
		GUILayout.TextArea ("SOmething about //n /n /n Mary");
		GUILayout.EndVertical ();

	}
}
