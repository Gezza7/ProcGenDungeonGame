using UnityEngine;

public class RoomDecorate : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int length;
    public GameObject[] gameObjects;
    [SerializeField] Transform centerTrans;
    Vector3 center;
    public bool toggle;

    private void Start()
    {
        center = centerTrans.position;
    }

    private void Update()
    {
        if(toggle)
        {
            SetUpDecorations();
        }
    }
    public void SetUpDecorations()
    {
        toggle = false;
        int objectIndex = Random.Range(0, gameObjects.Length-1);
        bool directionCheck = true;
        int direction =0;
        while (directionCheck)
        {
            direction = Random.Range(0, 3);
            switch (direction)
            {
                case 0:
                    if(!this.GetComponent<RoomConnections>().south)
                    {
                        directionCheck = false;
                    }
                    break;
                case 1:
                    if (!this.GetComponent<RoomConnections>().west)
                    {
                        directionCheck = false;
                    }
                    break;
                case 2:
                    if (!this.GetComponent<RoomConnections>().north)
                    {
                        directionCheck = false;
                    }
                    break;
                case 3:
                    if (!this.GetComponent<RoomConnections>().east)
                    {
                        directionCheck = false;
                    }
                    break;
                default:
                    Debug.Log("Error: invalid direction");
                    break;
            }
        }
        

        switch(direction)
        {
            case 0:
                int x = Random.Range(-width, width);
                Vector3 pos = new Vector3(center.x +x, center.y,center.z-4);
                Instantiate(gameObjects[objectIndex], pos, Quaternion.Euler(0,direction*90,0));
                break;
            case 1:
                int z = Random.Range(-length, length);
                pos = new Vector3(center.x - 4, center.y, center.z + z);
                Instantiate(gameObjects[objectIndex], pos, Quaternion.Euler(0, direction * 90, 0));
                break;
            case 2:
                x = Random.Range(-width, width);
                pos = new Vector3(center.x + x, center.y, center.z + 4);
                Instantiate(gameObjects[objectIndex], pos, Quaternion.Euler(0, direction * 90, 0));
                break;
            case 3:
                z = Random.Range(-length, length);
                pos = new Vector3(center.x + 4, center.y, center.z + z);
                Instantiate(gameObjects[objectIndex], pos, Quaternion.Euler(0, direction * 90, 0));
                break;
            default:
                Debug.Log("Error: invalid direction");
                break;
        }

    }

}
