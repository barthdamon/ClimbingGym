using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class CGUtils
{
	public static Quaternion FlatSpriteLocalTowardsTarget(Transform spriteTransform, Vector3 targetPostion, bool awayFromTarget = false)
	{
		Vector3 normalizedOffset = Vector3.zero;
		if (awayFromTarget)
			normalizedOffset = Position2D((spriteTransform.transform.position - targetPostion)).normalized;
		else
			normalizedOffset = Position2D((targetPostion - spriteTransform.transform.position)).normalized;

		var localDirection = spriteTransform.InverseTransformDirection(normalizedOffset);
		Vector3 euler = new Vector3(90f, Mathf.Rad2Deg * localDirection.x, Mathf.Rad2Deg * localDirection.z);
		return Quaternion.Euler(euler);
	}

	public static Rect RectTransformToScreenSpace(RectTransform transform)
	{
		Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
		return new Rect((Vector2)transform.position - (size * 0.5f), size);
	}

	public static Vector3 ClosestPointOnNavMesh(Vector3 point)
	{
		Vector3 pointOnMesh = point;
		float pointY = point.y;
		NavMeshHit hit;
		if (NavMesh.SamplePosition(point, out hit, 5.0f, NavMesh.AllAreas))
		{
			pointOnMesh = hit.position;
			//pointOnMesh.y = pointY;
		}

		return pointOnMesh;
	}

	public static Vector3 Position2D(Vector3 vector, float yPlane = 0f)
	{
		var twoD = vector;
		twoD.y = yPlane;
		return twoD;
	}

	public static bool Equivalent2D(Vector3 v1, Vector3 v2)
	{
		return v1.x == v2.x && v1.z == v2.z;
	}

	public static Color ShadeColor(Color color, float RGB_OFFSET)
	{
		var newR = color.r * (1 - RGB_OFFSET);
		var newG = color.g * (1 - RGB_OFFSET);
		var newB = color.b * (1 - RGB_OFFSET);
		return new Color(newR, newG, newB, color.a);
	}


}
