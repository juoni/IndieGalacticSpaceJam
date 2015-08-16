using UnityEngine;
using System.Collections;

public class LaunchSeq : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= 35.0f) {
			// Break for now.... Replace with load new scene....
			Application.LoadLevel(2);
//			Debug.Break();
//			Application.Quit();
		}
	}
}
