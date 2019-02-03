using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour 
{
    public bool useToggle = false;
    public float defaultSize = 6f;
    public float zoomOutSize = 18f;
    public float lerpSpeed = 10f;
    public float sizeLeniency = 0.05f;
    public Camera playerCamera;

    private bool zoomedOut = false;
    private float targetSize;

	void Start () 
    {
		playerCamera.orthographicSize = defaultSize;
        targetSize = defaultSize;
	}
	
	void Update () 
    {
		if(useToggle)
        {
            // toggle
            if(Input.GetButtonDown("zoom"))
            {
                zoomedOut = !zoomedOut;
            }
        }
        else
        {
            // hold
            if(Input.GetButton("zoom"))
            {
                zoomedOut = true;
            }
            else
            {
                zoomedOut = false;
            }
        }

        if(zoomedOut)
        {
            targetSize = zoomOutSize;
        }
        else
        {
            targetSize = defaultSize;
        }

        AdjustZoom(Time.deltaTime);
	}

    private void AdjustZoom(float dt)
    {
        if( Mathf.Abs(playerCamera.orthographicSize - targetSize) > sizeLeniency )
        {
            playerCamera.orthographicSize = Mathf.Lerp(playerCamera.orthographicSize, targetSize, lerpSpeed * dt);
        }
    }
}
