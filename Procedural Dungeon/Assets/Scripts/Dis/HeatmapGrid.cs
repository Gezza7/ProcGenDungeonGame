using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HeatmapGrid : MonoBehaviour
{
    
    public List<GameObject> gridObjects;
    public List<float> gridValues;
    public Vector3 startPos;
    public GameObject blockPrefab;
    public Grid gridComponent;
    public GameObject player;
    private Vector3 lastPosition;
    private GameObject lastObject;
    public float maxValue = 1;
    public float scale = 1;

    //Dictionary<Vector3Int, CellData> grid;

    public struct CellData
    {
        public GameObject obj;
        public float value;
    }
    public Dictionary<Vector3Int, CellData> grid = new();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        /*
        if (checkPlayerMoved())
        {
            if(getCell() != null)
            {
                updateCell(lastObject);
            }
            else
            {
                createGridCell();
            }
        }
        else
        {
            updateCell(lastObject);
        }*/


        Vector3Int cellPos = gridComponent.WorldToCell(player.transform.position);

        if (checkPlayerMoved())
        {
            if (grid.ContainsKey(cellPos))
            {
                UpdateCell(cellPos);
            }
            else
            {
                createGridCell();
            }
        }
        else
        {
            if (grid.ContainsKey(cellPos))
                UpdateCell(cellPos);
        }

        lastPosition = player.transform.position;


    }

    public Vector3 getPos(int index)
   {
        int z = (index % 20) * 5;
        int x = (index / 20) * 5;
        Vector3 pos = new Vector3(startPos.x - x, startPos.y, startPos.z + z);
        return pos;
   }

    public void createHeatmap()
    {
        /*
        for (int i= 0;i<gridObjects.Count;i++)
        {
            //set colour depending on how much the player is gone over the cell
            float red = (gridValues[i] / (maxValue / scale));
            if (red >1)
            {
                gridObjects[i].GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
            }
            else
            {
                gridObjects[i].GetComponent<Renderer>().material.color = new Color(red, 0, 0, 1);
            }
            gridObjects[i].GetComponent<MeshRenderer>().enabled = true;
        }*/

        foreach (var kvp in grid)
        {
            GameObject go = kvp.Value.obj;
            float value = kvp.Value.value;

            float red = value / (maxValue / scale);
            red = Mathf.Clamp01(red);

            go.GetComponent<Renderer>().material.color = new Color(red, 0, 0, 1);
            go.GetComponent<MeshRenderer>().enabled = true;
        }

    }

    public void deleteHeatmap()
    {
        foreach (GameObject go in gridObjects)
        {
            gridObjects.Remove(go);
            Destroy(go);
        }
    }

    public bool checkPlayerMoved()
    {
        if(player.transform.position != lastPosition) 
            return true;
        else 
            return false; 
    }

    public bool TryGetCell(out CellData cell)
    {
        Vector3Int cellPos = gridComponent.WorldToCell(player.transform.position);
        return grid.TryGetValue(cellPos, out cell);
    }


    public GameObject getCell()
    {
        foreach (GameObject go in gridObjects)
        {
            //if there is a tile in the players position
            if (go.transform.position == gridComponent.GetCellCenterWorld(Vector3Int.FloorToInt(gridComponent.WorldToCell(player.transform.position))))
            {
                lastObject = go;
                return go;
            }
        }
        return null;
    }
    
    public void createGridCell()
    {
        /*
        GameObject go = Instantiate(blockPrefab, gridComponent.GetCellCenterWorld(Vector3Int.FloorToInt(gridComponent.WorldToCell(player.transform.position))), Quaternion.Euler(0, 0, 0));
        gridObjects.Add(go);
        gridValues.Add(1f);
        lastObject = go;
        */


        Vector3Int cellPos = gridComponent.WorldToCell(player.transform.position);
        Vector3 worldPos = gridComponent.GetCellCenterWorld(cellPos);

        //GameObject go = Instantiate(blockPrefab, worldPos, Quaternion.identity);

        grid[cellPos] = new CellData
        {
            obj = null, //go,
            value = 1f
        };


    }

    /*
    public void updateCell(GameObject go)
    {
        for(int i=0;i<gridObjects.Count;i++)
        {
            if (gridObjects[i] == go)
            {
                gridValues[i] += 1f;
                if (gridValues[i]>maxValue) maxValue = gridValues[i];
            }
        }
    }
    */

    public void UpdateCell(Vector3Int cellPos)
    {
        CellData data = grid[cellPos];
        data.value += 1f;

        if (data.value > maxValue)
            maxValue = data.value;

        grid[cellPos] = data;
    }


    public void gridTest()
    {
        //get mouse position
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Vector3 lastPosition = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100)) 
        {
            lastPosition = hit.point;
        }

        //find grid cell mouse is located in
        Vector3 gridPosition = gridComponent.WorldToCell(lastPosition);

        //transform the cell indicator to that cell
        Vector3 pos = gridComponent.GetCellCenterWorld(Vector3Int.FloorToInt(gridPosition));
        pos.y = 0.5f;
        blockPrefab.transform.position = pos;
        //cellIndicator.transform.position = grid.GetCellCenterWorld(Vector3Int.FloorToInt(gridPosition));
    
    }

   
}
