using System.Collections;
using UnityEngine.SceneManagement;
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
	public GameObject PuzzleCorridor;
	public int CorridorPercChance = 50;
	private float roll;										  // Variable to hold the roll on the randomly instantiated puzzel rooms
	public int PercentChance = 50;							  // Variable for the percent chance of the randomly instantiated puzzel rooms.
	public int hubOpening = 10;

	public bool reloadLevelNeeded = false;					 //Boolean for whether the level needs to reload
	public int triedCounter = 0;							 //Variable to hold how many tries a corridor/room would have before triggering the reload

    private TileType[][] tiles;                               // A jagged array of tile types representing the board, like a grid.
    private Room[] rooms;                                     // All the rooms that are created for this board.
    private Corridor[] corridors;                             // All the corridors that connect the rooms.
    private GameObject boardHolder;                           // GameObject that acts as a container for all other tiles.


    private void Start()
    {
		//Set to false when starting the generation
		reloadLevelNeeded = false;

        // Create the board holder.
        boardHolder = new GameObject("BoardHolder");

        SetupTilesArray();

        CreateRoomsAndCorridors();

		//Even after reloading the level these functions will still execute.
		//If statement needed to prevent time wasted generating the map when
		//the level is going to reload
		if (reloadLevelNeeded == false)
		{
			
			SetTilesValuesForRooms ();
			SetTilesValuesForCorridors ();

			InstantiateTiles ();
			InstantiateOuterWalls ();
		}
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

		//Non IntRange representation of how many rooms there are
		int numbRooms = rooms.Length;

		// There should be one less corridor than there is rooms.
		corridors = new Corridor[rooms.Length - 1];

		// Create the first room and corridor.
		rooms[0] = new Room();
		corridors[0] = new Corridor();

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

			//If generation has tried different corridor/room placements exceeding the number of rooms there are
			if (triedCounter >= numbRooms)
			{
				//Then reload the level
				reloadLevelNeeded = true;
				SceneManager.LoadScene (2);
				break;
			}
			// If room overlaps with any other rooms, create entirely new corridor leaving from the last created room
			while (!goodRoomPlacement)
			{
				// Create test corridor and room
				Corridor corridorToBePlaced = new Corridor();
				corridorToBePlaced.SetupCorridor (rooms [i-1], corridorLength, roomWidth, roomHeight, columns, rows, false);
				triedCounter++;
				Room roomToBePlaced = new Room ();
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

				//Break out of loop if genertion has tried generating corridors/rooms more than the number of rooms
				if (triedCounter >= numbRooms)
					break;
				
				// Room doesn't overlap with any other rooms, so add corridor and room to their arrays
				if (goodRoomPlacement)
				{
					//If room is good, then reset the tried counter
					triedCounter = 0;

					corridors [i - 1] = corridorToBePlaced;
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

			//If a room in the rooms array is null, this means generation did not succeed
			//Reloading the level is necessary, else continue with generation
			if (currentRoom == null) 
			{
				SceneManager.LoadScene (2);
				break;
			} 
			else 
			{
				// ... and for each room go through it's width.
				for (int j = 0; j < currentRoom.roomWidth; j++) {
					int xCoord = currentRoom.xPos + j;

					// For each horizontal tile, go up vertically through the room's height.
					for (int k = 0; k < currentRoom.roomHeight; k++) {
						int yCoord = currentRoom.yPos + k;

						// The coordinates in the jagged array are based on the room's position and it's width and height.
						tiles [xCoord] [yCoord] = TileType.Floor;
					}
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

			//If a room in the rooms array is null, this means generation did not succeed
			//Reloading the level is necessary, else continue with generation
			if (currentCorridor == null)
			{				
				SceneManager.LoadScene (2);
				break;
			} 
			else 
			{
				// and go through it's length.
				for (int j = 0; j < currentCorridor.corridorLength; j++) {
					// Start the coordinates at the start of the corridor.
					int xCoord = currentCorridor.startXPos;
					int yCoord = currentCorridor.startYPos;

					// Depending on the direction, add or subtract from the appropriate
					// coordinate based on how far through the length the loop is.
					switch (currentCorridor.direction) {
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
					for (int k = 0; k < currentCorridor.corridorWidth; k++) {
						switch (currentCorridor.direction) {
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
						tiles [xCoord] [yCoord] = TileType.Floor;
					}
                
				}
        
    
				//Makes a roll
				roll = Random.Range (0, 100);

				//If the roll is a success
				if (roll <= CorridorPercChance) {
					//Spawn the prefab in the correct position with respect to the direction of the corridor
					switch (currentCorridor.direction) {
					case Direction.North:
						Instantiate (PuzzleCorridor, new Vector3 (currentCorridor.startXPos + currentCorridor.corridorWidth / 2.0f, currentCorridor.startYPos + currentCorridor.corridorLength / 2.0f, 0), Quaternion.identity);
						break;
					case Direction.East:
						Instantiate (PuzzleCorridor, new Vector3 (currentCorridor.startXPos + currentCorridor.corridorLength / 2.0f, currentCorridor.startYPos + currentCorridor.corridorWidth / 2.0f, 0), Quaternion.identity);
						break;
					case Direction.South:
						Instantiate (PuzzleCorridor, new Vector3 (currentCorridor.startXPos + currentCorridor.corridorWidth / 2.0f, currentCorridor.startYPos - currentCorridor.corridorLength / 2.0f, 0), Quaternion.identity);
						break;
					case Direction.West:
						Instantiate (PuzzleCorridor, new Vector3 (currentCorridor.startXPos - currentCorridor.corridorLength / 2.0f, currentCorridor.startYPos + currentCorridor.corridorWidth / 2.0f, 0), Quaternion.identity);
						break;
					}
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
            // If connected to hub
			if (xCoord == -1f && (currentY >= (columns / 2) && currentY < (columns / 2 + hubOpening))) {
				InstantiateFromArray (floorTiles, xCoord, currentY);
			} else {
				// ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
				InstantiateFromArray (outerWallTiles, xCoord, currentY);
			}

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
