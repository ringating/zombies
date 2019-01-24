using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisHandler : MonoBehaviour
{
	public float deadZoneInner = 0.15f;
	public float deadZoneOuter = 0.975f;

	public Vector2 leftAxis;
	public Vector2 rightAxis;

    public Transform kbmOrigin;
    public Transform kbmMouse;

	void Update()
	{
		UpdateAxes();
	}

	private void ResetAxes()
	{
		leftAxis = Vector2.zero;
		rightAxis = Vector2.zero;
	}

	private void UpdateAxes()
	{
		// zero the axes
		ResetAxes();

		// update left stick axis with raw input
		leftAxis.Set(Input.GetAxisRaw("left stick x"), Input.GetAxisRaw("left stick y"));
		// filter the left stick input
		leftAxis = leftAxis.normalized * Tools.Map(leftAxis.magnitude, deadZoneInner, deadZoneOuter, 0, 1);

		// update right stick axis with raw input
		rightAxis.Set(Input.GetAxisRaw("right stick x"), Input.GetAxisRaw("right stick y"));
		// filter the right stick input
		rightAxis = rightAxis.normalized * Tools.Map(rightAxis.magnitude, deadZoneInner, deadZoneOuter, 0, 1);

        // mouse and keyboard overwrite
        if(Input.GetButton("kbWalk"))
        {
            Vector3 temp = (kbmMouse.position - kbmOrigin.position);
            leftAxis = new Vector2(temp.x, temp.z);
			leftAxis.Normalize();
        }
	}
}
