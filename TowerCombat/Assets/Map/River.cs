using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class River
{
	private static int MAX_FAILURE_TOLERANCE = 50;

	private int[] MapGridEnd;

	public int[] StartNode;
	public int[] EndNode;

	List<int[]> RiverTiles;


	public void Initialize(MapGrid MapGrid)
	{
		int[] startGrid = MapGrid.GetFirstMapGrid();
		int[] endGrid = MapGrid.GetLastMapGrid();

		int[] xRange = { startGrid[0], endGrid[0] };
		int[] yRange = { startGrid[1], endGrid[1] };

		InitializeStartNode(xRange, yRange);
		InitializeEndNode(xRange, yRange);

		MapGridEnd = endGrid;
	}

	private void InitializeStartNode(int[] xRange, int[] yRange)
	{
		StartNode = GetRandomIntArrayByRange(xRange, yRange);
	}

	private void InitializeEndNode(int[] xRange, int[] yRange)
	{
		EndNode = GetRandomIntArrayByRange(xRange, yRange);
	}

	public void BuildRiver(MapGrid map)
	{
		RiverTiles.Add(StartNode);

		while ((!RiverTiles.Contains(EndNode)) && (ViablePathToEndNode(RiverTiles[(RiverTiles.Count - 1)])))
		{
			List<int[]> gridCandidates = GetGridCandidates(RiverTiles[(RiverTiles.Count - 1)]);
			int[] chosenCandidate = ChooseCandidate(gridCandidates);
			if (ViablePathToEndNode(chosenCandidate))
			{
				RiverTiles.Add(chosenCandidate);
			}
		}
	}

	//Check if the tile provided has a viable path to the end node.
	//Do a desirability check with zero tolerance for failure (select most desirable path everytime)
	private bool ViablePathToEndNode(int[] tileToCheck)
	{
		return true;
	}

	//Generate a list of "touching" coordinates from the provided coordinate (max possible should be 8 touching coordinates)
	//Possible coordinates equal +-1 from given [X,Y]
	//Exclude invalid candidates at this step (out of bounds, coordinates occupied, etc.) 
	private List<int[]> GetGridCandidates(int[] v)
	{
		int x = v[0];
		int y = v[1];

		List<int[]> candidates = new List<int[]>();
		List<int> xCandidates = new List<int>();
		List<int> yCandidates = new List<int>();

		int xMax = x + 1;
		if (xMax <= EndNode[0])
			xCandidates.Add(xMax);

		int xMin = x - 1;
		if (xMin >= 0)
			xCandidates.Add(xMin);

		int yMax = y + 1;
		if (yMax <= EndNode[1])
			yCandidates.Add(yMax);

		int yMin = y - 1;
		if (yMin >= 0)
			yCandidates.Add(yMin);

		foreach (int xCan in xCandidates)
		{
			foreach (int yCan in yCandidates)
			{
				candidates.Add(new int[] { xCan, yCan });
			}
		}

		return candidates;
	}

	//loop through all candidates and choose the one with the highest desirability
	private int[] ChooseCandidate(List<int[]> gridCandidates)
	{
		int[] chosenCandidate = gridCandidates[0];
		float highestDesirability = 0.0f;
		foreach (int[] candidate in gridCandidates)
		{
			float desirability = GetCandidateDesirability(candidate);
			if (desirability > highestDesirability)
				chosenCandidate = candidate;
		}

		return chosenCandidate;
	}

	//Get the desirability of a candidate
	//Rivers == distance to end node, with desirabiltiy "failure" so it doesn't just choose the straightest line 
	private float GetCandidateDesirability(int[] candidate)
	{
		int[] distanceToNodes = GetDistanceToEndNode(candidate);
		int totalDistance = distanceToNodes[0] + distanceToNodes[1];
		if (totalDistance == 0)
			return 0.0f;

		float failureAmount = GetDesirabilityFailureAmount(totalDistance);

		return (1 / (totalDistance + failureAmount));
	}

	//return an amount to "fail" by, returned as a range of -0.x to 0.x 
	private float GetDesirabilityFailureAmount(int distanceFromEnd)
	{
		int tolerance = GetToleranceByDistance(distanceFromEnd);
		System.Random rnd = new System.Random();
		int chosenFailureAmount = rnd.Next((tolerance * -1), tolerance);
		return chosenFailureAmount / 100;
	}

	//Depending on the distance to the end node, the tolerance for failure changes
	//The closer to the end node, the less tolerant of failure, the further, the more tolerant
	//divide the sum of current nodes x+y by the sum of the total node x+y to give you the distance percentage
	//Get the actual tolerance by multiplying the Max possible tolerance by the distance percentage 
	private int GetToleranceByDistance(int distanceFromEnd)
	{
		int mapTotal = MapGridEnd[0] + MapGridEnd[1];
		float percentageFromEnd = distanceFromEnd / mapTotal;
		return (int)(MAX_FAILURE_TOLERANCE * percentageFromEnd);
	}

	//return the distance of the X/Y coordinates to the end River Node 
	private int[] GetDistanceToEndNode(int[] candidate)
	{
		candidate[0] = EndNode[0] - candidate[0];
		candidate[1] = EndNode[1] - candidate[1];
		return candidate;
	}

	private int[] GetRandomIntArrayByRange(int[] xRange, int[] yRange)
	{
		System.Random r = new System.Random();
		var x = r.Next(xRange[0], xRange[1]);
		var y = r.Next(yRange[0], yRange[1]);
		return new int[] { x, y };
	}
}
