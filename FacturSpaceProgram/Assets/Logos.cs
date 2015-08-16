using UnityEngine;
using System.Collections;

public class Logos : MonoBehaviour {

	public Texture2D[] logos;
	public float fadeTime = .9f;


	public int timesToFade = 0;
	public int timesFaded = 0;

	public Color origcolor;
	void Awake(){
		origcolor = this.GetComponent<GUITexture> ().color;

		for(int i = 0; i < logos.Length; i++)
		{

				timesToFade++;
		}
	}
	// Use this for initialization
	void Start () {

		StartCoroutine_Auto (fade(1,0,2));

	}
	
	// Update is called once per frame
	void Update () {
	

	}

	IEnumerator fade ( float startLevel, float endLevel, float duration ) {
			float speed = 1.0f/duration;  
		Color c = this.GetComponent<GUITexture> ().color;
			for (float t =0.0f; t < 1.0; t += Time.deltaTime*speed) {
				c.a = Mathf.Lerp(startLevel, endLevel, t);
			this.GetComponent<GUITexture>().color = c;
			yield return new WaitForEndOfFrame();
			}
		timesFaded++;

		if (timesFaded < timesToFade) {
			this.GetComponent<GUITexture> ().color = origcolor;
			this.GetComponent<GUITexture> ().texture = logos [timesFaded];
			StartCoroutine_Auto (fade (1, 0, 2));
		} else {
			StopAllCoroutines();
			Application.LoadLevel(1);
		}


		}

	void FixedUpdate(){

	}
}
