using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json.Linq;

public delegate void APICallback(JObject json, string error);
public delegate void TextureLoadCallback(Texture2D json, string error);

public class CGAPI : MonoBehaviourSingleton<CGAPI>
{

	// const string URL = "localhost:8090";
	const string URL = "https://dry-ridge-16720.herokuapp.com";
	const string S3 = "localhost:8090";

	void Start()
	{
		//GET("ping", null);
		//var json = new JObject();
		//json.Add("username", "test_user_0");
		//json.Add("password", "testpassword");
		//POST("login", json, null);
	}

	private string FormatRoute(string route)
	{
		return URL + "/" + route;
	}

	private void ParseResponse(UnityWebRequest req, APICallback cb)
	{
		if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
		{
			string error = ": Error: " + req.error;
			CGLogChannels.instance().LogChannel(CGLogChannel.API, error);
			cb?.Invoke(null, error);
		}
		else
		{
			JObject json = JObject.Parse(req.downloadHandler.text);
			CGLogChannels.instance().LogChannelRaw(CGLogChannel.API, json.ToString());
			cb?.Invoke(json, null);
		}
	}

	private void SetHeaders(UnityWebRequest req)
	{
		req.SetRequestHeader("Content-Type", "application/json");
		//req.SetRequestHeader("Authorization", CGPlayer.AUTH_TOKEN);
	}

	#region GET

	public void GET(string route, APICallback cb)
	{
		StartCoroutine(GET_Internal(route, cb));
	}

	IEnumerator GET_Internal(string route, APICallback cb)
	{
		using (UnityWebRequest req = UnityWebRequest.Get(FormatRoute(route)))
		{
			SetHeaders(req);
			yield return req.SendWebRequest();
			ParseResponse(req, cb);
		}
	}
	#endregion

	#region POST
	public void POST(string route, JObject json, APICallback cb)
	{
		StartCoroutine(POSTPUT_Internal(route, json, cb));
	}

	public void PUT(string route, JObject json, APICallback cb)
	{
		// put uses post and
		StartCoroutine(POSTPUT_Internal(route, json, cb));
	}

	IEnumerator POSTPUT_Internal(string route, JObject json, APICallback cb)
	{
		byte[] myData = System.Text.Encoding.UTF8.GetBytes(json.ToString());
		using (UnityWebRequest req = UnityWebRequest.Put(FormatRoute(route), myData))
		{
			SetHeaders(req);
			yield return req.SendWebRequest();
			ParseResponse(req, cb);
		}
	}
	#endregion

	#region Textures

	public void LOAD(string name, TextureLoadCallback cb)
	{
		StartCoroutine(LOAD_Internal(name, cb));
	}

	IEnumerator LOAD_Internal(string name, TextureLoadCallback cb)
	{
		using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(S3 + name))
		{
			yield return uwr.SendWebRequest();

			if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.Log(uwr.error);
			}
			else
			{
				// Get downloaded asset bundle
				var texture = DownloadHandlerTexture.GetContent(uwr);
			}
		}
	}

	#endregion
}
