using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class PathingCheck : MonoBehaviour
{
    public GameObject[][] ogPath;
    public RoomConnections[][] path;
    public GameObject[][] keepList;
    public GameObject[][] deleteList;
    public int size;


    public bool checkpath(GameObject[][] checkPath, int size, int halfSize)
    {
        Debug.Log("Made it here");
        ogPath = checkPath;
        path = new RoomConnections[size][];
        keepList = new GameObject[size][];
        deleteList = new GameObject[size][];

        for (int i =0;i<size; i++)
        {
            path[i] = new RoomConnections[size];
            keepList[i] = new GameObject[size];
            deleteList[i] = new GameObject[size];
        }
        Debug.Log("Made it here 2");
        this.size = size;

        for (int i =0;i<size; i++)
        {
            for(int j=0; j<size;j++)
            {
                path[i][j] = ogPath[i][j].GetComponent<RoomConnections>();
            }
        }
        Debug.Log("Made it here 3");
        //check first
        checkRoom(halfSize, 0);
        Debug.Log("Made it here 4");
        int count = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (keepList[i][j] == null)
                {
                    Debug.Log("Delete Path");
                    
                    deleteList[i][j] = ogPath[i][j];
                    count++;
                }
            }
        }
        if(((size*size) -count) <(size*2))
        {
            return false;
        }
        else
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (keepList[i][j] == null)
                    {
                        Debug.Log("Delete Path");

                        Destroy(deleteList[i][j]);
                    }
                }
            }
            return true;
        }
    }

    IEnumerator DeletePaths()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (keepList[i][j] == null)
                {
                    Debug.Log("Delete Path");
                    yield return new WaitForSeconds(0.1f);
                    Destroy(deleteList[i][j]);
                }
            }
        }
    }

    

    public void checkRoom(int y, int x)
    {
        // Bounds check
        if (y < 0 || y >= size || x < 0 || x >= size)
            return;

        // Already visited
        if (keepList[y][x] != null)
            return;

        // Mark as reachable
        keepList[y][x] = path[y][x].gameObject;

        // North
        if (path[y][x].north &&
            y + 1 < size &&
            path[y + 1][x].south)
        {
            checkRoom(y + 1, x);
        }

        // East
        if (path[y][x].east &&
            x + 1 < size &&
            path[y][x + 1].west)
        {
            checkRoom(y, x + 1);
        }

        // South
        if (path[y][x].south &&
            y - 1 >= 0 &&
            path[y - 1][x].north)
        {
            checkRoom(y - 1, x);
        }

        // West
        if (path[y][x].west &&
            x - 1 >= 0 &&
            path[y][x - 1].east)
        {
            checkRoom(y, x - 1);
        }
    }
}
