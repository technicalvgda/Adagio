using System.Collections;
using System.Collections.Generic;
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
	public int minCorridorLength = 10;						//The minimum a corridor must be
	public GameObject[] floorTiles;                           // An array of floor tile prefabs.
	public GameObject[] wallTiles;                            // An array of wall tile prefabs.
	public GameObject[] outerWallTiles;                       // An array of outer wall tile prefabs.
	public GameObject player;
	public GameObject playerTeleportPlat;
	public GameObject teleporter;
	public GameObject PuzzelRoom;							  // The prefab for the puzzel room
	public GameObject HorizontalPuzzleCorridor;
    public GameObject VerticalPuzzleCorridor;
    public int CorridorPercChance = 50;
	private float roll;										  // Variable to hold the roll on the randomly instantiated puzzel rooms
	public int PercentChance = 50;							  // Variable for the percent chance of the randomly instantiated puzzel rooms.
	public int hubOpening = 10;
	public int DeadEndChance = 50;								//Variable for the percent chance that a dead-end corridor will be created
	private Corridor[] deadEndCorridorsArray;					//The array that holds the dead end corridors

	public bool reloadLevelNeeded = false;					 //Boolean for whether the level needs to reload
	public int triedCounter = 0;							 //Variable to hold how many tries a corridor/room would have before triggering the reload

	private TileType[][] tiles;                               // A jagged array of tile types representing the board, like a grid.
	private Room[] rooms;                                     // All the rooms that are created for this board.
	private Corridor[] corridors;                             // All the corridors that connect the rooms.
	private Corridor[] aCorridors;							  // All the appending corridors that connects to the main corridor.

	private GameObject boardHolder;                           // GameObject that acts as a container for all other tiles.
	public GameObject[] RandomPrefabs;                          //An array o

	public int element;
	private List<int> unusedRooms = new List<int>() {0,1,2,3,4,5,6,7}; //List of unused puzzle room indexes
	private int numAppend = 0;

	private Vector3 playerPos;								//The position of the player
	private GameObject[][] ActiveTiles;						//The array that holds the instantiated wall tiles of the board
	public int ActiveTileLength = 20;						//The range of which tiles will be active from the players perspective in the X direction
	public int ActiveTileHeight = 20;						//The range of which tiles will be active from the players perspective in the Y direction
	private float timer = 0;								//The timer that keeps track of how long it has been since the most recent board refresh
	public float TileInactiveTimer = 5;						//Set amount of seconds before tiles that are outside of the players visible range will be turned inactive
	public int CodexPercChance = 50;
	public GameObject[] codexArray;							//The array that will hold the codexes to be spawned

	private bool boardTilesAreActive;
	private bool activateOnce;

	public GameObject LoadingScreenCanvas;

	private Rect alreadyPlacedRect;							//Rectangle for the room/corridor that is already placed on the board
	private Rect toBePlacedRect;							//Rectangle for the room/corridor that is to be placed on the board
	private bool placementNeeded;							//bool for entering and exiting placement loops for rooms/corridors

	public GameObject AudioTrigger1;
	public GameObject AudioTrigger2;

	bool needRoomsAndCorridorsCreation;

    private void Start()
    {
        LoadingScreenCanvas.SetActive(true);
		needRoomsAndCorridorsCreation = true;
        //Set to false when starting the generation
        reloadLevelNeeded = false;

        // Create the board holder.
        boardHolder = new GameObject("BoardHolder");

        SetupTilesArray();
        //Create the array that holds the instantiated tile objects
        SetupActiveTilesArray();
        CreateRoomsAndCorridors();

        //Even after reloading the level these functions will still execute.
        //If statement needed to prevent time wasted generating the map when
        //the level is going to reload
        if (reloadLevelNeeded == false)
        {

            SetTilesValuesForRooms();
            SetTilesValuesForCorridors();
            SetTilesValuesForAppendedCorridors();
            SetTilesValuesForDeadEndCorridors();
            InstantiateTiles();
            InstantiateOuterWalls();

            //SetTilesUnactive(ActiveTiles);
        }
        if (corridors[2] != null)
            spawnAudioTrigger(corridors[2], AudioTrigger1);
        if (corridors[3] != null)
            spawnAudioTrigger(corridors[3], AudioTrigger2);

    }
            void FixedUpdate()
	{	
		

		if (LoadingScreenCanvas.activeSelf == true)
		{
			LoadingScreenCanvas.SetActive(false);
		}

		if (Input.GetKeyDown (KeyCode.LeftControl)) 
		{
			boardTilesAreActive = !boardTilesAreActive;
		}

		if (boardTilesAreActive == true) 
		{
			if (activateOnce == true) {
				SetTilesActive (ActiveTiles);
				activateOnce = false;
			}
		} 
		else if(boardTilesAreActive == false)
		{
			if (activateOnce == false) 
			{
				SetTilesUnactive (ActiveTiles);
				activateOnce = true;
			}
		}

		if(!boardTilesAreActive)
		{
			StartCoroutine ("makeTilesActiveInSquare");
		}
	}

	IEnumerator makeTilesActiveInSquare()
	{
		timer += Time.deltaTime;
		//Get the players position
		if(player == null)
		{
			player = GameObject.Find("Player");
		}
		playerPos = player.GetComponent<Transform> ().position;

		//Activates the tiles that are within the player's viewable range
		for (int i = (int)playerPos.x - ActiveTileLength / 2; i < (int)playerPos.x + ActiveTileLength / 2; i++) 
		{
			for (int j = (int)playerPos.y - ActiveTileHeight / 2; j < (int)playerPos.y + ActiveTileHeight / 2; j++) 
			{
				if (i >= -1 && i <= 200 && j >= -1 && j <= 200)
				{
					if (ActiveTiles [i+1] [j+1] != null) 
					{
						ActiveTiles [i + 1] [j + 1].SetActive (true);
					}
				}
			}
		}
		//If the timer reaches the determined amount of time
		if (timer > TileInactiveTimer) 
		{
			//Reset the timer and set blocks outside of player area inactive
			for (int i = -1; i < columns; i++) 
			{
				for (int j = -1; j < rows; j++) 
				{
					if ((i <= (int)playerPos.x - ActiveTileLength) || (i >= (int)playerPos.x + ActiveTileLength) || (j <= (int)playerPos.y - ActiveTileHeight) || (j >= (int)playerPos.y + ActiveTileHeight))
					if(ActiveTiles [i + 1] [j + 1] != null)
					if (ActiveTiles [i + 1] [j + 1].activeSelf == true)
						ActiveTiles [i + 1] [j + 1].SetActive (false);
				}
			}
			timer = 0;
		}
		yield return null;
	}
	//Sets the tiles unactive
	void SetTilesUnactive(GameObject[][] o)
	{
		for (int i = 0; i < o.Length; i++) {
			for (int j = 0; j < o [i].Length; j++) 
			{
				if(o[i][j] != null)
					o [i] [j].SetActive (false);
			}
		}
	}

	void SetTilesActive(GameObject[][] o)
	{
		for (int i = 0; i < o.Length; i++) {
			for (int j = 0; j < o [i].Length; j++) 
			{
				if(o[i][j] != null)
					o [i] [j].SetActive (true);
			}
		}
	}

	//Allocates memory for the 2D array that holds the instantiated object tiles
	void SetupActiveTilesArray()
	{
		// Set the tiles jagged array to the correct width.
		ActiveTiles = new GameObject[columns+2][];

		// Go through all the tile arrays...
		for (int i = 0; i < ActiveTiles.Length; i++)
		{
			// ... and set each tile array is the correct height.
			ActiveTiles[i] = new GameObject[rows+2];
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
		while (needRoomsAndCorridorsCreation == true) 
		{
			// Create the rooms array with a random size.
			rooms = new Room[numRooms.Random];

			//Non IntRange representation of how many rooms there are
			int numbRooms = rooms.Length;

			// There should be one less corridor than there is rooms.
			corridors = new Corridor[rooms.Length - 1];
			// There will be a specified number of appending corridor
			aCorridors = new Corridor[rooms.Length - 2];
			//Make dead end corridor array
			deadEndCorridorsArray = new Corridor[corridors.Length];
			// Create the first room and corridor.
			rooms [0] = new Room ();
			corridors [0] = new Corridor ();

			// Setup the first room, there is no previous corridor so we do not use one.
			rooms [0].SetupRoom (roomWidth, roomHeight, columns, rows);

			// Setup the first corridor using the first room.
			corridors [0].SetupCorridor (rooms [0], corridorLength, roomWidth, roomHeight, columns, rows, true);

			// Set up second room. Check for overlap is not necessary
			rooms [1] = new Room ();
			rooms [1].SetupRoom (roomWidth, roomHeight, columns, rows, corridors [0]);

			// Set up the rest of the rooms and corridors, checking for overlaps

			for (int i = 2; i < rooms.Length; i++) {
				bool goodRoomPlacement = false;
				bool goodCorridorPlacement = false;
				bool goodAppCorridorPlacement = false;
				bool goodDeadEndCorridorPlacement = false;
				//bool goodCorridorNotOverlapRoomPlacement = false;
				bool goodRoomNotOverlapCorridorPlacement = false;
				//bool roomOverlapsCorridor = false;
				//bool roomOverlapsAppCorridor = false;
				//bool roomOverlapsDeadEndCorridor = false;
				bool makeDeadEndCorridor = true;

				//If generation has tried different corridor/room placements exceeding the number of rooms there are
				if (triedCounter >= numbRooms * 2) {
					//Then reload the level
					reloadLevelNeeded = true;
                
					SceneManager.LoadScene (3);
					break;
				}
            




				// If room overlaps with any other rooms, create entirely new corridor leaving from the last created room
				while (!goodRoomPlacement && !goodCorridorPlacement && !goodDeadEndCorridorPlacement && !goodAppCorridorPlacement && !goodRoomNotOverlapCorridorPlacement) {
					bool appendCorridor = false;
					// Create test corridor and room
					Corridor corridorToBePlaced = new Corridor ();
					Corridor corridorToAppend = new Corridor ();
					if (numAppend < rooms.Length - 1)
						appendCorridor = true;

					//If a room in the array is null then there was an error with generation. Restart the scene
					if (rooms [i - 1] == null) {
						reloadLevelNeeded = true;
						SceneManager.LoadScene (3);
						break;
					}

					//Set tried counter to 0 and enter into while loop with placementNeeded as true
					triedCounter = 0;
					placementNeeded = true;
					//Stays in this loop until a good placement or the amount of tries has been exceeded
					while (placementNeeded == true) {
						triedCounter++;
						if (triedCounter >= numbRooms * 2)
							break;
						//Create the corridor


						//Make the corridor
						corridorToBePlaced.SetupCorridor (rooms [i - 1], corridorLength, roomWidth, roomHeight, columns, rows, false);

						//Stay in this loop until the corridor is longer than the minimum length or until tries are exhausted
						while (corridorToBePlaced.corridorLength < minCorridorLength) {
							triedCounter++;

							corridorToBePlaced.SetupCorridor (rooms [i - 1], corridorLength, roomWidth, roomHeight, columns, rows, false);
							if (triedCounter >= numbRooms * 2)
								break;
						}
						if (triedCounter >= numbRooms * 2)
							break;
						if (corridorToBePlaced.corridorLength < minCorridorLength) {
							//Do nothing
						} else {
							//Check to see if it overlaps any rooms/corridors
							for (int j = 0; j < i; j++) {
								//Check if it overlaps a regular corridor
								if (corridors [j] != null) {
									if (doCorridorsOverlapCorridor (corridors [j], corridorToBePlaced))
										break;
								}
								//Check if it overlaps with an appended corridor
								if (j < aCorridors.Length) {
									if (aCorridors [j] != null) {
										if (doCorridorsOverlapCorridor (aCorridors [j], corridorToBePlaced))
											break;
									}
								}
								//check if it overlaps with another dead end corridor
								if (deadEndCorridorsArray [j] != null) {
									if (doCorridorsOverlapCorridor (deadEndCorridorsArray [j], corridorToBePlaced))
										break;
								}
								// If last room has been checked and room to be placed doesn't overlap with it...
								if (j == (i - 1)) {
									//Dead end corridorto be placed doesn't overlap with any existing rooms, so exit while loop
									goodCorridorPlacement = true;
									//Exit the loop since a good placement has been achieved
									placementNeeded = false;
								}								
							}
						}
					}
					if (triedCounter >= numbRooms * 2)
						break;
				
					//triedCounter++;
					Room roomToBePlaced = new Room ();

					if (appendCorridor) {
						triedCounter = 0;
						placementNeeded = true;

						while (placementNeeded == true) {
							triedCounter++;
							if (triedCounter >= numbRooms * 2)
								break;
							//Create the Corridor


							//create the corridor 
							corridorToAppend.SetUpAppendedCorridor (corridorToBePlaced, corridorLength, roomWidth, roomHeight, columns, rows, corridorToBePlaced.EndPositionX, corridorToBePlaced.EndPositionY);

							//Stay in this loop until the corridor is longer than the minimum length or until tries are exhausted
							while (corridorToAppend.corridorLength < minCorridorLength) {
								triedCounter++;

								corridorToAppend.SetUpAppendedCorridor (corridorToBePlaced, corridorLength, roomWidth, roomHeight, columns, rows, corridorToBePlaced.EndPositionX, corridorToBePlaced.EndPositionY);
								if (triedCounter >= numbRooms * 2)
									break;
							}
							if (triedCounter >= numbRooms * 2)
								break;
							//Checks to see if the appended corridor overlaps with the dead end corridor that is to be placed
							//if(doCorridorsOverlapCorridor(deadEndCorridor,corridorToAppend))
							//	break;
						
							if (corridorToAppend.corridorLength < minCorridorLength) {
								//break;
							} else {
								//Check to see if it overlaps with any rooms/corridors
								for (int j = 0; j < i; j++) {
									//checks to see if the appending corridor overlaps with rooms
									if (doCorridorsOverlapRooms (rooms [j], corridorToAppend)) {
										break;
									}
									//checks to see if the appending corridor overlaps with regular corridors
									if (corridors [j] != null) {
										if (doCorridorsOverlapCorridor (corridors [j], corridorToAppend))
											break;
									}
									//checks to see if it overlaps with the other appending corridors
									if (j < aCorridors.Length) {
										if (aCorridors [j] != null) {
											if (doCorridorsOverlapCorridor (aCorridors [j], corridorToAppend))
												break;
										}
									}
									//checks to see if it overlaps with any dead end corridors
									if (deadEndCorridorsArray [j] != null) {
										if (doCorridorsOverlapCorridor (deadEndCorridorsArray [j], corridorToAppend))
											break;
									}

									if (j == (i - 1)) {
										// Room to be placed doesn't overlap with any existing rooms, so exit while loop
										goodAppCorridorPlacement = true;
										placementNeeded = false;
									}
								}
							}
						}
						if (goodAppCorridorPlacement == false)
							break;
						if (triedCounter >= numbRooms * 2)
							break;
						triedCounter = 0;
						placementNeeded = true;
						//Stays in this loop until a good placement or the amount of tries has been exceeded
						while (placementNeeded == true) {
							triedCounter++;
							if (triedCounter >= numbRooms * 2)
								break;
							//Create Room
							roomToBePlaced.SetupRoom (roomWidth, roomHeight, columns, rows, corridorToAppend);
							//if(doRoomsOverlapCorridor(deadEndCorridor,roomToBePlaced))
							//{
							//	break;
							//}
							//Check to see if it overlaps any corridors
							for (int j = 0; j < i; j++) {
								//checks to see if the room overlaps with regular corridors
								if (corridors [j] != null) {
									if (doRoomsOverlapCorridor (corridors [j], roomToBePlaced))
										break;
								}
								//checks to see if it overlaps any appending corridors
								if (j < aCorridors.Length) {
									if (aCorridors [j] != null) {
										if (doRoomsOverlapCorridor (aCorridors [j], roomToBePlaced))
											break;
									}
								}
								//checks to see if it overlaps with any dead end corridors
								if (deadEndCorridorsArray [j] != null) {
									if (doRoomsOverlapCorridor (deadEndCorridorsArray [j], roomToBePlaced))
										break;
								}
								if (j == (i - 1)) {
									// Room to be placed doesn't overlap with any existing rooms, so exit while loop
									goodRoomNotOverlapCorridorPlacement = true;
									placementNeeded = false;
								}
							}
						}
						if (goodRoomNotOverlapCorridorPlacement == false)
							break;
						if (triedCounter >= numbRooms * 2)
							break;
					} else {
						triedCounter = 0;
						placementNeeded = true;
						//Stays in this loop until a good placement or the amount of tries has been exceeded
						while (placementNeeded == true) {
							triedCounter++;
							if (triedCounter >= numbRooms * 2)
								break;
							//Create Room
							roomToBePlaced.SetupRoom (roomWidth, roomHeight, columns, rows, corridorToAppend);
							//if(doRoomsOverlapCorridor(deadEndCorridor,roomToBePlaced))
							//{
							//	break;
							//}
							//Check to see if it overlaps any corridors
							for (int j = 0; j < i; j++) {
								if (corridors [j] != null) {
									if (doRoomsOverlapCorridor (corridors [j], roomToBePlaced))
										break;
								}
								if (j < aCorridors.Length) {
									if (aCorridors [j] != null) {
										if (doRoomsOverlapCorridor (aCorridors [j], roomToBePlaced))
											break;
									}
								}
								if (deadEndCorridorsArray [j] != null) {
									if (doRoomsOverlapCorridor (deadEndCorridorsArray [j], roomToBePlaced))
										break;
								}
								if (j == (i - 1)) {
									// Room to be placed doesn't overlap with any existing rooms, so exit while loop
									goodRoomNotOverlapCorridorPlacement = true;
									placementNeeded = false;
								}
							}
						}
						if (goodRoomNotOverlapCorridorPlacement == false)
							break;
						if (triedCounter >= numbRooms * 2)
							break;
					}

					// Loop over all other rooms created, except for one to be placed
					for (int j = 0; j < i; j++) {

						// If room to be placed overlaps with j-th room...
						if (doRoomsOverlap (rooms [j], roomToBePlaced)) {
							/* No need to check other rooms, so break from
							 * for loop and setup a new corridor and room */
							break;
						} 


						// If last room has been checked and room to be placed doesn't overlap with it...
						if (j == (i - 1)) {
							// Room to be placed doesn't overlap with any existing rooms, so exit while loop
							goodRoomPlacement = true;
						}
					}
					//If there are no good room placements then break here and start over
					if (goodRoomPlacement == false)
						break;
				
					//Break out of loop if genertion has tried generating corridors/rooms more than the number of rooms
					if (triedCounter >= numbRooms * 2)
						break;

					Corridor deadEndCorridor = new Corridor ();
					roll = Random.Range (0, 100);
					if (roll <= DeadEndChance) {
						makeDeadEndCorridor = true;
						triedCounter = 0;
						placementNeeded = true;
						//Stays in this loop until a good placement or the amount of tries has been exceeded
						while (placementNeeded == true) {
							triedCounter++;
							if (triedCounter >= numbRooms * 2)
								break;
							//Make the corridor

							deadEndCorridor.SetupDeadEndCorridor (corridorToBePlaced, corridorLength, roomWidth, roomHeight, columns, rows, corridorToBePlaced.startXPos, corridorToBePlaced.startYPos);
							bool deadEndOverlaps = false;

							if (doCorridorsOverlapCorridor (corridorToAppend, deadEndCorridor))
								deadEndOverlaps = true;
							
							if (doCorridorsOverlapRooms (roomToBePlaced, deadEndCorridor))
								deadEndOverlaps = true;

							//If dead end corridor overlaps
							if (deadEndOverlaps == true) {
								
							} 
							//else if the dead end corridor does not overlap with currently placing corridor or room
							//then check with the rest of the arrays
							else {
								//Check placement with all other corridors/rooms
								for (int j = 0; j < i; j++) {
									//Check if it overlaps a room
									if (doCorridorsOverlapRooms (rooms [j], deadEndCorridor)) {
										break;
									}
									//Check if it overlaps a regular corridor
									if (corridors [j] != null) {
										if (doCorridorsOverlapCorridor (corridors [j], deadEndCorridor))
											break;
									}
									//Check if it overlaps with an appended corridor
									if (j < aCorridors.Length) {
										if (aCorridors [j] != null) {
											if (doCorridorsOverlapCorridor (aCorridors [j], deadEndCorridor))
												break;
										}
									}
									//check if it overlaps with another dead end corridor
									if (deadEndCorridorsArray [j] != null) {
										if (doCorridorsOverlapCorridor (deadEndCorridorsArray [j], deadEndCorridor))
											break;
									}
									// If last room has been checked and room to be placed doesn't overlap with it...
									if (j == (i - 1)) {
										//Dead end corridorto be placed doesn't overlap with any existing rooms, so exit while loop
										goodDeadEndCorridorPlacement = true;
										placementNeeded = false;
									}								
								}
							}
						}
					} else {
						goodDeadEndCorridorPlacement = true;
					}
					if (triedCounter >= numbRooms * 2)
						break;
				



					// Room doesn't overlap with any other rooms, so add corridor and room to their arrays
					if (goodRoomPlacement && goodCorridorPlacement && goodDeadEndCorridorPlacement && goodAppCorridorPlacement && goodRoomNotOverlapCorridorPlacement) {
						//If room is good, then reset the tried counter
						triedCounter = 0;

						corridors [i - 1] = corridorToBePlaced;
						if (appendCorridor) {
							if (numAppend < aCorridors.Length) {
								aCorridors [numAppend] = corridorToAppend;
								numAppend++;
							}
						}
						rooms [i] = roomToBePlaced;
						if (makeDeadEndCorridor)
							deadEndCorridorsArray [i - 1] = deadEndCorridor;
						//Rolls the dice
						roll = Random.Range (0, 100);
						//If the roll is between 0 and the PercentChance value
						if (roll <= PercentChance) {
							//Spawn the prefab

							//Randomly select from remaining unused rooms list
							if(unusedRooms.Count != 0)
								element = unusedRooms[Random.Range(0,unusedRooms.Count)];


							//Spawn the prefab
							//NOTE: when spawing in the random prefabs from the elements, i needed to divide the points by 2 so that each prefab AKA the images are spawned in the center of the room.
							//	Instantiate (PuzzelRoom, new Vector3 (roomToBePlaced.xPos+roomToBePlaced.roomWidth, roomToBePlaced.yPos+roomToBePlaced.roomHeight, 0), Quaternion.identity);          
							Instantiate (RandomPrefabs [element], new Vector3 (roomToBePlaced.xPos + (roomToBePlaced.roomWidth / 2) - 0.5f, roomToBePlaced.yPos + (roomToBePlaced.roomHeight / 2) -0.2f, 0), Quaternion.identity);

							//Remove used room from list
							unusedRooms.Remove(element);
						}
					}

					//Instantiates player in the i-th/2 room created
					//Cast as int so condition is always reachable
					if (i == (int)(rooms.Length * .5f)) {


						Vector3 playerTeleportPlatPos = new Vector3 (rooms [0].xPos, rooms [0].yPos, 0);
						Instantiate (playerTeleportPlat, playerTeleportPlatPos, Quaternion.identity);

					}

					if (i == (int)(rooms.Length - 1)) {
						if (rooms [rooms.Length - 1] != null) {
							Vector3 teleporterPos = new Vector3 (rooms [rooms.Length - 1].xPos, rooms [rooms.Length - 1].yPos, 0);
							Instantiate (teleporter, teleporterPos, Quaternion.identity);
						}
					}
				}
			}

			//Need to check if any rooms or corridors are null, indicating bad generation
			for (int i = 0; i < rooms.Length; i++) {
				if (rooms [i] == null) {
					triedCounter = 0;
					needRoomsAndCorridorsCreation = true;
					break;
				} else if (i == rooms.Length - 1 && rooms [i] != null)
					needRoomsAndCorridorsCreation = false;

			}
			for (int i = 0; i < corridors.Length; i++) {
				
				if (corridors [i] == null) {
					triedCounter = 0;
					needRoomsAndCorridorsCreation = true;
					break;
				} else if (i == corridors.Length - 1 && corridors [i] != null) {
					needRoomsAndCorridorsCreation = false;
				}
			}
		}
	}


        /*
    playerPos = new Vector3(rooms[0].xPos, rooms[0].yPos, 0);
    Instantiate (player, playerPos, Quaternion.identity);
    */
        
    


	// Method takes two rooms as arguments and returns true/false if they overlap/don't overlap
	bool doRoomsOverlap(Room alreadyPlaced, Room toBePlaced) 
	{
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
			//if (currentRoom == null) 
			//{
			//	SceneManager.LoadScene (3);
			//	break;
			//} 
			//else 
			//{
				// ... and for each room go through it's width.
				for (int j = 0; j < currentRoom.roomWidth; j++) 
				{
					int xCoord = currentRoom.xPos + j;

					// For each horizontal tile, go up vertically through the room's height.
					for (int k = 0; k < currentRoom.roomHeight; k++) 
					{
						int yCoord = currentRoom.yPos + k;

						// The coordinates in the jagged array are based on the room's position and it's width and height.
						tiles [xCoord] [yCoord] = TileType.Floor;
					}
				}
			//}
		}
        
	}

	void SetTilesValuesForCorridors()
	{
		// Go through every corridor...
		for (int i = 0; i < corridors.Length; i++) 
		{
			Corridor currentCorridor = corridors [i];

			//If a room in the rooms array is null, this means generation did not succeed
			//Reloading the level is necessary, else continue with generation
			if (currentCorridor == null) 
			{				
				SceneManager.LoadScene (3);
				break;
			} 
			else 
			{
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
					for (int k = 0; k < currentCorridor.corridorWidth; k++) 
					{
						switch (currentCorridor.direction) 
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
						tiles [xCoord] [yCoord] = TileType.Floor;
					}

				}


				//Makes a roll
				roll = Random.Range (0, 100);

				//If the roll is a success
				if (roll <= CorridorPercChance) 
				{
					//Spawn the prefab in the correct position with respect to the direction of the corridor
					switch (currentCorridor.direction) 
					{
					case Direction.North:
						Instantiate (VerticalPuzzleCorridor, new Vector3 (currentCorridor.startXPos + currentCorridor.corridorWidth / 2.0f, currentCorridor.startYPos + currentCorridor.corridorLength / 2.0f, 0), Quaternion.identity);
						break;
					case Direction.East:
						Instantiate (HorizontalPuzzleCorridor, new Vector3 (currentCorridor.startXPos + currentCorridor.corridorLength / 2.0f, currentCorridor.startYPos + currentCorridor.corridorWidth / 2.0f, 0), Quaternion.identity);
						break;
					case Direction.South:
						Instantiate (VerticalPuzzleCorridor, new Vector3 (currentCorridor.startXPos + currentCorridor.corridorWidth / 2.0f, currentCorridor.startYPos - currentCorridor.corridorLength / 2.0f, 0), Quaternion.identity);
						break;
					case Direction.West:
						Instantiate (HorizontalPuzzleCorridor, new Vector3 (currentCorridor.startXPos - currentCorridor.corridorLength / 2.0f, currentCorridor.startYPos + currentCorridor.corridorWidth / 2.0f, 0), Quaternion.identity);
						break;
					}
				}
			}
		}
    }


	void SetTilesValuesForAppendedCorridors()
	{
		// Go through every corridor...
		for (int i = 0; i < numAppend; i++) {
			Corridor currentCorridor = aCorridors [i];
			// and go through it's length.
			if (currentCorridor == null) 
			{				
				SceneManager.LoadScene (3);
				break;
			} 
			else
			{
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
						tiles [xCoord] [yCoord] = TileType.Floor;
					}
				}
			}
		}
	}


	void SetTilesValuesForDeadEndCorridors()
	{
		// Go through every corridor...
		for (int i = 0; i < deadEndCorridorsArray.Length; i++) 
		{
			Corridor currentCorridor = deadEndCorridorsArray [i];

			//check if it is null - used in case not all non-dead 
			//corridors will have dead end corridors spawning from them
			//since it is based off percent chance but the array needs to be
			//the same size as the non-dead corridors array to accomodate
			//every non-dead corridor having a dead end corridor
			if (currentCorridor != null) 
			{				
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
					for (int k = 0; k < currentCorridor.corridorWidth; k++) 
					{
						switch (currentCorridor.direction) 
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
							tiles [xCoord] [yCoord] = TileType.Floor;
					}
				}
				//Makes a roll
				roll = Random.Range (0, 100);

				//If the roll is a success
				if (roll <= CodexPercChance) 
				{
					//Spawn the prefab in the correct position with respect to the direction of the corridor
					element = Random.Range(0, codexArray.Length-1);
					switch (currentCorridor.direction) 
					{
					case Direction.North:
						Instantiate (codexArray[element], new Vector3 (currentCorridor.startXPos + currentCorridor.corridorWidth / 2.0f, currentCorridor.startYPos + currentCorridor.corridorLength-2, 0), Quaternion.identity);
						break;
					case Direction.East:
						Instantiate (codexArray[element], new Vector3 (currentCorridor.startXPos + currentCorridor.corridorLength-2, currentCorridor.startYPos + currentCorridor.corridorWidth / 2.0f, 0), Quaternion.identity);
						break;
					case Direction.South:
						Instantiate (codexArray[element], new Vector3 (currentCorridor.startXPos + currentCorridor.corridorWidth / 2.0f, currentCorridor.startYPos - currentCorridor.corridorLength+2, 0), Quaternion.identity);
						break;
					case Direction.West:
						Instantiate (codexArray[element], new Vector3 (currentCorridor.startXPos - currentCorridor.corridorLength+2, currentCorridor.startYPos + currentCorridor.corridorWidth / 2.0f, 0), Quaternion.identity);
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
		if (tileInstance.tag == "WallTile") 
		{
			ActiveTiles [(int)xCoord+1] [(int)yCoord+1] = tileInstance;
			//Set the tile boject to inactive
			tileInstance.SetActive (false);
		}
	}


	bool doCorridorsOverlapCorridor(Corridor alreadyPlaced, Corridor toBePlaced)
	{
		bool doesOverlap = false;

		//finds the direction of the alreadyPlaced corridor and makes the correct rectangle
		switch(alreadyPlaced.direction)
		{
		case Direction.North:
			alreadyPlacedRect = new Rect (alreadyPlaced.startXPos, alreadyPlaced.startYPos, alreadyPlaced.corridorWidth+2, alreadyPlaced.corridorLength);
			break;
		case Direction.East:
			alreadyPlacedRect = new Rect (alreadyPlaced.startXPos, alreadyPlaced.startYPos, alreadyPlaced.corridorLength, alreadyPlaced.corridorWidth+2);
			break;
		case Direction.South:
			alreadyPlacedRect = new Rect (alreadyPlaced.startXPos, alreadyPlaced.EndPositionY, alreadyPlaced.corridorWidth+2, alreadyPlaced.corridorLength);
			break;
		case Direction.West:
			alreadyPlacedRect = new Rect (alreadyPlaced.EndPositionX, alreadyPlaced.startYPos, alreadyPlaced.corridorLength, alreadyPlaced.corridorWidth+2);
			break;
		}
		//finds the direction of the toBePlaced corridor and makes the correct rectangle
		switch(toBePlaced.direction)
		{
		case Direction.North:
			toBePlacedRect = new Rect (toBePlaced.startXPos, toBePlaced.startYPos, toBePlaced.corridorWidth+2, toBePlaced.corridorLength);
			break;
		case Direction.East:
			toBePlacedRect = new Rect (toBePlaced.startXPos, toBePlaced.startYPos, toBePlaced.corridorLength, toBePlaced.corridorWidth+2);
			break;
		case Direction.South:
			toBePlacedRect = new Rect (toBePlaced.startXPos, toBePlaced.EndPositionY, toBePlaced.corridorWidth+2, toBePlaced.corridorLength);
			break;
		case Direction.West:
			toBePlacedRect = new Rect (toBePlaced.EndPositionX, toBePlaced.startYPos, toBePlaced.corridorLength, toBePlaced.corridorWidth+2);
			break;
		}
		//checks to see if the toBePlaced rectangle overlaps the already placed
		if (alreadyPlacedRect.Overlaps (toBePlacedRect)) 
		{
			doesOverlap = true;
		}
		//if previous did not register then checks vise versa
		else if (toBePlacedRect.Overlaps (alreadyPlacedRect))
		{
			doesOverlap = true;
		} 
		else 
		{
			//Lastly if code gets to this point it individually checks each block and see if it is contained within the other
			for (float i = toBePlacedRect.x; i <= toBePlacedRect.x + toBePlacedRect.width; i++) {
				for (float j = toBePlacedRect.y; j <= toBePlacedRect.y + toBePlacedRect.height; j++) {
					if (alreadyPlacedRect.Contains (new Vector2 (i, j))) {
						doesOverlap = true;
						break;
					}
				}
			}
		}

		return doesOverlap;

	}

	bool doCorridorsOverlapRooms(Room alreadyPlaced, Corridor toBePlaced)
	{
		bool doesOverlap = false;

		//creates the room rectangle
		alreadyPlacedRect = new Rect (alreadyPlaced.xPos-2, alreadyPlaced.yPos-2, alreadyPlaced.roomWidth+3, alreadyPlaced.roomHeight+3);

		//finds the direction of the toBePlaced corridor and makes the correct rectangle
		switch(toBePlaced.direction)
		{
		case Direction.North:
			toBePlacedRect = new Rect (toBePlaced.startXPos, toBePlaced.startYPos, toBePlaced.corridorWidth+2, toBePlaced.corridorLength);
			break;
		case Direction.East:
			toBePlacedRect = new Rect (toBePlaced.startXPos, toBePlaced.startYPos, toBePlaced.corridorLength, toBePlaced.corridorWidth+2);
			break;
		case Direction.South:
			toBePlacedRect = new Rect (toBePlaced.startXPos, toBePlaced.EndPositionY, toBePlaced.corridorWidth+2, toBePlaced.corridorLength);
			break;
		case Direction.West:
			toBePlacedRect = new Rect (toBePlaced.EndPositionX, toBePlaced.startYPos, toBePlaced.corridorLength, toBePlaced.corridorWidth+2);
			break;
		}
		//checks to see if the toBePlaced rectangle overlaps the already placed
		if (alreadyPlacedRect.Overlaps (toBePlacedRect)) 
		{
			doesOverlap = true;
		}
		//if previous did not register then checks vise versa
		else if (toBePlacedRect.Overlaps (alreadyPlacedRect)) 
		{
			doesOverlap = true;
		} 
		else 
		{
			//lastly if code reaches here then it individually checks each tile in one rectangle and checks if it is within the other
			for (float i = toBePlacedRect.x; i <= toBePlacedRect.x + toBePlacedRect.width; i++) {
				for (float j = toBePlacedRect.y; j <= toBePlacedRect.y + toBePlacedRect.height; j++) {
					if (alreadyPlacedRect.Contains (new Vector2 (i, j))) {
						doesOverlap = true;
						break;
					}
				}
			}
		}
		return doesOverlap;
	}


	bool doRoomsOverlapCorridor(Corridor alreadyPlaced, Room toBePlaced)
	{
		bool doesOverlap = false;
		//finds the direction of the corridor and makes the correct rectangle
		switch(alreadyPlaced.direction)
		{
		case Direction.North:
			alreadyPlacedRect = new Rect (alreadyPlaced.startXPos, alreadyPlaced.startYPos, alreadyPlaced.corridorWidth+2, alreadyPlaced.corridorLength);
			break;
		case Direction.East:
			alreadyPlacedRect = new Rect (alreadyPlaced.startXPos, alreadyPlaced.startYPos, alreadyPlaced.corridorLength, alreadyPlaced.corridorWidth+2);
			break;
		case Direction.South:
			alreadyPlacedRect = new Rect (alreadyPlaced.startXPos, alreadyPlaced.EndPositionY, alreadyPlaced.corridorWidth+2, alreadyPlaced.corridorLength);
			break;
		case Direction.West:
			alreadyPlacedRect = new Rect (alreadyPlaced.EndPositionX, alreadyPlaced.startYPos, alreadyPlaced.corridorLength, alreadyPlaced.corridorWidth+2);
			break;
		}

		//makes the room rectangle
		toBePlacedRect = new Rect (toBePlaced.xPos-2, toBePlaced.yPos-2, toBePlaced.roomWidth+3, toBePlaced.roomHeight+3);

		//checks to see if the toBePlaced rectangle overlaps the already placed
		if (alreadyPlacedRect.Overlaps (toBePlacedRect)) 
		{
			doesOverlap = true;
		}
		//if previous did not register then checks vise versa
		else if (toBePlacedRect.Overlaps (alreadyPlacedRect)) 
		{
			doesOverlap = true;
		} 
		else 
		{
			//Lastly if code gets here then it individually checks each tile and checks if it is within the other rect
			for (float i = toBePlacedRect.x; i <= toBePlacedRect.x + toBePlacedRect.width; i++) {
				for (float j = toBePlacedRect.y; j <= toBePlacedRect.y + toBePlacedRect.height; j++) {
					if (alreadyPlacedRect.Contains (new Vector2 (i, j))) {
						doesOverlap = true;
						break;
					}
				}
			}
		}
		return doesOverlap;
	}

	void spawnAudioTrigger(Corridor corridor, GameObject audioTrigger)
	{
		switch(corridor.direction)
		{
		case Direction.North:
			Instantiate(audioTrigger, new Vector3(corridor.startXPos+3,corridor.startYPos+3,0), Quaternion.identity);
			break;
		case Direction.East:
			Instantiate(audioTrigger, new Vector3(corridor.startXPos+3,corridor.startYPos+3,0), Quaternion.Euler(0,0,90));
			break;
		case Direction.South:
			Instantiate(audioTrigger, new Vector3(corridor.startXPos+3,corridor.startYPos-4,0), Quaternion.identity);
			break;
		case Direction.West:
			Instantiate(audioTrigger, new Vector3(corridor.startXPos-4,corridor.startYPos+3,0), Quaternion.Euler(0,0,90));
			break;
		}
	}
}

/*
					//Go through and make sure that no corridors overlap any corridors in the regular corridors array
for (int j = 0; j < i; j++) {
	if (corridors [j] != null) {
		if (doCorridorsOverlapCorridor (corridors [j], corridorToBePlaced) &&
			doCorridorsOverlapCorridor (corridors [j], corridorToAppend) &&
			doCorridorsOverlapCorridor (corridors [j], deadEndCorridor)) {
			Debug.Log ("CORRIDORS *** OVERLAPS");
			break;
		}
	}


	// If last room has been checked and no corridor is overlapping
	if (j == (i - 1)) {
		// exit the loop
		goodCorridorPlacement = true;
	}
}

//If there was any corridor overlapping the regular corridors array then exit the loop and start over
if (goodCorridorPlacement == false)
	break;*/
/*
for (int j = 0; j < i; j++) {
	if (deadEndCorridorsArray [j] != null) {
		if (doCorridorsOverlapCorridor (deadEndCorridorsArray [j], corridorToBePlaced) &&
			doCorridorsOverlapCorridor (deadEndCorridorsArray [j], corridorToAppend) &&
			doCorridorsOverlapCorridor (deadEndCorridorsArray [j], deadEndCorridor)) {
			Debug.Log ("DEADEND *** OVERLAPS");
			break;
		}
	}

	if (j == (i - 1)) {

		goodDeadEndCorridorPlacement = true;
	}
}

if (goodDeadEndCorridorPlacement == false)
	break;
*/
/*
for (int j = 0; j < i; j++) {
	if (j < aCorridors.Length) {
		if (aCorridors [j] != null) {
			if (doCorridorsOverlapCorridor (aCorridors [j], corridorToBePlaced) &&
				doCorridorsOverlapCorridor (aCorridors [j], corridorToAppend) &&
				doCorridorsOverlapCorridor (aCorridors [j], deadEndCorridor)) {
				Debug.Log ("APP *** OVERLAPS");
				break;
			}
		}
	}

	if (j == (i - 1)) {
		goodAppCorridorPlacement = true;
	}

}

if (goodAppCorridorPlacement == false)
	break;
*/
/*
for (int j = 0; j < i; j++) {
	if (rooms [j] != null) {
		if (doCorridorsOverlapRooms (rooms [j], corridorToBePlaced) &&
			doCorridorsOverlapRooms (rooms [j], corridorToAppend) &&
			doCorridorsOverlapRooms (rooms [j], deadEndCorridor)) {
			Debug.Log ("CORRIDOR OVERLAPS ROOM - BREAK");
			break;
		}
	}

	if (j == (i - 1)) {
		goodCorridorNotOverlapRoomPlacement = true;
	}
}

if (goodCorridorNotOverlapRoomPlacement == false)
	break;*/
/*
for (int j = 0; j < i; j++) {
	if (corridors [j] != null)
	if (doRoomsOverlapCorridor (corridors [j], roomToBePlaced))
		roomOverlapsCorridor = true;

	if (j < aCorridors.Length)
	if (aCorridors [j] != null)
	if (doRoomsOverlapCorridor (aCorridors [j], roomToBePlaced))
		roomOverlapsAppCorridor = true;

	if (deadEndCorridorsArray [j] != null)
	if (doRoomsOverlapCorridor (deadEndCorridorsArray [j], roomToBePlaced))
		roomOverlapsDeadEndCorridor = true;

	if (roomOverlapsCorridor && roomOverlapsAppCorridor && roomOverlapsDeadEndCorridor)
		break;

	if (j == (i - 1)) {
		goodRoomNotOverlapCorridorPlacement = true;
	}
}

if (goodRoomNotOverlapCorridorPlacement == false)
	break;
*/














/*
bool doRoomsOverlapCorridor(Corridor alreadyPlaced, Room toBePlaced)
{
	bool doesOverlap = false;
	switch (alreadyPlaced.direction) 
	{
	case Direction.North:
		//If the room was placed with the corridor exiting from the top
		if (toBePlaced.yPos <= alreadyPlaced.startYPos && toBePlaced.yPos + toBePlaced.roomHeight >= alreadyPlaced.startYPos) 
		{
			if (toBePlaced.xPos <= alreadyPlaced.startXPos && toBePlaced.xPos + toBePlaced.roomWidth >= alreadyPlaced.startXPos + alreadyPlaced.corridorWidth)
				doesOverlap = true;
		}
		//If the room was placed with the corridor exiting from the bottom
		else if (toBePlaced.yPos <= alreadyPlaced.startYPos + alreadyPlaced.corridorLength && toBePlaced.yPos >= alreadyPlaced.startYPos) 
		{
			if (toBePlaced.xPos <= alreadyPlaced.startXPos && toBePlaced.xPos + toBePlaced.roomWidth >= alreadyPlaced.startXPos + alreadyPlaced.corridorWidth)
				doesOverlap = true;
		}
		//if the room was placed with the corridor cutting through the entire room
		else if (toBePlaced.yPos >= alreadyPlaced.startYPos && toBePlaced.yPos + toBePlaced.roomHeight <= alreadyPlaced.EndPositionY) 
		{
			if (toBePlaced.xPos <= alreadyPlaced.startXPos && toBePlaced.xPos + toBePlaced.roomWidth >= alreadyPlaced.startXPos + alreadyPlaced.corridorWidth)
				doesOverlap = true;
		}
		break;
	case Direction.East:
		//If the room was placed with the corridor exiting from the left
		if (toBePlaced.xPos >= alreadyPlaced.startXPos && toBePlaced.xPos <= alreadyPlaced.EndPositionX) 
		{
			if (toBePlaced.yPos <= alreadyPlaced.startYPos && toBePlaced.yPos + toBePlaced.roomHeight >= alreadyPlaced.startYPos + alreadyPlaced.corridorWidth)
				doesOverlap = true;
		}
		//If the room was placed with the corridor exiting from the right
		else if (toBePlaced.xPos <= alreadyPlaced.startXPos && toBePlaced.xPos + toBePlaced.roomWidth >= alreadyPlaced.startXPos)
		{
			if (toBePlaced.yPos <= alreadyPlaced.startYPos && toBePlaced.yPos + toBePlaced.roomHeight >= alreadyPlaced.startYPos + alreadyPlaced.corridorWidth)
				doesOverlap = true;
		}
		//If the room was placed with the corridor cutting through the entire room
		else if (toBePlaced.xPos >= alreadyPlaced.startXPos && toBePlaced.xPos + toBePlaced.roomWidth <= alreadyPlaced.EndPositionX) 
		{
			if (toBePlaced.yPos <= alreadyPlaced.startYPos && toBePlaced.yPos + toBePlaced.roomHeight >= alreadyPlaced.startYPos + alreadyPlaced.corridorWidth)
				doesOverlap = true;
		}
		break;
	case Direction.South:
		//If the room is placed with the corridor exiting from the top
		if (toBePlaced.yPos + toBePlaced.roomHeight <= alreadyPlaced.startYPos && toBePlaced.yPos + toBePlaced.roomHeight >= alreadyPlaced.EndPositionY) 
		{
			if (toBePlaced.xPos <= alreadyPlaced.startXPos + alreadyPlaced.corridorWidth && toBePlaced.xPos + toBePlaced.roomWidth >= alreadyPlaced.startXPos)
				doesOverlap = true;
		}
		//If the room is placed with the corridor exiting from the bottom
		else if (toBePlaced.yPos <= alreadyPlaced.startYPos && toBePlaced.yPos >= alreadyPlaced.EndPositionY) 
		{
			if (toBePlaced.xPos <= alreadyPlaced.startXPos + alreadyPlaced.corridorWidth && toBePlaced.xPos + toBePlaced.roomWidth >= alreadyPlaced.startXPos)
				doesOverlap = true;
		}
		//If the room is placed with the corridor cutting through the entire room
		else if (toBePlaced.yPos + toBePlaced.roomHeight <= alreadyPlaced.startYPos && toBePlaced.yPos >= alreadyPlaced.EndPositionY) 
		{
			if (toBePlaced.xPos <= alreadyPlaced.startXPos + alreadyPlaced.corridorWidth && toBePlaced.xPos + toBePlaced.roomWidth >= alreadyPlaced.startXPos)
				doesOverlap = true;
		}
		break;
	case Direction.West:
		//If the room is placed with the corridor exiting from the left
		if (toBePlaced.xPos <= alreadyPlaced.startXPos && toBePlaced.xPos >= alreadyPlaced.EndPositionX) 
		{
			if (toBePlaced.yPos <= alreadyPlaced.startYPos + alreadyPlaced.corridorWidth && toBePlaced.yPos + toBePlaced.roomHeight >= toBePlaced.yPos)
				doesOverlap = true;
		}
		//If the room is placed with the corridor exiting from the right
		else if (toBePlaced.xPos + toBePlaced.roomWidth >= alreadyPlaced.EndPositionX && toBePlaced.xPos + toBePlaced.roomWidth <= alreadyPlaced.startXPos) 
		{
			if (toBePlaced.yPos <= alreadyPlaced.startYPos + alreadyPlaced.corridorWidth && toBePlaced.yPos + toBePlaced.roomHeight >= toBePlaced.yPos)
				doesOverlap = true;
		}
		//If the room is placed with the corridor is cutting through the entire room
		else if (toBePlaced.xPos >= alreadyPlaced.EndPositionX && toBePlaced.xPos + toBePlaced.roomWidth <= alreadyPlaced.startXPos) 
		{
			if (toBePlaced.yPos <= alreadyPlaced.startYPos + alreadyPlaced.corridorWidth && toBePlaced.yPos + toBePlaced.roomHeight >= toBePlaced.yPos)
				doesOverlap = true;
		}
		break;
	}



	return doesOverlap;
}
*/
/*
bool doCorridorsOverlapRooms(Room alreadyPlaced, Corridor toBePlaced)
{
	bool doesOverlap = false;

	switch (toBePlaced.direction) 
	{
	case Direction.North:
		//If the corridor starts below the room
		if (toBePlaced.startYPos <= alreadyPlaced.yPos && toBePlaced.EndPositionY >= alreadyPlaced.yPos) 
		{
			if (toBePlaced.startXPos >= alreadyPlaced.xPos && toBePlaced.startXPos <= alreadyPlaced.xPos + alreadyPlaced.roomWidth)
				doesOverlap = true;
		}
		//If the corridor exits the room from above
		else if (toBePlaced.startYPos <= alreadyPlaced.yPos + alreadyPlaced.roomHeight && toBePlaced.EndPositionY >= alreadyPlaced.yPos + alreadyPlaced.roomHeight) 
		{
			if (toBePlaced.startXPos >= alreadyPlaced.xPos && toBePlaced.startXPos <= alreadyPlaced.xPos + alreadyPlaced.roomWidth)
				doesOverlap = true;
		}
		//If the corridor goes through the room completely
		else if (toBePlaced.startYPos <= alreadyPlaced.yPos && toBePlaced.EndPositionY >= alreadyPlaced.yPos + alreadyPlaced.roomHeight) 
		{
			if (toBePlaced.startXPos >= alreadyPlaced.xPos && toBePlaced.startXPos <= alreadyPlaced.xPos + alreadyPlaced.roomWidth)
				doesOverlap = true;
		}
		break;
	case Direction.East:
		//Entering from the left side
		if (toBePlaced.startXPos <= alreadyPlaced.xPos && toBePlaced.EndPositionX >= alreadyPlaced.xPos) 
		{
			if (toBePlaced.startYPos >= alreadyPlaced.yPos && toBePlaced.startYPos <= alreadyPlaced.yPos + alreadyPlaced.roomHeight)
				doesOverlap = true;
		}
		//Exiting from the right side
		else if (toBePlaced.startXPos <= alreadyPlaced.xPos + alreadyPlaced.roomWidth && toBePlaced.EndPositionX >= alreadyPlaced.yPos + alreadyPlaced.roomWidth) 
		{
			if (toBePlaced.startYPos >= alreadyPlaced.yPos && toBePlaced.startYPos <= alreadyPlaced.yPos + alreadyPlaced.roomHeight)
				doesOverlap = true;
		}
		//If the corridor goes through the room completely
		else if (toBePlaced.startXPos <= alreadyPlaced.xPos && toBePlaced.EndPositionX >= alreadyPlaced.xPos + alreadyPlaced.roomWidth) 
		{
			if (toBePlaced.startYPos >= alreadyPlaced.yPos && toBePlaced.startYPos <= alreadyPlaced.yPos + alreadyPlaced.roomHeight)
				doesOverlap = true;
		}
		break;
	case Direction.South:		
		//If the corridor exits the room from the bottom	
		if (toBePlaced.startYPos >= alreadyPlaced.yPos && toBePlaced.EndPositionY <= alreadyPlaced.yPos) 
		{
			if (toBePlaced.startXPos >= alreadyPlaced.xPos && toBePlaced.startXPos <= alreadyPlaced.xPos + alreadyPlaced.roomWidth)
				doesOverlap = true;
		}
		//If the corridor enters the room from above
		else if (toBePlaced.startYPos >= alreadyPlaced.yPos + alreadyPlaced.roomHeight && toBePlaced.EndPositionY <= alreadyPlaced.yPos + alreadyPlaced.roomHeight)
		{
			if (toBePlaced.startXPos >= alreadyPlaced.xPos && toBePlaced.startXPos <= alreadyPlaced.xPos + alreadyPlaced.roomWidth)
				doesOverlap = true;
		} 
		else if (toBePlaced.startYPos >= alreadyPlaced.yPos + alreadyPlaced.roomHeight && toBePlaced.EndPositionY <= alreadyPlaced.yPos) 
		{
			if (toBePlaced.startXPos >= alreadyPlaced.xPos && toBePlaced.startXPos <= alreadyPlaced.xPos + alreadyPlaced.roomWidth)
				doesOverlap = true;
		}
		break;
	case Direction.West:
		//If the corridor exits the room on the right side
		if (toBePlaced.startXPos >= alreadyPlaced.xPos + alreadyPlaced.roomWidth && toBePlaced.EndPositionX <= alreadyPlaced.xPos + alreadyPlaced.roomWidth) 
		{
			if (toBePlaced.startYPos+toBePlaced.corridorWidth >= alreadyPlaced.yPos && toBePlaced.startYPos <= alreadyPlaced.yPos + alreadyPlaced.roomHeight)
				doesOverlap = true;
		}
		//If the corridor exits the room on the left side
		else if (toBePlaced.startXPos >= alreadyPlaced.xPos && toBePlaced.EndPositionX <= alreadyPlaced.xPos) 
		{
			if (toBePlaced.startYPos+toBePlaced.corridorWidth >= alreadyPlaced.yPos && toBePlaced.startYPos <= alreadyPlaced.yPos + alreadyPlaced.roomHeight)
				doesOverlap = true;
		}
		//If the corridor goes through the room completely
		else if (toBePlaced.startXPos >= alreadyPlaced.xPos + alreadyPlaced.roomWidth && toBePlaced.EndPositionX <= alreadyPlaced.xPos) 
		{
			if (toBePlaced.startYPos+toBePlaced.corridorWidth >= alreadyPlaced.yPos && toBePlaced.startYPos <= alreadyPlaced.yPos + alreadyPlaced.roomHeight)
				doesOverlap = true;
		}
		break;
	}


	return doesOverlap;
}
bool doCorridorsOverlapCorridor(Corridor alreadyPlaced, Corridor toBePlaced)
{
	//Need to check if X and Y of both corridors are the same
	//If so then they are overlapping
	bool doesOverlap = false;

	switch (toBePlaced.direction) 
	{
	case Direction.North:
		//If the X position of the corridor to be placed is within the range of the already placed corridor
		//EAST DIRECTION alreadyPlaced
		if (toBePlaced.startXPos >= alreadyPlaced.startXPos && toBePlaced.startXPos <= alreadyPlaced.EndPositionX) 
		{
			if (alreadyPlaced.startYPos >= toBePlaced.startYPos && alreadyPlaced.startYPos <= toBePlaced.EndPositionY)
				doesOverlap = true;				
		}
		//WEST DIRECTION alreadyPlaced
		else if (toBePlaced.startXPos <= alreadyPlaced.startXPos && toBePlaced.startXPos >= alreadyPlaced.EndPositionX) 
		{
			if (alreadyPlaced.startYPos >= toBePlaced.startYPos && alreadyPlaced.startYPos <= toBePlaced.EndPositionY)
				doesOverlap = true;	
		}
		break;
	case Direction.East:
		//NORTH DIRECTION alreadyPlaced
		if (toBePlaced.startYPos >= alreadyPlaced.startYPos && toBePlaced.startYPos <= alreadyPlaced.EndPositionY) 
		{
			if (alreadyPlaced.startXPos >= toBePlaced.startXPos && alreadyPlaced.startXPos <= toBePlaced.EndPositionX)
				doesOverlap = true;
		}
		//SOUTH DIRECTION alreadyPlaced
		else if (toBePlaced.startYPos <= alreadyPlaced.startYPos && toBePlaced.startYPos >= alreadyPlaced.EndPositionY) 
		{
			if (alreadyPlaced.startXPos >= toBePlaced.startXPos && alreadyPlaced.startXPos <= toBePlaced.EndPositionX)
				doesOverlap = true;				
		}
		break;
	case Direction.South:
		//EAST DIRECTION alreadyPlaced
		if (toBePlaced.startXPos >= alreadyPlaced.startXPos && toBePlaced.startXPos <= alreadyPlaced.EndPositionX) 
		{
			if (alreadyPlaced.startYPos <= toBePlaced.startYPos && alreadyPlaced.startYPos >= toBePlaced.EndPositionY)
				doesOverlap = true;			
		}
		//WEST DIRECTION alreadyPlaced
		else if (toBePlaced.startXPos <= alreadyPlaced.startXPos && toBePlaced.startXPos >= alreadyPlaced.EndPositionX) 
		{
			if (alreadyPlaced.startYPos <= toBePlaced.startYPos && alreadyPlaced.startYPos >= toBePlaced.EndPositionY)
				doesOverlap = true;
		}
		break;
	case Direction.West:
		//NORTH DIRECTION alreadyPlaced
		if (toBePlaced.startYPos >= alreadyPlaced.startYPos && toBePlaced.startYPos <= alreadyPlaced.EndPositionY) 
		{
			if (alreadyPlaced.startXPos >= toBePlaced.EndPositionX && alreadyPlaced.startXPos <= toBePlaced.startXPos)
				doesOverlap = true;
		}
		//SOUTH DIRECTION alreadyPlaced
		else if (toBePlaced.startYPos <= alreadyPlaced.startYPos && toBePlaced.startYPos >= alreadyPlaced.EndPositionY) 
		{
			if (alreadyPlaced.startXPos >= toBePlaced.EndPositionX && alreadyPlaced.startXPos <= toBePlaced.startXPos)
				doesOverlap = true;		
		}
		break;
	}

	return doesOverlap;
}

/*\\*/