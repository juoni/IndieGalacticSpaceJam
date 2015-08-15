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
public virtual void Down(){}
public virtual void Left(){}
public virtual void LightMeterBase(){}
public virtual void LightMeter(){}
public virtual void Refresh(){}
public virtual void Right(){}
public virtual void Rover(){}
}