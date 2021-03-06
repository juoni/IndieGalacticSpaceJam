﻿#define DEBUG
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using System;



public class OpenGimbal : MonoBehaviour {



	public GearDirection gearDirection  = GearDirection.NONE;


	public List<SocketStateHandler> _robots = null;


	public enum GearDirection 
	{
		UP,
		DOWN,
		LEFT,
		RIGHT,
		NONE
	}
	//Break it out
	public string address         = "ws://192.168.42.1:8888/ws";
	public string address2        = "ws://192.168.42.2:8888/ws";

	public string connectedString = "Connected to:";
	public string guiLabel        = "Disconnected";
	public bool   autoConnect     = true;

	public string toScope = "";


	public float socketConnectionWaitTime = 60f;
	public int   timeWaitedForConnection  = 0;
	public float lastTimeCaptured         =0f;

	public int currentBot = 0;

    //public GestureListener 	gestureListener = null;

	#region Unity methods

	// Use this for initialization
	void Start () 
	{
		_robots = new List<SocketStateHandler> ();
	


			_robots.Add(gameObject.AddComponent<SocketStateHandler>());
			_robots[_robots.Count -1].Address = address;
			_robots[_robots.Count -1].InitializeSocket(autoConnect,address);
			_robots [_robots.Count - 1].RobotName = "Star Bot";

			currentBot = 0;

		GameObject roverSocket = new GameObject ();
		roverSocket.name = "RoverSocket";
		roverSocket.AddComponent<SocketStateHandler> ();
			_robots.Add(roverSocket.GetComponent<SocketStateHandler>());
			_robots[_robots.Count -1].Address = address2;
			_robots[_robots.Count -1].InitializeSocket(autoConnect,address2);
		    _robots [_robots.Count - 1].RobotName = "Rover";
	
				

		
		

    //    gestureListener = Camera.main.GetComponent<GestureListener>();

	}

	void OnGUI()
	{
		for (int i = 0; i < _robots.Count; i++) {
			GUILayout.Label(_robots[i].RobotName + ": " +_robots[i].GetSocketState().ToString());
		}
	

	}

	void FixedUpdate()
	{
		if (Input.GetKeyUp (KeyCode.UpArrow)) 
		{

			_robots[currentBot].BroadcastMessage(toScope);
		}
	}
	
	// Update is called once per frame
//	void Update ()
//	{
//
//        if (_robots[currentBot] != null)
//        {
//        //    KinectManager kinectManager = KinectManager.Instance;
//
//
//          //  if (kinectManager != null)
//            //{
//              //  if (kinectManager.IsInitialized())
//                //{
//                  //  if (kinectManager.IsUserDetected())
//                    //{
//                      //  if (gestureListener.IsSwipeLeft())
//                        //{
//                          //  GameObject.Find("DirectionalGear").GetComponent<DirectionalGearController>().n();
//
//                        //}
//
//                        //else if (gestureListener.IsSwipeRight())
//                        //{
//                          //  GameObject.Find("DirectionalGear").GetComponent<DirectionalGearController>().n();
//
//
//                    //    /}
//                    //}
//                //}
//            //}
//        
//        }
//
// 
//	}
	#endregion
//
//	void ConnectToSocketServer()
//	{
////		if (socket == null) {
////			CreateSocket(address);
////			socket.Connect ();
////			if (socket.IsAlive) {
////				guiLabel = "connected";
////				connecting = false;
////				disconnected = false;
////			}
////		} else {
////			connecting = true;
////			guiLabel = "connecting";
////			socket.Connect ();
////			
////			if (socket.IsAlive) {
////				guiLabel = "connected";
////				connecting = false;
////				disconnected = false;
////			}
////		}
//	}
//
//	void DisconnectFromServer()
//	{
////		if (socket != null)
////		{
////			connected    = false;
////			socket.Dispose();
////			disconnected = true;
////			socket       = null;
////			guiLabel = "Disconnected";
////		}
//	}


	void TimeOut(){
//#if(DEBUG)
//		Debug.Log("Timed out after: " + socketConnectionWaitTime + " seconds");
//#endif
//		connecting = false;
//		guiLabel = "Server timed out  Timed out";
//		lastTimeCaptured = 0f;
//		timeWaitedForConnection = 0;

	}


}
