using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisHandler : MonoBehaviour
{
	private float deadZoneInner = 0.15f;
	private float deadZoneOuter = 0.975f;

	public Vector2 axisL;
	public Vector2 axisR;

	void Update()
	{
		UpdateAxes();
	}

	private void ResetAxes()
	{
		axisL = Vector2.zero;
		axisR = Vector2.zero;
	}

	private void UpdateAxes()
	{
		// zero the axes
		ResetAxes();

		// update left stick axis with raw input
		axisL.Set(Input.GetAxisRaw("left stick x"), Input.GetAxisRaw("left stick y"));
		// filter the left stick input
		axisL = axisL.normalized * Tools.Map(axisL.magnitude, deadZoneInner, deadZoneOuter, 0, 1);

		// update right stick axis with raw input
		axisR.Set(Input.GetAxisRaw("right stick x"), Input.GetAxisRaw("right stick y"));
		// filter the right stick input
		axisR = axisR.normalized * Tools.Map(axisR.magnitude, deadZoneInner, deadZoneOuter, 0, 1);
	}
}
