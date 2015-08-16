using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;

public class SocketStateHandler : MonoBehaviour
{

	private  string  _name = "New Robot";
	private  string  _address = "ws://192.168.42.1:8888/ws";

    [SerializeField]
	private  float   _lastTimeCaptured = 0f;

    [SerializeField]
	private  float   _socketConnectionWaitTime = 60f;

    [SerializeField]
	private  int     _timeWaitedForConnection = 0;


	private WebSocket _socket = null;


    private bool _autoConnect = false;

    string origin;
    string readystate;
    string data;
    string code;

	SerialIn serialIn= null;
	// Use this for initialization
	void Start ()
	{
	  try
		{
			serialIn = GameObject.Find("SerialIn").GetComponent<SerialIn>();
			serialIn.AddMessage("Serial Connection engaged");
		}catch(System.NullReferenceException nre){
			Debug.Log(nre + " Serial in not found");
		}
	}

    void OnGUI()
    {

//        GUILayout.BeginArea(new Rect(200f, 200f, 250f, 500f));
//        GUILayout.BeginVertical();
//        GUILayout.TextArea(" ORIGIN :|" + origin + System.Environment.NewLine +
//                            " READY_STATE: |" + readystate + System.Environment.NewLine +
//                            " DATA:   |" + data + System.Environment.NewLine +
//                            " CODE: | " + code);
//        GUILayout.EndVertical();
//        GUILayout.EndArea();
    }
	// Update is called once per frame
	void Update ()
	{
        if (_autoConnect)
        {
            if (_timeWaitedForConnection < _socketConnectionWaitTime)
            {
                if (_lastTimeCaptured == 0)
                {
                    _lastTimeCaptured = Time.time + 1f;
                }
                else
                {

                    if (_lastTimeCaptured < Time.time)
                    {
                        _timeWaitedForConnection += 1;
                        _lastTimeCaptured = Time.time + 1f;
                    }
                    
                }
                if (_socket.ReadyState != WebSocketState.OPEN || _socket.ReadyState == WebSocketState.CONNECTING || _socket.ReadyState == WebSocketState.CLOSING)
                {
                    InitializeSocket(_autoConnect,Address);
                }


            }
            else
            {
                TimeOut();
            }
        }
       
       
	}

    private void TimeOut()
    {
        Debug.Log("Socket Timed out after " + _socketConnectionWaitTime + " seconds");
        Debug.Log("Disabling Auto Connect");
        _autoConnect = false;
    }
	//========== WebSocket Sharp Events ====================== 

	public string RobotName{
		get{
			return _name;
		}
		set{
			_name = value;
		}
	}


	public float GetLastTimeCaptured {
		get { 
			return _lastTimeCaptured;
		}
		private set { 
			_lastTimeCaptured = value;
		}

	}
	

	public void InitializeSocket(bool autoConnect, string address)
	{
		if (!string.IsNullOrEmpty (address)) 
		{
            if (_socket == null)
            {
				Debug.Log("Socket Address:" + address);
				_socket = new WebSocket(address);
                SetupSocketEvents(_socket);
            }
			
			
		}
		
		if (autoConnect) {
            _autoConnect = autoConnect;
			_socket.Connect();
		}

	}

	public void InitializeSocket(bool autoConnect, float timeout,string address)
	{
		if (timeout > 3)
		{
			_socketConnectionWaitTime = timeout;
		}
	
		if (!string.IsNullOrEmpty (address)) 
		{
            if (_socket == null)
            {
				_socket = new WebSocket(address);
                SetupSocketEvents(_socket);
            }

		}

		if (autoConnect) {
            _autoConnect = autoConnect;
			_socket.Connect();
		}
		
	}

	public void  OpenConnection(){

	}

	public void OpenConnectionAsync(){

	}

	public void  CloseSocket()
	{
		if (_socket != null)
		{
			_socket.Dispose();
			_socket = null;
		}
	}

	public WebSocketState GetSocketState(){
		return _socket.ReadyState;
	}


	public string Address{
		get{
			return _address;
		}
		set{
			_address = value;
		}
	}

	public void BroadcastMessage(string message){
		serialIn.AddSerialOut (message);
		Debug.Log ("BroadCast Address " + Address);
		_socket.Send (message);

	}
	//========== WebSocket Sharp Events ====================== 

	#region WebSocket Sharp Events
	//========== WebSocket Sharp Events ====================== 

	private void SetupSocketEvents(WebSocket socket){
		if (_socket != null) {
			SetOnClose(socket);
			SetOnErrorEvent(socket);
			SetOnMessage(socket);
			SetOnOpenEvent(socket);
		}
	}
	private void SetOnErrorEvent (WebSocket ws)
	{
		ws.OnError += (object sender, ErrorEventArgs e) => 
		{
			try{

			serialIn.AddMessage(e.Message);
			}catch(System.NullReferenceException nre){
				Debug.Log(nre.Message);
			}
		};
	}

	private void SetOnOpenEvent (WebSocket ws)
	{
		ws.OnOpen += (object sender, System.EventArgs e) => {
			WebSocket s = ((WebSocketSharp.WebSocket)sender);
			serialIn.AddMessage(origin);
			serialIn.AddMessage(readystate);
			serialIn.AddMessage(data);
			s.Send("n");
		};
	}

	private void SetOnMessage (WebSocket ws)
	{

		ws.OnMessage += (object sender, MessageEventArgs e) => {
			WebSocket s = ((WebSocketSharp.WebSocket)sender);
			Debug.Log(s.Cookies.ToString() + " COokies");

            serialIn.AddMessage(s.Origin);
            serialIn.AddMessage(s.ReadyState.ToString());
			serialIn.AddMessage(e.Data);
          
		};

	}

	private void SetOnClose (WebSocket ws)
	{
		ws.OnClose += (object sender, CloseEventArgs e) => {
            code = e.Code.ToString();
			serialIn.AddMessage("Closed Reason: " + e.Reason); 
			serialIn.AddMessage("Code: " + code);
		};
	}
	//========== WebSocket Sharp Events ====================== 
	#endregion
}

