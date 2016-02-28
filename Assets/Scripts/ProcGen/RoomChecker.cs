using UnityEngine;
using System.Collections.Generic;

public class RoomChecker : MonoBehaviour {
    //ArrayList of all the Rooms on the Board.
    public List<Room> RoomList;
    public float bonusThreshhold;

	void Start () {
        RoomList = new List<Room>();
	}
	public void Add(Room r)
    {
        RoomList.Add(r);
    }
	public Room getRoomIn(Vector2 pos)
    {
        foreach(Room r in RoomList)
        {
            if (pos.x >= r.xPos-bonusThreshhold && pos.x <= r.xPos + r.roomWidth + bonusThreshhold && pos.y >= r.yPos -bonusThreshhold && pos.y <= r.yPos + r.roomHeight + bonusThreshhold)
            {
                return r;
            }
        }
        return null;
    }
}
