using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDungeon2 : MonoBehaviour
{
    public bool start;
    public GameObject[] paths = new GameObject[12];

    private GameObject[][] randomPath;
    private int currentX;
    private int currentY;
    private List<GameObject> possiblePaths = new List<GameObject>();
    private List<GameObject> possiblePaths2 = new List<GameObject>();

    public bool singleEntAndExt;
    private bool bottomLeft;
    private bool topRight = false;
    public int size;
    private int halfSize;
    public float pieceSize;
    private int[] order;
    public PathingCheck pc;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        randomPath = new GameObject[size][];
        int temp = Mathf.CeilToInt(size/2);
        if(temp %2 == 0)
        {
            size += 1;
            halfSize = Mathf.CeilToInt(size / 2) - 1;
            size -= 1;
        }
        else halfSize = Mathf.CeilToInt(size / 2) - 1;

        makeOrder();

        //initialise array
        for (int i = 0; i < size; i++)
        {
            randomPath[i] = new GameObject[size];
        }
        if(start)
        {
            BeginGeneration();
        }
        //pc.checkpath(randomPath, size, halfSize);

        
    }

    public void BeginGeneration()
    {
        //coroutine for showing the path being made
        //StartCoroutine(generatePieces());


        //virtical loop
        for (int i = 0; i < size; i++)
        {
            //horizontal loop
            for (int j = 0; j < size; j++)
            {
                randomFittingPiece(order[i], j);
            }
        }
        if (!pc.checkpath(randomPath, size, halfSize))
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Destroy(randomPath[i][j]);
                }
            }
            BeginGeneration();
        }

    }

    IEnumerator generatePieces()
    {
        //virtical loop
        for (int i = 0; i < size; i++)
        {
            //horizontal loop
            for (int j = 0; j < size; j++)
            {
                randomFittingPiece(order[i], j);
                yield return new WaitForSeconds(0.5f);
            }
        }
        if(!pc.checkpath(randomPath, size, halfSize))
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Destroy(randomPath[i][j]);
                }
            }
            BeginGeneration();
        }

        yield return new WaitForSeconds(0.5f);
    }

   public void makeOrder()
    {
        order = new int[size];
        order[0] = halfSize;
        int count = 1;
        for(int i =halfSize -1; i>=0;i--)
        {
            order[count] = i;
            count++;
        }
        for(int i = halfSize +1;i<size;i++)
        {
            order[count] = i;
            count++;
        }
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    void randomFittingPiece(int y, int x)
    {
        //clear lists
        possiblePaths.Clear();                                                                      //  -------CHANGE TO MAKE FIRST ROOM AT 0,0 -------
        possiblePaths2.Clear();                                                 //  -------THEN MAKE A STARTING ROOM TO PREVENT PLAYER FROM SEEING THE LEVEL LOAD-------
                                                                         // -------THIS IS MOSTLY DONE, YOU JUST NEED TO ADJUST THE PREFABS AND MAKE EVERYTHING CONNECT PROPERLY -------
        //add first piece
        if (y == halfSize && x == 0)
        {
            randomPath[halfSize][0] = Instantiate(paths[15], new Vector3(0, 0, 0), Quaternion.identity);
        }

        //make middle row first 
        else if (y == halfSize)
        {
            if (x == size - 1)
            {
                for (int i = 0; i < paths.Length; i++)
                {
                    //check piece is valid
                    if (paths[i].GetComponent<RoomConnections>().west == randomPath[y][x - 1].GetComponent<RoomConnections>().east && paths[i].GetComponent<RoomConnections>().east == false)
                    {
                        possiblePaths.Add(paths[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < paths.Length; i++)
                {
                    //check piece is valid
                    if (paths[i].GetComponent<RoomConnections>().west == randomPath[y][x - 1].GetComponent<RoomConnections>().east)
                    {
                        possiblePaths.Add(paths[i]);
                    }
                }
            }
            int rand = Random.Range(0, possiblePaths.Count);
            randomPath[y][x] = Instantiate(possiblePaths[rand], new Vector3(x * pieceSize, 0, 0), Quaternion.identity);
        }

        //rows below middle excluding bottom row
        else if (y < halfSize && y > 0)
        {
            //first wont check west
            if (x == 0)
            {
                for (int i = 0; i < paths.Length; i++)
                {
                    //check piece is valid
                    if (paths[i].GetComponent<RoomConnections>().north == randomPath[y + 1][x].GetComponent<RoomConnections>().south && paths[i].GetComponent<RoomConnections>().west == false)
                    {
                        possiblePaths.Add(paths[i]);
                    }
                    
                }
                int rand = Random.Range(0, possiblePaths.Count);
                randomPath[y][x] = Instantiate(possiblePaths[rand], new Vector3(x * pieceSize, 0, -(halfSize * pieceSize) + (y * pieceSize)), Quaternion.identity);
            }
            //all others check west and north
            else
            {
                if (x == size - 1)
                {
                    for (int i = 0; i < paths.Length; i++)
                    {
                        //check piece is valid
                        if (paths[i].GetComponent<RoomConnections>().west == randomPath[y][x - 1].GetComponent<RoomConnections>().east && paths[i].GetComponent<RoomConnections>().north == randomPath[y + 1][x].GetComponent<RoomConnections>().south && paths[i].GetComponent<RoomConnections>().east == false)
                        {
                            possiblePaths.Add(paths[i]);
                        }

                    }
                }
                else
                {

                    //chekc west and north
                    for (int i = 0; i < paths.Length; i++)
                    {
                        //check piece is valid
                        if (paths[i].GetComponent<RoomConnections>().west == randomPath[y][x - 1].GetComponent<RoomConnections>().east && paths[i].GetComponent<RoomConnections>().north == randomPath[y + 1][x].GetComponent<RoomConnections>().south)
                        {
                            possiblePaths.Add(paths[i]);
                        }

                    }
                }
                int rand = Random.Range(0, possiblePaths.Count);
                randomPath[y][x] = Instantiate(possiblePaths[rand], new Vector3(x * pieceSize, 0, -(halfSize * pieceSize) + (y * pieceSize)), Quaternion.identity);
            }
        }

        //bottom row
        else if (y < halfSize)
        {
            //check west and north and south must be false
            //first wont check west
            if (x == 0)
            {
                for (int i = 0; i < paths.Length; i++)
                {
                    //check piece is valid
                    if (paths[i].GetComponent<RoomConnections>().north == randomPath[y + 1][x].GetComponent<RoomConnections>().south && paths[i].GetComponent<RoomConnections>().south == false && paths[i].GetComponent<RoomConnections>().west == false)
                    {
                        possiblePaths.Add(paths[i]);
                    }
                    
                }
                int rand = Random.Range(0, possiblePaths.Count);
                randomPath[y][x] = Instantiate(possiblePaths[rand], new Vector3(x * pieceSize, 0, -(halfSize * pieceSize) + (y * pieceSize)), Quaternion.identity);
            }
            //all others check west and north
            else
            {
                if (x == size - 1)
                {
                    //chekc west and north
                    for (int i = 0; i < paths.Length; i++)
                    {
                        //check piece is valid
                        if (paths[i].GetComponent<RoomConnections>().west == randomPath[y][x - 1].GetComponent<RoomConnections>().east && paths[i].GetComponent<RoomConnections>().north == randomPath[y + 1][x].GetComponent<RoomConnections>().south && paths[i].GetComponent<RoomConnections>().south == false && paths[i].GetComponent<RoomConnections>().east == false)
                        {
                            possiblePaths.Add(paths[i]);
                        }

                    }
                }
                else
                {
                    //chekc west and north
                    for (int i = 0; i < paths.Length; i++)
                    {
                        //check piece is valid
                        if (paths[i].GetComponent<RoomConnections>().west == randomPath[y][x - 1].GetComponent<RoomConnections>().east && paths[i].GetComponent<RoomConnections>().north == randomPath[y + 1][x].GetComponent<RoomConnections>().south && paths[i].GetComponent<RoomConnections>().south == false)
                        {
                            possiblePaths.Add(paths[i]);
                        }

                    }
                }
                int rand = Random.Range(0, possiblePaths.Count);
                randomPath[y][x] = Instantiate(possiblePaths[rand], new Vector3(x * pieceSize, 0, -(halfSize * pieceSize) + (y * pieceSize)), Quaternion.identity);
            }
        }

        //rows above middle excluding top row
        else if (y > halfSize && y < size - 1)
        {
            //check west and south
            //first wont check west
            if (x == 0)
            {
                for (int i = 0; i < paths.Length; i++)
                {
                    //check piece is valid
                    if (paths[i].GetComponent<RoomConnections>().south == randomPath[y - 1][x].GetComponent<RoomConnections>().north && paths[i].GetComponent<RoomConnections>().west == false)
                    {
                        possiblePaths.Add(paths[i]);
                    }
                    
                }
                int rand = Random.Range(0, possiblePaths.Count);
                randomPath[y][x] = Instantiate(possiblePaths[rand], new Vector3(x * pieceSize, 0, -(halfSize * pieceSize) + (y * pieceSize)), Quaternion.identity);
            }
            //all others check west and north
            else
            {
                if(x == size-1)
                {
                    //chekc west and north
                    for (int i = 0; i < paths.Length; i++)
                    {
                        //check piece is valid
                        if (paths[i].GetComponent<RoomConnections>().west == randomPath[y][x - 1].GetComponent<RoomConnections>().east && paths[i].GetComponent<RoomConnections>().south == randomPath[y - 1][x].GetComponent<RoomConnections>().north && paths[i].GetComponent<RoomConnections>().east == false)
                        {
                            possiblePaths.Add(paths[i]);
                        }

                    }
                }
                else
                {
                    //chekc west and north
                    for (int i = 0; i < paths.Length; i++)
                    {
                        //check piece is valid
                        if (paths[i].GetComponent<RoomConnections>().west == randomPath[y][x - 1].GetComponent<RoomConnections>().east && paths[i].GetComponent<RoomConnections>().south == randomPath[y - 1][x].GetComponent<RoomConnections>().north)
                        {
                            possiblePaths.Add(paths[i]);
                        }

                    }
                }  
                int rand = Random.Range(0, possiblePaths.Count);
                randomPath[y][x] = Instantiate(possiblePaths[rand], new Vector3(x * pieceSize, 0, -(halfSize * pieceSize) + (y * pieceSize)), Quaternion.identity);
            }
        }

        //top row
        else if (y > halfSize)
        {
            //check west and south and north must be false
            //first wont check west
            if (x == 0)
            {
                for (int i = 0; i < paths.Length; i++)
                {
                    //check piece is valid
                    if (paths[i].GetComponent<RoomConnections>().south == randomPath[y - 1][x].GetComponent<RoomConnections>().north && paths[i].GetComponent<RoomConnections>().north == false && paths[i].GetComponent<RoomConnections>().west == false)
                    {
                        possiblePaths.Add(paths[i]);
                    }
                    
                }
                int rand = Random.Range(0, possiblePaths.Count);
                randomPath[y][x] = Instantiate(possiblePaths[rand], new Vector3(x * pieceSize, 0, -(halfSize * pieceSize) + (y * pieceSize)), Quaternion.identity);
            }
            //all others check west and north
            else
            {
                if (x == size - 1)
                {
                    //chekc west and north
                    for (int i = 0; i < paths.Length; i++)
                    {
                        //check piece is valid
                        if (paths[i].GetComponent<RoomConnections>().west == randomPath[y][x - 1].GetComponent<RoomConnections>().east && paths[i].GetComponent<RoomConnections>().south == randomPath[y - 1][x].GetComponent<RoomConnections>().north && paths[i].GetComponent<RoomConnections>().north == false && paths[i].GetComponent<RoomConnections>().east == false)
                        {
                            possiblePaths.Add(paths[i]);
                        }

                    }
                }
                else
                {
                    //chekc west and north
                    for (int i = 0; i < paths.Length; i++)
                    {
                        //check piece is valid
                        if (paths[i].GetComponent<RoomConnections>().west == randomPath[y][x - 1].GetComponent<RoomConnections>().east && paths[i].GetComponent<RoomConnections>().south == randomPath[y - 1][x].GetComponent<RoomConnections>().north && paths[i].GetComponent<RoomConnections>().north == false)
                        {
                            possiblePaths.Add(paths[i]);
                        }

                    }
                }
                int rand = Random.Range(0, possiblePaths.Count);
                randomPath[y][x] = Instantiate(possiblePaths[rand], new Vector3(x * pieceSize, 0, -(halfSize * pieceSize) + (y * pieceSize)), Quaternion.identity);
            }
        }
    }


}

//this is the first version with simple shapes 
//get this to make a dungeon but all connecting pieces are the same "size"

//next make a version where it makes a floor plan in tiles not through connecting doors
//then have the walls and celings made around them 
//with environmentals added after like tourches and decorations