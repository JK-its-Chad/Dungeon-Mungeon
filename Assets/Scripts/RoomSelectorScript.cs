using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelectorScript : MonoBehaviour
{

    public GameObject U, D, L, R,
                      UD, RL, UR, UL, DR, DL,
                      ULD, URL, DRU, DLR, UDLR,
                      KU, KD, KL, KR,
                      KUD, KRL, KUR, KUL, KDR, KDL,
                      KULD, KURL, KDRU, KDLR, KUDLR;

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

        if (roomSpawn == null)
        {
            print("HUGE PROBLEM");
        }

        Room newRoom = roomSpawn.GetComponent<Room>();

        newRoom.KeyAccess = room.KeyAccess;

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

    public GameObject PickKeyRoom(KeyRoom room)
    {
        if (room.NorthDoor)
        {
            if (room.SouthDoor)
            {
                if (room.EastDoor)
                {
                    if (room.WestDoor)
                    {
                        roomSpawn = KUDLR;
                    }
                    else
                    {
                        roomSpawn = KDRU;
                    }
                }
                else if (room.WestDoor)
                {
                    roomSpawn = KULD;
                }
                else
                {
                    roomSpawn = KUD;
                }
            }
            else
            {
                if (room.EastDoor)
                {
                    if (room.WestDoor)
                    {
                        roomSpawn = KURL;
                    }
                    else
                    {
                        roomSpawn = KUR;
                    }
                }
                else if (room.WestDoor)
                {
                    roomSpawn = KUL;
                }
                else
                {
                    roomSpawn = KU;
                }
            }
        }
        else if (room.SouthDoor)
        {
            if (room.EastDoor)
            {
                if (room.WestDoor)
                {
                    roomSpawn = KDLR;
                }
                else
                {
                    roomSpawn = KDR;
                }
            }
            else if (room.WestDoor)
            {
                roomSpawn = KDL;
            }
            else
            {
                roomSpawn = KD;
            }
        }
        else if (room.EastDoor)
        {
            if (room.WestDoor)
            {
                roomSpawn = KRL;
            }
            else
            {
                roomSpawn = KR;
            }
        }
        else if (room.WestDoor)
        {
            roomSpawn = KL;
        }

        if (roomSpawn == null)
        {
            print("HUGE PROBLEM");
        }

        KeyRoom newRoom = roomSpawn.GetComponent<KeyRoom>();

        newRoom.KeyAccess = room.KeyAccess;
        newRoom.KeyNumber = room.KeyNumber;

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
