using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBLDebug : MonoBehaviourSingleton<TBLDebug>
{
	public GameObject m_DebugPanel;

	public GameObject m_Debug_Finger_One;
	public GameObject m_Debug_Finger_Two;
	public float m_Debug_Scroll_Speed = .5f;
	public bool CanDebug()
	{
		//return false;
#if UNITY_EDITOR
		return true;
#else
		return false;
#endif
	}

	private void Awake()
	{
		if (m_DebugPanel.activeSelf)
			ToggleDebug();
	}

	public void HandleInitialLaunch()
	{
		if (!CanDebug())
			return;
	}

	public void SetDebugFinger(int fingerIdx, bool isActive)
	{
		if (!CanDebug())
		{
			return;
		}
		
		switch (fingerIdx)
		{
			case 0:	
				m_Debug_Finger_One.SetActive(isActive);
				break;
			case 1:
				m_Debug_Finger_Two.SetActive(isActive);
				break;
			default:
				Debug.Log($"Invalid Finger Idx Passed {fingerIdx}");
				break;
		}
	}

	public void MoveDebugFinger(int fingerIdx, float x, float y)
	{
		if (!CanDebug())
		{
			return;
		}
		
		switch (fingerIdx)
		{
			case 0:	
				m_Debug_Finger_One.GetComponent<RectTransform>().SetPositionAndRotation(new Vector3(x,y,0), Quaternion.identity);
				break;
			case 1:
				m_Debug_Finger_Two.GetComponent<RectTransform>().SetPositionAndRotation(new Vector3(x,y,0), Quaternion.identity);
				break;
			default:
				Debug.Log($"Invalid Finger Idx Passed {fingerIdx}");
				break;
		}
	}


	private void Update()
	{
		if (!CanDebug())
			return;

		if (Input.GetKeyDown("="))
		{
			ToggleDebug();
		}
	}

	private void ToggleDebug()
	{
		m_DebugPanel.SetActive(!m_DebugPanel.activeSelf);
	}
}
