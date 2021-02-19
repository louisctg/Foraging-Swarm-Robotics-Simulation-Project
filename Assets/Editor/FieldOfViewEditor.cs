using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{

	void OnSceneGUI()
	{
		FieldOfView FOV = (FieldOfView)target;

		Vector3 rightViewDirection = FOV.DirectionFromAngle(-FOV.viewAngle / 2, FOV.transform.forward, FOV.transform.right);
		Vector3 leftViewDirection = FOV.DirectionFromAngle(FOV.viewAngle / 2, FOV.transform.forward, FOV.transform.right);

		Handles.color = Color.white;
		Handles.DrawWireArc(FOV.transform.position, FOV.transform.forward, leftViewDirection, 360.0f - FOV.viewAngle, FOV.viewRadius);

		Handles.color = Color.red;
		// Draw the arc view of the robot
		Handles.DrawLine(FOV.transform.position, FOV.transform.position + leftViewDirection * FOV.viewRadius);

		Handles.DrawWireArc(FOV.transform.position, FOV.transform.forward, rightViewDirection, FOV.viewAngle, FOV.viewRadius);

		Handles.DrawLine(FOV.transform.position, FOV.transform.position + rightViewDirection * FOV.viewRadius);
	}

}
