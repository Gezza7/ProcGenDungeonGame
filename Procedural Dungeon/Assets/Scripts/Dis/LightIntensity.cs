using UnityEngine;

public class LightIntensity : MonoBehaviour
{
    public GameObject[] lightsSec1;
    public GameObject[] lightsSec2;
    public GameObject[] lightsSec3;
    public GameObject[] lightsSec4;
    public int currentSection;

    private int lightcheckNum = 0;
    public float averageLightLevel = 0;
    public float minLightLevel = 0;
    public float maxLightLevel = 0;
    private int count;
    public Camera cam;

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        if (Physics.Raycast(ray, out hit, 100))
        {
            switch(currentSection)
            {
                case 0:
                    checkLights(lightsSec1, hit.transform.position);
                    break;
                case 1:
                    checkLights(lightsSec2, hit.transform.position);
                    break;
                case 2:
                    checkLights(lightsSec3, hit.transform.position);
                    break;
                case 3:
                    checkLights(lightsSec4, hit.transform.position);
                    break;
                default:
                    Debug.Log("ERROR: Invalid Section");
                    break;

            }
        }
    }

    public void checkLights(GameObject[] lights, Vector3 hitPos)
    {
        foreach (GameObject light in lights)
        {
            RaycastHit hit;
            Vector3 dir = (light.transform.position - hitPos).normalized;
            float dist = Vector3.Distance(light.transform.position, hitPos);


            if (Physics.Raycast(light.transform.position, dir, out hit, dist))
            {
                //Debug.Log("first Hit " + hitPos + " second hit " +hit.transform.position);
                if (hit.transform.position == hitPos)
                {
                    //light is in sight
                    float temp = 100 - (20 * dist);
                    averageLightLevel += temp;
                    count++;
                }
            }


        }
    }

    
}
