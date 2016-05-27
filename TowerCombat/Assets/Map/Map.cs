using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

class Map
{
	private static int MAX_MAP_SIZE_X = 5;
	private static int MAX_MAP_SIZE_Y = 5;
	public MapGrid MapGrid;

	public Map()
	{
		MapGrid = new MapGrid();
		MapGrid.GenerateGrid(MAX_MAP_SIZE_X, MAX_MAP_SIZE_Y);
	}

}
