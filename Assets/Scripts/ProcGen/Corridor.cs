using UnityEngine;

// Enum to specify the direction is heading.
public enum Direction
{
    North, East, South, West,
}

public class Corridor
{
    public int startXPos;         // The x coordinate for the start of the corridor.
    public int startYPos;         // The y coordinate for the start of the corridor.
    public int corridorLength;            // How many units long the corridor is.
    public int corridorWidth = 5;
    public Direction direction;   // Which direction the corridor is heading from it's room.


    // Get the end position of the corridor based on it's start position and which direction it's heading plus the corridor width if applicable.
    public int EndPositionX
    {
        get
        {
            if (direction == Direction.North || direction == Direction.South)
				return startXPos + corridorWidth;
            if (direction == Direction.East)
                return startXPos + corridorLength - 1;
            return startXPos - corridorLength + 1;
        }
    }


    public int EndPositionY
    {
        get
        {
            if (direction == Direction.East || direction == Direction.West)
				return startYPos + corridorWidth;
            if (direction == Direction.North)
                return startYPos + corridorLength - 1;
            return startYPos - corridorLength + 1;
        }
    }


    public void SetupCorridor(Room room, IntRange length, IntRange roomWidth, IntRange roomHeight, int columns, int rows, bool firstCorridor)
    {
        // Set a random direction (a random index from 0 to 3, cast to Direction).
        //First corridor cannot head West because it is in the West most room
		if (firstCorridor) 
		{
			direction = (Direction)Random.Range (0, 3);
		} 
		else 
		{
			direction = (Direction)Random.Range (0, 4);
		}

        // Find the direction opposite to the one entering the room this corridor is leaving from.
        // Cast the previous corridor's direction to an int between 0 and 3 and add 2 (a number between 2 and 5).
        // Find the remainder when dividing by 4 (if 2 then 2, if 3 then 3, if 4 then 0, if 5 then 1).
        // Cast this number back to a direction.
        // Overall effect is if the direction was South then that is 2, becomes 4, remainder is 0, which is north.
        Direction oppositeDirection = (Direction)(((int)room.enteringCorridor + 2) % 4);

        // If this is noth the first corridor and the randomly selected direction is opposite to the previous corridor's direction...
        if (!firstCorridor && direction == oppositeDirection)
        {
            // Rotate the direction 90 degrees clockwise (North becomes East, East becomes South, etc).
            // This is a more broken down version of the opposite direction operation above but instead of adding 2 we're adding 1.
            // This means instead of rotating 180 (the opposite direction) we're rotating 90.
            int directionInt = (int)direction;
            directionInt++;
            directionInt = directionInt % 4;
            direction = (Direction)directionInt;

        }

        // Set a random length.
        corridorLength = length.Random;

        // Create a cap for how long the length can be (this will be changed based on the direction and position).
        int maxLength = length.m_Max;

        switch (direction)
        {
            // If the choosen direction is North (up)...
            case Direction.North:
                // ... the starting position in the x axis can be random but within the width of the room.
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth - corridorWidth - 1);

                // The starting position in the y axis must be the top of the room.
                startYPos = room.yPos + room.roomHeight;

                // The maximum length the corridor can be is the height of the board (rows) but from the top of the room (y pos + height).
                maxLength = rows - startYPos - roomHeight.m_Min;
                break;
            case Direction.East:
                startXPos = room.xPos + room.roomWidth;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight - corridorWidth - 1);
                maxLength = columns - startXPos - roomWidth.m_Min;
                break;
            case Direction.South:
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth - corridorWidth);
                startYPos = room.yPos;
                maxLength = startYPos - roomHeight.m_Min;
                break;
            case Direction.West:
                startXPos = room.xPos;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight - corridorWidth);
                maxLength = startXPos - roomWidth.m_Min;
                break;
        }

        // We clamp the length of the corridor to make sure it doesn't go off the board.
        corridorLength = Mathf.Clamp(corridorLength, 1, maxLength);
    }

	public void SetUpAppendedCorridor(Corridor corridor, IntRange length, IntRange roomWidth, IntRange roomHeight, int columns, int rows, int xStart, int yStart)
    {	
		Direction oppositeDirection = (Direction)(((int)corridor.direction + 2) % 4);

		// If this is noth the first corridor and the randomly selected direction is opposite to the previous corridor's direction...
		if (direction == oppositeDirection)
		{
			// Rotate the direction 90 degrees clockwise (North becomes East, East becomes South, etc).
			// This is a more broken down version of the opposite direction operation above but instead of adding 2 we're adding 1.
			// This means instead of rotating 180 (the opposite direction) we're rotating 90.
			int directionInt = (int)direction;
			directionInt++;
			directionInt = directionInt % 4;
			direction = (Direction)directionInt;

		}

		// Set a random length.
		corridorLength = length.Random;

		// Create a cap for how long the length can be (this will be changed based on the direction and position).
		int maxLength = length.m_Max;

		switch (direction)
		{
			case Direction.North:
				startXPos = xStart - corridorWidth;
				startYPos = yStart - corridorWidth + 1;
				maxLength = rows - startYPos - roomHeight.m_Min;
				//maxLength = rows - startYPos;
				break;
			case Direction.East:

				if (oppositeDirection == Direction.North || oppositeDirection == Direction.South)
					startXPos = xStart - corridorWidth + 1;	
				else
					startXPos = xStart + 1;

				startYPos = yStart - corridorWidth;
				maxLength = columns - startXPos - roomWidth.m_Min;
				//maxLength = columns - startXPos;
				break;
		case Direction.South:
				startXPos = xStart - corridorWidth;
				startYPos = yStart;
				maxLength = startYPos - roomHeight.m_Min;
				//maxLength = startYPos;
				break;
			case Direction.West:
			
				if (corridor.direction == Direction.North || corridor.direction == Direction.South)
					startXPos = xStart;	
				else
					startXPos = xStart + 1;

				startYPos = yStart - corridorWidth;
				maxLength = startXPos - roomWidth.m_Min;
				//maxLength = columns - startXPos;
				break;
		}

		// Set a random length, ensure the length is greater than corridor width
		// for proper corridor appending
		do
		{
			corridorLength = length.Random;
		}
		while(corridorLength <= corridorWidth + 1);

		// We clamp the length of the corridor to make sure it doesn't go off the board.
		corridorLength = Mathf.Clamp(corridorLength, 1, maxLength);
	}


	public void SetupDeadEndCorridor(Corridor corridor, IntRange length, IntRange roomWidth, IntRange roomHeight, int columns, int rows, int xStart, int yStart)
	{
		startXPos = xStart;
		startYPos = yStart;

		// Set a random length.
		corridorLength = length.Random;
		// Create a cap for how long the length can be (this will be changed based on the direction and position).
		int maxLength = length.m_Max;

		//Determine which way the dead end corridors goes with respect to the original corridor
		switch(corridor.direction)
		{
		//If non-dead end corridor is generated going north
			case Direction.North:
				//set dead end corridor to go east
				direction = Direction.East;
				break;
			case Direction.East:
				direction = Direction.South;
				break;
			case Direction.South:
				direction = Direction.West;
				break;
			case Direction.West:
				direction = Direction.North;
				break;
		}

		//Set the values for the dead end corridors to go in their respective directions.
		switch (direction)
		{
			//If dead end corridor is going in the North Direction
			case Direction.North:
				//Then set the values
				startXPos = Random.Range(corridor.startXPos-6, corridor.startXPos-corridor.corridorLength+6);
				startYPos = corridor.startYPos + corridor.corridorWidth; 
				maxLength = rows - startYPos - roomHeight.m_Min;
				break;
			case Direction.East:
				startXPos = corridor.startXPos + corridor.corridorWidth;
				startYPos = Random.Range(corridor.startYPos+8, corridor.startYPos+ corridor.corridorWidth-8);
				maxLength = columns - startXPos - roomWidth.m_Min;
				break;
			case Direction.South:
				startXPos = Random.Range (corridor.startXPos+8, corridor.startXPos + corridor.corridorLength-8);
				startYPos = corridor.startYPos;
				maxLength = startYPos - roomHeight.m_Min;
				break;
			case Direction.West:
				startXPos = corridor.startXPos;
				startYPos = Random.Range (corridor.startYPos-corridor.corridorLength+8, corridor.startYPos-8);
				maxLength = startXPos - roomWidth.m_Min;
				break;
		}
		// We clamp the length of the corridor to make sure it doesn't go off the board.
		corridorLength = Mathf.Clamp(corridorLength, 1, maxLength);
	}
}
