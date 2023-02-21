using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CGLogChannel
{
	Open,
	Debug,
	JSON,
	API,
}

[CreateAssetMenu(fileName = "CGLogChannelSettings", menuName = "CG/LogChannelSettings")]
public class CGLogChannelSettingsData : ScriptableObject
{
	[HideInInspector]
	public CGLogChannel LOG_CHANNEL;

	[HideInInspector]
	public string m_MetaTag;
}

