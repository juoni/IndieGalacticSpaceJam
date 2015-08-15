using UnityEngine;
using PsiEngine.@Input;
using PsiEngine.Interface3D;



public class DirectionalGearController : AnimatedGenericMenuController{





void Awake()
{


}


void Start()
{


 try{
InitilizeOptions();
}
 catch(System.Exception ex)
{
Debug.Log(ex.ToString());}
}


public virtual void n(){

		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();
		switch (ogp.gearDirection) {
		case OpenGimbal.GearDirection.UP:
			ogp._robots[ogp.currentBot].BroadcastMessage("y");
			break;

		case OpenGimbal.GearDirection.DOWN:
			ogp._robots[ogp.currentBot].BroadcastMessage("g");
			break;
		
		case OpenGimbal.GearDirection.LEFT:
			ogp._robots[ogp.currentBot].BroadcastMessage("h");
			break;

		case OpenGimbal.GearDirection.RIGHT:
			ogp._robots[ogp.currentBot].BroadcastMessage("j");
			break;
		

		}
	}
public virtual void m(){
		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();
		switch (ogp.gearDirection) {
		case OpenGimbal.GearDirection.UP:
			ogp._robots[ogp.currentBot].BroadcastMessage("w");
			break;
			
		case OpenGimbal.GearDirection.DOWN:
			ogp._robots[ogp.currentBot].BroadcastMessage("z");
			break;
			
		case OpenGimbal.GearDirection.LEFT:
			ogp._robots[ogp.currentBot].BroadcastMessage("a");
			break;
			
		case OpenGimbal.GearDirection.RIGHT:
			ogp._robots[ogp.currentBot].BroadcastMessage("m");
			break;
			
			
		}
	}
public virtual void o(){

		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();
		switch (ogp.gearDirection) {
		case OpenGimbal.GearDirection.UP:
			ogp._robots[ogp.currentBot].BroadcastMessage("7");
			break;
			
		case OpenGimbal.GearDirection.DOWN:
			ogp._robots[ogp.currentBot].BroadcastMessage("9");
			break;
			
		case OpenGimbal.GearDirection.LEFT:
			ogp._robots[ogp.currentBot].BroadcastMessage("2");
			break;
			
		case OpenGimbal.GearDirection.RIGHT:
			ogp._robots[ogp.currentBot].BroadcastMessage("4");
			break;
			
			
		}
	}
public virtual void s(){
		OpenGimbal ogp = GameObject.Find ("OpenGimbal").GetComponent<OpenGimbal> ();
		switch (ogp.gearDirection) {
		case OpenGimbal.GearDirection.UP:
			ogp._robots[ogp.currentBot].BroadcastMessage("sqd");
			break;
			
		case OpenGimbal.GearDirection.DOWN:
			ogp._robots[ogp.currentBot].BroadcastMessage("squ");
			break;
			
		case OpenGimbal.GearDirection.LEFT:
			ogp._robots[ogp.currentBot].BroadcastMessage("sqr");
			break;
			
		case OpenGimbal.GearDirection.RIGHT:
			ogp._robots[ogp.currentBot].BroadcastMessage("sql");
			break;
			
			
		}
	}
}


