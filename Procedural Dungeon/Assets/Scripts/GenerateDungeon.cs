using System.Collections.Generic;
using UnityEngine;

public class GenerateDungeon : MonoBehaviour
{
    public int size = 10;
    public int floors = 1;
    public GameObject tripple;
    public GameObject[] rooms;
    public GameObject[] stairs;

    public GameObject[,,] dungeon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        dungeon = new GameObject[floors, size, size];
        makeDungeon();
    }

    public void makeDungeon()
    {
       
    }




}
