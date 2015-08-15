using UnityEngine;
using PsiEngine.@Input;
using PsiEngine.Interface3D;



public class TestTestController : AnimatedGenericMenuController{
void Awake(){}


void Start(){


 try{
InitilizeOptions();
}
 catch(System.Exception ex)
{
Debug.Log(ex.ToString());}
}
public virtual void Testing(){}
}