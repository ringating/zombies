﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow2D : MonoBehaviour 
{
    public Transform target;
    private Rigidbody r;

    void Start()
    {
        r = this.GetComponent<Rigidbody>();
    }
	
	void Update () 
    {
		r.MovePosition(new Vector3(target.position.x, this.transform.position.y, target.position.z));
	}
}
