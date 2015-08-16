using UnityEngine;
using PsiEngine.@Input;
using PsiEngine.Interface3D;



public class RoverDirectionController : AnimatedGenericMenuController{

	public GameObject[] rover;
void Awake(){}


void Start(){


 try{
InitilizeOptions();
}
 catch(System.Exception ex)
{
Debug.Log(ex.ToString());}

//		if (GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ().currentBot == 0) {
//			foreach(GameObject go in rover){
//				go.SetActive(false);
//			}
//		}
}

	void Update(){
	
	}
public virtual void Camera(){}
public virtual void Compass(){}

public virtual void LightMeterBase(){}
	public virtual void LightMeter(){Light ();}
public virtual void Refresh(){

		StartCoroutine_Auto( GameObject.Find ("OpenGimbal").transform.Find ("OgpImageHandler").GetComponent<OgpSftpHandler>().download("imagesmall"));

	}

	public virtual void Rover(){OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();
		Debug.Log (ogp.currentBot + "Current bot");
		//ogp.currentBot = ogp.currentBot == 0 ? 1 : 0;
		Debug.Log (ogp.currentBot + "Current bot");
		Refresh ();}
public virtual void Slider()
	{

}

	public virtual void Light(){
		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();
		ogp._robots [ogp.currentBot].BroadcastMessage ("light");
	
	}
	//up
	//down
	//left
	//right

	//for
	//back


	//hright
	//hleft

	public virtual void Up()
	{
		
		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();	
		ogp.gearDirection = OpenGimbal.GearDirection.UP;
		Debug.Log (ogp.currentBot + "Current bot");
		ogp._robots[ogp.currentBot].BroadcastMessage("y");
		ogp._robots[ogp.currentBot].BroadcastMessage("c3");
		Refresh ();
	}
	public virtual void Down()
	{
		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();	
		ogp.gearDirection = OpenGimbal.GearDirection.DOWN;
		ogp._robots[ogp.currentBot].BroadcastMessage("g");
		ogp._robots[ogp.currentBot].BroadcastMessage("c3");
		Refresh ();
	
	}
	public virtual void Left(){
		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();	
		ogp.gearDirection = OpenGimbal.GearDirection.LEFT;
		ogp._robots[ogp.currentBot].BroadcastMessage("h");
		ogp._robots[ogp.currentBot].BroadcastMessage("c3");
		Refresh ();
	}
	public virtual void Right(){
		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();	
		ogp.gearDirection = OpenGimbal.GearDirection.RIGHT;
		ogp._robots[ogp.currentBot].BroadcastMessage("j");
		ogp._robots[ogp.currentBot].BroadcastMessage("c3");
		Refresh ();
	}
}