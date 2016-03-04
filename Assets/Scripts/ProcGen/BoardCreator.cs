﻿using System.Collections;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    // The type of tile that will be laid in a specific position.
    public enum TileType
    {
        Wall, Floor,
    }

    public int columns = 100;                                 // The number of columns on the board (how wide it will be).
    public int rows = 100;                                    // The number of rows on the board (how tall it will be).
    public IntRange numRooms = new IntRange(15, 20);         // The range of the number of rooms there can be.
    public IntRange roomWidth = new IntRange(3, 10);         // The range of widths rooms can have.
    public IntRange roomHeight = new IntRange(3, 10);        // The range of heights rooms can have.
    public IntRange corridorLength = new IntRange(6, 10);    // The range of lengths corridors between rooms can have.
    public GameObject[] floorTiles;                           // An array of floor tile prefabs.
    public GameObject[] wallTiles;                            // An array of wall tile prefabs.
    public GameObject[] outerWallTiles;                       // An array of outer wall tile prefabs.
    public GameObject player;
	public GameObject PuzzelRoom;							  // The prefab for the puzzel room
	private float roll;										  // Variable to hold the roll on the randomly instantiated puzzel rooms
	public int PercentChance = 50;							  // Variable for the percent chance of the randomly instantiated puzzel rooms.

    private TileType[][] tiles;                               // A jagged array of tile types representing the board, like a grid.
    private Room[] rooms;                                     // All the rooms that are created for this board.
    private Corridor[] corridors;                             // All the corridors that connect the rooms.
	private Corridor[] aCorridors;							  // All the appending corridors that connects to the main corridor.
    private GameObject boardHolder;                           // GameObject that acts as a container for all other tiles.


    private void Start()
    {
        // Create the board holder.
        boardHolder = new GameObject("BoardHolder");

        SetupTilesArray();

        CreateRoomsAndCorridors();

        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();

        InstantiateTiles();
        InstantiateOuterWalls();
    }


    void SetupTilesArray()
    {
        // Set the tiles jagged array to the correct width.
        tiles = new TileType[columns][];

        // Go through all the tile arrays...
        for (int i = 0; i < tiles.Length; i++)
        {
            // ... and set each tile array is the correct height.
            tiles[i] = new TileType[rows];
        }
    }


	void CreateRoomsAndCorridors()
	{
		// Create the rooms array with a random size.
		rooms = new Room[numRooms.Random];

		// There should be one less corridor than there is rooms.
		corridors = new Corridor[rooms.Length - 1];
		
		// There will be a specified number of appending corridor
		aCorridors = new Corridor[(rooms.Length - 1) / 2];
		
		// Create the first room and corridor.
		rooms[0] = new Room();
		corridors[0] = new Corridor();
		
		int numAppend = 0;

		// Setup the first room, there is no previous corridor so we do not use one.
		rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);

		// Setup the first corridor using the first room.
		corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);

		// Set up second room. Check for overlap is not necessary
		rooms[1] = new Room();
		rooms[1].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[0]);

		// Set up the rest of the rooms and corridors, checking for overlaps
		for (int i = 2; i < rooms.Length; i++)
		{
			bool goodRoomPlacement = false;

			// If room overlaps with any other rooms, create entirely new corridor leaving from the last created room
			while (!goodRoomPlacement)
			{
				bool appendCorridor = false;
							
				// Create test corridor and room
				Corridor corridorToBePlaced = new Corridor();
				Corridor corridorToAppend = new Corridor();
				if (numAppend < (rooms.Length - 1) / 2)
				{
					Debug.Log("Appending...");
					appendCorridor = true;
				}
				
				corridorToBePlaced.SetupCorridor (rooms [i-1], corridorLength, roomWidth, roomHeight, columns, rows, false);
				
				Room roomToBePlaced = new Room ();
				
				if (appendCorridor)
				{
					corridorToAppend.appendCorridor (corridorToBePlaced, corridorLength, corridorToBePlaced.EndPositionX, corridorToBePlaced.EndPositionY);
					roomToBePlaced.SetupRoom(roomWidth, roomHeight, columns, rows, corridorToAppend);
				}
				else
					roomToBePlaced.SetupRoom (roomWidth, roomHeight, columns, rows, corridorToBePlaced);

				// Loop over all other rooms created, except for one to be placed
				for (int j = 0; j < i; j++)
				{
					// If room to be placed overlaps with j-th room...
					if (doRoomsOverlap (rooms [j], roomToBePlaced))
					{
						/* No need to check other rooms, so break from
						 * for loop and setup a new corridor and room */
						break;
					} 

					// If last room has been checked and room to be placed doesn't overlap with it...
					if (j == (i - 1))
					{
						// Room to be placed doesn't overlap with any existing rooms, so exit while loop
						goodRoomPlacement = true;
					}
				}

				// Room doesn't overlap with any other rooms, so add corridor and room to their arrays
				if (goodRoomPlacement)
				{
					corridors [i - 1] = corridorToBePlaced;
					if (appendCorridor)
					{
						Debug.Log("Saving...");
						aCorridors[numAppend] = corridorToAppend;
						numAppend++;
					}
					
					rooms [i] = roomToBePlaced;

					//Rolls the dice
					roll = Random.Range (0, 100);
					//If the roll is between 0 and the PercentChance value
					if (roll <= PercentChance)
					{
						//Spawn the prefab
						Instantiate (PuzzelRoom, new Vector3 (roomToBePlaced.xPos+roomToBePlaced.roomWidth, roomToBePlaced.yPos+roomToBePlaced.roomHeight, 0), Quaternion.identity);
					}
				}
			}

			//Instantiates player in the i-th/2 room created
			//Cast as int so condition is always reachable
			if (i == (int) (rooms.Length * .5f))
			{
				Vector3 playerPos = new Vector3(rooms[0].xPos, rooms[0].yPos, 0);
				Instantiate(player, playerPos, Quaternion.identity);
			}
		}
	}


	// Method takes two rooms as arguments and returns true/false if they overlap/don't overlap
	bool doRoomsOverlap(Room alreadyPlaced, Room toBePlaced) {
		// Check if one rectangle is on the left side of the other
		if((alreadyPlaced.xPos > (toBePlaced.xPos + toBePlaced.roomWidth)) || (toBePlaced.xPos > (alreadyPlaced.xPos + alreadyPlaced.roomWidth)))
			return false;
		// Check if one rectangle is above the top edge of the other
		if (((alreadyPlaced.yPos + alreadyPlaced.roomHeight) < toBePlaced.yPos) || ((toBePlaced.yPos + toBePlaced.roomHeight) < alreadyPlaced.yPos))
			return false;

		// Both condition weren't met, so rooms must be overlapping
		return true;
	}


    void SetTilesValuesForRooms()
    {
        // Go through all the rooms...
        for (int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];

            // ... and for each room go through it's width.
            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;

                // For each horizontal tile, go up vertically through the room's height.
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;

                    // The coordinates in the jagged array are based on the room's position and it's width and height.
                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }

    void SetTilesValuesForCorridors()
    {
        // Go through every corridor...
        for (int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];

            // and go through it's length.
            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {
                // Start the coordinates at the start of the corridor.
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                // Depending on the direction, add or subtract from the appropriate
                // coordinate based on how far through the length the loop is.
                switch (currentCorridor.direction)
                {
                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }

                //Widens the corridor to set width
                for ( int k = 0; k < currentCorridor.corridorWidth; k++) {
                    switch(currentCorridor.direction)
                    {
                        case Direction.North:
                            xCoord++;
                            break;
                        case Direction.East:
                            yCoord++;
                            break;
                        case Direction.South:
                            xCoord++;
                            break;
                        case Direction.West:
                            yCoord++;
                            break;

                    }
                    // Set the tile at these coordinates to Floor.
                    tiles[xCoord][yCoord] = TileType.Floor;
                }
                
            }
        }
		
		// Go through every corridor...
        for (int i = 0; i < aCorridors.Length; i++)
        {
            Corridor currentCorridor = aCorridors[i];

            // and go through it's length.
            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {
				Debug.Log("Corridor...");
                // Start the coordinates at the start of the corridor.
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                // Depending on the direction, add or subtract from the appropriate
                // coordinate based on how far through the length the loop is.
                switch (currentCorridor.direction)
                {
                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }

                //Widens the corridor to set width
                for ( int k = 0; k < currentCorridor.corridorWidth; k++) {
                    switch(currentCorridor.direction)
                    {
                        case Direction.North:
                            xCoord++;
                            break;
                        case Direction.East:
                            yCoord++;
                            break;
                        case Direction.South:
                            xCoord++;
                            break;
                        case Direction.West:
                            yCoord++;
                            break;

                    }
                    // Set the tile at these coordinates to Floor.
                    tiles[xCoord][yCoord] = TileType.Floor;
                }
                
            }
        }
    }


    void InstantiateTiles()
    {
        // Go through all the tiles in the jagged array...
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                // ... and instantiate a floor tile for it.
                InstantiateFromArray(floorTiles, i, j);

                // If the tile type is Wall...
                if (tiles[i][j] == TileType.Wall)
                {
                    // ... instantiate a wall over the top.
                    InstantiateFromArray(wallTiles, i, j);
                }
            }
        }
    }


    void InstantiateOuterWalls()
    {
        // The outer walls are one unit left, right, up and down from the board.
        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = rows + 0f;

        // Instantiate both vertical walls (one on each side).
        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

        // Instantiate both horizontal walls, these are one in left and right from the outer walls.
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }


    void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {
        // Start the loop at the starting value for Y.
        float currentY = startingY;

        // While the value for Y is less than the end value...
        while (currentY <= endingY)
        {
            // ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
            InstantiateFromArray(outerWallTiles, xCoord, currentY);

            currentY++;
        }
    }


    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        // Start the loop at the starting value for X.
        float currentX = startingX;

        // While the value for X is less than the end value...
        while (currentX <= endingX)
        {
            // ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
            InstantiateFromArray(outerWallTiles, currentX, yCoord);

            currentX++;
        }
    }


    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        // Create a random index for the array.
        int randomIndex = Random.Range(0, prefabs.Length);

        // The position to be instantiated at is based on the coordinates.
        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        // Create an instance of the prefab from the random index of the array.
        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        // Set the tile's parent to the board holder.
        tileInstance.transform.parent = boardHolder.transform;
    }
}
