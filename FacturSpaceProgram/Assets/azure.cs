using UnityEngine;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Globalization;
using System.Collections;
using System.Text;
using System.Net;	
using System.IO;
using System;

public class azure : MonoBehaviour {
	
	string accessKey = "28ezMSon32bgAtjIBHaYDn5L/yjA+Rrv7kEyqY6IlMPsL9x5B8vDYeJbOui1R4p3+4obHzWEpc27JoyiAMWHGw==";
	string accountName = "facturspaceprogram";
	string container = "ogp"; //Storage container created on azure portal called "test"
	
	// Use this for initialization
	void Start () {
		GetBlob_Test();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// GetBlob_Test
	void GetBlob_Test()
	{
		Debug.Log ("Attempting to GET from server");
		DateTime dt = DateTime.UtcNow;
		
		string stringToSign = String.Format("GET\n"
		                                    + "\n" // content md5
		                                    + "\n" // content type
		                                    + "x-ms-date:" + dt.ToString("R") + "\nx-ms-version:2012-02-12\n" // headers
		                                    + "/{0}/{1}\ncomp:list\nrestype:container", accountName, container);
		
		string authorizationKey = SignThis(stringToSign, accessKey, accountName);
		string method = "GET";
	//https://facturspaceprogram.blob.core.windows.net/ogp	
		string urlPath = string.Format("https://{0}.blob.core.windows.net/{1}?restype=container&comp=list", accountName, container);
		Uri uriTest = new Uri(urlPath);
		
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create (uriTest);
		request.Method = method;
		request.Headers.Add("x-ms-date", dt.ToString("R"));
		request.Headers.Add("x-ms-version", "2012-02-12");
		request.Headers.Add("Authorization", authorizationKey);
		
		Debug.Log ("Authorization: " + authorizationKey);
		
		using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
		{
			Debug.Log("Response = " + response);
		}
	}
	
	private static String SignThis(String StringToSign, string Key, string Account)
	{
		String signature = string.Empty;
		byte[] unicodeKey = Convert.FromBase64String(Key);
		using (HMACSHA256 hmacSha256 = new HMACSHA256(unicodeKey))
		{
			Byte[] dataToHmac = System.Text.Encoding.UTF8.GetBytes(StringToSign);
			signature = Convert.ToBase64String(hmacSha256.ComputeHash(dataToHmac));
		}
		
		String authorizationHeader = String.Format(
			CultureInfo.InvariantCulture,
			"{0} {1}:{2}",
			"SharedKeyLite",
			Account,
			signature);
		
		return authorizationHeader;
	}
}