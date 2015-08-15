using UnityEngine;
using System.Collections;
using Renci.SshNet.Common;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.IO;
using System.Net;


[RequireComponent(typeof(GUITexture))]
public class OgpSftpHandler : MonoBehaviour {


     public  WebRequest request = null;
     public WebResponse response = null;

     public bool quickLoad = true;
     public string imagename = "imagesmall";
     WWW www;
     GUITexture r;
    private int                _allowedConnections   = 1;
    private bool loading = true;
    //[SerializeField]
    //private SftpConnectionInfo _sftpObjects          = null;

   
    void Awake()
    {
        //HardCoded for now
       // _sftpObjects = new SftpConnectionInfo("192.168.42.1", 22, "banjobob", "pi");
 
      

    }

   public IEnumerator download(string image)
    {
        www = new WWW("http://192.168.42.1/"+image+".jpg");
        yield return www;
		r = gameObject.GetComponent<GUITexture> ();
     //   r.texture = www.texture;
        loading = false;
        try
        {
            www.LoadImageIntoTexture((Texture2D)r.texture);
        }
        catch (WebException webe)
        {
            Debug.Log(webe.Message);
        }

        StopAllCoroutines();
      
    }
	// Use this for initialization
	void Start () {
        StartCoroutine(download(imagename));
            //// Create a request for the URL. 		
            //WebRequest request = WebRequest.Create ("http://192.168.42.1/imagesmall.jpg");
            //// If required by the server, set the credentials.
            //request.Credentials = CredentialCache.DefaultCredentials;
            //// Get the response.
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse ();
            //// Display the status.
            //Console.WriteLine (response.StatusDescription);
            //// Get the stream containing content returned by the server.
            //Stream dataStream = response.GetResponseStream ();

       


            //// Open the stream using a StreamReader for easy access.
            //StreamReader reader = new StreamReader (dataStream);


            //// Read the content. 
            //string responseFromServer = reader.ReadToEnd ();
            //// Display the content.
            //Debug.Log(responseFromServer);
            //// Cleanup the streams and the response.
            //reader.Close ();
            //dataStream.Close ();
            //response.Close ();
        
	}
	
	// Update is called once per frame
	void Update () {


            if (quickLoad && !loading)
            {
                loading = true;
                StartCoroutine(download(imagename));
            }
            if (Input.GetKeyUp(KeyCode.Space) && !loading)
            {
                loading = true;
                StartCoroutine(download(imagename));
            }
            


	}

    void OnGUI()
    {
     

    }


    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            StopAllCoroutines();
        }
    }

    private void GetNextImage()
    {
        KeyboardInteractiveConnectionInfo connectionInfo = new KeyboardInteractiveConnectionInfo("192.168.42.1", 22, "pi");

     
        connectionInfo.AuthenticationPrompt += delegate(object sender, AuthenticationPromptEventArgs e)
        {
         
            foreach (var prompt in e.Prompts)
            {
                if (prompt.Request.Equals("Password: ", StringComparison.InvariantCultureIgnoreCase))
                {
                    prompt.Response = "banjobob";
                }
                else
                {
                    Debug.Log(prompt.Request + " Prompt Request");
                }
            }
        };

        string password = "banjobob";
        //using (var sftp = new SftpClient(new ConnectionInfo("192.168.42.1","pi",)))
        //{
        //    Debug.Log( " :Is connected" + sftp.IsConnected  );
        //    try
        //    {

        //        sftp.Connect();
        //    }
        //    catch (UnityException e)
        //    {
        //        Debug.Log(e.Message);
        //    }
          
        //    var files = sftp.ListDirectory("/var/www/");
        //    Debug.Log(Application.persistentDataPath);
        //    foreach (var file in files)
        //    {
        //        if (!file.Name.StartsWith("."))
        //        {
        //            if (file.Name.EndsWith(".jpg"))
        //            {
        //                DownloadFile(sftp, file, Application.dataPath);
        //            }
        //        }
        //    }
        //}
    }

    void ConnectionInfo_AuthenticationBanner(object sender, AuthenticationBannerEventArgs e)
    {
       
    }



    [System.Serializable]
    public class SftpConnectionInfo
    {
            [SerializeField]
        private string    _host;
            [SerializeField]
        private int       _port;
            [SerializeField]
        private string    _userName;
            [SerializeField]
        private string    _password;

        public SftpConnectionInfo(string host,int port, string password, string username)
        {
            this._host = host;
            this._port     = port;
            this._userName = username;
            this._password = password;
        }

        public string GetUserName
        {
            get
            {
                return _userName;
            }

            set
            {
                System.Console.Write("You cannot set this password this way please load from config file or create one");
            }
        }

        public string GetHost
        {
            get { return _host; }
            set
            {
                System.Console.WriteLine("Sorry you cannot set the host from here please supply config file or create a new one");
            }
        }

        public string GetPassword
        {
             get
            {
                return _password;
            }

            set
            {
                System.Console.WriteLine("You can not set password from here please use a config file or create a new one");
            }
        }

        public int GetPort
        {
            get
            {
                return _port;
            }
        }

    }

   
    

}
