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
	public bool mouseWalk;

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
        if(Input.GetButton("kbWalkUp") || Input.GetButton("kbWalkDown") || Input.GetButton("kbWalkRight") || Input.GetButton("kbWalkLeft"))
        {
            if(mouseWalk)
			{
				Vector3 temp = (kbmMouse.position - kbmOrigin.position);
				leftAxis = new Vector2(temp.x, temp.z);
				leftAxis.Normalize();
			}
			else
			{
				// wasd movement
				leftAxis = Vector2.zero;

				if(Input.GetButton("kbWalkUp")){ leftAxis += Vector2.up; }
				if(Input.GetButton("kbWalkDown")){ leftAxis += Vector2.down; }
				if(Input.GetButton("kbWalkRight")){ leftAxis += Vector2.right; }
				if(Input.GetButton("kbWalkLeft")){ leftAxis += Vector2.left; }
				
				leftAxis.Normalize();
			}
			
        }
	}
}
