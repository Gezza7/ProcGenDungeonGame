using UnityEngine;

public class TrackData : MonoBehaviour
{
    public float timeTotal = 0;
    private float deltaTime = 0;
    public float[] times = { 0, 0, 0, 0 };
    public float timeSec1 = 0;
    public float timeSec2 = 0;
    public float timeSec3 = 0;
    public float timeSec4 = 0;
    public int currentSection = 0;

    public float averageLightLevel = 0;
    public float minLightLevel = 0;
    public float maxLightLevel = 0;
    public LightIntensity lint;

    private SaveData sd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime;
        switch (currentSection)
        {
            case 0:
                timeSec1 += deltaTime;
                timeTotal += deltaTime;
                break;
            case 1:
                timeSec2 += deltaTime;
                timeTotal += deltaTime;
                break;
            case 2:
                timeSec3 += deltaTime;
                timeTotal += deltaTime;
                break;
            case 3:
                timeSec4 += deltaTime;
                timeTotal += deltaTime;
                break;
            default:
                Debug.Log("Error: Invalid section");
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //entered first section
        if(other.tag == "Lvl1")
        {
            currentSection = 0;
        }

        //entered second section
        else if(other.tag == "Lvl2")
        {
            currentSection = 1;
            if (times[0] == 0)
            {
                times[0] = timeSec1;
            }
        }

        //entered third section
        else if ( other.tag == "Lvl3")
        {
            currentSection = 2;
            if (times[1] == 0)
            {
                times[1] = timeSec2;
            }
        }

        //entered fourth section
        else if (other.tag == "Lvl4")
        {
            currentSection = 3;
            if (times[2] == 0)
            {
                times[2] = timeSec3;
            }
        }

        //found door
        else if (other.tag == "Door")
        {
            times[3] = timeSec4;
            //save data and change scene
        }
    }

    public void levelEnd()
    {
        sd = new SaveData();
    }
}
