using UnityEngine;
using PsiEngine.@Input;
using PsiEngine.Interface3D;



public class GearDirectionController : AnimatedGenericMenuController{

	public GUITexture background = new GUITexture();
	public GUIText    label = null;
void Awake(){
		label = this.gameObject.transform.FindChild ("Label").GetComponent<GUIText>();
	}


void Start(){


 try{
InitilizeOptions();
}
 catch(System.Exception ex)
{
Debug.Log(ex.ToString());}
}
public virtual void Up()
{
		
		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();	
		ogp.gearDirection = OpenGimbal.GearDirection.UP;
		label.text = ogp.gearDirection.ToString ();
}
public virtual void Down()
{
		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();	
		ogp.gearDirection = OpenGimbal.GearDirection.DOWN;
		label.text = ogp.gearDirection.ToString ();
}
public virtual void Left(){
		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();	
		ogp.gearDirection = OpenGimbal.GearDirection.LEFT;
		label.text = ogp.gearDirection.ToString ();
	}
public virtual void Right(){
		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();	
		ogp.gearDirection = OpenGimbal.GearDirection.RIGHT;
		label.text = ogp.gearDirection.ToString ();
	}
public virtual void Disengage(){
		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();	
		switch (ogp.gearDirection) {
		case OpenGimbal.GearDirection.DOWN:
		case OpenGimbal.GearDirection.UP:
			ogp._robots[0].BroadcastMessage ("8");
			break;

		case OpenGimbal.GearDirection.LEFT:
		case OpenGimbal.GearDirection.RIGHT:
			ogp._robots[0].BroadcastMessage ("3");
			break;
		}
		ogp.gearDirection = OpenGimbal.GearDirection.NONE;
		label.text = ogp.gearDirection.ToString ();
	
	
	}
}