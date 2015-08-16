using UnityEngine;
using PsiEngine.@Input;
using PsiEngine.Interface3D;



public class RoverDirectionController : AnimatedGenericMenuController{
void Awake(){}


void Start(){


 try{
InitilizeOptions();
}
 catch(System.Exception ex)
{
Debug.Log(ex.ToString());}
}
public virtual void Camera(){}
public virtual void Compass(){}

public virtual void LightMeterBase(){}
public virtual void LightMeter(){}
public virtual void Refresh(){

		StartCoroutine_Auto( GameObject.Find ("OpenGimbal").transform.Find ("OgpImageHandler").GetComponent<OgpSftpHandler>().download("imagesmall"));

	}

	public virtual void Rover(){OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();
		ogp.currentBot = ogp.currentBot == 0 ? 1 : 0;}
public virtual void Slider(){}


	public virtual void Up()
	{
		
		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();	
		ogp.gearDirection = OpenGimbal.GearDirection.UP;
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