using UnityEngine;
using System.Collections;

public class ddol : MonoBehaviour {
	bool hyperion_dist_reached = false;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {

		if (!hyperion_dist_reached) {
			if (Application.loadedLevelName.Equals ("HyperionLanding")) {
				if (Vector3.Distance (Camera.main.transform.position, GameObject.Find ("Hyperion_Moon").transform.position) < 4) {
					if (GameObject.Find ("Calibration").GetComponent<GUIText> ().text.Equals ("WAITING FOR USERS")) {
						hyperion_dist_reached = true;

					}
				}
			

			}
		}
	
	}

	void OnGUI(){
		if (Application.loadedLevelName.Equals ("HyperionLanding")) {
			if (GameObject.Find ("Calibration").GetComponent<GUIText> ().text.Equals ("WAITING FOR USERS")) {
				if (hyperion_dist_reached) {
					GUILayout.TextArea ("Calibrate Kinect with standard T Pose");
			
				}
			} else {
				Application.LoadLevel (3);
			}
		}
	}
}
