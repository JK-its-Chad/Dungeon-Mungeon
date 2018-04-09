using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelectorScript : MonoBehaviour {

    public GameObject U, D, L, R,
                      UD, RL, UR, UL, DR, DL,
                      ULD, URL, DRU, DLR, UDLR;

    public GameObject bossU, bossL, bossR, bossD;
    public GameObject itemU, itemL, itemR, itemD;

    public bool up, down, left, right;

    public int type; // 0 = normal, 1 = start

    public GameObject roomSpawn;
	
    public void PickRoom()
    {
        //if (type == 0 || type == 1)
        //{
            if (up)
            {
                if (down)
                {
                    if (right)
                    {
                        if (left)
                        {
                            roomSpawn = UDLR;
                        }
                        else
                        {
                            roomSpawn = DRU;
                        }
                    }
                    else if (left)
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
                    if (right)
                    {
                        if (left)
                        {
                            roomSpawn = URL;
                        }
                        else
                        {
                            roomSpawn = UR;
                        }
                    }
                    else if (left)
                    {
                        roomSpawn = UL;
                    }
                    else
                    {
                        roomSpawn = U;
                    }
                }
            }
            else if (down)
            {
                if (right)
                {
                    if (left)
                    {
                        roomSpawn = DLR;
                    }
                    else
                    {
                        roomSpawn = DR;
                    }
                }
                else if (left)
                {
                    roomSpawn = DL;
                }
                else
                {
                    roomSpawn = D;
                }
            }
            else if (right)
            {
                if (left)
                {
                    roomSpawn = RL;
                }
                else
                {
                    roomSpawn = R;
                }
            }
            else if (left)
            {
                roomSpawn = L;
            }
        //}

        if(type == 2)
        {
            if(up)
            {
                roomSpawn = bossU;
            }
            if(down)
            {
                roomSpawn = bossD;
            }
            if(left)
            {
                roomSpawn = bossL;
            }
            if(right)
            {
                roomSpawn = bossR;
            }
        }
        if (type == 3)
        {
            if (up)
            {
                roomSpawn = itemU;
            }
            if (down)
            {
                roomSpawn = itemD;
            }
            if (left)
            {
                roomSpawn = itemL;
            }
            if (right)
            {
                roomSpawn = itemR;
            }
        }

        if (roomSpawn == null)
        {
            print("HUGE PROBLEM");
        }
        
    }
    
}
