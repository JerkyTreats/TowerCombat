using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

class MapGrid
{
	public List<int[]> Grid;

	public MapGrid(){}

	//Generate a grid of items 
	public void GenerateGrid(int maxSizeX, int maxSizeY)
	{
		Grid = new List<int[]>();
		for (int x = 0; x < maxSizeX; x++)
		{
			for (int y = 0; y < maxSizeY; y++)
			{
				int[] node = { x, y };
				Debug.Log(string.Format("Adding [{0},{1}] to MapGrid", node[0], node[1]));
				Grid.Add(node);
			}
		}
		Debug.Log(string.Format("Creating MapGrid finished, [{0}] nodes created.", Grid.Count));
	}

	//Get the last item in the grid
	public int[] GetLastMapGrid()
	{
		return GetMapGridByIndex((Grid.Count - 1));
	}

	//Get the origin of the grid
	public int[] GetFirstMapGrid()
	{
		return GetMapGridByIndex(0);
	}

	//Get the results of the grid by index
	private int[] GetMapGridByIndex(int v)
	{
		//Debug.Log(string.Format("Getting map index [{0}], returning [{1},{2}]", v, MapGrid[v][0], MapGrid[v][1]));
		return Grid[v];
	}
}