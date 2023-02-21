using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGGameObjectParent : MonoBehaviourSingleton<CGGameObjectParent>
{
	public static CGGameObjectParent CurrentParent
	{
		get
		{
			return instance();
		}
	}

	private void OnDestroy()
	{
		transform.DestroyChildren();
	}
}
