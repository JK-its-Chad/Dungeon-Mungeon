using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int KeyRooms = 3;
    public int ExtraRooms = 5;

    private int roomsize = 51;

    public GameObject Room;
    public GameObject KeyRoom;
    public GameObject ExtraRoom;
    public GameObject Door, FinalDoor;
    public int Seed = 0;

    private GameObject[] KeyPath;
    public bool Draw = true;

    private void Start()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        KeyPath = new GameObject[100];
        PlaceKeyRooms();
        PlaceExtraRooms();
        CreateRooms();
        CreateDoors();

        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
    }

    private void PlaceKeyRooms()
    {
        GameObject LastRoom = GameObject.Find("Spawn");
        bool successful;
        int count = 0;

        for (int i = 0; i < KeyRooms && count < 500;)
        {
            count++;
            if (count > 495) Debug.Log("Break Infinite Loop -Key");
            successful = true;
            int X = Random.Range(0, 5);
            int Z = Random.Range(0, 5);
            int Direction = Random.Range(1, 5);
            Vector3 KeyVector = new Vector3(0, 1000, 0);

            if (Direction == 1)
            {
                KeyVector = new Vector3(X * roomsize, 0, Z * roomsize);
            }
            else if (Direction == 2)
            {
                KeyVector = new Vector3(-X * roomsize, 0, Z * roomsize);
            }
            else if (Direction == 3)
            {
                KeyVector = new Vector3(-X * roomsize, 0, -Z * roomsize);
            }
            else if (Direction == 4)
            {
                KeyVector = new Vector3(X * roomsize, 0, -Z * roomsize);
            }

            GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");

            foreach (GameObject room in rooms)
            {
                if (room.transform.position.x == KeyVector.x && room.transform.position.z == KeyVector.z)
                {
                    successful = false;
                    break;
                }
                else if (room.transform.position.x == (KeyVector.x + roomsize) && room.transform.position.z == KeyVector.z)
                {
                    successful = false;
                    break;
                }
                else if (room.transform.position.x == KeyVector.x && room.transform.position.z == (KeyVector.z + roomsize))
                {
                    successful = false;
                    break;
                }
                else if (room.transform.position.x == (KeyVector.x - roomsize) && room.transform.position.z == KeyVector.z)
                {
                    successful = false;
                    break;
                }
                else if (room.transform.position.x == KeyVector.x && room.transform.position.z == (KeyVector.z - roomsize))
                {
                    successful = false;
                    break;
                }
            }
            if (successful)
            {
                KeyPath[i] = Instantiate(KeyRoom, KeyVector, Quaternion.identity);
                KeyPath[i].GetComponent<KeyRoom>().KeyNumber = i + 1;
                KeyPath[i].GetComponent<Room>().KeyAccess = i;
                LinkRooms(LastRoom, KeyPath[i], KeyPath[i].GetComponent<Room>().KeyAccess);
                LastRoom = KeyPath[i];
                i++;
            }

        }
    }

    private void PlaceExtraRooms()
    {
        Draw = false;
        bool successful;
        int count = 0;

        for (int i = 0; i < ExtraRooms && count < 500;)
        {
            count++;
            if (count > 495) Debug.Log("Break Infinite Loop - Extra");
            successful = true;
            int X = Random.Range(0, 5);
            int Z = Random.Range(0, 5);
            int Direction = Random.Range(1, 5);
            Vector3 KeyVector = new Vector3(0, 1000, 0);

            if (Direction == 1)
            {
                KeyVector = new Vector3(X * roomsize, 0, Z * roomsize);
            }
            else if (Direction == 2)
            {
                KeyVector = new Vector3(-X * roomsize, 0, Z * roomsize);
            }
            else if (Direction == 3)
            {
                KeyVector = new Vector3(-X * roomsize, 0, -Z * roomsize);
            }
            else if (Direction == 4)
            {
                KeyVector = new Vector3(X * roomsize, 0, -Z * roomsize);
            }

            GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");

            foreach (GameObject room in rooms)
            {
                if (room.transform.position.x == KeyVector.x && room.transform.position.z == KeyVector.z)
                {
                    successful = false;
                    break;
                }
            }
            if (successful)
            {
                GameObject LastRoom = gameObject;
                List<GameObject> ClosestRooms = new List<GameObject>();

                foreach (GameObject room in rooms)
                {
                    if (getDistance(room.transform.position, KeyVector) < getDistance(LastRoom.transform.position, KeyVector))
                    {
                        LastRoom = room;
                        ClosestRooms = new List<GameObject>();
                        ClosestRooms.Add(room);
                        i = 0;
                    }
                    else if (getDistance(room.transform.position, KeyVector) == getDistance(LastRoom.transform.position, KeyVector))
                    {
                        i++;
                        ClosestRooms.Add(room);
                    }
                }

                GameObject NewRoom = Instantiate(ExtraRoom, KeyVector, Quaternion.identity);
                NewRoom.GetComponent<Room>().KeyAccess = Random.Range(0, KeyRooms);
                LinkRooms(LastRoom, NewRoom, NewRoom.GetComponent<Room>().KeyAccess);
                i++;
            }

        }
    }

    private void LinkRooms(GameObject FirstRoom, GameObject EndRoom, int KeyPermission)
    {
        GameObject currentGO = FirstRoom;

        int i = 0;
        GameObject[] path = new GameObject[30];

        path[0] = currentGO;

        int count = getDistance(currentGO.transform.position, EndRoom.transform.position);
        for (int tiles = count; tiles > 0; tiles--, i++)
        {
            if (currentGO.transform.position.x < EndRoom.transform.position.x && currentGO.transform.position.z < EndRoom.transform.position.z)
            {
                int UpOrRight = Random.Range(0, 2);
                if (UpOrRight == 1)
                {
                    currentGO.GetComponent<Room>().NorthRoom = RequestSpawn(Room, new Vector3(currentGO.transform.position.x, 0, currentGO.transform.position.z + roomsize), Quaternion.identity, KeyPermission);
                    currentGO.GetComponent<Room>().NorthRoom.GetComponent<Room>().SouthRoom = currentGO;
                    currentGO = currentGO.GetComponent<Room>().NorthRoom;
                    AddDoor(currentGO, currentGO.GetComponent<Room>().SouthRoom);
                }
                else
                {
                    currentGO.GetComponent<Room>().EastRoom = RequestSpawn(Room, new Vector3(currentGO.transform.position.x + roomsize, 0, currentGO.transform.position.z), Quaternion.identity, KeyPermission);
                    currentGO.GetComponent<Room>().EastRoom.GetComponent<Room>().WestRoom = currentGO;
                    currentGO = currentGO.GetComponent<Room>().EastRoom;
                    AddDoor(currentGO, currentGO.GetComponent<Room>().WestRoom);
                }
            }
            else if (currentGO.transform.position.x > EndRoom.transform.position.x && currentGO.transform.position.z < EndRoom.transform.position.z)
            {
                int UpOrLeft = Random.Range(0, 2);
                if (UpOrLeft == 1)
                {
                    currentGO.GetComponent<Room>().NorthRoom = RequestSpawn(Room, new Vector3(currentGO.transform.position.x, 0, currentGO.transform.position.z + roomsize), Quaternion.identity, KeyPermission);
                    currentGO.GetComponent<Room>().NorthRoom.GetComponent<Room>().SouthRoom = currentGO;
                    currentGO = currentGO.GetComponent<Room>().NorthRoom;
                    AddDoor(currentGO, currentGO.GetComponent<Room>().SouthRoom);
                }
                else
                {
                    currentGO.GetComponent<Room>().WestRoom = RequestSpawn(Room, new Vector3(currentGO.transform.position.x - roomsize, 0, currentGO.transform.position.z), Quaternion.identity, KeyPermission);
                    currentGO.GetComponent<Room>().WestRoom.GetComponent<Room>().EastRoom = currentGO;
                    currentGO = currentGO.GetComponent<Room>().WestRoom;
                    AddDoor(currentGO, currentGO.GetComponent<Room>().EastRoom);
                }
            }
            else if (currentGO.transform.position.x > EndRoom.transform.position.x && currentGO.transform.position.z > EndRoom.transform.position.z)
            {
                int DownOrLeft = Random.Range(0, 2);
                if (DownOrLeft == 1)
                {
                    currentGO.GetComponent<Room>().SouthRoom = RequestSpawn(Room, new Vector3(currentGO.transform.position.x, 0, currentGO.transform.position.z - roomsize), Quaternion.identity, KeyPermission);
                    currentGO.GetComponent<Room>().SouthRoom.GetComponent<Room>().NorthRoom = currentGO;
                    currentGO = currentGO.GetComponent<Room>().SouthRoom;
                    AddDoor(currentGO, currentGO.GetComponent<Room>().NorthRoom);
                }
                else
                {
                    currentGO.GetComponent<Room>().WestRoom = RequestSpawn(Room, new Vector3(currentGO.transform.position.x - roomsize, 0, currentGO.transform.position.z), Quaternion.identity, KeyPermission);
                    currentGO.GetComponent<Room>().WestRoom.GetComponent<Room>().EastRoom = currentGO;
                    currentGO = currentGO.GetComponent<Room>().WestRoom;
                    AddDoor(currentGO, currentGO.GetComponent<Room>().EastRoom);
                }
            }
            else if (currentGO.transform.position.x < EndRoom.transform.position.x && currentGO.transform.position.z > EndRoom.transform.position.z)
            {
                int DownOrRight = Random.Range(0, 2);
                if (DownOrRight == 1)
                {
                    currentGO.GetComponent<Room>().SouthRoom = RequestSpawn(Room, new Vector3(currentGO.transform.position.x, 0, currentGO.transform.position.z - roomsize), Quaternion.identity, KeyPermission);
                    currentGO.GetComponent<Room>().SouthRoom.GetComponent<Room>().NorthRoom = currentGO;
                    currentGO = currentGO.GetComponent<Room>().SouthRoom;
                    AddDoor(currentGO, currentGO.GetComponent<Room>().NorthRoom);
                }
                else
                {
                    currentGO.GetComponent<Room>().EastRoom = RequestSpawn(Room, new Vector3(currentGO.transform.position.x + roomsize, 0, currentGO.transform.position.z), Quaternion.identity, KeyPermission);
                    currentGO.GetComponent<Room>().EastRoom.GetComponent<Room>().WestRoom = currentGO;
                    currentGO = currentGO.GetComponent<Room>().EastRoom;
                    AddDoor(currentGO, currentGO.GetComponent<Room>().WestRoom);
                }
            }
            else if (currentGO.transform.position.x == EndRoom.transform.position.x && currentGO.transform.position.z < EndRoom.transform.position.z)
            {
                currentGO.GetComponent<Room>().NorthRoom = RequestSpawn(Room, new Vector3(currentGO.transform.position.x, 0, currentGO.transform.position.z + roomsize), Quaternion.identity, KeyPermission);
                currentGO.GetComponent<Room>().NorthRoom.GetComponent<Room>().SouthRoom = currentGO;
                currentGO = currentGO.GetComponent<Room>().NorthRoom;
                AddDoor(currentGO, currentGO.GetComponent<Room>().SouthRoom);
            }
            else if (currentGO.transform.position.x > EndRoom.transform.position.x && currentGO.transform.position.z == EndRoom.transform.position.z)
            {
                currentGO.GetComponent<Room>().WestRoom = RequestSpawn(Room, new Vector3(currentGO.transform.position.x - roomsize, 0, currentGO.transform.position.z), Quaternion.identity, KeyPermission);
                currentGO.GetComponent<Room>().WestRoom.GetComponent<Room>().EastRoom = currentGO;
                currentGO = currentGO.GetComponent<Room>().WestRoom;
                AddDoor(currentGO, currentGO.GetComponent<Room>().EastRoom);
            }
            else if (currentGO.transform.position.x == EndRoom.transform.position.x && currentGO.transform.position.z > EndRoom.transform.position.z)
            {
                currentGO.GetComponent<Room>().SouthRoom = RequestSpawn(Room, new Vector3(currentGO.transform.position.x, 0, currentGO.transform.position.z - roomsize), Quaternion.identity, KeyPermission);
                currentGO.GetComponent<Room>().SouthRoom.GetComponent<Room>().NorthRoom = currentGO;
                currentGO = currentGO.GetComponent<Room>().SouthRoom;
                AddDoor(currentGO, currentGO.GetComponent<Room>().NorthRoom);
            }
            else if (currentGO.transform.position.x < EndRoom.transform.position.x && currentGO.transform.position.z == EndRoom.transform.position.z)
            {
                currentGO.GetComponent<Room>().EastRoom = RequestSpawn(Room, new Vector3(currentGO.transform.position.x + roomsize, 0, currentGO.transform.position.z), Quaternion.identity, KeyPermission);
                currentGO.GetComponent<Room>().EastRoom.GetComponent<Room>().WestRoom = currentGO;
                currentGO = currentGO.GetComponent<Room>().EastRoom;
                AddDoor(currentGO, currentGO.GetComponent<Room>().WestRoom);
            }
            else
            {
                currentGO = EndRoom;
            }
            path[i + 1] = currentGO;
        }
        if (Draw) drawPath(path);
    }

    private int getDistance(Vector3 Position, Vector3 Target)
    {
        int count = 0;
        while (Position.x != Target.x || Position.z != Target.z)
        {
            if (Position.x < Target.x)
            {
                Position.x += roomsize;
                count++;
            }
            else if (Position.x > Target.x)
            {
                Position.x -= roomsize;
                count++;
            }
            else if (Position.z < Target.z)
            {
                Position.z += roomsize;
                count++;
            }
            else if (Position.z > Target.z)
            {
                Position.z -= roomsize;
                count++;
            }
        }

        return count;
    }

    private GameObject AddDoor(GameObject FirstRoom, GameObject SecondRoom)
    {
        Vector3 DoorVector = new Vector3(((FirstRoom.transform.position.x + SecondRoom.transform.position.x) / 2), 0, ((FirstRoom.transform.position.z + SecondRoom.transform.position.z) / 2));
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

        foreach (GameObject door in doors)
        {
            if (door.transform.position.x == DoorVector.x && door.transform.position.z == DoorVector.z)
            {
                if (FirstRoom.transform.position.x < SecondRoom.transform.position.x)
                {
                    FirstRoom.GetComponent<Room>().EastDoor = door;
                    SecondRoom.GetComponent<Room>().WestDoor = door;
                }
                else if (FirstRoom.transform.position.x > SecondRoom.transform.position.x)
                {
                    FirstRoom.GetComponent<Room>().WestDoor = door;
                    SecondRoom.GetComponent<Room>().EastDoor = door;
                }
                else if (FirstRoom.transform.position.z < SecondRoom.transform.position.z)
                {
                    FirstRoom.GetComponent<Room>().NorthDoor = door;
                    SecondRoom.GetComponent<Room>().SouthDoor = door;
                }
                else if (FirstRoom.transform.position.z > SecondRoom.transform.position.z)
                {
                    FirstRoom.GetComponent<Room>().SouthDoor = door;
                    SecondRoom.GetComponent<Room>().NorthDoor = door;
                }
                return door;
            }
        }

        GameObject newDoor = Instantiate(Door, DoorVector, Quaternion.identity);

        newDoor.GetComponent<Door>().FirstRoom = FirstRoom;
        newDoor.GetComponent<Door>().SecondRoom = SecondRoom;

        if (FirstRoom.transform.position.x < SecondRoom.transform.position.x)
        {
            FirstRoom.GetComponent<Room>().EastDoor = newDoor;
            SecondRoom.GetComponent<Room>().WestDoor = newDoor;
        }
        else if (FirstRoom.transform.position.x > SecondRoom.transform.position.x)
        {
            FirstRoom.GetComponent<Room>().WestDoor = newDoor;
            SecondRoom.GetComponent<Room>().EastDoor = newDoor;
        }
        else if (FirstRoom.transform.position.z < SecondRoom.transform.position.z)
        {
            FirstRoom.GetComponent<Room>().NorthDoor = newDoor;
            SecondRoom.GetComponent<Room>().SouthDoor = newDoor;
        }
        else if (FirstRoom.transform.position.z > SecondRoom.transform.position.z)
        {
            FirstRoom.GetComponent<Room>().SouthDoor = newDoor;
            SecondRoom.GetComponent<Room>().NorthDoor = newDoor;
        }

        FirstRoom.GetComponent<Room>();
        if (FirstRoom.GetComponent<Room>().KeyAccess > SecondRoom.GetComponent<Room>().KeyAccess) newDoor.GetComponent<Door>().KeyAccess = FirstRoom.GetComponent<Room>().KeyAccess;
        else newDoor.GetComponent<Door>().KeyAccess = SecondRoom.GetComponent<Room>().KeyAccess;

        newDoor.GetComponent<Door>().setColor();

        return newDoor;
    }

    public GameObject RequestSpawn(GameObject SObject, Vector3 Position, Quaternion Rotation, int SpawnNumber)
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        int count = 1;

        foreach (GameObject room in rooms)
        {
            if (room.transform.position.x == Position.x && room.transform.position.z == Position.z)
            {
                return room;
            }
            count++;
        }
        SObject.name = "Room " + count;
        SObject.GetComponent<Room>().KeyAccess = SpawnNumber;
        return Instantiate(SObject, Position, Rotation);
    }

    private void drawPath(GameObject[] Path)
    {
        GameObject gObject = new GameObject("MyGameObject");
        LineRenderer lRend = gObject.AddComponent<LineRenderer>();
        GameObject LastPoint = gameObject;

        lRend.startColor = Color.red;
        lRend.endColor = Color.blue;
        lRend.material = new Material(Shader.Find("Particles/Additive"));
        lRend.startWidth = 1;
        lRend.endWidth = 1;
        lRend.positionCount = Path.Length + 1;
        lRend.SetPosition(0, new Vector3(Path[0].transform.position.x, 2, Path[0].transform.position.z));

        for (int i = 0; i < Path.Length; i++)
        {
            if (Path[i] == null) lRend.SetPosition(i + 1, new Vector3(LastPoint.transform.position.x, 2, LastPoint.transform.position.z));
            else
            {
                lRend.SetPosition(i + 1, new Vector3(Path[i].transform.position.x, 2, Path[i].transform.position.z));
                LastPoint = Path[i];
            }
        }
    }

    private void CreateRooms()
    {
        GameObject[] Rooms = GameObject.FindGameObjectsWithTag("Room");
        RoomSelectorScript RoomChanger = GameObject.Find("RoomSelector").GetComponent<RoomSelectorScript>();
        foreach (GameObject room in Rooms)
        {
            GameObject SpawnedRoom;
            if (room.GetComponent<KeyRoom>())
            {
                SpawnedRoom  = Instantiate(RoomChanger.PickKeyRoom(room.GetComponent<KeyRoom>()),
                     room.transform.position,
                     Quaternion.identity);
            }
            else
            {
                SpawnedRoom = Instantiate(RoomChanger.PickRoom(room.GetComponent<Room>()),
                     room.transform.position,
                     Quaternion.identity);
            }
            Room oldRoom = room.GetComponent<Room>();
            Room newRoom = SpawnedRoom.GetComponent<Room>();

            newRoom.NorthDoor = oldRoom.NorthDoor;
            newRoom.SouthDoor = oldRoom.SouthDoor;
            newRoom.WestDoor = oldRoom.WestDoor;
            newRoom.EastDoor = oldRoom.EastDoor;

            newRoom.NorthRoom = oldRoom.NorthRoom;
            newRoom.SouthRoom = oldRoom.SouthRoom;
            newRoom.WestRoom = oldRoom.WestRoom;
            newRoom.EastRoom = oldRoom.EastRoom;

            Destroy(room);
        }
        Rooms = GameObject.FindGameObjectsWithTag("Final Room");
        foreach (GameObject room in Rooms)
        {
            Room oldRoom = room.GetComponent<Room>();
            if (oldRoom.NorthRoom)
            {
                foreach (GameObject otherroom in Rooms)
                {
                    if (otherroom.transform.position.z == room.transform.position.z + 51 && otherroom.transform.position.x == room.transform.position.x)
                    {
                        oldRoom.NorthRoom = otherroom;
                    }
                }
            }
            if (oldRoom.SouthRoom)
            {
                foreach (GameObject otherroom in Rooms)
                {
                    if (otherroom.transform.position.z == room.transform.position.z - 51 && otherroom.transform.position.x == room.transform.position.x)
                    {
                        oldRoom.SouthRoom = otherroom;
                        break;
                    }
                }
            }
            if (oldRoom.EastRoom)
            {
                foreach (GameObject otherroom in Rooms)
                {
                    if (otherroom.transform.position.x == room.transform.position.x + 51 && otherroom.transform.position.z == room.transform.position.z)
                    {
                        oldRoom.EastRoom = otherroom;
                    }
                }
            }
            if (oldRoom.WestRoom)
            {
                foreach (GameObject otherroom in Rooms)
                {
                    if (otherroom.transform.position.x == room.transform.position.x - 51 && otherroom.transform.position.z == room.transform.position.z)
                    {
                        oldRoom.WestRoom = otherroom;
                    }
                }
            }
        }
        foreach (GameObject room in Rooms)
        {
            Room oldRoom = room.GetComponent<Room>();
            if (oldRoom.NorthDoor)
            {
                oldRoom.NorthDoor.GetComponent<Door>().FirstRoom = room;
            }
            if (oldRoom.SouthDoor)
            {
                oldRoom.SouthDoor.GetComponent<Door>().SecondRoom = room;
            }
            if (oldRoom.EastDoor)
            {
                oldRoom.EastDoor.GetComponent<Door>().FirstRoom = room;
            }
            if (oldRoom.WestDoor)
            {
                oldRoom.WestDoor.GetComponent<Door>().SecondRoom = room;
            }
        }
    }

    private void CreateDoors()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        foreach(GameObject door in doors)
        {
            if(door.GetComponent<Door>().SecondRoom.transform.position.z > door.GetComponent<Door>().FirstRoom.transform.position.z)
            {
                GameObject newDoor = Instantiate(FinalDoor, door.transform.position, Quaternion.identity);
                newDoor.GetComponent<Door>().KeyAccess = door.GetComponent<Door>().KeyAccess;
                newDoor.GetComponent<Door>().FirstRoom = door.GetComponent<Door>().FirstRoom;
                newDoor.GetComponent<Door>().SecondRoom = door.GetComponent<Door>().SecondRoom;
            }
            if (door.GetComponent<Door>().FirstRoom.transform.position.x < door.GetComponent<Door>().SecondRoom.transform.position.x)
            {
                GameObject newDoor = Instantiate(FinalDoor, door.transform.position, Quaternion.Euler(0, 90, 0));
                newDoor.GetComponent<Door>().KeyAccess = door.GetComponent<Door>().KeyAccess;
                newDoor.GetComponent<Door>().FirstRoom = door.GetComponent<Door>().FirstRoom;
                newDoor.GetComponent<Door>().SecondRoom = door.GetComponent<Door>().SecondRoom;
            }
        }
    }

    private GameObject findRoom(Vector3 OldRoom)
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        foreach (GameObject room in rooms)
        {
            if (room.transform.position == OldRoom) return room;
        }
        return null;
    }
}
