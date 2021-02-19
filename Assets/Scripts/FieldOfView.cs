using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
	[Range(0,1000)]
	public float viewRadius;

	[Range(0, 360)]
	public float viewAngle;

    public Vector3 DirectionFromAngle(float angle, Vector3 axis, Vector3 zeroDirection)
	{
		return Quaternion.AngleAxis(angle, axis) * zeroDirection;
	}
}
