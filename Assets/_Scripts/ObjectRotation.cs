﻿using UnityEngine;
using System.Collections;

public class ObjectRotation : MonoBehaviour
{
	public float Speed = 50;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		transform.Rotate(new Vector3(0f, 0f, Speed));
	}
}
