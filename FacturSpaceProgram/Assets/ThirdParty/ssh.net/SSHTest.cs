using UnityEngine;
using Renci.SshNet;
using System.Net;

public class SSHTest : MonoBehaviour
{
	private GUIText text = null;
	
	private static string _host     = "192.168.42.1";
	private static string _username = "pi";
	private static string _password = "banjobob";
    private static int    _port     = 22;

    private static SshClient _client = null;

    private string _nextCommand = "";


    private static SshClient getSSH()
    {
        var connectionInfo = new PasswordConnectionInfo(_host, _port, _username, _password);

        if (_client == null)
        {

            _client = new SshClient(connectionInfo);
            _client.Connect();


           
        }

        return _client;
    }

    private static string runCommand(string command)
    {
        if (command == string.Empty)
        {
            return " Sent Empty String.... Ignoring";
        }
        var c = getSSH().RunCommand(command);

        try
        {
            Debug.LogError(c.Result);
        }
        catch (UnityException ue)
        {
            Debug.Log(ue.Message + "While Logging Command result Error");
        }

        return c.Result;
      
    }
	void Start()
	{

    
        text = this.GetComponent<GUIText> ();
		
        //try
        //{
        //    var connectionInfo = new PasswordConnectionInfo(_host, 22, _username, _password);
        //    text.text += "connection infos : ok\n";
			
        //    using (var client = new SshClient(connectionInfo ))
        //    {
        //        text.text += "Connecting...\n";
        //        client.Connect();
        //        text.text += "OK\n";
				
        //        var command = client.RunCommand("pwd");
        //        text.text += command.Result;

        //        //var startPython = client.RunCommand("sudo reboot");
        //       // text.text += startPython.Result + " Start Python";


        //        text.text += "Disconnecting...\n";
        //        client.Disconnect();
        //        text.text += "OK\n";
				
        //        Debug.Log (text.text);
        //    }
        //}
        //catch(System.Exception e)
        //{
        //    text.text = "Error\n" + e;
        //    Debug.Log(text.text + e);
			
        //}
	}


    void OnGUI()
    {


        _nextCommand = GUILayout.TextArea(_nextCommand,GUILayout.Width(512),GUILayout.Height(64));


        if (text != null)
        {
            if (GUILayout.Button("Send Command"))
            {
                
                runCommand(_nextCommand);

            }
        }
      
    }
}