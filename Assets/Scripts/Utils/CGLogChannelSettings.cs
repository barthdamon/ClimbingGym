using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGLogChannelSettings : MonoBehaviourSingleton<CGLogChannelSettings>
{
	public GameObject m_Test;

	private GameObject GetPrefab<T>()
	{
		System.Type prefabType = typeof(T);
		if (prefabType == typeof(GameObject))
			return m_Test;

		return null;
	}

	#region Helpers

	public T Instantiate<T>(Transform parentOverride = null) where T : MonoBehaviour
	{
		GameObject prefab = GetPrefab<T>();
		if (prefab == null)
			return null;

		return GameObject.Instantiate(prefab, parentOverride != null ? parentOverride : CGGameObjectParent.CurrentParent.transform).GetComponent<T>();
	}

	#endregion
}
