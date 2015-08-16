using UnityEngine;
using System.Collections;

public class SerialIn : MonoBehaviour {

	public string serial_in;
	public string serial_out;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){

		GUI.Label (new Rect (Screen.width - 110, 10, 150, 150),"Serial In");
		GUI.TextArea (new Rect (Screen.width - 150,Screen.height * .1f,120,Screen.height * .75f), serial_in);

		GUI.Label    (new Rect (Screen.width - 110,Screen.height - 45, 100, 50), "Serial Out");
		GUI.TextArea (new Rect (Screen.width - 150,Screen.height * .9f,120,Screen.height * .20f), serial_out);
	}

	public void AddMessage( string msg){
		serial_in += System.Environment.NewLine + msg;
	}

	public void AddSerialOut(string msg){
		serial_out += System.Environment.NewLine + msg;
	}
}
