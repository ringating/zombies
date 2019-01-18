using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
	public static Vector3 V2toV3(Vector2 v2)
	{
		return new Vector3(v2.x, 0, v2.y);
	}

	public static Vector3 V2toV3(Vector2 v2, float y)
	{
		return new Vector3(v2.x, y, v2.y);
	}

	public static Vector2 V3toV2(Vector3 v3)
	{
		return new Vector2(v3.x, v3.z);
	}

    // maps a value from an input range to an output range
    public static float Map(float val, float inMin, float inMax, float outMin, float outMax)
	{
		return Mathf.Lerp(outMin, outMax, Mathf.InverseLerp(inMin, inMax, val));
	}
}
