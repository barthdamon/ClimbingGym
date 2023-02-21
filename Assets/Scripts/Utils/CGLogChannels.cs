using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CGLogChannels : MonoBehaviourSingleton<CGLogChannels>
{
	private int m_EntryIndex = 0;
	private CGLogChannelSettingsData m_ChannelSettings;
	public string META_TAG
	{
		get
		{
#if UNITY_EDITOR
			if (m_ChannelSettings == null)
				m_ChannelSettings = AssetDatabase.LoadAssetAtPath<CGLogChannelSettingsData>("Assets/Data/Settings/CGLogChannelSettingsData.asset");

			if (m_ChannelSettings != null)
				return m_ChannelSettings.m_MetaTag;
			else
				return null;
#else
			return null;
#endif
		}
	}

	public CGLogChannel LOG_CHANNEL
	{
		get
		{
#if UNITY_EDITOR
			if (m_ChannelSettings == null)
				m_ChannelSettings = AssetDatabase.LoadAssetAtPath<CGLogChannelSettingsData>("Assets/Settings/CGLogChannelSettingsData.asset");

			if (m_ChannelSettings != null)
				return m_ChannelSettings.LOG_CHANNEL;
			else
				return CGLogChannel.Open;
#else
			return TBLLogChannel.Open;
#endif
		}
	}

	public void LogChannel(CGLogChannel channel, string text, params object[] args)
	{
#if UNITY_EDITOR
		if (LOG_CHANNEL != channel && LOG_CHANNEL != CGLogChannel.Open) { return; }
#endif
		string log = string.Format("[" + (m_EntryIndex++) + channel.ToString() + "] " + text, args);
		UnityEngine.Debug.Log(log);
	}

	public void LogChannelRaw(CGLogChannel channel, string text)
	{
#if UNITY_EDITOR
		if (LOG_CHANNEL != channel && LOG_CHANNEL != CGLogChannel.Open) { return; }
#endif
		UnityEngine.Debug.Log(text);
	}
}




