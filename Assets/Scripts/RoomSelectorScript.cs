using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelectorScript : MonoBehaviour
{

    public GameObject U, D, L, R,
                      UD, RL, UR, UL, DR, DL,
                      ULD, URL, DRU, DLR, UDLR;

    public GameObject bossU, bossL, bossR, bossD;
    public GameObject itemU, itemL, itemR, itemD;

    public int type; // 0 = normal, 1 = start

    public GameObject roomSpawn;

    public GameObject PickRoom(Room room)
    {
        if (room.NorthDoor)
        {
            if (room.SouthDoor)
            {
                if (room.EastDoor)
                {
                    if (room.WestDoor)
                    {
                        roomSpawn = UDLR;
                    }
                    else
                    {
                        roomSpawn = DRU;
                    }
                }
                else if (room.WestDoor)
                {
                    roomSpawn = ULD;
                }
                else
                {
                    roomSpawn = UD;
                }
            }
            else
            {
                if (room.EastDoor)
                {
                    if (room.WestDoor)
                    {
                        roomSpawn = URL;
                    }
                    else
                    {
                        roomSpawn = UR;
                    }
                }
                else if (room.WestDoor)
                {
                    roomSpawn = UL;
                }
                else
                {
                    roomSpawn = U;
                }
            }
        }
        else if (room.SouthDoor)
        {
            if (room.EastDoor)
            {
                if (room.WestDoor)
                {
                    roomSpawn = DLR;
                }
                else
                {
                    roomSpawn = DR;
                }
            }
            else if (room.WestDoor)
            {
                roomSpawn = DL;
            }
            else
            {
                roomSpawn = D;
            }
        }
        else if (room.EastDoor)
        {
            if (room.WestDoor)
            {
                roomSpawn = RL;
            }
            else
            {
                roomSpawn = R;
            }
        }
        else if (room.WestDoor)
        {
            roomSpawn = L;
        }

        if (type == 2)
        {
            if (room.NorthDoor)
            {
                roomSpawn = bossU;
            }
            if (room.SouthDoor)
            {
                roomSpawn = bossD;
            }
            if (room.WestDoor)
            {
                roomSpawn = bossL;
            }
            if (room.EastDoor)
            {
                roomSpawn = bossR;
            }
        }
        if (type == 3)
        {
            if (room.NorthDoor)
            {
                roomSpawn = itemU;
            }
            if (room.SouthDoor)
            {
                roomSpawn = itemD;
            }
            if (room.WestDoor)
            {
                roomSpawn = itemL;
            }
            if (room.EastDoor)
            {
                roomSpawn = itemR;
            }
        }

        if (roomSpawn == null)
        {
            print("HUGE PROBLEM");
        }

        Room newRoom = roomSpawn.GetComponent<Room>();

        newRoom.NorthDoor = room.NorthDoor;
        newRoom.SouthDoor = room.SouthDoor;
        newRoom.WestDoor = room.WestDoor;
        newRoom.EastDoor = room.EastDoor;

        newRoom.NorthRoom = room.NorthRoom;
        newRoom.SouthRoom = room.SouthRoom;
        newRoom.WestRoom = room.WestRoom;
        newRoom.EastRoom = room.EastRoom;

        return roomSpawn;
    }

}
