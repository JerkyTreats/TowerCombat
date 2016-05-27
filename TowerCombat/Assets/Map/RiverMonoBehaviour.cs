using UnityEngine;
using System.Collections.Generic;
using System;

public class RiverMB : MonoBehaviour
{
	private River Controller;

	void Start()
	{
		Controller = new River();
	}

	public void GenerateRiver(MapGrid MapGrid)
	{
		var river = gameObject.AddComponent<RiverMB>();

		Debug.Log("Initializing River");

		Controller.Initialize(MapGrid);
		Controller.BuildRiver(MapGrid);
	}


	
}